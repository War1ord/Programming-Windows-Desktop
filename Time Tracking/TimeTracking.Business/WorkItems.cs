using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracking.Business.Models;
using TimeTracking.Models;

namespace TimeTracking.Business
{
	public class WorkItems
	{
		public Result Start(WorkItem item)
		{
            try
            {
                using (var db = new TimeTracking.Data.DataContext())
                {
                    item.StartDateTime = DateTime.Now;
                    db.WorkItems.Add(item);
                    db.SaveChanges();
                    return Results.SuccessResult("Started");
                }
            }
            catch (Exception e)
            {
                return Results.ErrorResult();
            }
		}

        public Result End(WorkItem item)
		{
            try
            {
                using (var db = new TimeTracking.Data.DataContext())
                {
                    var workItem = db.WorkItems.FirstOrDefault(i => i.Guid == item.Guid);
                    if (workItem != null)
                    {
                        workItem.EndDateTime = DateTime.Now;
                        workItem.Description = item.Description;
                        workItem.Reference = item.Reference;
                        db.SaveChanges();
                        return Results.SuccessResult("Ended");
                    }
                    else
                    {
                        return Results.ErrorResult("Work Item not Found");
                    }
                }
            }
            catch (Exception e)
            {
                return Results.ErrorResult();
            }
		}

        public List<ClientOrProject> GetClientsOrProjectsList()
        {
            try
            {
                using (var db = new TimeTracking.Data.DataContext())
                {
                    return db.ClientsOrProjects.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<ClientOrProject>();
            }
        }
	}
}