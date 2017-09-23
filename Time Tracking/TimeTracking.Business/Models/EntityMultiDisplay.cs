using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracking.Business.Models
{
	public class EntityMultiDisplay
	{
		public EntityMultiDisplay()
		{
			ValueProperties = new Dictionary<string, string>();
			EntityProperties = new Dictionary<string, KeyValuePair<Guid, string>>();
			NavigationProperties = new Dictionary<string, EntityMultiDisplay>();
		}
		public EntityMultiDisplay(Guid id, List<RequestForService.Business.Models.ValueProperty> valueProperties)
		{
			Id = id;
			ValueProperties = valueProperties.ToDictionary(i => i.Name, i => i.Value);
		}
		public EntityMultiDisplay(Guid id, List<RequestForService.Business.Models.ValueProperty> valueProperties, List<EntityProperty> entityProperties)
		{
			Id = id;
			ValueProperties = valueProperties.ToDictionary(i => i.Name, i => i.Value);
			EntityProperties = entityProperties.ToDictionary(i => i.Name, i => new KeyValuePair<Guid, string>(i.Id, i.Value));
		}
		public EntityMultiDisplay(Guid id, List<RequestForService.Business.Models.ValueProperty> valueProperties, Dictionary<string, EntityMultiDisplay> navigationProperties)
		{
			Id = id;
			ValueProperties = valueProperties.ToDictionary(i => i.Name, i => i.Value);
			NavigationProperties = navigationProperties;
		}
		public EntityMultiDisplay(Guid id, List<RequestForService.Business.Models.ValueProperty> valueProperties, List<EntityProperty> entityProperties, Dictionary<string, EntityMultiDisplay> navigationProperties)
		{
			Id = id;
			ValueProperties = valueProperties.ToDictionary(i => i.Name, i => i.Value);
			EntityProperties = entityProperties.ToDictionary(i => i.Name, i => new KeyValuePair<Guid, string>(i.Id, i.Value));
			NavigationProperties = navigationProperties;
		}

		public Guid Id { get; set; }
		public Dictionary<string, string> ValueProperties { get; set; }
		public Dictionary<string, EntityMultiDisplay> NavigationProperties { get; set; }
		public Dictionary<string, KeyValuePair<Guid, string>> EntityProperties { get; set; }
	}
}