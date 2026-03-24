using System.ComponentModel.DataAnnotations;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
    public class CreateInventoryRequestDto
    {
        [Required]
        public int WarehouseID { get; set; }

        [Required]
        public string Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateOnly DateAdded { get; set; }

        [Required]
        public InventoryStatus Status { get; set; }
    }
}