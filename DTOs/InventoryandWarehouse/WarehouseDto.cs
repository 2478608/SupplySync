using SupplySync.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplySync.DTOs.InventoryandWarehouse
{

    public class UpdateWarehouseRequestDto
    {
        [Required]
        public int WarehouseID { get; set; }

        public string? Location { get; set; }

        public int? Capacity { get; set; }

        public WarehouseStatus? Status { get; set; }

        public bool? IsDeleted { get; set; }
    }
    public class CreateWarehouseDto
    {
        [Required]
        public string Location { get; set; }

        public int? Capacity { get; set; }

        [Required]
        public WarehouseStatus Status { get; set; }
    }
    public class WarehouseListResponseDto
    {
        public int WarehouseID { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public WarehouseStatus Status { get; set; }
    }
    public class WarehouseResponseDto
    {
        public int WarehouseID { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public WarehouseStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
