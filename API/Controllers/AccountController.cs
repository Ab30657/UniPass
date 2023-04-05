using System.Globalization;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            this._tokenService = tokenService;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role);
            //
            //Add to tables
            // Future add eror handling
            if (registerDto.Role == "Student")
            {
                await _unitOfWork.UserRepository.AddStudentAsync(user.Id);
            }
            else if (registerDto.Role == "Instructor")
            {
                await _unitOfWork.UserRepository.AddInstructorAsync(user.Id);
            }
            if (!await _unitOfWork.CompleteAsync())
                return BadRequest();
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Name = user.FirstName + " " + user.LastName
                // KnownAs = user.KnownAs,
                // Gender = user.Gender
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid username");
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Name = myTI.ToTitleCase(user.FirstName) + " " + myTI.ToTitleCase(user.LastName)
            };

        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
