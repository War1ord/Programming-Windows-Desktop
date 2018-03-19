using Toggl_Reports.Extentions;

namespace Toggl_Reports.Models
{
    public abstract class ResultBase
    {
        private readonly ResultType type;
        private readonly string message;

        protected ResultBase() { }

        protected ResultBase(ResultType type, string message)
        {
            this.type = type;
            this.message = message;
        }

        public ResultType Type => this.type;

        public string Message => this.message;

        public bool GetIsMessageSet() => !this.Message.IsNullOrWhiteSpace();

        public bool GetIsSuccessful() => this.Type == ResultType.Success;

        public bool GetIsInformation() => this.Type == ResultType.Information;

        public bool GetIsWarning() => this.Type == ResultType.Warning;

        public bool GetIsError() => this.Type == ResultType.Error;

        public bool GetIsCriticalError() => this.Type == ResultType.CriticalError;
    }

    public class Result : ResultBase
    {
        public Result(ResultType type, string message) : base(type, message) { }

        public Result<T> ToResult<T>(T entity)
        {
            return new Result<T>(this.Type, this.Message, entity);
        }
    }

    public class Result<T> : ResultBase
    {
        private readonly T entity;

        public Result(ResultType type, string message, T entity) : base(type, message)
        {
            this.entity = entity;
        }

        public T Entity => this.entity;

        public bool GetIsValidEntity() => this.Entity != null;

        public Result ToResult()
        {
            return new Result(this.Type, this.Message);
        }
    }

    public static class Results
    {
        public static string GetSuccessMessage() => "Success";
        public static string GetFailedMessage() => "An unexpected error occurred.";
        public static string GetInvalidMessage() => "Invalid";

        public static Result Result(ResultType type, string message) => new Result(type, message);
        public static Result<T> Result<T>(ResultType type, string message, T entity) => new Result<T>(type, message, entity);
        public static Result<T> Result<T>(ResultType type, T entity) => new Result<T>(type, null, entity);

        public static Result SuccessResult() => Result(ResultType.Success, GetSuccessMessage());
        public static Result ErrorResult() => Result(ResultType.Error, GetFailedMessage());
        public static Result InvalidResult() => Result(ResultType.Warning, GetInvalidMessage());

        public static Result SuccessResult(string message) => Result(ResultType.Success, message);
        public static Result ErrorResult(string message) => Result(ResultType.Error, message);
        public static Result InvalidResult(string message) => Result(ResultType.Warning, message);

        public static Result<T> NoneResult<T>(T entity) => Result(ResultType.None, entity);
        public static Result<T> SuccessResult<T>(T entity) => Result(ResultType.Success, GetSuccessMessage(), entity);
        public static Result<T> ErrorResult<T>(T entity) => Result(ResultType.Error, GetFailedMessage(), entity);
        public static Result<T> InvalidResult<T>(T entity) => Result(ResultType.Warning, GetInvalidMessage(), entity);

        public static Result<T> NoneResult<T>(string message, T entity) => Result(ResultType.None, entity);
        public static Result<T> SuccessResult<T>(string message, T entity) => Result(ResultType.Success, message, entity);
        public static Result<T> ErrorResult<T>(string message, T entity) => Result(ResultType.Error, message, entity);
        public static Result<T> InvalidResult<T>(string message, T entity) => Result(ResultType.Warning, message, entity);

        public static Result<T> ErrorResult<T>(string message) => Result(ResultType.Error, message, default(T));

        public static Result<T> ErrorResult<T>() => Result(ResultType.Error, GetFailedMessage(), default(T));
    }

    public enum ResultType
    {
        None = 0,
        Success = 10,
        Information = 20,
        Warning = 30,
        Error = 40,
        CriticalError = 50,
    }
}