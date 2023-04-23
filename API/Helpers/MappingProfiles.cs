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
            CreateMap<Assignment, AssignmentDto>();
            CreateMap<Assignment, AssignmentDetailDto<InstructorQuestionDto>>()
                .ForMember(x => x.Questions, opt => opt.MapFrom(y => y.Questions));
            // .ForMember(x => x.TakeAssignments, opt => opt.MapFrom(y => y.TakeAssignments));
            CreateMap<Question, InstructorQuestionDto>()
                .ForMember(x => x.PerformanceIndicators, opt => opt.MapFrom(src => src.QuestionPIs.Select(x => x.PerformanceIndicator)))
                .ForMember(x => x.Answers, opt => opt.MapFrom(y => y.Answers));
            CreateMap<Assignment, AssignmentDetailDto<StudentQuestionDto>>()
                .ForMember(x => x.Questions, opt => opt.MapFrom(y => y.Questions))
                .ForMember(x => x.TakeAssignments, opt => opt.Ignore());
            CreateMap<Question, StudentQuestionDto>()
                .ForMember(x => x.PerformanceIndicators, opt => opt.MapFrom(src => src.QuestionPIs.Select(x => x.PerformanceIndicator)))
                .ForMember(x => x.Answers, opt => opt.MapFrom(src => src.Answers));
            CreateMap<Answer, InstructorAnswerDto>();
            CreateMap<Answer, StudentAnswerDto>();
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
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<Course, CreateCourseDto>();
            CreateMap<Course, CourseDto>()
                .ForMember(x => x.Instructors, opt => opt.MapFrom(src => src.Teaches.Select(x => x.Instructor)));
            CreateMap<Semester, SemesterDto>();
            CreateMap<Course, CourseDetailDto>()
                .ForMember(x => x.Instructors, opt => opt.MapFrom(src => src.Teaches.Select(x => x.Instructor)))
                .ForMember(x => x.Semesters, opt => opt.MapFrom(x => x.Teaches.Select(x => x.Semester)))
                .ForMember(x => x.Students, opt => opt.MapFrom(x => x.Takes.Select(x => x.Student)))
                .ForMember(x => x.PerformanceIndicators, opt => opt.MapFrom(x => x.CoursePIs.Select(x => x.PerformanceIndicator)));

            CreateMap<TeachesDto, Teaches>();

            CreateMap<CreateTakeAssignmentDto, TakeAssignment>()
                .ForMember(x => x.TakeQuestions, opt => opt.MapFrom(x => x.TakeQuestions));
            CreateMap<CreateTakeQuestionDto, TakeQuestion>();
            CreateMap<TakeAssignmentPIScore, PIScoreDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.PerformanceIndicator.Name));
            CreateMap<TakeQuestion, TakeQuestionWithAnswerDto>()
                .ForMember(x => x.Correct, opt => opt.MapFrom(x => x.Answer.Correct))
                .ForMember(x => x.AnswerText, opt => opt.MapFrom(x => x.Answer.AnswerText))
                .ForMember(x => x.CorrectAnswer, opt => opt.MapFrom(x => x.Question.Answers.Where(x => x.Correct).FirstOrDefault().AnswerText));
            CreateMap<TakeAssignment, AssignmentAttemptGradeDto>()
                .ForMember(x => x.Student, opt => opt.MapFrom(x => x.Student))
                .ForMember(x => x.PIScores, opt => opt.MapFrom(x => x.PIScores))
                .ForMember(x => x.TakeAssignment, opt => opt.MapFrom(x => x));
            //Relate all PI Join tables to main PI tabler, but when making inserts 
            //check the parent if it contains that PI
            //for question - check Assignment PI
            //for assignment - check CoursePI
            //For course - check PI
            // CreateMap<TakesCoursePI, PIScoreDto>()
            //     .ForMember(x => x.)
            CreateMap<TakesCoursePI, PIScoreDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.PerformanceIndicatorId))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.FullMarks, opt => opt.MapFrom(x => x.Takes.Course.CoursePIs.Where(a => a.PerformanceIndicatorId == x.PerformanceIndicatorId).FirstOrDefault().PIFullMarks))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.PerformanceIndicator.Name));
            CreateMap<CoursePI, PIScoreDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.PerformanceIndicatorId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.PerformanceIndicator.Name))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.PIFullMarks));
            CreateMap<Takes, StudentWithScoreDto>()
                .ForMember(x => x.Student, opt => opt.MapFrom(x => x.Student))
                .ForMember(x => x.FullMarks, opt => opt.MapFrom(x => x.Course.Assignments.Sum(x => x.FullMarks)))
                .ForMember(x => x.PerformanceIndicatorScores, opt => opt.MapFrom(x => x.TakesCoursePIs));
            CreateMap<Takes, StudentDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Student.AppUser.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Student.AppUser.LastName))
                .ForMember(x => x.AppUserId, opt => opt.MapFrom(x => x.Student.AppUserId));
            CreateMap<TakeAssignment, StudentAssignmentGradesDto>()
                .ForMember(x => x.Student, opt => opt.MapFrom(x => x.Student))
                .ForMember(x => x.FullMarks, opt => opt.MapFrom(x => x.Assignment.Questions.Sum(x => x.FullMarks)))
                .ForMember(x => x.Grade, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.PerformanceIndicatorScores, opt => opt.MapFrom(x => x.PIScores))
                .ForMember(x => x.TakeQuestions, opt => opt.MapFrom(x => x.TakeQuestions));
            CreateMap<TakeAssignmentPIScore, PIScoreDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.PerformanceIndicatorId))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Score))
                .ForMember(x => x.FullMarks, opt => opt.MapFrom(x => x.TakeAssignment.Assignment.AssignmentPIs.Where(a => a.PerformanceIndicatorId == x.PerformanceIndicatorId).FirstOrDefault().FullScore))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.PerformanceIndicator.Name));
            CreateMap<CoursePI, PerformanceIndicatorDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.PerformanceIndicatorId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.PerformanceIndicator.Name));
            // CreateMap<Takes, CourseWithStudentScoresDto>()
            //     .ForMember(x => x.FullMarks, opt => opt.MapFrom(x => x.Course.Assignments.Sum(x => x.FullMarks)))
            //     .ForMember(x => x.PIFullMarksDtos, opt => opt.MapFrom(x => x.Course.CoursePIs))
            //     .ForMember(x => x.StudentWithScores, opt => opt.MapFrom(x => x));
            // /////////////////////////////////
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
