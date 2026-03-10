using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplySync.Constants.Enums;

namespace SupplySync.Models
{
	public class Warehouse
	{
      	    [Key]
			public int WarehouseID { get; set; }

			[Required]
			[MaxLength(150)]
			public string Location { get; set; } = default!;
			public int? Capacity { get; set; }

			[Required]
			public WarehouseStatus Status { get; set; }

			[Required]
			public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

			public DateTime? UpdatedAt { get; set; }
			public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
			public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
		
	}


}


