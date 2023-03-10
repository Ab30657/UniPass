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
        public UnitOfWork(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._context = context;
        }

        public IUserAuthenticationRepository UserAuthenticationRepository => new UserAuthenticationRepository(_context, _userManager, _mapper);
        ////////////////////////////////////////////
        ///Add other repositories below this section 
        ////////////////////////////////////////////
        /*    
W
        */
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}