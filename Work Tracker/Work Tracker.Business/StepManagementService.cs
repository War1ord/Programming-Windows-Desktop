using System;
using System.Linq;
using Work_Tracker.Business.Models;
using Work_Tracker.Data.Extentions;

namespace Work_Tracker.Business
{
    public class StepManagementService : ServiceBase
    {
        /// <summary>
        /// The _step
        /// </summary>
        private Step _step;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepManagementService"/> class.
        /// </summary>
        /// <param name="step">The step.</param>
        public StepManagementService(Step step)
        {
            _step = step ?? new Step();
        }

        /// <summary>
        /// Adds a check to this step
        /// </summary>
        public StepManagementService Add(Check check)
        {
            try
            {
                _step.Checks.Add(check);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Removes a check from this step
        /// </summary>
        public StepManagementService Remove(Check check)
        {
            try
            {
                _step.Checks.Remove(check);
                //mark this item to be deleted on the db
                Db.Delete(check);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Adds an extra to this step
        /// </summary>
        public StepManagementService Add(Extra extra)
        {
            try
            {
                _step.Extras.Add(extra);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Removes an extra from this step
        /// </summary>
        public StepManagementService Remove(Extra extra)
        {
            try
            {
                _step.Extras.Remove(extra);
                //mark this item to be deleted on the db
                Db.Delete(extra);
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Loads the checks for this step
        /// </summary>
        public StepManagementService LoadChecks()
        {
            try
            {
                _step.Checks = Db.Steps.Where(i => i.Id == _step.Id).Select(i => i.Checks.Where(c => !c.IsDefault).ToList()).FirstOrDefault();
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Saves these checks
        /// </summary>
        public StepManagementService SaveChecks()
        {
            try
            {
                foreach (var check in _step.Checks)
                {
                    Db.Save(check);
                }
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Loads the extras for this step
        /// </summary>
        public StepManagementService LoadExtras()
        {
            try
            {
                _step.Extras = Db.Steps.Where(i => i.Id == _step.Id).Select(i => i.Extras).FirstOrDefault();
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Saves these extras
        /// </summary>
        public StepManagementService SaveExtras()
        {
            try
            {
                foreach (var extra in _step.Extras)
                {
                    Db.Save(extra);
                }
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Loads all the associated lists
        /// </summary>
        public StepManagementService Load()
        {
            try
            {
                _step.Checks = Db.Steps.Where(i => i.Id == _step.Id).Select(i => i.Checks).FirstOrDefault();
                _step.Extras = Db.Steps.Where(i => i.Id == _step.Id).Select(i => i.Extras).FirstOrDefault();
            }
            catch (Exception e)
            {
                Result = e.ToErrorResult();
            }
            return this;
        }

        /// <summary>
        /// Saves all the associated lists
        /// </summary>
        public StepManagementService Save()
        {
            try
            {
                foreach (var check in _step.Checks)
                {
                    Db.Save(check);
                }
                foreach (var extra in _step.Extras)
                {
                    Db.Save(extra);
                }
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
