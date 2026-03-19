using SupplySync.Models;
namespace SupplySync.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> InsertAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllAsync();
    }
}
