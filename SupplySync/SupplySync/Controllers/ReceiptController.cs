using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs.InventoryandWarehouse;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _service;

        public ReceiptController(IReceiptService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReceiptRequestDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { Message = "Receipt created", ReceiptID = id });
        }

        [HttpPut("{receiptId}")]
        public async Task<IActionResult> Update(int receiptId, [FromBody] UpdateReceiptRequestDto dto)
        {
            var updated = await _service.UpdateAsync(receiptId, dto);
            if (updated == null)
                return NotFound(new { Message = "Receipt not found" });

            return Ok(updated);
        }

        [HttpGet("{receiptId}")]
        public async Task<IActionResult> Get(int receiptId)
        {
            var record = await _service.GetByIdAsync(receiptId);
            if (record == null)
                return NotFound(new { Message = "Receipt not found" });

            return Ok(record);
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] int? warehouseId,
            [FromQuery] int? deliveryId,
            [FromQuery] string? status,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate)
        {
            var list = await _service.ListAsync(warehouseId, deliveryId, status, fromDate, toDate);
            return Ok(list);
        }

        [HttpDelete("{receiptId}")]
        public async Task<IActionResult> Delete(int receiptId)
        {
            var ok = await _service.DeleteAsync(receiptId);
            if (!ok) return NotFound(new { Message = "Receipt not found" });

            return Ok(new { Message = "Receipt deleted" });
        }
    }
}
