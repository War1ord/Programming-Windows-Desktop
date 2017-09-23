using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockSystem.Models
{
	public class AreaType : Base
	{
		[Required]
		public string Name { get; set; }

		public List<Area> Areas { get; set; }
	}
}