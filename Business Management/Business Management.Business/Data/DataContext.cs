using Business_Management.Business.Models;
using System;

namespace Business_Management.Business.Data
{
	public static class DataContext
	{
		public static Result InitializeDatabase()
		{
			try
			{
				Business_Management.Data.DataContext.InitializeDatabase();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				return Results.ErrorResult(e);
			}
		}
	}
}
