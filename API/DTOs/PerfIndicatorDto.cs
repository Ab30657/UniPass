using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PerfIndicatorDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Performance Indicator Name is required")]
        public string Name { get; set; }
    }
}
