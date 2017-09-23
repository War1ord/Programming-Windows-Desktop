using System;

namespace TimeTracking.Business.Models
{
	public class EntityProperty
	{
		public EntityProperty(){}
		public EntityProperty(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
		public EntityProperty(Guid id, string name, string value)
		{
			Id = id;
			Name = name;
			Value = value;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
}