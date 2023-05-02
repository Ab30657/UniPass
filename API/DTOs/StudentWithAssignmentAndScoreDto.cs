
using API.DTOs;

namespace API.DTOs
{
    public class StudentWithAssignmentAndScoreDto : StudentWithScoreDto
    {
        public List<StudentAssignmentGradesWithoutQuestionsDto> AssignmentGradesDtos { get; set; }
    }
}
