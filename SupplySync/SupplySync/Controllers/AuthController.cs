// /SupplySync/Controllers/AuthController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs.User;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
		{
				var result = await _authService.LoginAsync(dto);
				return Ok(result);
		}
		[HttpPost("refresh")]
		[AllowAnonymous]
		public async Task<IActionResult> Refresh()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			var result = await _authService.RefreshAsync(refreshToken!);

			return Ok(new
			{
				Token = result.Token,
				ExpiresAtUtc = result.ExpiresAt
			});
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			await _authService.LogoutAsync(refreshToken!);

			return Ok(new { Message = "Logged out successfully." });
		}
	}
}
