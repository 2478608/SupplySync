using SupplySync.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplySync.DTOs.UserRoles
{
    public class CreateUserRoleRequestDto
    {
        [Required]
        public RoleType RoleType { get; set; }
    }

    public class UpdateUserRoleRequestDto
    {

        [Required]
        public RoleType CurrentRoleType { get; set; }

        [Required]
        public RoleType NewRoleType { get; set; }

        public bool? IsDeleted { get; set; }
    }

    public class UserRoleListResponseDto
    {
        public List<UserRoleResponseDto> Items { get; set; } = new();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class UserRoleResponseDto
    {

        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleType { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
