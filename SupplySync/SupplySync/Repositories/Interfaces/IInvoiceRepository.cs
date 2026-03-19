using SupplySync.Models;

namespace SupplySync.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> InsertAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);

        Task<Invoice?> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
    }
}
