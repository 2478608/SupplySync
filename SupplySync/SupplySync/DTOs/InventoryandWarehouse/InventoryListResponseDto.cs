using System;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
    public class InventoryListResponseDto
    {
        public int InventoryID { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public DateOnly DateAdded { get; set; }
        public InventoryStatus Status { get; set; }
    }
}