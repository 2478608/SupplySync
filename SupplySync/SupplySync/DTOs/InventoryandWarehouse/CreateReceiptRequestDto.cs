using System.ComponentModel.DataAnnotations;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
    public class CreateReceiptRequestDto
    {
        [Required]
        public int DeliveryID { get; set; }

        [Required]
        public int WarehouseID { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public ReceiptStatus Status { get; set; }
    }
}