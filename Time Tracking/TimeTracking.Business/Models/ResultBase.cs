using TimeTracking.Business.Extensions;

namespace TimeTracking.Business.Models
{
	public abstract class ResultBase
	{
		public ResultBase()
		{
		}

		public ResultBase(DataTypes.Enums.ResultType type, string message)
		{
			Type = type;
			Message = message;
		}

		public DataTypes.Enums.ResultType Type { get; set; }
		public string Message { get; set; }

		public string TypeDescription
		{
			get { return Type.ToDescription(); }
		}

		public bool IsMessageSet
		{
			get { return !string.IsNullOrWhiteSpace(Message); }
		}
		public bool IsSuccessful
		{
			get { return Type == DataTypes.Enums.ResultType.Success; }
		}
		public bool IsInformation
		{
			get { return Type == DataTypes.Enums.ResultType.Information; }
		}
		public bool IsWarning
		{
			get { return Type == DataTypes.Enums.ResultType.Warning; }
		}
		public bool IsError
		{
			get { return Type == DataTypes.Enums.ResultType.Error; }
		}
		public bool IsCriticalError
		{
			get { return Type == DataTypes.Enums.ResultType.CriticalError; }
		}
	}
}