using System;

namespace Work_Tracker.Business
{
    public static class ResultWrapper
    {
        /// <summary>
        /// Returns the error result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static Result ToErrorResult(this Exception exception)
        {
            return new Result(ResultType.Error, exception.Message, exception);
        }

        /// <summary>
        /// Returns the error result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public static Result ToErrorResult(this Exception exception, string message)
        {
            return new Result(ResultType.Error, message, exception);
        }
    }
}
