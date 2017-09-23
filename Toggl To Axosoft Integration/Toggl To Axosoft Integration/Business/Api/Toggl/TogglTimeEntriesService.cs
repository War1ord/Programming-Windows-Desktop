using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.QueryObjects;
using Toggl.Services;
using Toggl_To_Axosoft_Integration.Extentions;
using Toggl_To_Axosoft_Integration.Models;

namespace Toggl_To_Axosoft_Integration.Business.Api.Toggl
{
    public class ToggleEntries2
    {
        public int? Ticket { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public static class TogglTimeEntriesService
    {
        public static List<Models.Toggl.TimeEntryProjectCollection> GetTogglCalculatedDateEntries(DateRange dateRange)
        {
            var projects = new ProjectService(Config.ToggleApiKey).List();
            var entries = new TimeEntryService(Config.ToggleApiKey)
                .List(new TimeEntryParams
                {
                    StartDate = dateRange.FromDate,
                    EndDate = dateRange.ToDate,
                });
            var calculated = (
                from e in entries
                let t = e.Stop.AsDateTime()
                let f = e.Start.AsDateTime()
                join p in projects on e.ProjectId equals p.Id
                select new 
                {
                    Ticket = e.TagNames.FirstNumber(), 
                    ProjectName = p.Name,
                    Description = e.Description,
                    Start = f, 
                    Stop = t, 
                    Duration = t - f
                }
            ).ToList();
            var grouping = calculated
                .GroupBy(p => p.ProjectName)
                .ToDictionary(p => p.Key
                    , i => i.GroupBy(t => t.Ticket ?? "")
                        .ToDictionary(t => t.Key
                            , p => p.GroupBy(d => d.Start.Date)
                                .ToDictionary(t => t.Key
                                    , t => new
                                    {
                                        Duration = t.Aggregate(TimeSpan.Zero, (a1, a2) => a1 + a2.Duration),
                                        Details = t.ToList(),
                                    })));
            return grouping.Select(p => new Models.Toggl.TimeEntryProjectCollection
            {
                Project = p.Key,
                Tickets = p.Value.Select(t => new Models.Toggl.TimeEntryTicketCollection
                {
                    Ticket = t.Key.IsSet() ? t.Key.AsInt() : (int?) null,
                    Dates = t.Value.Select(d => new Models.Toggl.TimeEntryDateCollection
                    {
                        Date = d.Key,
                        Duration = d.Value.Duration,
                        TimeEntries = d.Value.Details.Select(i => new Models.Toggl.TimeEntry
                        {
                            Description = i.Description,
                            Duration = i.Duration,
                            ProjectName = i.ProjectName,
                            Start = i.Start,
                            Stop = i.Stop,
                            Ticket = i.Ticket.IsSet() ? i.Ticket.AsInt() : (int?)null,
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList();
        }

    }
}