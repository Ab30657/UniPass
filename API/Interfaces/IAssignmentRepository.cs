using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IAssignmentRepository
    {
        void Update(Assignment assignment);
        Task<IEnumerable<AssignmentDto>> GetAssignmentsByCourseIdAsync(int courseId);
        Task AddAssignmentAsync(Assignment assignment);
        Task AddAnswerAsync(Answer answer);
        Task AddQuestionAsync(Question question);
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<AssignmentDetailDto<StudentQuestionDto>> GetAssignmentByIdWithAttemptsAsync(int id, int studentId);
        //Multiple assignment attempts are doable with this architecture
        //For now focus on just one submission
        Task<TakeAssignment> GetAssignmentAttemptByStudentIdAsync(int studentId, int assignmentId);
        Task<TakeQuestion> GetQuestionAttemptByTakeAssignmentIdAsync(int takeAssignmentId, int questionId);
        Task AddTakeQuestionAsync(TakeQuestion takequestion);
        Task AddTakeAssignmentAsync(TakeAssignment assignmentTake);
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<Answer> GetAnswerByIdAsync(int answerId);
        Task AddAssignmentPIScoreAsync(TakeAssignmentPIScore piScore);
        Task<AssignmentAttemptGradeDto> GetTakeAssignmentByIdAsync(int id);
    }
}
