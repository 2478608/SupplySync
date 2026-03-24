using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs.InventoryandWarehouse;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { Message = "Warehouse created", WarehouseID = id });
        }

        [HttpPut("{warehouseId}")]
        public async Task<IActionResult> Update(int warehouseId, [FromBody] UpdateWarehouseRequestDto dto)
        {
            var updated = await _service.UpdateAsync(warehouseId, dto);
            if (updated == null)
                return NotFound(new { Message = "Warehouse not found" });

            return Ok(updated);
        }

        [HttpGet("{warehouseId}")]
        public async Task<IActionResult> Get(int warehouseId)
        {
            var record = await _service.GetByIdAsync(warehouseId);
            if (record == null)
                return NotFound(new { Message = "Warehouse not found" });

            return Ok(record);
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] string? location,
            [FromQuery] string? status,
            [FromQuery] int? capacity)
        {
            var list = await _service.ListAsync(location, status, capacity);
            return Ok(list);
        }

        [HttpDelete("{warehouseId}")]
        public async Task<IActionResult> Delete(int warehouseId)
        {
            var ok = await _service.DeleteAsync(warehouseId);
            if (!ok) return NotFound(new { Message = "Warehouse not found" });

            return Ok(new { Message = "Warehouse deleted" });
        }
    }
}
