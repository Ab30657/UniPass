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

        public async Task AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
        }

        public async Task<TakeAssignment> GetAssignmentAttemptByStudentIdAsync(int studentId, int assignmentId)
        {
            return await _context.TakeAssignments.Where(x => x.AssignmentId == assignmentId && x.StudentId == studentId).FirstOrDefaultAsync();
        }

        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        public async Task<IEnumerable<AssignmentDto>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            return await _context.Assignments.Where(x => x.CourseId == courseId).ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TakeQuestion> GetQuestionAttemptByTakeAssignmentIdAsync(int takeAssignmentId, int questionId)
        {
            return await _context.TakeQuestions.Where(x => x.TakeAssignmentId == takeAssignmentId && x.QuestionId == questionId).FirstOrDefaultAsync();
        }

        public void Update(Assignment assignment)
        {
            _context.Entry<Assignment>(assignment).State = EntityState.Modified;
        }
    }
}
