namespace Toggl_To_Axosoft_Integration.Models.Toggl
{
    public class TimeEntryProjectCollection
    {
        public string Project { get; set; }
        public System.Collections.Generic.List<TimeEntryTicketCollection> Tickets { get; set; }
    }
}