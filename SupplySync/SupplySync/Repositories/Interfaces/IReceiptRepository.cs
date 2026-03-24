using SupplySync.Models;

namespace SupplySync.Repositories.Interfaces
{
    public interface IReceiptRepository
    {
        Task<Receipt> InsertAsync(Receipt entity);
        Task<Receipt?> GetByIdAsync(int receiptId);
        Task<Receipt> UpdateAsync(Receipt entity);

        // Soft-delete
        Task<bool> SoftDeleteAsync(int receiptId);

        // List with filters
        Task<List<Receipt>> ListAsync(
            int? warehouseId,
            int? deliveryId,
            string? status,
            DateOnly? fromDate,
            DateOnly? toDate
        );
    }
}
