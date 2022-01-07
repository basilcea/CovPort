namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBookingRepository Booking { get; }
        IUserRepository User {get;}
        ISpaceRepository Space {get;}
        ILocationRepository Location {get;}
        IResultRepository Location {get;}
        Task CompleteAsync();
        void Dispose();
    
    }
}