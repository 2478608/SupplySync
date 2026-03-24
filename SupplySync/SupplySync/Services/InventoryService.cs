using AutoMapper;
using SupplySync.DTOs.InventoryandWarehouse;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repo;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateInventoryRequestDto dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            var created = await _repo.InsertAsync(entity);
            return created.InventoryID;
        }

        public async Task<InventoryResponseDto?> GetByIdAsync(int inventoryId)
        {
            var entity = await _repo.GetByIdAsync(inventoryId);
            return entity == null ? null : _mapper.Map<InventoryResponseDto>(entity);
        }

        public async Task<InventoryResponseDto?> UpdateAsync(int inventoryId, UpdateInventoryRequestDto dto)
        {
            var existing = await _repo.GetByIdAsync(inventoryId);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.UpdatedAt = DateTime.UtcNow;
            var updated = await _repo.UpdateAsync(existing);

            return _mapper.Map<InventoryResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(int inventoryId)
        {
            return await _repo.SoftDeleteAsync(inventoryId);
        }

        public async Task<List<InventoryListResponseDto>> ListAsync(
            int? warehouseId,
            string? item,
            string? status,
            DateOnly? fromDate,
            DateOnly? toDate)
        {
            var list = await _repo.ListAsync(warehouseId, item, status, fromDate, toDate);
            return _mapper.Map<List<InventoryListResponseDto>>(list);
        }
    }
}
