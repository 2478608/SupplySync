using SupplySync.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplySync.DTOs.User
{

    public class CreateUserRequestDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = default!;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = default!;

        [Phone]
        public string? Phone { get; set; }

        [Required, MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        public UserStatus Status { get; set; } = UserStatus.Pending;
    }
    public class LoginRequestDto
    {
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required, MinLength(8)]
        public string Password { get; set; } = default!;
    }
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = new();
    }
    public class UserListResponseDto
    {
        public List<UserResponseDto> Items { get; set; } = new();

        // Basic pagination metadata
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class UpdateUserRequestDto
    {
        [Required]
        public int UserID { get; set; }

        [MaxLength(150)]
        public string? Name { get; set; }

        [EmailAddress, MaxLength(100)]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [MinLength(8), MaxLength(255)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public UserStatus? Status { get; set; }
        public bool? IsDeleted { get; set; }

        // new: allow assigning/updating warehouse
        public int? WarehouseId { get; set; }
    }
    public class UserResponseDto
    {
        // changed to property and nullable so mapping works and absence is representable
        public int? WarehouseId { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Phone { get; set; }

        public UserStatus Status { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<UserRoleSummaryDto> Roles { get; set; } = new();
    }

    public class UserRoleSummaryDto
    {
        public int RoleID { get; set; }
        public string RoleType { get; set; } = default!;
    }

}
