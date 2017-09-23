using Common.Messages;
using Data;
using EntityFramework.Extensions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
	/// <summary>
	/// Log errors
	/// </summary>
	public static class Errors
	{
		#region Properties

		/// <summary>
		/// Gets the error message.
		/// </summary>
		/// <value>
		/// The error.
		/// </value>
		public static string Error { get; private set; }
		/// <summary>
		/// Gets a value indicating whether there is an error.
		/// </summary>
		/// <value>
		///   <c>true</c> if [has error]; otherwise, <c>false</c>.
		/// </value>
		public static bool HasError { get { return !string.IsNullOrWhiteSpace(Error); } }
		/// <summary>
		/// Gets the list of all the errors.
		/// </summary>
		/// <value>
		/// The list.
		/// </value>
		public static List<Error> List
		{
			get
			{
				using (var db = new DataContext())
				{
					return db.Errors.OrderByDescending(i => i.ErrorDate).ToList();
				}
			}
		}

		#endregion

		#region Delegates

		/// <summary>
		/// The delegate to handle the logging of the exception to the Data Context.
		/// </summary>
		/// <param name="exception">The Exception.</param>
		private delegate void LogExceptionCompletedEventHandler(Exception exception);

		#endregion

		#region Methods

		/// <summary>
		/// Log exceptions to database (this will be coding exceptions)
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <returns>A Friendly error text.</returns>
		public static string Log(Exception exception)
		{
			LogExceptionCompletedEventHandler log = WriteToDb;
			log.BeginInvoke(exception, null, null);
			return string.Format(CommonMessages.UnexpectedErrorFormat, exception.Message);
		}
		/// <summary>
		/// Write the error to the database
		/// </summary>
		/// <param name="exception">The exception.</param>
		private static void WriteToDb(Exception exception)
		{
		    try
		    {
		        using (var db = new DataContext())
		        {
		            db.Errors.Add(new Error(exception));
		            db.SaveChanges();
		        }
		    }
			catch (Exception e){}
		}
		/// <summary>
		/// Deletes all the errors.
		/// </summary>
		/// <returns></returns>
		public static bool DeleteAll()
		{
			try
			{
				using (var db = new DataContext())
				{
					return db.Errors.Delete() > 0;
				}
			}
			catch (Exception e)
			{
				Error = string.Format(CommonMessages.UnexpectedErrorFormat, e.Message);
				return false;
			}
		}

		#endregion
	}
}