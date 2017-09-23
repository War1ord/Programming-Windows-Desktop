using System;
using System.Linq;
using EntityFramework.Extensions;

namespace TimeTracking.Business
{
    /// <summary>
    /// a class containing all business logic for Client or Project data
    /// </summary>
    public class ClientsOrProjects
    {
        /// <summary>
        /// Deletes the specified client or project.
        /// </summary>
        /// <param name="clientOrProject">The client or project.</param>
        /// <returns></returns>
        public Models.Result Delete(TimeTracking.Models.ClientOrProject clientOrProject)
        {
            try
            {
                using (var db = new TimeTracking.Data.DataContext())
                {
                    if (db.ClientsOrProjects.Any(i => i.Guid == clientOrProject.Guid))
                    {
                        if (!db.WorkItems.Any(i => i.ClientOrProjectGuid == clientOrProject.Guid))
                        {
                            db.ClientsOrProjects.Where(i => i.Guid == clientOrProject.Guid).Delete();
                            return Models.Results.SuccessResult("Client Or Project Deleted.");
                        }
                        else
                        {
                            return Models.Results.ErrorResult("There is some work items linked to this Client or Project. You need to relink these work items to another Client or Project before you can delete this Client of Project.");
                        }
                    }
                    else
                    {
                        return Models.Results.ErrorResult("This Client or Project does not exists or was not yet saved.");
                    }
                }
            }
            catch (Exception e)
            {
                return Models.Results.ErrorResult();
            }
        }

        public Models.Result Update(TimeTracking.Models.ClientOrProject clientOrProject)
        {
            try
            {
                using (var db = new TimeTracking.Data.DataContext())
                {
                    if (db.ClientsOrProjects.Any(i => i.Guid == clientOrProject.Guid))
                    {
                        db.ClientsOrProjects
                            .Where(i => i.Guid == clientOrProject.Guid)
                            .Update(i => new TimeTracking.Models.ClientOrProject
                            {
                                Name = clientOrProject.Name
                            });
                    }
                    else
                    {
                        db.ClientsOrProjects.Add(clientOrProject);
                        db.SaveChanges();
                    }
                }
                return Models.Results.SuccessResult("Client or Project saved successfully.");
            }
            catch (Exception e)
            {
                return Models.Results.ErrorResult();
            }
        }
    }
}