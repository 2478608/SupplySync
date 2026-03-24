using SupplySync.DTOs.InventoryandWarehouse;

namespace SupplySync.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<int> CreateAsync(CreateWarehouseDto dto);
        Task<WarehouseResponseDto?> GetByIdAsync(int warehouseId);
        Task<WarehouseResponseDto?> UpdateAsync(int warehouseId, UpdateWarehouseRequestDto dto);
        Task<bool> DeleteAsync(int warehouseId);

        Task<List<WarehouseListResponseDto>> ListAsync(
            string? location,
            string? status,
            int? capacity
        );
    }
}
