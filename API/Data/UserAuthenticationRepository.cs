using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //Not used anymore
    [Obsolete]
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        public UserAuthenticationRepository(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._userManager = userManager;
            this._context = context;

        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return IdentityResult.Failed();

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role);

            return roleResult;
        }

        public async Task<bool> ValidateUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            var result = user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password);
            return result;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
