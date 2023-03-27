using System.Globalization;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
        }

        public async Task AddAssignmentAsync(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
        }

        public async Task AddAssignmentPIScoreAsync(TakeAssignmentPIScore piScore)
        {
            await _context.PIScores.AddAsync(piScore);
        }

        public async Task AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
        }

        public async Task AddTakeAssignmentAsync(TakeAssignment assignmentTake)
        {
            await _context.TakeAssignments.AddAsync(assignmentTake);
        }

        public async Task AddTakeQuestionAsync(TakeQuestion takequestion)
        {
            await _context.TakeQuestions.AddAsync(takequestion);
        }

        public async Task<Answer> GetAnswerByIdAsync(int answerId)
        {
            return await _context.Answers.Where(x => x.Id == answerId).FirstOrDefaultAsync();
        }

        public async Task<TakeAssignment> GetAssignmentAttemptByStudentIdAsync(int studentId, int assignmentId)
        {
            return await _context.TakeAssignments.Where(x => x.AssignmentId == assignmentId && x.StudentId == studentId).FirstOrDefaultAsync();
        }

        public async Task<StudentAssignmentDto> GetAssignmentByIdAsync(int id)
        {
            return await _context.Assignments.Where(x => x.Id == id).ProjectTo<StudentAssignmentDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InstructorAssignmentDto>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            // if (role == "Student")
            // return await _context.Assignments.Where(x => x.CourseId == courseId).ProjectTo<>(_mapper.ConfigurationProvider).ToListAsync();
            return await _context.Assignments.Where(x => x.CourseId == courseId).ProjectTo<InstructorAssignmentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<StudentAssignmentDto>> GetAssignmentsForStudentAsync(int courseId)
        {
            return await _context.Assignments.Where(x => x.CourseId == courseId).ProjectTo<StudentAssignmentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TakeQuestion> GetQuestionAttemptByTakeAssignmentIdAsync(int takeAssignmentId, int questionId)
        {
            return await _context.TakeQuestions.Where(x => x.TakeAssignmentId == takeAssignmentId && x.QuestionId == questionId).FirstOrDefaultAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions.Where(x => x.Id == questionId).Include(x => x.Answers).Include(x => x.QuestionPIs).FirstOrDefaultAsync();
        }

        public async Task<AssignmentAttemptGradeDto> GetTakeAssignmentByIdAsync(int id)
        {
            return await _context.TakeAssignments.Where(x => x.Id == id).ProjectTo<AssignmentAttemptGradeDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public void Update(Assignment assignment)
        {
            _context.Entry<Assignment>(assignment).State = EntityState.Modified;
        }
    }
}
