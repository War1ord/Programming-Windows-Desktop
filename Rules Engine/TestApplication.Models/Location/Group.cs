using System.Collections.Generic;

namespace TestApplication.Models.Location
{
	public class Group : ModelBase//EntityTypeConfiguration<Group>
	{
		public string Name { get; set; }

		public List<Category> Categories { get; set; }
	}
}