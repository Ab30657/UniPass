using API.Interfaces;
using AutoMapper;

namespace API.Controllers
{
    public class InstructorController
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public InstructorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // Your Actions here //
        // have consequences //
    }
}
