using System;
using Work_Tracker.Business.Models;

namespace Work_Tracker.Business
{
    public class Result : ModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public Result(ResultType type, string message)
        {
            Type = type;
            Message = message;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public Result(ResultType type, string message, Exception exception)
        {
            Type = type;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ResultType Type { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }
    }

    public class Result<T> : Result
    {
        public Result(ResultType type, string message) : base(type, message) { }
        public Result(ResultType type, string message, Exception exception) : base(type, message, exception) { }
        public Result(ResultType type, string message, T return_object) : base(type, message) { Return = return_object; }
        public Result(ResultType type, string message, T return_object, Exception exception) : base(type, message, exception) { Return = return_object; }

        public T Return { get; set; }

    }
}
