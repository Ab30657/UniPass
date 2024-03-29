using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UnitOfWork(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._mapper = mapper;
            this._context = context;
        }

        // public IUserAuthenticationRepository UserAuthenticationRepository => new UserAuthenticationRepository(_context, _userManager, _signInManager, _mapper);
        ////////////////////////////////////////////
        ///Add other repositories below this section 
        ////////////////////////////////////////////
        /*    
W
        */
        public IAssignmentRepository AssignmentRepository => new AssignmentRepository(_context, _mapper);
        public ICourseRepository CourseRepository => new CourseRepository(_context, _mapper);
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_context, _mapper);
        public IPerfIndicatorRepository PerfIndicatorRepository => new PerfIndicatorRepository(_context, _mapper);
        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
