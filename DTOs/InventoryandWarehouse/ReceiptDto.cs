using SupplySync.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplySync.DTOs.InventoryandWarehouse
{

    public class ReceiptListResponseDto
    {
        public int ReceiptID { get; set; }
        public int DeliveryID { get; set; }
        public int WarehouseID { get; set; }
        public DateOnly Date { get; set; }
        public int Quantity { get; set; }
        public ReceiptStatus Status { get; set; }
    }
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
    public class ReceiptResponseDto
    {
        public int ReceiptID { get; set; }
        public int DeliveryID { get; set; }
        public int WarehouseID { get; set; }
        public DateOnly Date { get; set; }
        public int Quantity { get; set; }
        public ReceiptStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class UpdateReceiptRequestDto
    {
        [Required]
        public int ReceiptID { get; set; }

        public int? DeliveryID { get; set; }

        public int? WarehouseID { get; set; }

        public DateOnly? Date { get; set; }

        public int? Quantity { get; set; }

        public ReceiptStatus? Status { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
