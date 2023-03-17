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
            CreateMap<CourseDto, Course>();
            /////////////////////////////////
            /// Add necessary mappings here
            ////////////////////////////////
        }
    }
}
