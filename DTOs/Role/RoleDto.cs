using SupplySync.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplySync.DTOs.Role
{
    public class RoleListResponseDto
    {
        public List<RoleResponseDto> Items { get; set; } = new();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class RoleResponseDto
    {
        public int RoleID { get; set; }

        public RoleType RoleType { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateRoleRequestDto
    {
        [Required]
        public int RoleID { get; set; }

        public RoleType? RoleType { get; set; }

        public bool? IsDeleted { get; set; }
    }

}
