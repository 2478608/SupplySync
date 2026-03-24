using System;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
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