using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class Unit : Base
	{
	    public UnitNames Key { get; set; }
	    [StringLength(20)]
	    public string Name { get; set; }
	    [StringLength(20)]
	    public string Group { get; set; }
	}
}
