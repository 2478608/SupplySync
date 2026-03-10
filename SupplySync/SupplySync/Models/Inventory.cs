using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplySync.Constants.Enums;

namespace SupplySync.Models
{
	public class Inventory
	{
			[Key]
			public int InventoryID { get; set; }

			[Required]
			public int WarehouseID { get; set; } 

			[Required]
			[MaxLength(150)]
			public string Item { get; set; } = default!;

			[Required]
			[Range(0, int.MaxValue)]
			public int Quantity { get; set; }

			[Required]
			public DateOnly DateAdded { get; set; }

			[Required]
			public InventoryStatus Status { get; set; }

			[Required]
			public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

			public DateTime? UpdatedAt { get; set; }
			public virtual Warehouse Warehouse { get; set; } = default!;
		
	}

}


