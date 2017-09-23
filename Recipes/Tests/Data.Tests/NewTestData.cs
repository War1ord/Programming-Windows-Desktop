using Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Data;
using System.Linq;

namespace Data.Tests
{
    [TestClass]
    public class NewTestData
    {
        [TestMethod]
        public void NewRecipe()
        {
            using (var db = new DataContext())
            {
                if (db.Online) CreateNewRecipe(db);
            }
        }

        [TestMethod]
        public void New1000Recipes()
        {
            using (var db = new DataContext())
            {
                if (db.Online)
                {
					for (int i = 0; i < 1000; i++)
                    {
                        CreateNewRecipe(db);
                    }
                }
            }
        }

        [TestMethod]
        public void New500Recipes()
        {
            using (var db = new DataContext())
            {
                if (db.Online)
                {
					for (int i = 0; i < 500; i++)
                    {
                        CreateNewRecipe(db);
                    }
                }
            }
        }

        [TestMethod]
        public void New50Categories()
        {
            using (var db = new DataContext())
            {
                if (db.Online)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        CreateNewCategory(db);
                    }
                }
            }
        }

        #region Helpers

        private static void CreateNewRecipe(DataContext db)
        {
            var text = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            var miliLitersUnitId = db.Units.Where(i => i.Key == UnitNames.MiliLiters).Select(i => i.Id).FirstOrDefault();
            var cupsUnitId = db.Units.Where(i => i.Key == UnitNames.Cups).Select(i => i.Id).FirstOrDefault();
            int categoryId = 0;

            if (db.Categories.Any())
            {
                categoryId = db.Categories.Select(i => i.Id).FirstOrDefault();
            }
            else
            {
                const string name = "Soap";
                var category = new Category {Name = name};
                db.Entry(db.Categories.Attach(category)).State = EntityState.Added;
                db.SaveChanges();
                categoryId = category.Id;
            }
            
            var hotingredient = new Ingredient
            {
                Name = "Hot" + text,
                UnitId = miliLitersUnitId,
            };
            
            var hotingredientResults = db.Entry(hotingredient).GetValidationResult();
            if (hotingredientResults.IsValid)
            {
                db.Ingredients.Add(hotingredient);
                db.SaveChanges();
            }
            
            var cakesingredient = new Ingredient
            {
                Name = "Cakes" + text,
                UnitId = cupsUnitId,
            };
            
            var cakesingredientResults = db.Entry(cakesingredient).GetValidationResult();
            if (cakesingredientResults.IsValid)
            {
                db.Ingredients.Add(cakesingredient);
                db.SaveChanges();
            }
            
            var newRecipe = new Recipe
            {
                Name = "Hot Cakes " + text,
                Method = "Mix the hot with the Cake." + text,
                FromBook = new Book
                {
                    Page = 5,
                    Name = "Hot Books " + text,
                    ISDN = text,
                },
                Servings = 5,
                CategoryId = categoryId,
            };
            
            var newRecipeResults = db.Entry(newRecipe).GetValidationResult();
            if (newRecipeResults.IsValid)
            {
                db.Recipes.Add(newRecipe);
                db.SaveChanges();
            }
            
            var hotingredientscount = new Ingredient
            {
                RecipeId = newRecipe.Id,
                Count = 3,
            };
            
            var hotingredientscountResults = db.Entry(hotingredientscount).GetValidationResult();
            if (hotingredientscountResults.IsValid)
            {
                db.Ingredients.Add(hotingredientscount);
                db.SaveChanges();
            }
            
            var cakesingredientscount = new Ingredient
            {
                RecipeId = newRecipe.Id,
                Count = 5,
            };
            
            var cakesingredientscountResults = db.Entry(cakesingredientscount).GetValidationResult();
            if (cakesingredientscountResults.IsValid)
            {
				db.Ingredients.Add(cakesingredientscount);
                db.SaveChanges();
            }
        }

        private static void CreateNewCategory(DataContext db)
        {
            var category = new Category { Name = Guid.NewGuid().ToString() };
            db.Categories.Add(category);
            db.SaveChanges();
        }

        #endregion
    }
}
