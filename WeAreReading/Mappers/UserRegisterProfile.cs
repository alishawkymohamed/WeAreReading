﻿using System;
using AutoMapper;
using Helpers.Contracts;
using Microsoft.Extensions.Options;
using Models.DbModels;
using Models.DTOs;
using Models.HelperModels;

namespace WeAreReading.Mappers
{
    public class UserRegisterProfile : Profile
    {
        private readonly IEncryptionService encryptionService;
        private readonly IOptions<AppSettings> appSettings;

        public UserRegisterProfile(IEncryptionService encryptionService, IOptions<AppSettings> appSettings)
        {
            this.encryptionService = encryptionService;
            this.appSettings = appSettings;

            CreateMap<RegisterUserDTO, User>()
                .BeforeMap((src, dest) => dest.Password = encryptionService.EncryptString(src.Password, appSettings.Value.EncryptionSettings.SecretPassword, appSettings.Value.EncryptionSettings.Salt))
                .ForMember(dest => dest.Password, src => src.Ignore())
                .ForMember(dest => dest.NotificationByMail, src => src.Ignore())
                .ForMember(dest => dest.DeletedAt, src => src.Ignore())
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                .ForMember(dest => dest.LastLoggedIn, src => src.Ignore())
                .ForMember(dest => dest.ProfileImage, src => src.Ignore())
                .ForMember(dest => dest.UserRoles, src => src.Ignore())
                .ForMember(dest => dest.UserTokens, src => src.Ignore())
                .ForMember(dest => dest.SerialNumber, src => src.MapFrom(x => Guid.NewGuid()));
        }
    }
}
