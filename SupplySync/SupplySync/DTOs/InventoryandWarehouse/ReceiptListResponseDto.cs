using System;
using SupplySync.Constants.Enums;

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
}