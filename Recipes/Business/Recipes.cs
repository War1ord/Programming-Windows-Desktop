using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Common.Messages;
using EntityFramework.Extensions;
using Extentions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
	public class Recipes : Base
	{
		#region Fields

		private Recipe _recipe;

		#endregion

		#region Constructors

		public Recipes() { }
		public Recipes(Recipe recipe)
		{
			Set(recipe);
		}

		#endregion

		#region Properties

		public Recipe Recipe
		{
			get { return _recipe; }
			private set { _recipe = value; }
		}
		public List<Recipe> List
		{
			get
			{
				return Db.Recipes
					.Include(i => i.Ingredients)
					.ToList();
			}
		}
		public List<Category> Categories
		{
			get { return Db.Categories.ToList(); }
		}
		public List<Unit> Units
		{
			get { return Db.Units.ToList(); }
		}
		public bool IsValid
		{
			get { return Recipe != null && ValidationResult != null && ValidationResult.IsValid; }
		}
		public string ValidationErrors
		{
			get { return ValidationResult != null ? ValidationResult.ToMultilineString() : string.Empty; }
		}

		public DbEntityValidationResult ValidationResult { get; set; }

		#endregion

		#region Indexes

		public Recipe this[int index]
		{
			get { return Db.Recipes.FirstOrDefault(i => i.Id.Equals(index)); }
		}
		public Recipe this[string name]
		{
			get { return Db.Recipes.FirstOrDefault(i => i.Name.Equals(name)); }
		}

		#endregion

		#region Helpers

		public Recipes Set(Recipe recipe)
		{
			_recipe = recipe;
			LoadValidationResult();
			return this;
		}
		public void LoadValidationResult()
		{
			if (_recipe != null) ValidationResult = Db.Entry(_recipe).GetValidationResult();
		}
		public bool Delete(Recipe recipe)
		{
			Db.Entry(recipe).State = EntityState.Deleted;
			return Db.SaveChanges() > 0;
		}

		#endregion

		#region Methods

		public bool Add()
		{
			try
			{
				return IsValid && SaveEntity(Recipe);
			}
			catch (Exception e)
			{
				UserFriendlyError = string.Format(CommonMessages.UnexpectedErrorFormat, e.Message);
				return false;
			}
		}
		public bool Delete()
		{
			try
			{
				return IsValid && Delete(Recipe);
			}
			catch (Exception e)
			{
				UserFriendlyError = string.Format(CommonMessages.UnexpectedErrorFormat, e.Message);
				return false;
			}
		}
		public bool DeleteAll()
		{
			try
			{
				foreach (var ingredient in Db.Ingredients)
				{
					Db.Entry(Db.Ingredients.Attach(ingredient)).State = EntityState.Deleted;
				}
				Db.SaveChanges();
				foreach (var recipe in Db.Recipes)
				{
					Db.Entry(Db.Recipes.Attach(recipe)).State = EntityState.Deleted;
				}
				Db.SaveChanges();
				return true;
				//return Db.Recipes.Delete() > 0;
			}
			catch (Exception e)
			{
				string message = e.Message;
				if (e.InnerException != null)
				{
					message += Environment.NewLine + "Inner Exception : \r\n" + e.InnerException.Message;
					if (e.InnerException.InnerException != null)
					{
						message = message + Environment.NewLine + "Inner Exception : \r\n" + e.InnerException.InnerException.Message;
					}
				}
				UserFriendlyError = string.Format(CommonMessages.UnexpectedErrorFormat, message);
				return false;
			}
		}
		public bool Save()
		{
			try
			{
				return IsValid && SaveEntity(Recipe);
			}
			catch (Exception e)
			{
				string message = e.Message;
				if (e.InnerException != null)
					message = message + Environment.NewLine + "Inner Exception : \r\n" + e.InnerException.Message;
				UserFriendlyError = string.Format(CommonMessages.UnexpectedErrorFormat, message);
				return false;
			}
		}
		public bool Save(IEnumerable<Recipe> recipes)
		{
			try
			{
				var entities = recipes as List<Recipe> ?? recipes.ToList();
				var results = entities.Select(i => new KeyValuePair<string, DbEntityValidationResult>(i.Name, Db.Entry(i).GetValidationResult())).ToList();
				var recipesIsValid = results.All(i => i.Value.IsValid);
				if (recipesIsValid)
				{
					return SaveEntities(entities);
				}
				else
				{
					UserFriendlyError = results.ToMultilineString();
					return false;
				}
			}
			catch (Exception e)
			{
				UserFriendlyError = string.Format(CommonMessages.UnexpectedErrorFormat, e.Message);
				return false;
			}
		}
		public bool AddCategory(Category newCategory)
		{
			Db.Entry(Db.Categories.Attach(newCategory)).State = EntityState.Added;
			Db.SaveChanges();
			return true;
		}
		public bool AddIngredient(Ingredient newIngredient)
		{
			Db.Entry(Db.Ingredients.Attach(newIngredient)).State = EntityState.Added;
			Db.SaveChanges();
			return true;
		}
		public bool ClearUnusedCategories()
		{
			var existing = Db.Recipes.Select(i => i.CategoryId).Distinct().ToList();
			return Db.Categories.Delete(i => !existing.Contains(i.Id)) > 0;
		}

		#endregion

		#region Validation

		public bool CategoryIsValid(Category newCategory)
		{
			return Db.Entry(newCategory).GetValidationResult().IsValid;
		}
		public bool IngredientsCountIsValid(Ingredient newIngredientCount)
		{
			return Db.Entry(newIngredientCount).GetValidationResult().IsValid;
		}
		public bool IngredientsIsValid(Ingredient newIngredient)
		{
			return Db.Entry(newIngredient).GetValidationResult().IsValid;
		}

		#endregion
	}
}
