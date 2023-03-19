namespace API.DTOs
{
    public class SemesterDto
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
