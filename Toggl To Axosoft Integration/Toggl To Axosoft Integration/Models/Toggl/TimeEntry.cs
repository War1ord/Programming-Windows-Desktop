namespace Toggl_To_Axosoft_Integration.Models.Toggl
{
    public class TimeEntry
    {
        public string Description { get; set; }
        public System.TimeSpan Duration { get; set; }
        public string ProjectName { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime Stop { get; set; }
        public int? Ticket { get; set; }
    }
}