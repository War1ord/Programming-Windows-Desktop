using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Category : Base
    {
		[Required]
		[StringLength(50)]
        public string Name { get; set; }
    }
}