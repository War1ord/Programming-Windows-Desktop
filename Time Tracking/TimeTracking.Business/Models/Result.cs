namespace TimeTracking.Business.Models
{
	public class Result : ResultBase
	{
		public Result(DataTypes.Enums.ResultType type, string message) 
			: base(type, message){}

		public Result<T> ToResult<T>(T entity)
		{
			return new Result<T>(Type, Message, entity);
		}

	}

	public class Result<T> : ResultBase
	{
		public Result(DataTypes.Enums.ResultType type, string message, T entity) 
			: base(type, message)
		{
			Entity = entity;
		}

		public T Entity { get; private set; }

		public bool IsValidEntity { get { return Entity != null; } }

		public Result ToResult()
		{
			return new Result(Type, Message);
		}

	}

	public static class Results
	{
		public static string SuccessMessage
		{
			get { return "Success"; }
		}
		public static string FailedMessage
		{
			get { return "An unexpected error occurred."; }
		}

		public static Result Result(DataTypes.Enums.ResultType type, string message)
		{
			return new Result(type, message);
		}
		public static Result<T> Result<T>(DataTypes.Enums.ResultType type, string message, T entity)
		{
			return new Result<T>(type, message, entity);
		}
		public static Result<T> Result<T>(DataTypes.Enums.ResultType type, T entity)
		{
			return new Result<T>(type, null, entity);
		}

		public static Result SuccessResult()
		{
			return Result(DataTypes.Enums.ResultType.Success, SuccessMessage);
		}
		public static Result ErrorResult()
		{
			return Result(DataTypes.Enums.ResultType.Error, FailedMessage);
		}
		public static Result InvalidResult()
		{
			return Result(DataTypes.Enums.ResultType.Warning, "Invalid");
		}

		public static Result SuccessResult(string message)
		{
			return Result(DataTypes.Enums.ResultType.Success, message);
		}
		public static Result ErrorResult(string message)
		{
			return Result(DataTypes.Enums.ResultType.Error, message);
		}
		public static Result InvalidResult(string message)
		{
			return Result(DataTypes.Enums.ResultType.Warning, message);
		}

		public static Result<T> NoneResult<T>(T entity)
		{
			return Result(DataTypes.Enums.ResultType.None, entity);
		}
		public static Result<T> SuccessResult<T>(T entity)
		{
			return Result(DataTypes.Enums.ResultType.Success, SuccessMessage, entity);
		}
		public static Result<T> ErrorResult<T>(T entity)
		{
			return Result(DataTypes.Enums.ResultType.Error, FailedMessage, entity);
		}
		public static Result<T> InvalidResult<T>(T entity)
		{
			return Result(DataTypes.Enums.ResultType.Warning, "Invalid", entity);
		}

		public static Result<T> NoneResult<T>(string message, T entity)
		{
			return Result(DataTypes.Enums.ResultType.None, entity);
		}
		public static Result<T> SuccessResult<T>(string message, T entity)
		{
			return Result(DataTypes.Enums.ResultType.Success, message, entity);
		}
		public static Result<T> ErrorResult<T>(string message, T entity)
		{
			return Result(DataTypes.Enums.ResultType.Error, message, entity);
		}
		public static Result<T> InvalidResult<T>(string message, T entity)
		{
			return Result(DataTypes.Enums.ResultType.Warning, message, entity);
		}

		public static Result<T> ErrorResult<T>(string message)
		{
			return Result(DataTypes.Enums.ResultType.Error, message, default(T));
		}

		public static Result<T> ErrorResult<T>()
		{
			return Result(DataTypes.Enums.ResultType.Error, FailedMessage, default(T));
		}
	}
}