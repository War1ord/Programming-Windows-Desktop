using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.Models
{
	public abstract class ModelBase
	{
		[Key, Column(Order = 0)]
		public int ID { get; set; }
	}
}