namespace RequestForService.Business.Models
{
	public class ValueProperty
	{
		public ValueProperty(){}
		public ValueProperty(string name)
		{
			Name = name;
		}
		public ValueProperty(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public string Value { get; set; }
	}
}