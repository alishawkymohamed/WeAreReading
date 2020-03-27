using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Services.Contracts
{
    public interface ITokenValidatorService : IService
    {
        void Validate(TokenValidatedContext context);
    }
}
