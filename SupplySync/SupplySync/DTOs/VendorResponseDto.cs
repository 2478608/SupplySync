using SupplySync.Constants.Enums;

namespace SupplySync.DTOs
{
	public class VendorResponseDto
	{
		public string Name { get; set; }
		public string ContactInfo { get; set; }
		public VendorCategory Category { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
