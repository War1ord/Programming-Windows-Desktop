using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockSystem.Models
{
	public class StockTypeGroup : Base
	{
		[Required]
		public string Name { get; set; }

		public List<StockType> StockTypes { get; set; }
	}
}