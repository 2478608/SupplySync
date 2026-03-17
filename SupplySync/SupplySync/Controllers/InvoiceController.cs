using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs.Finance;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitInvoice([FromBody] CreateInvoiceRequestDto dto)
        {
            var id = await _invoiceService.CreateInvoiceAsync(dto);
            return Ok(new
            {
                Message = "Invoice submitted successfully to the Finance Dashboard",
                InvoiceID = id
            });
        }
    }
}
