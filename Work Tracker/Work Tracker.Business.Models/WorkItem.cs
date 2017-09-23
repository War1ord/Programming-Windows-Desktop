using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work_Tracker.Business.Models
{
    public class WorkItem : ModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkItem"/> class.
        /// </summary>
        public WorkItem()
        {
            Steps = new List<Step>();
            DefaultChecks = new List<Check>();
        }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        public List<Step> Steps { get; set; }

        [NotMapped]
        /// <summary>
        /// Gets or sets the default checks.
        /// </summary>
        public List<Check> DefaultChecks { get; set; }
    }
}
