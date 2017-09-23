using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Extentions;

namespace Models
{
	public abstract class Base
	{
	    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public int Id { get; set; }
	}
}
