using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplySync.Constants.Enums;

namespace SupplySync.Models
{
	public class Receipt
	{

			[Key]
			public int ReceiptID { get; set; }

			[Required]
			public int DeliveryID { get; set; } 

			[Required]
			public int WarehouseID { get; set; } 

			[Required]
			public DateOnly Date { get; set; }

			[Required]
			[Range(1, int.MaxValue)]
			public int Quantity { get; set; }

			[Required]
			public ReceiptStatus Status { get; set; }

			[Required]
			public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

			public DateTime? UpdatedAt { get; set; }

			public virtual Warehouse Warehouse { get; set; } = default!;
			public virtual Delivery Delivery { get; set; } = default!; 
		
	}

}

	
	

