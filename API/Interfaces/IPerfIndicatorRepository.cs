using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IPerfIndicatorRepository
    {
        void AddPerfIndicator(PerformanceIndicator performanceIndicator);
        void Update(PerformanceIndicator performanceIndicator);
        Task<IEnumerable<PerformanceIndicatorDto>> GetPerfIndicatorsAsync();
        Task<IEnumerable<PerformanceIndicatorDto>> GetPerfIndicatorsForCourse(int courseId);
        Task<PerformanceIndicator> GetPerformanceIndicatorByIdAsync(int id);
        Task<bool> CourseExistsAsync(int courseId, int piId);
        Task UpdateTakesCoursePIAsync(int courseId, int studentId, int semesterId, int piId, int piScore);
        Task UpdateCoursePIFullMarksAsync(int courseId, Assignment assignment, int pid, int fullPoints);
        Task<bool> CourseHasPIAsync(int piId, int courseId);
    }
}
