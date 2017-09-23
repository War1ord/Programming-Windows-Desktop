using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace RulesEngine.Business
{
	/// <summary>
	/// The Rules Engine Business class, coded with a multithread safe singleton pattern for global access.
	/// </summary>
	public sealed class RulesEngine
	{
		#region Singleton Pattern
		private static volatile RulesEngine _instance;
		private static object Lock = new object();

		/// <summary>
		/// Prevents a default instance of the <see cref="RulesEngine"/> class from being created.
		/// </summary>
		private RulesEngine()
		{
			/**********************************************************************
			 * DO NOT CALL ANY CODE IN THIS CONSTRUCTOR, 
			 * REFER TO "Instance" PROPERTY THAT CALLS THE "Initialize" METHOD
			 **********************************************************************/
		}
		public static RulesEngine Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (Lock)
					{
						if (_instance == null)
						{
							var instance = new RulesEngine();
							_instance = instance;
							_instance.Initialize();
						}
					}
				}
				return _instance;
			}
		}
		#endregion

		public void Initialize()
		{

		}

		/// <summary>
		/// Runs the specified rule by Namespace and Class.
		/// </summary>
		/// <typeparam name="T">the Calling datacontext</typeparam>
		/// <param name="Namespace">The namespace.</param>
		/// <param name="Class">The class.</param>
		/// <param name="db">The database.</param>
		public void Run<T>(string Namespace, string Class, T db) where T : DbContext
		{
			try
			{
				var rule = db.Set<Rule>().FirstOrDefault(i => i.Namespace == Namespace && i.Class == Class);
				if (rule != null)
				{
					foreach (var codeBlock in rule.CodeBlocks)
					{
						string[] listOfReferences = codeBlock.AssemblyReferences.Any() ? codeBlock.AssemblyReferences.Select(i => i.FullName).ToArray() : new string[] { };
						RunCode.ExecuteCode(codeBlock.Code, rule.Namespace, rule.Class, codeBlock.Name, codeBlock.IsStatic, references: listOfReferences, args: null);
					}
				}
			}
			catch (DbEntityValidationException deve)
			{
				//TODO code error messaging/results
			}
			catch (Exception e)
			{
				//TODO code error messaging/results
			}
		}

		/// <summary>
		/// Saves the rules.
		/// </summary>
		/// <typeparam name="T">the Calling datacontext</typeparam>
		/// <param name="db">The database.</param>
		/// <param name="rules">The rules.</param>
		public void SaveRules<T>(T db, params Rule[] rules) where T : DbContext
		{
			try
			{
				if (rules != null && rules.Any())
				{
					SaveListOfEntities(rules, db);
				}
			}
			catch (DbEntityValidationException deve)
			{
				//TODO code error messaging/results
			}
			catch (Exception e)
			{
				//TODO code error messaging/results
			}
		}
		/// <summary>
		/// Links the code blocks to rule.
		/// </summary>
		/// <typeparam name="TContext">the Calling datacontext</typeparam>
		/// <param name="db">The database.</param>
		/// <param name="ruleID">The rule identifier.</param>
		/// <param name="codeBlocksIDs">The code block.</param>
		public void LinkCodeBlocksToRule<TContext>(TContext db, Guid ruleID, params Guid[] codeBlocksIDs) where TContext : DbContext
		{
			try
			{
				if (codeBlocksIDs != null && codeBlocksIDs.Any())
				{
					for (int i = 0; i < codeBlocksIDs.Length; i++)
					{
						var codeBlock = db.Set<CodeBlock>().FirstOrDefault(c => c.ID == codeBlocksIDs[i]);
						if (codeBlock != null)
						{
							codeBlock.RuleID = ruleID;
							CommitChanges(codeBlock, db);
						}
					}
					db.SaveChanges();
				}
			}
			catch (DbEntityValidationException deve)
			{
				//TODO code error messaging/results
			}
			catch (Exception e)
			{
				//TODO code error messaging/results
			}
		}
		/// <summary>
		/// Saves the code blocks.
		/// </summary>
		/// <typeparam name="TContext">the Calling datacontext</typeparam>
		/// <param name="db">The database.</param>
		/// <param name="codeBlock">The code block.</param>
		public void SaveCodeBlocks<TContext>(TContext db, params CodeBlock[] codeBlock) where TContext : DbContext
		{
			try
			{
				if (codeBlock != null && codeBlock.Any())
				{
					SaveListOfEntities(codeBlock, db);
				}
			}
			catch (DbEntityValidationException deve)
			{
				//TODO code error messaging/results
			}
			catch (Exception e)
			{
				//TODO code error messaging/results
			}
		}
		/// <summary>
		/// Links the assembly references to code block.
		/// </summary>
		/// <typeparam name="TContext">the Calling datacontext</typeparam>
		/// <param name="db">The database.</param>
		/// <param name="codeBlockID">The code block identifier.</param>
		/// <param name="assemblyReferencesIDs">The assembly references.</param>
		public void LinkAssemblyReferencesToCodeBlock<TContext>(TContext db, Guid codeBlockID, params Guid[] assemblyReferencesIDs) where TContext : DbContext
		{
			try
			{
				if (assemblyReferencesIDs != null && assemblyReferencesIDs.Any())
				{
					var codeBlock = db.Set<CodeBlock>().FirstOrDefault(i => i.ID == codeBlockID);
					if (codeBlock != null)
					{
						var assemblyReferences = db.Set<AssemblyReference>().Where(i => assemblyReferencesIDs.Contains(i.ID));
						codeBlock.AssemblyReferences.AddRange(assemblyReferences);
						db.SaveChanges();
					}
				}
			}
			catch (DbEntityValidationException deve)
			{
				//TODO code error messaging/results
			}
			catch (Exception e)
			{
				//TODO code error messaging/results
			}
		}
		/// <summary>
		/// Saves the assembly references.
		/// </summary>
		/// <typeparam name="TContext">the Calling datacontext</typeparam>
		/// <param name="db">The database.</param>
		/// <param name="assemblyReferences">The assembly reference.</param>
		public void SaveAssemblyReferences<TContext>(TContext db, params AssemblyReference[] assemblyReferences) where TContext : DbContext
		{
			try
			{
				if (assemblyReferences != null && assemblyReferences.Any())
				{
					SaveListOfEntities(assemblyReferences, db);
				}
			}
			catch (DbEntityValidationException deve)
			{
				//TODO code error messaging/results
			}
			catch (Exception e)
			{
				//TODO code error messaging/results
			}
		}
		
		#region Helpers
		/// <summary>
		/// Saves the entity.
		/// </summary>
		/// <typeparam name="TEntity">The entity from ModelBase</typeparam>
		/// <typeparam name="TContext">The type of the context.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <param name="db">The database.</param>
		private static void CommitChanges<TEntity, TContext>(TEntity entity, TContext db) where TEntity : ModelBase where TContext : DbContext
		{
			Guid id = entity.ID;
			db.Entry<TEntity>(entity).State =
				db.Set<TEntity>().Any(item => item.ID == id)
					? EntityState.Modified
					: EntityState.Added;
		}
		/// <summary>
		/// Commits the list of changes.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <typeparam name="TContext">the Calling datacontext</typeparam>
		/// <param name="entities">The entities.</param>
		/// <param name="db">The database.</param>
		private static void CommitListOfChanges<TEntity, TContext>(IList<TEntity> entities, TContext db) where TEntity : ModelBase where TContext : DbContext
		{
			if (entities != null)
			{
				for (int i = 0; i < entities.Count; i++)
				{
					CommitChanges(entities[i], db);
				}
			}
		}
		/// <summary>
		/// Saves the entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <typeparam name="TContext">the Calling datacontext</typeparam>
		/// <param name="entity">The entity.</param>
		/// <param name="db">The database.</param>
		private static void SaveEntity<TEntity, TContext>(TEntity entity, TContext db) where TEntity : ModelBase where TContext : DbContext
		{
			CommitChanges(entity, db);
			db.SaveChanges();
		}
		/// <summary>
		/// Saves the list of entities.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <typeparam name="TContext">The type of the context.</typeparam>
		/// <param name="entities">The entities.</param>
		/// <param name="db">The database.</param>
		private static void SaveListOfEntities<TEntity, TContext>(IList<TEntity> entities, TContext db) where TEntity : ModelBase where TContext : DbContext
		{
			if (entities != null)
			{
				CommitListOfChanges(entities, db);
				db.SaveChanges();
			}
		}
		#endregion
	}
}