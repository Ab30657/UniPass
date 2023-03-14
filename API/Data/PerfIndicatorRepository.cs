using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class PerfIndicatorRepository : IPerfIndicatorRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PerfIndicatorRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
