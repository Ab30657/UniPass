using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
