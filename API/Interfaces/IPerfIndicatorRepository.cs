using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IPerfIndicatorRepository
    {
        void AddPerfIndicator(PerformanceIndicator performanceIndicator);
        void Update(PerformanceIndicator performanceIndicator);
        Task<IEnumerable<PerformanceIndicatorDto>> GetPerfIndicatorsAsync();
        Task<PerformanceIndicator> GetPerformanceIndicatorByIdAsync(int id);
    }
}
