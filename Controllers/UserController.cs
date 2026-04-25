using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SupplySync.DTOs.User;
using SupplySync.Services.Interfaces;
using SupplySync.Security;

namespace SupplySync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // require authenticated users; global SingleRoleRequirement also applies
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Admin only: create internal users (ProcurementOfficer, WarehouseManager, FinanceOfficer, ComplianceOfficer, Admin)
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto dto)
        {
            var id = await _userService.CreateUserAsync(dto);
            return Ok(new { Message = "User created successfully", UserID = id });
        }

        // Admin or the user themself can update their profile
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequestDto dto)
        {
            if (id != dto.UserID) return BadRequest(new { Message = "Route id and body UserID must match." });

            var caller = User.GetUserId();
            if (!caller.HasValue) return Unauthorized(new { Message = "Invalid token." });

            // Admins can update any user; non-admins may only update their own account
            if (!User.IsInRole("Admin") && caller.Value != id)
                return Forbid("Only Admins or the user themself can update this account.");

            var response = await _userService.UpdateUserAsync(id, dto);
            return Ok(response);
        }

        // Admin or the user themself can view user details
        [HttpGet("users/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var caller = User.GetUserId();
            if (!caller.HasValue) return Unauthorized(new { Message = "Invalid token." });

            if (!User.IsInRole("Admin") && caller.Value != id)
                return Forbid("Only Admins or the user themself can view this account.");

            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-warehouse")]
        public async Task<IActionResult> AssignWarehouse([FromBody] UpdateUserRequestDto dto)
        {
            if (dto == null) return BadRequest("Request body is required.");
            try
            {
                var updated = await _userService.UpdateUserAsync(dto.UserID, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("user not found");
            }
        }




        // List users: Admin only
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> List([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _userService.ListUsersAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}