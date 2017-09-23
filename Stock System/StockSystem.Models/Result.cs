namespace StockSystem.Models
{
	public class Result
	{
		public Result()
		{
			Message = string.Empty;
			Type = ResultType.None;
		}

		public Result(string message, ResultType type)
		{
			Message = message;
			Type = type;
		}

		public string Message { get; set; }

		public ResultType Type { get; set; }
	}

	public enum ResultType
	{
		None,
		Information,
		Warning,
		Error,
		Success,
	}
}