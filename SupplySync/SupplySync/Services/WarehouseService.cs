using AutoMapper;
using SupplySync.DTOs.InventoryandWarehouse;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
	public class WarehouseService : IWarehouseService
	{
		private readonly IWarehouseRepository _repo;
		private readonly IMapper _mapper;

		public WarehouseService(IWarehouseRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<int> CreateAsync(CreateWarehouseDto dto)
		{
			var entity = _mapper.Map<Warehouse>(dto);
			entity.CreatedAt = DateTime.UtcNow;
			var created = await _repo.InsertAsync(entity);
			return created.WarehouseID;
		}

		public async Task<WarehouseResponseDto?> GetByIdAsync(int warehouseId)
		{
			var entity = await _repo.GetByIdAsync(warehouseId);
			return entity == null ? null : _mapper.Map<WarehouseResponseDto>(entity);
		}

		public async Task<WarehouseResponseDto?> UpdateAsync(int warehouseId, UpdateWarehouseRequestDto dto)
		{
			var existing = await _repo.GetByIdAsync(warehouseId);
			if (existing == null) return null;

			_mapper.Map(dto, existing);
			existing.UpdatedAt = DateTime.UtcNow;
			var updated = await _repo.UpdateAsync(existing);

			return _mapper.Map<WarehouseResponseDto>(updated);
		}

		public async Task<bool> DeleteAsync(int warehouseId)
		{
			return await _repo.SoftDeleteAsync(warehouseId);
		}

		public async Task<List<WarehouseListResponseDto>> ListAsync(
			string? location,
			string? status,
			int? capacity)
		{
			var list = await _repo.ListAsync(location, status, capacity);
			return _mapper.Map<List<WarehouseListResponseDto>>(list);
		}
	}
}

