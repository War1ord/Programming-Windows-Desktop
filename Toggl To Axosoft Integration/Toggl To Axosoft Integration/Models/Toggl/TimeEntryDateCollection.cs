namespace Toggl_To_Axosoft_Integration.Models.Toggl
{
    public class TimeEntryDateCollection
    {
        public System.DateTime Date { get; set; }
        public System.TimeSpan Duration { get; set; }
        public System.Collections.Generic.List<Models.Toggl.TimeEntry> TimeEntries { get; set; }
    }
}