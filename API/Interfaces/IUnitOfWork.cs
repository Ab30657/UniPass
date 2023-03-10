namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserAuthenticationRepository UserAuthenticationRepository { get; }
        ////////////////////////////////////////////
        /// Add other repository interfaces here
        ////////////////////////////////////////////
        Task<bool> CompleteAsync();
    }
}