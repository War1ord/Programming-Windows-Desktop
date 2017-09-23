using System;

namespace TimeTracking.Business.Models
{
	public class EntityDisplay
	{
		public EntityDisplay() {}
		public EntityDisplay(Guid id, string value)
		{
			Id = id;
			Value = value;
		}

		public Guid Id { get; set; }
		public string Value { get; set; }
	}
}