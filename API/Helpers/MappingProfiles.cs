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
            CreateMap<Instructor, InstructorDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
            CreateMap<Student, StudentDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
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
