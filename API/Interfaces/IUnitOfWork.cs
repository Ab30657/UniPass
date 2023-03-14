namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        // IUserAuthenticationRepository UserAuthenticationRepository { get; }
        ////////////////////////////////////////////
        /// Add other repository interfaces here
        ////////////////////////////////////////////
        IAssignmentRepository AssignmentRepository { get; }
        ICourseRepository CourseRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IPerfIndicatorRepository PerfIndicatorRepository { get; }

        Task<bool> CompleteAsync();
    }
}
