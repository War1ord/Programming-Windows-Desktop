using System.Data.Entity;
using System.Linq;
using Work_Tracker.Business.Models;

namespace Work_Tracker.Data.Extentions
{
    public static class WorkTrackerDataContextExtensions
    {
        /// <summary>
        /// Saves the specified data context.
        /// </summary>
        /// <typeparam name="T">the type of model entity</typeparam>
        /// <param name="dataContext">The data context.</param>
        /// <param name="entity">The entity.</param>
        public static void Save<T>(this WorkTrackerDataContext dataContext, T entity) where T : ModelBase
        {
            if (dataContext.Set<T>().Any(item => item.Id == entity.Id))
            {
                dataContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                dataContext.Entry(entity).State = EntityState.Added;
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataContext">The data context.</param>
        /// <param name="entity">The entity.</param>
        public static void Delete<T>(this WorkTrackerDataContext dataContext, T entity) where T : ModelBase
        {
            if (dataContext.Set<T>().Any(item => item.Id == entity.Id))
            {
                dataContext.Entry(entity).State = EntityState.Deleted;
            }
        }

    }
}
