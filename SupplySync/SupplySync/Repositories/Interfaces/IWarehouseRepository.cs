using SupplySync.Models;

namespace SupplySync.Repositories.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<Warehouse> InsertAsync(Warehouse entity);
        Task<Warehouse?> GetByIdAsync(int warehouseId);
        Task<Warehouse> UpdateAsync(Warehouse entity);

        // Soft-delete
        Task<bool> SoftDeleteAsync(int warehouseId);

        // List with filters
        Task<List<Warehouse>> ListAsync(
            string? location,
            string? status,
            int? capacity
        );
    }
}
