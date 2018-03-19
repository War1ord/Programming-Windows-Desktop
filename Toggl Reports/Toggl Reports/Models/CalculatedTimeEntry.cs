using System;

namespace Toggl_Reports.Models
{
    public class CalculatedTimeEntry
    {
        public string WorkspaceName { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }
        public TimeSpan DurationAggregated { get; set; }

        public override string ToString()
        {
            return $@"{this.ClientName} | {this.ProjectName} | {this.Description} | {this.Start?.ToShortDateString() ?? "None"} | {this.DurationAggregated}({this.DurationAggregated.TotalHours.ToString("0.##")})";
        }
    }
}