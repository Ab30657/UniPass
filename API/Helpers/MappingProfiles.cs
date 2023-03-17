using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<UpdateCourseDto, Course>();
            /////////////////////////////////
            /// Add necessary mappings here
            ////////////////////////////////
        }
    }
}
