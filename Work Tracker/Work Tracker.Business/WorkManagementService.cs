using System;
using System.Linq;
using Work_Tracker.Business.Models;
using Work_Tracker.Data.Extentions;

namespace Work_Tracker.Business
{
    public class WorkManagementService : ServiceBase
    {
        /// <summary>
        /// The _work
        /// </summary>
        private WorkItem _work;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkManagementService"/> class.
        /// </summary>
        /// <param name="work">The work.</param>
        public WorkManagementService(WorkItem work)
        {
            _work = work ?? new WorkItem();
        }

        /// <summary>
        /// Adds a step to this piece of work
        /// </summary>
        /// <param name="step">The step.</param>
        public WorkManagementService AddStep(Step step)
        {
            try
            {
                //Adds the default checks
                if (_work.DefaultChecks.Any() && step != null)
                {
                    step.Checks.AddRange(_work.DefaultChecks);
                }
                //Adds the step to the referenced piece of work 
                _work.Steps.Add(step);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Removes a step from this piece of work
        /// </summary>
        /// <param name="step">The step.</param>
        public WorkManagementService RemoveStep(Step step)
        {
            try
            {
                _work.Steps.Remove(step);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Adds a default step to this piece of work.
        /// </summary>
        /// <param name="check">The check.</param>
        public WorkManagementService AddDefaultCheck(Check check)
        {
            try
            {
                check.IsDefault = true;
                _work.DefaultChecks.Add(check);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Removes a default step from the piece of work.
        /// </summary>
        /// <param name="check">The check.</param>
        public WorkManagementService RemoveDefaultCheck(Check check)
        {
            try
            {
                _work.DefaultChecks.Remove(check);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Loads the steps for this piece of work
        /// </summary>
        public WorkManagementService Load()
        {
            try
            {
                var work = Db.WorkItems.FirstOrDefault(i => i.Id == _work.Id);
                if (work != null) _work.Steps = work.Steps;
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Saves these steps
        /// </summary>
        public WorkManagementService Save()
        {
            try
            {
                Db.Save(_work);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

    }
}
