using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IAssignmentRepository
    {
        void Update(Assignment assignment);
        Task<IEnumerable<InstructorAssignmentDto>> GetAssignmentsByCourseIdAsync(int courseId);
        Task<IEnumerable<StudentAssignmentDto>> GetAssignmentsForStudentAsync(int courseId);
        Task AddAssignmentAsync(Assignment assignment);
        Task AddAnswerAsync(Answer answer);
        Task AddQuestionAsync(Question question);
        Task<Assignment> GetAssignmentByIdAsync(int id);
        //Multiple assignment attempts are doable with this architecture
        //For now focus on just one submission
        Task<TakeAssignment> GetAssignmentAttemptByStudentIdAsync(int studentId, int assignmentId);
        Task<TakeQuestion> GetQuestionAttemptByTakeAssignmentIdAsync(int takeAssignmentId, int questionId);
    }
}
