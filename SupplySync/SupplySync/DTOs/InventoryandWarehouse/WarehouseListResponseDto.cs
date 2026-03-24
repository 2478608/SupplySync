using System;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
    public class WarehouseListResponseDto
    {
        public int WarehouseID { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public WarehouseStatus Status { get; set; }
    }
}