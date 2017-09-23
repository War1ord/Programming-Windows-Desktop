namespace TestApplication.Models.Location
{
	public class Category : ModelBase
	{
		public string Name { get; set; }

		public Group Group { get; set; }
	}
}