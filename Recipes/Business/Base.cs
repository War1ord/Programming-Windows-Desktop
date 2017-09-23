using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using Data;
using Data.Extensions;
using System;
using System.Collections.Generic;
using Extentions;
using Models;

namespace Business
{
	public abstract class Base : IDisposable
	{
		#region Fields

		protected readonly DataContext Db = null;

		#endregion

		#region Properties

		public string UserFriendlyError { get; protected set; }
		public string UserFriendlyMessage { get; protected set; }
		public bool HasError
		{
			get { return !string.IsNullOrWhiteSpace(UserFriendlyError); }
		}
		public bool HasMessage
		{
			get { return !string.IsNullOrWhiteSpace(UserFriendlyMessage); }
		}
		public bool DbOnline
		{
			get { return Db != null && Db.Online; }
		}

		#endregion

		#region Constructors

		protected Base()
		{
			UserFriendlyError = string.Empty;
			UserFriendlyMessage = string.Empty;
			Db = new DataContext();
		}

		#endregion

		#region Save

		protected bool SaveEntity<T>(T entity) where T : Models.Base
		{
			try
			{
				Db.Save(entity);
				Db.SaveChanges();
				return true;
			}
			catch (Exception e) { return Log(e); }
		}

		protected bool SaveEntities<T>(IEnumerable<T> entities) where T : Models.Base
		{
			try
			{
				foreach (var entity in entities)
					Db.Save(entity);
				Db.SaveChanges();
				return true;
			}
			catch (Exception e) { return Log(e); }
		}

		#endregion

		#region On Exception

		private bool Log(Exception e)
		{
			UserFriendlyError = Errors.Log(e);
			return false;
		}

		#endregion

		#region Validation

		public DbEntityValidationResult GetValidationResult<T>(T entity) where T : class
		{
			return Db.Entry(entity).GetValidationResult();
		}

		#endregion

		#region Implementation of IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Db.Dispose();
		}

		#endregion
	}
}