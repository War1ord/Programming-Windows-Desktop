using System;

namespace Sql_Auto_Data_Discovery.Business.Models.Commom
{
    public abstract class ResultBase
    {
        protected ResultBase() { }

        protected ResultBase(ResultType type, string message)
        {
            Type = type;
            Message = message;
        }

        protected ResultBase(ResultType type, string message, Exception exception)
        {
            Type = type;
            Message = message;
            Exception = exception;
        }

        public ResultType Type { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public bool IsMessageSet
        {
            get { return !string.IsNullOrEmpty(Message); }
        }
        public bool IsSuccessful
        {
            get { return Type == ResultType.Success; }
        }
        public bool IsInformation
        {
            get { return Type == ResultType.Information; }
        }
        public bool IsWarning
        {
            get { return Type == ResultType.Warning; }
        }
        public bool IsError
        {
            get { return Type == ResultType.Error; }
        }
        public bool IsCriticalError
        {
            get { return Type == ResultType.CriticalError; }
        }

        public bool IsNotSuccessful
        {
            get { return Type != ResultType.Success; }
        }
        public bool IsNotInformation
        {
            get { return Type != ResultType.Information; }
        }
        public bool IsNotWarning
        {
            get { return Type != ResultType.Warning; }
        }
        public bool IsNotError
        {
            get { return Type != ResultType.Error; }
        }
        public bool IsNotCriticalError
        {
            get { return Type != ResultType.CriticalError; }
        }

    }

    public class Result : ResultBase
    {
        public Result(ResultType type, string message, Exception exception) : base(type, message, exception) { }

        public Result(ResultType type, string message) : base(type, message) { }

        public Result<T> ToResult<T>(T entity)
        {
            return new Result<T>(Type, Message, entity);
        }
    }

    public class Result<T> : ResultBase
    {
        public Result(ResultType type, string message, T entity) : base(type, message)
        {
            Entity = entity;
        }

        public T Entity { get; private set; }

        public bool IsEntityValid { get { return IsTypeAbleToCheckForNull ? Entity != null : true; } }

        private bool IsTypeAbleToCheckForNull
        {
            get
            {
                var type = Entity.GetType();
                return type.IsClass
                       || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
            }
        }

        public Result ToResult()
        {
            return new Result(Type, Message);
        }
    }

    public static class Results
    {
        public static string SuccessMessage { get { return "Success"; } }
        public static string FailedMessage { get { return "An unexpected error occurred."; } }
        public static string InvalidMessage { get { return "Invalid"; } }

        public static Result Result(ResultType type, string message)
        {
            return new Result(type, message);
        }
        public static Result<T> Result<T>(ResultType type, string message, T entity)
        {
            return new Result<T>(type, message, entity);
        }
        public static Result<T> Result<T>(ResultType type, T entity)
        {
            return new Result<T>(type, null, entity);
        }

        public static Result Result(ResultType type, string message, Exception exception)
        {
            return new Result(type, message, exception);
        }

        public static Result None()
        {
            return Result(ResultType.None, SuccessMessage);
        }
        public static Result Success()
        {
            return Result(ResultType.Success, SuccessMessage);
        }
        public static Result Error()
        {
            return Result(ResultType.Error, FailedMessage);
        }
        public static Result Invalid()
        {
            return Result(ResultType.Warning, InvalidMessage);
        }

        public static Result Success(string message)
        {
            return Result(ResultType.Success, message);
        }
        public static Result Error(string message)
        {
            return Result(ResultType.Error, message);
        }
        public static Result Invalid(string message)
        {
            return Result(ResultType.Warning, message);
        }

        public static Result<T> None<T>(T entity)
        {
            return Result(ResultType.None, entity);
        }
        public static Result<T> Success<T>(T entity)
        {
            return Result(ResultType.Success, SuccessMessage, entity);
        }
        public static Result<T> Error<T>(T entity)
        {
            return Result(ResultType.Error, FailedMessage, entity);
        }
        public static Result<T> Invalid<T>(T entity)
        {
            return Result(ResultType.Warning, InvalidMessage, entity);
        }

        public static Result<T> None<T>(string message, T entity)
        {
            return Result(ResultType.None, entity);
        }
        public static Result<T> Success<T>(string message, T entity)
        {
            return Result(ResultType.Success, message, entity);
        }
        public static Result<T> Error<T>(string message, T entity)
        {
            return Result(ResultType.Error, message, entity);
        }
        public static Result<T> Invalid<T>(string message, T entity)
        {
            return Result(ResultType.Warning, message, entity);
        }

        public static Result<T> Error<T>(string message)
        {
            return Result(ResultType.Error, message, default(T));
        }

        public static Result<T> Invalid<T>(string message)
        {
            return Result(ResultType.Warning, message, default(T));
        }

        public static Result<T> Error<T>()
        {
            return Result(ResultType.Error, FailedMessage, default(T));
        }
        public static Result<T> Invalid<T>()
        {
            return Result(ResultType.Warning, InvalidMessage, default(T));
        }

        public static Result Error(string message, Exception exception)
        {
            return Result(ResultType.Error, message, exception);
        }

        public static Result ToResult(this Exception exception)
        {
            return Error(string.Format("{0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace), exception);
        }

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