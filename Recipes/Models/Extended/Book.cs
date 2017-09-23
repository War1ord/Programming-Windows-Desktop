using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [ComplexType]
    public class Book
    {
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string ISDN { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please select a page number.")]
        public int Page { get; set; }
    }
}