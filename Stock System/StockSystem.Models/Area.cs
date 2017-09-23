using System.ComponentModel.DataAnnotations;

namespace StockSystem.Models
{
	public class Area : Base
	{
		[Required]
		public string Name { get; set; }

		public AreaType Type { get; set; }
	}
}