using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<bool> ValidateUserAsync(LoginDto loginDto);
    }
}