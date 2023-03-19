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
            CreateMap<PerfIndicatorDto, PerformanceIndicator>();
            CreateMap<PerformanceIndicator, PerfIndicatorDto>();
            CreateMap<Instructor, InstructorDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
            CreateMap<Student, StudentDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<Course, CreateCourseDto>();
            CreateMap<Semester, SemesterDto>();
            CreateMap<Course, GetCourseDto>()
                .ForMember(x => x.Instructor, opt => opt.MapFrom(src => src.Teaches.Select(x => x.Instructor).FirstOrDefault()))
                .ForMember(x => x.Semester, opt => opt.MapFrom(x => x.Teaches.Select(x => x.Semester).FirstOrDefault()));
            CreateMap<TeachesDto, Teaches>();
            /////////////////////////////////
            /// Add necessary mappings here
            ////////////////////////////////

            // CreateMap<UserFriend, FriendDto>()
            //     .ForMember(x => x.Username, opt => opt.MapFrom(src => src.ReqReceiverUser.UserName))
            //     .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ReqReceiverUser.Name))
            //     .ForMember(x => x.Gender, opt => opt.MapFrom(sourceMember => sourceMember.ReqReceiverUser.Gender))
            //     .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.ReqReceiverUser.Photos.FirstOrDefault(x => x.IsMain).Url))
            //     .ForMember(x => x.Status, opt => opt.MapFrom(src => src.RequestStatus));
        }
    }
}
