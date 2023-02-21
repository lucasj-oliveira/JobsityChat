using JobsityChat.Model;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace JobsityChat.Services
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> Login(LoginModel model);
        Task<Response> Register(RegisterModel model);
    }
}