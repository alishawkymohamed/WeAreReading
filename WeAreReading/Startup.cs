using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.HelperModels;
using Newtonsoft.Json;
using Services.Contracts;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeAreReading
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(options => Configuration.Bind(options));
            services.AddAutoMapper(typeof(Startup));

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                // You Can use fastest compression
                options.Level = CompressionLevel.Optimal;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();
            services.AddCors();
            services.InjectDependancies();
            services.AddAutoMapperProfiles();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerGeneratorOptions = new SwaggerGeneratorOptions()
                {
                    OperationIdSelector = x => x.ActionDescriptor.AttributeRouteInfo.Template.Replace('{', '_').Replace('}', '_')
                };
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeAreReading", Version = "v1" });
            });

            services.AddEntityFrameworkSqlServer().AddDbContext<MainDbContext>(options =>
            {
                options.UseLazyLoadingProxies(true)
                .UseSqlServer(Configuration.GetConnectionString("MainConnectionString"), serverDbContextOptionsBuilder =>
                {
                    int minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                    serverDbContextOptionsBuilder.CommandTimeout(minutes);
                    serverDbContextOptionsBuilder.EnableRetryOnFailure();
                });
            });

            services.AddSingleton(typeof(DbContextOptionsBuilder<MainDbContext>), new DbContextOptionsBuilder<MainDbContext>().UseSqlServer(Configuration.GetConnectionString("MainConnectionString")));

            services
               .AddAuthentication(options =>
               {
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = Configuration["BearerTokens:Issuer"], // site that makes the token
                       ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                       ValidAudience = Configuration["BearerTokens:Audience"], // site that consumes the token
                       ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"])),
                       ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                       ValidateLifetime = true, // validate the expiration
                       ClockSkew = TimeSpan.FromMinutes(5) // tolerance for the expiration date
                   };
                   cfg.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("Authentication failed.", context.Exception);
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           ITokenValidatorService tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                           tokenValidatorService.Validate(context);
                           return Task.CompletedTask;
                       },
                       OnChallenge = context =>
                       {
                           ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                           return Task.CompletedTask;
                       },
                       //OnMessageReceived = context =>
                       //{
                       //    Microsoft.Extensions.Primitives.StringValues accessToken = context.Request.Query["access_token"];

                       //    // If the request is for our hub...
                       //    PathString path = context.HttpContext.Request.Path;
                       //    if (!string.IsNullOrEmpty(accessToken) &&
                       //        (path.StartsWithSegments("/SignalR")))
                       //    {
                       //        // Read the token out of the query string
                       //        context.Token = accessToken;
                       //    }
                       //    return Task.CompletedTask;
                       //}
                   };
               });

            services.AddControllers().AddFluentValidation();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSession();
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    IExceptionHandlerFeature error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = 401,
                            Message = "Token Expired"
                        }));
                    }
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = 500,
                            Message = error.Error.Message
                        }));
                    }
                    else
                    {
                        await next();
                    }
                });
            });

            app.UseResponseCompression();
            app.UseCors(builder => builder.AllowCredentials().AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:44354").Build());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeAreReading V1");
            });

            app.UseAuthentication();

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>() {
                    { ".xlf","application/x-msdownload"},
                    { ".exe","application/octect-stream"},
                })
            });
            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseStatusCodePages();
            app.UseDefaultFiles(); // so index.html is not required

            app.UseFileServer(new FileServerOptions()
            {
                EnableDirectoryBrowsing = false
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromMinutes(5);
                }
            });

            //if (env.IsDevelopment())
            //{
            //    Task.Run(() =>
            //    {
            //        using (HttpClient httpClient = new HttpClient())
            //        {
            //            string SourceDocumentAbsoluteUrl = Configuration["SwaggerToTypeScriptSettings:SourceDocumentAbsoluteUrl"];
            //            string OutputDocumentRelativePath = Configuration["SwaggerToTypeScriptSettings:OutputDocumentRelativePath"];
            //            using (Stream contentStream = httpClient.GetStreamAsync(SourceDocumentAbsoluteUrl).Result)
            //            using (StreamReader streamReader = new StreamReader(contentStream))
            //            {
            //                string json = streamReader.ReadToEnd();

            //                var document = NSwag.OpenApiDocument.FromJsonAsync(json).Result;

            //                var settings = new TypeScriptClientGeneratorSettings
            //                {
            //                    ClassName = "SwaggerClient",
            //                    Template = TypeScriptTemplate.Angular,
            //                    RxJsVersion = 6.0M,
            //                    HttpClass = HttpClass.HttpClient,
            //                    InjectionTokenType = InjectionTokenType.InjectionToken,
            //                    BaseUrlTokenName = "API_BASE_URL",
            //                    UseSingletonProvider = true
            //                };

            //                var generator = new TypeScriptClientGenerator(document, settings);
            //                var code = generator.GenerateFile();
            //                //NSwag.SwaggerDocument document = NSwag.SwaggerDocument.FromJsonAsync(json).Result;
            //                //SwaggerToTypeScriptClientGeneratorSettings settings = new SwaggerToTypeScriptClientGeneratorSettings
            //                //{
            //                //    ClassName = "SwaggerClient",
            //                //    Template = TypeScriptTemplate.Angular,
            //                //    RxJsVersion = 6.0M,
            //                //    HttpClass = HttpClass.HttpClient,
            //                //    InjectionTokenType = InjectionTokenType.InjectionToken,
            //                //    BaseUrlTokenName = "API_BASE_URL",
            //                //    UseSingletonProvider = true
            //                //};
            //                //SwaggerToTypeScriptClientGenerator generator = new SwaggerToTypeScriptClientGenerator(document, settings);
            //                //string code = generator.GenerateFile();
            //                new FileInfo(OutputDocumentRelativePath).Directory.Create();
            //                try
            //                {
            //                    File.WriteAllText(OutputDocumentRelativePath, code);
            //                }
            //                catch (Exception ex)
            //                {
            //                    throw ex;
            //                }
            //            }
            //        }
            //    });
            //}
        }
    }
}
