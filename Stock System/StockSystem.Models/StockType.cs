using System.ComponentModel.DataAnnotations;

namespace StockSystem.Models
{
	public class StockType : Base
	{
		[Required]
		public string Name { get; set; }

		public StockTypeGroup Group { get; set; }
	}
}