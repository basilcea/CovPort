using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IWorkerService
    {
        Task CloseOldSpacesBookings();
    }
}