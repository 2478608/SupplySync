using SupplySync.DTOs.InventoryandWarehouse;

namespace SupplySync.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<int> CreateAsync(CreateReceiptRequestDto dto);
        Task<ReceiptResponseDto?> GetByIdAsync(int receiptId);
        Task<ReceiptResponseDto?> UpdateAsync(int receiptId, UpdateReceiptRequestDto dto);
        Task<bool> DeleteAsync(int receiptId);

        Task<List<ReceiptListResponseDto>> ListAsync(
            int? warehouseId,
            int? deliveryId,
            string? status,
            DateOnly? fromDate,
            DateOnly? toDate
        );
    }
}
