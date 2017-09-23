using System.ComponentModel.DataAnnotations;

namespace TimeTracking.Models
{
    public class ClientOrProject : ModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}