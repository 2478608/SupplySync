using System.ComponentModel.DataAnnotations;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.InventoryandWarehouse
{
	public class CreateWarehouseDto
	{
		[Required]
		public string Location { get; set; }

		public int? Capacity { get; set; }

		[Required]
		public WarehouseStatus Status { get; set; }
	}
}
