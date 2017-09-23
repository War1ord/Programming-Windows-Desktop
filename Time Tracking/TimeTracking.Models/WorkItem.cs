using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracking.Models
{
	public class WorkItem : ModelBase
	{
	    private List<ClientOrProject> _clientsOrProjects;

        [Required]
	    public DateTime StartDateTime { get; set; }
		public DateTime? EndDateTime { get; set; }
        [Required]
        public string Description { get; set; }
		public string Reference { get; set; }
		public string ExtraReference { get; set; }
		public bool IsLogged { get; set; }

        [Required]
	    public Guid ClientOrProjectGuid { get; set; }

        [Display(Name = "Client Or Project")]
        [ForeignKey("ClientOrProjectGuid")]
        public ClientOrProject ClientOrProject { get; set; }
        
        [NotMapped]
        public string Display { get { return string.Format("{0} {1}", StartDateTime.ToString("dd MMM yyyy hh:mm"), Reference); } }

	}
}