using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplySync.Services.Interfaces;
using SupplySync.DTOs.Finance;
namespace SupplySync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequestDto dto)
        {
            if(dto.Amount <= 0) return BadRequest("Amount must be greater than zero.");
            var id = await _paymentService.CreatePaymentAsync(dto);
            return Ok(new { Message = "Payment recorded", PaymentId = id });
        }

        [HttpPut("{paymentId}")]
        public async Task<IActionResult> UpdatePayment(int paymentId, [FromBody] UpdatePaymentRequestDto dto)
        {
            await _paymentService.UpdatePaymentAsync(paymentId, dto);
            return Ok(new { Message = "Payment updated" });
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
                return NotFound(new { Message = $"Payment with ID {paymentId} not found." });
            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
    }
}
