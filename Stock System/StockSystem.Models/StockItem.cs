using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockSystem.Models
{
	public class StockItem : Base
	{
		[Required]
		public string Name { get; set; }

		public StockType Type { get; set; }

		public Area Area { get; set; }

		[NotMapped]
		public string Display { get { return string.Format("{0}, Area: {1}, Type: {2}", Name, Area != null ? Area.Name : "None", Type != null ? Type.Name : "None"); } }

		public override string ToString()
		{
			return Display;
		}
	}
}