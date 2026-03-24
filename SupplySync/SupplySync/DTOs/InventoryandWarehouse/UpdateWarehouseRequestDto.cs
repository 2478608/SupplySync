using System.ComponentModel.DataAnnotations;
using SupplySync.Constants.Enums;

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
}