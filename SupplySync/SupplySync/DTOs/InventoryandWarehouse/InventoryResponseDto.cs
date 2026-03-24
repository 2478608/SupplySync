using System;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
    public class InventoryResponseDto
    {
        public int InventoryID { get; set; }
        public int WarehouseID { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public DateOnly DateAdded { get; set; }
        public InventoryStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}