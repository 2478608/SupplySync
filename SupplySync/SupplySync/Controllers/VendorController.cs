using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VendorController : ControllerBase
	{
		private readonly IVendorService _vendorService;
		public VendorController(IVendorService vendorService) 
		{
			_vendorService = vendorService;
		}

		[HttpPost("")]
		public async Task<IActionResult> CreateVendor([FromBody] CreateVendorRequestDto createVendorRequestDto)
		{
			VendorResponseDto createdVendor = await _vendorService.CreateVendor(createVendorRequestDto);

			return Ok();
		}

	}
}
