namespace API.Models
{
    //////////////
    /// Might need the section table later
    /// If assignments are also based on semesters, then so should the attempts?
    /// Things like grade, assignments, attempts align more closely with Takes table or Section Table  
    //////////////
    public class Student
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Takes> Takes { get; set; }
        public ICollection<TakeAssignment> TakeAssignments { get; set; }

    }
}