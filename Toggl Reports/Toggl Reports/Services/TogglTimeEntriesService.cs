using System;
using System.Linq;
using Toggl.QueryObjects;
using Toggl.Services;
using Toggl_Reports.Extentions;
using Toggl_Reports.Models;

namespace Toggl_Reports.Services
{
    public class TogglTimeEntriesService
    {
        private string apiKey;

        public TogglTimeEntriesService() { }
        public TogglTimeEntriesService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public string ApiKey { get => this.apiKey; set => this.apiKey = value; }

        public DateCollection[] GetTogglCalculatedEntriesByDate(DateRange dateRange)
        {
            var workspaces = new WorkspaceService(this.ApiKey).List();
            var clients = new ClientService(this.ApiKey).List();
            var projects = new ProjectService(this.ApiKey).List();
            var timeEntries = new TimeEntryService(this.ApiKey)
                .List(new TimeEntryParams
                {
                    StartDate = dateRange.FromDate,
                    EndDate = dateRange.ToDate,
                });
            var calculated = (
                from timeEntry in timeEntries
                let start = timeEntry.Start.AsDateTime()
                let stop = timeEntry.Stop.AsDateTime()
                let project = (from p in projects where timeEntry?.ProjectId == p.Id select p).FirstOrDefault()
                let client = (from c in clients where project?.ClientId == c.Id select c).FirstOrDefault()
                let workspace = (from w in workspaces where client?.WorkspaceId == w.Id select w).FirstOrDefault()
                select new
                {
                    WorkspaceName = workspace?.Name ?? "None",
                    ClientName = client?.Name ?? "None",
                    ProjectName = project?.Name ?? "None",
                    Description = timeEntry?.Description ?? "None",
                    Start = start,
                    Stop = stop,
                    Duration = stop - start,
                }
            )
            .GroupBy(c => new { c.WorkspaceName, c.ClientName, c.ProjectName, c.Description, Date = c.Start.Date })
            .GroupBy(g => g.Key.Date,
                g => new CalculatedTimeEntry
                {
                    WorkspaceName = g.Key.WorkspaceName,
                    ClientName = g.Key.ClientName,
                    ProjectName = g.Key.ProjectName,
                    Description = g.Key.Description,
                    Start = g.OrderBy(i => i.Start).FirstOrDefault()?.Start, /* Get the first Start Date */
                    Stop = g.OrderByDescending(i => i.Stop).FirstOrDefault()?.Stop, /* Get the last Stop Date */
                    DurationAggregated = g.Aggregate(TimeSpan.Zero, (a1, a2) => a1 + a2.Duration),
                }
            )
            ;
            var result = calculated
                .Select(g => new DateCollection
                {
                    Date = g.Key,
                    TimeEntries = g.ToArray(),
                })
                ;
            return result.ToArray();
        }

    }
}