using System.Globalization;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public AssignmentRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
    }
}
