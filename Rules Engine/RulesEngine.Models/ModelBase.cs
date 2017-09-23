using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RulesEngine.Models
{
	public abstract class ModelBase
	{
		[Key, Column(Order = 0)]
		public Guid ID { get; set; }
	}
}
