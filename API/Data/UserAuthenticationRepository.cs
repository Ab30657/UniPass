using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserAuthenticationRepository(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._context = context;

        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {

        }

        public Task<bool> ValidateUserAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}