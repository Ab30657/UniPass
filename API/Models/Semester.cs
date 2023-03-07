namespace API.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Teaches> Teaches { get; set; }
        public ICollection<Takes> Takes { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}