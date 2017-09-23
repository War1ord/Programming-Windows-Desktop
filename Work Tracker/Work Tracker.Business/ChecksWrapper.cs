using Work_Tracker.Business.Models;

namespace Work_Tracker.Business
{
    public static class StepManagementWrapper
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="step">The step.</param>
        public static StepManagementService GetService(this Step step)
        {
            return new StepManagementService(step);
        }
    }
}
