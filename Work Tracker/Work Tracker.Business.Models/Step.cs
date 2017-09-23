using System.Collections.Generic;

namespace Work_Tracker.Business.Models
{
    public class Step : ModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Step"/> class.
        /// </summary>
        public Step()
        {
            Checks = new List<Check>();
            Extras = new List<Extra>();
        }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the checks.
        /// </summary>
        public List<Check> Checks { get; set; }
        /// <summary>
        /// Gets or sets the extras.
        /// </summary>
        public List<Extra> Extras { get; set; }
    }
}
