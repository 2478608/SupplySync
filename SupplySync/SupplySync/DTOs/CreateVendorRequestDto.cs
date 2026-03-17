using System.ComponentModel.DataAnnotations;
using SupplySync.Constants.Enums;

namespace SupplySync.DTOs
{
	public class CreateVendorRequestDto
	{
		public string Name { get; set; }
		public string ContactInfo { get; set; }
		public VendorCategory Category { get; set; }
	}
}
