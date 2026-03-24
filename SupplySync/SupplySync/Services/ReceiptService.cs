using AutoMapper;
using SupplySync.DTOs.InventoryandWarehouse;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _repo;
        private readonly IMapper _mapper;

        public ReceiptService(IReceiptRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateReceiptRequestDto dto)
        {
            var entity = _mapper.Map<Receipt>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            var created = await _repo.InsertAsync(entity);
            return created.ReceiptID;
        }

        public async Task<ReceiptResponseDto?> GetByIdAsync(int receiptId)
        {
            var entity = await _repo.GetByIdAsync(receiptId);
            return entity == null ? null : _mapper.Map<ReceiptResponseDto>(entity);
        }

        public async Task<ReceiptResponseDto?> UpdateAsync(int receiptId, UpdateReceiptRequestDto dto)
        {
            var existing = await _repo.GetByIdAsync(receiptId);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.UpdatedAt = DateTime.UtcNow;
            var updated = await _repo.UpdateAsync(existing);

            return _mapper.Map<ReceiptResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(int receiptId)
        {
            return await _repo.SoftDeleteAsync(receiptId);
        }

        public async Task<List<ReceiptListResponseDto>> ListAsync(
            int? warehouseId,
            int? deliveryId,
            string? status,
            DateOnly? fromDate,
            DateOnly? toDate)
        {
            var list = await _repo.ListAsync(warehouseId, deliveryId, status, fromDate, toDate);
            return _mapper.Map<List<ReceiptListResponseDto>>(list);
        }
    }
}
