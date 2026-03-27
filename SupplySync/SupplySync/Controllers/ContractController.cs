using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs.Contract;
using SupplySync.Models;
using SupplySync.Services;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContractController : ControllerBase
	{
		private readonly IContractService _contractService;

		public ContractController(IContractService contractService)
		{
			_contractService = contractService;
		}
		/// <summary>
		/// Contract endpoints
		/// </summary>

		/// <summary>
		/// Retrieves all contracts associated with the specified vendor ID,
		/// applying any provided filter criteria.
		/// </summary>
		[Authorize]
		[HttpGet("vendor/{vendorId}")]
		public async Task<IActionResult> GetAllContractsByVendorId([FromRoute] int vendorId, [FromQuery] ContractFiltersRequestDto contractFiltersRequestDto) {

			List<ContractWithTermsResponseDto> contractWithTermsResponseDtos = await _contractService.GetAllContractsByVendorId(vendorId, contractFiltersRequestDto);

			return Ok(contractWithTermsResponseDtos);
		}

		/// <summary>
		/// Retrieves a single contract using its unique contract ID.
		/// </summary>
		[Authorize]
		[HttpGet("{contractId}")]
		public async Task<IActionResult> GetContractById([FromRoute] int contractId)
		{
			ContractResponseDto contractResponseDto = await _contractService.GetContractById(contractId);
			return Ok(contractResponseDto);
		}

		/// <summary>
		/// Creates a new contract based on the provided contract details.
		/// Accessible by VendorUser and Admin roles.
		/// </summary>
		[Authorize(Roles = "VendorUser,Admin")]
		[HttpPost("")]
		public async Task<IActionResult> CreateContract([FromBody] CreateContractRequestDto createContractRequestDto)
		{
			ContractResponseDto contractResponseDto = await _contractService.CreateContract(createContractRequestDto);
			return Ok(contractResponseDto);
		}

		/// <summary>
		/// Updates an existing contract identified by the given contract ID.
		/// </summary>
		[Authorize(Roles = "VendorUser,Admin")]
		[HttpPut("{contractId}")]
		public async Task<IActionResult> UpdateContract([FromRoute] int contractId, UpdateContractRequestDto updateContractRequestDto)
		{
			ContractResponseDto contractResponseDto = await _contractService.UpdateContract(contractId ,updateContractRequestDto);
			return Ok();
		}



		/// <summary>
		/// Contract Terms endpoints
		/// </summary>

		/// <summary>
		/// Creates a new contract term for the specified contract.
		/// Accessible by VendorUser and Admin roles.
		/// </summary>
		[Authorize(Roles = "VendorUser,Admin")]
		[HttpPost("{contractId}/terms")]
		public async Task<IActionResult> CreateContractTerm([FromBody] CreateContractTermRequestDto createContractTermRequestDto)
		{
			ContractTermResponseDto contractTermResponseDto = await _contractService.CreateContractTerm(createContractTermRequestDto);
			return Ok(contractTermResponseDto);
		}
		/// <summary>
		/// Retrieves all contract terms for the specified contract ID,
		/// with optional filter criteria.
		/// </summary>
		[Authorize]
		[HttpGet("{contractId}/terms")]
		public async Task<IActionResult> GetAllContractTermByContractId([FromRoute] int contractId,[FromQuery] ContractTermFiltersRequestDto contractTermFiltersRequestDto)
		{
			List<ContractTermResponseDto> contractResponseDtos = await _contractService.GetAllContractTermByContractId(contractId, contractTermFiltersRequestDto);
			return Ok(contractResponseDtos);
		}
	}
}
