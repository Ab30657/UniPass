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
            CreateMap<PerformanceIndicatorDto, PerformanceIndicator>();
            CreateMap<PerformanceIndicator, PerformanceIndicatorDto>();
            CreateMap<Instructor, InstructorDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
            CreateMap<Student, StudentDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.AppUser.LastName));
            CreateMap<Assignment, InstructorAssignmentDto>()
                .ForMember(x => x.Questions, opt => opt.MapFrom(y => y.Questions))
                .ForMember(x => x.TakeAssignments, opt => opt.MapFrom(y => y.TakeAssignments));
            CreateMap<Question, QuestionDto>()
                .ForMember(x => x.PerformanceIndicators, opt => opt.MapFrom(src => src.QuestionPIs.Select(x => x.PerformanceIndicator)))
                .ForMember(x => x.Answers, opt => opt.MapFrom(y => y.Answers));
            CreateMap<Assignment, StudentAssignmentDto>()
                .ForMember(x => x.Questions, opt => opt.MapFrom(y => y.Questions))
                .ForMember(x => x.TakeAssignments, opt => opt.MapFrom(y => y.TakeAssignments));
            CreateMap<Question, StudentQuestionDto>()
                .ForMember(x => x.PerformanceIndicators, opt => opt.MapFrom(src => src.QuestionPIs.Select(x => x.PerformanceIndicator)));

            CreateMap<Answer, AnswerDto>();
            CreateMap<TakeAssignment, TakeAssignmentDto>()
                .ForMember(x => x.TakeQuestions, opt => opt.MapFrom(y => y.TakeQuestions));
            CreateMap<TakeQuestion, TakeQuestionDto>()
                .ForMember(x => x.Correct, opt => opt.MapFrom(x => x.Answer.Correct))
                .ForMember(x => x.AnswerText, opt => opt.MapFrom(x => x.Answer.AnswerText));

            CreateMap<CreateAssignmentDto, Assignment>()
                .ForMember(x => x.Questions, opt => opt.MapFrom(x => x.Questions));
            CreateMap<CreateQuestionDto, Question>()
                .ForMember(x => x.Answers, opt => opt.MapFrom(x => x.Answers))
                .AfterMap((src, dest) =>
                {
                    dest.QuestionPIs = src.PerformanceIndicators.Select(x => new QuestionPI { PerformanceIndicatorId = x, Question = dest }).ToList();
                });
            // CreateMap<QuestionPIDto, QuestionPI>();
            CreateMap<CreateAnswerDto, Answer>();

            CreateMap<Course, CourseDto>()
                .ForMember(x => x.Students, opt => opt.MapFrom(x => x.Takes.Select(x => x.Student)))
                .ForMember(x => x.PerformanceIndicators, opt => opt.MapFrom(x => x.CoursePIs.Select(x => x.PerformanceIndicator)));
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
