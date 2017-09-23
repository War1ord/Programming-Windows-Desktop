using System;
using System.Collections.Generic;
using System.Linq;
using Work_Tracker.Business.Models;

namespace Work_Tracker.Business
{
    public class WorkTrackerService : ServiceBase
    {
        /// <summary>
        /// The _work list
        /// </summary>
        private List<WorkItem> _workList;

        /// <summary>
        /// Gets a default new instance of this class.
        /// </summary>
        public static WorkTrackerService Default
        {
            get
            {
                return new WorkTrackerService();
            }
        }

        /// <summary>
        /// Contains a list of work
        /// </summary>
        public List<WorkItem> WorkItems
        {
            get { return _workList ?? (_workList = new List<WorkItem>()); }
            set { _workList = value; }
        }

        /// <summary>
        /// Adds a piece of work to the work list
        /// </summary>
        public WorkTrackerService Add(WorkItem work)
        {
            try
            {
                WorkItems.Add(work);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Removes a piece of work from the work list
        /// </summary>
        public WorkTrackerService Remove(WorkItem work)
        {
            try
            {
                WorkItems.Remove(work);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Loads the work list from db.
        /// </summary>
        public WorkTrackerService Load()
        {
            try
            {
                WorkItems = Db.WorkItems.ToList();
                foreach (var work in WorkItems)
                {
                    var itemQuery = Db.WorkItems.Where(i => i.Id == work.Id);
                    work.DefaultChecks = itemQuery.Select(i => i.DefaultChecks).FirstOrDefault();
                    work.Steps = itemQuery.Select(i => i.Steps).FirstOrDefault();
                    foreach (var step in work.Steps)
                    {
                        var stepQuery = Db.Steps.Where(i => i.Id == step.Id);
                        step.Checks = stepQuery.Select(i => i.Checks).FirstOrDefault();
                        step.Extras = stepQuery.Select(i => i.Extras).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        ///// <summary>
        ///// Saves the list of work to the db
        ///// </summary>
        //public WorkTrackerService Save()
        //{
        //    try
        //    {
        //        foreach (var item in WorkItems)
        //        {
        //            Db.Save(item);
        //        }
        //        Db.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Result = e.ToErrorResult();
        //    }
        //    return this;
        //}

    }
}
