namespace Toggl_To_Axosoft_Integration.Models.Toggl
{
    public class TimeEntryTicketCollection
    {
        public int? Ticket { get; set; }
        public System.Collections.Generic.List<Models.Toggl.TimeEntryDateCollection> Dates { get; set; }
    }
}