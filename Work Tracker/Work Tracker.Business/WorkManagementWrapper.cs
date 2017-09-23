using Work_Tracker.Business.Models;

namespace Work_Tracker.Business
{
    public static class WorkManagementWrapper
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="work">The work.</param>
        public static WorkManagementService GetService(this WorkItem work)
        {
            return new WorkManagementService(work);
        }
    }
}
