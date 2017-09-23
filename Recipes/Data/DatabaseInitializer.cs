using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class DatabaseInitializer<T> : IDatabaseInitializer<T> where T : DbContext
	{
		private readonly List<IDatabaseInitializer<T>> _initializers = new List<IDatabaseInitializer<T>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseInitializer{T}"/> class.
		/// </summary>
		/// <param name="databaseInitializers">The database initializers.</param>
		public DatabaseInitializer(params IDatabaseInitializer<T>[] databaseInitializers)
		{
			_initializers.AddRange(databaseInitializers);
		}

		/// <summary>
		/// Executes the strategy to initialize the database for the given context.
		/// </summary>
		/// <param name="context">The context.</param>
		public void InitializeDatabase(T context)
		{
			foreach (var databaseInitializer in _initializers)
			{
				databaseInitializer.InitializeDatabase(context);
			}
		}
	}
}
