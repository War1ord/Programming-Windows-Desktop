using System.Collections.Generic;
using Business;
using GalaSoft.MvvmLight.Command;
using Models;
using Recipes.WindowCommands;
using Recipes.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Recipes.ViewModels
{
	public class MainViewModel : Base
	{
		public MainViewModel()
		{
			InitCommands();
			InitData();
			LoadData();
		}

		private ObservableCollection<Recipe> _collection;
		private ObservableCollection<Category> _categories;
		private Recipe _current;
		private Recipe _newRecipe;
		private Ingredient _newIngredient;
		private Category _newCategory;
		private string _filterRecipeString;

		#region Properties

		static public bool DbOnline { get; private set; }
		public TimeSpan TimeInterval { get; set; }

		public ObservableCollection<Recipe> Collection
		{
			get { return _collection; }
			set
			{
				_collection = value;
				RaisePropertyChanged(() => Collection);
			}
		}
		public ICollectionView CollectionView { get; set; }
		public ObservableCollection<Category> Categories
		{
			get { return _categories; }
			set
			{
				_categories = value;
				RaisePropertyChanged(() => Categories);
			}
		}
		public ObservableCollection<Unit> Units { get; private set; }

		public bool IsValidCurrent
		{
			get
			{
				using (var recipes = new Business.Recipes(Current))
				{
					return recipes.DbOnline && recipes.IsValid;
				}
			}
		}
		public bool IsValidNewRecipe
		{
			get
			{
				using (var recipes = new Business.Recipes(NewRecipe))
				{
					return recipes.DbOnline && recipes.IsValid;
				}
			}
		}
		public bool IsValidNewCategory
		{
			get
			{
				using (var recipes = new Business.Recipes())
				{
					return recipes.DbOnline && NewCategory != null && recipes.CategoryIsValid(NewCategory);
				}
			}
		}
		public bool IsValidNewIngredient
		{
			get
			{
				if (NewIngredient == null)
					return false;
				if (NewIngredient.Recipe == null || NewIngredient.Recipe.Id == 0)
					NewIngredient.Recipe = Current;
				using (var recipes = new Business.Recipes())
				{
					return recipes.DbOnline
						&& NewIngredient != null
						&& recipes.IngredientsCountIsValid(NewIngredient);
				}
			}
		}

		#region Current & New

		public Recipe Current
		{
			get { return _current; }
			set
			{
				_current = value;
				if (_newIngredient != null)
				{
					_newIngredient.Recipe = _current;
					_newIngredient.RecipeId = _current.Id;
				}
				RaisePropertyChanged(() => Current);
			}
		}
		public Recipe NewRecipe
		{
			get { return _newRecipe; }
			set { _newRecipe = value; RaisePropertyChanged(() => NewRecipe); }
		}
		public Category NewCategory
		{
			get { return _newCategory; }
			private set { _newCategory = value; RaisePropertyChanged(() => NewCategory); }
		}
		public Ingredient NewIngredient
		{
			get { return _newIngredient; }
			set { _newIngredient = value; RaisePropertyChanged(() => NewIngredient); }
		}

		#endregion

		#region Filters

		public string FilterRecipeString
		{
			get { return _filterRecipeString; }
			private set
			{
				_filterRecipeString = value;
				RaisePropertyChanged(() => FilterRecipeString);
				CollectionView.Filter += FilterRecipe;
				CollectionView.Refresh();
			}
		}
		public ObservableCollection<Ingredient> FiltersIngredient { get; private set; }
		public ObservableCollection<string> FilterRecipeNames { get; set; }

		#endregion

		#endregion

		#region Commands

		public ICommand CreateDatabaseCommand { get; private set; }

		public ICommand LoadDataCommand { get; private set; }

		public ICommand AddRecipeCommand { get; private set; }
		public ICommand AddCategoryCommand { get; private set; }
		public ICommand AddIngredientCommand { get; private set; }

		public ICommand UpdateRecipeCommand { get; private set; }
		public ICommand UpdateAllRecipeCommand { get; private set; }

		public ICommand DeleteRecipeCommand { get; private set; }
		public ICommand DeleteAllRecipeCommand { get; private set; }
		public ICommand ClearUnusedCategoriesCommand { get; private set; }

		public ICommand NewRecipeCommand { get; private set; }
		public ICommand NewRecipeWindowCommand { get; private set; }
		public ICommand ClearFilterCommand { get; private set; }

		public static ICommand CloseWindowCommand { get; private set; }

		public ICommand AddIngredientFilterCommand { get; private set; }
		public ICommand AddRecipeNameFilterCommand { get; private set; }

		#endregion

		#region Command Methods

		#region Create Db

        private static void DropDataBase()
        {
            try
            {

            }
            catch (Exception e) { DbOnline = false; /*Log(e)*/; }
        }
		private static void CreateDatabase()
		{
			try
			{
				DataContextDbCreate.Initialize();
				CreateStaticData();
				DbOnline = DataContextDbCreate.Initialized;
			}
			catch (Exception e) { DbOnline = false; /*Log(e)*/; }
		}
		private static void CreateStaticData()
		{
			try
			{
				StaticDataCreator.Create();
			}
			catch (Exception e) { DbOnline = Log(e); }
		}

		#endregion

		#region Load

		private void LoadData()
		{
			try
			{
				var loadingView = new LoadingView();
				loadingView.Show();
				using (var recipes = new Business.Recipes())
				{
					DbOnline = recipes.DbOnline;
					if (DbOnline)
					{

						Collection = new ObservableCollection<Recipe>(recipes.List);
						Categories = new ObservableCollection<Category>(recipes.Categories);
						Units = new ObservableCollection<Unit>(recipes.Units);

						CollectionView = CollectionViewSource.GetDefaultView(Collection);
					}
				}
				loadingView.Close();
			}
			catch (Exception e) { Log(e); }
		}

		#endregion

		#region Add

		private void AddRecipe()
		{
			try
			{
				bool saved = false;
				using (var recipes = new Business.Recipes(NewRecipe))
				{
					if (recipes.IsValid) saved = recipes.Save();
					if (saved)
					{
						Collection.Add(NewRecipe);
						ClearNewRecipe();
					}
				}
			}
			catch (Exception e) { Log(e); }
		}
		private void AddCategory()
		{
			try
			{
				using (var recipes = new Business.Recipes())
				{
					if (recipes.AddCategory(NewCategory))
					{
						if (!Categories.Contains(NewCategory))
							Categories.Add(NewCategory);
						NewCategory = new Category();
					}
				}
			}
			catch (Exception e) { Log(e); }
		}

		private void AddIngredient()
		{
			try
			{
				using (var recipes = new Business.Recipes())
				{
					if (recipes.AddIngredient(NewIngredient))
					{
						NewIngredient = new Ingredient();
					}
				}
			}
			catch (Exception e) { Log(e); }
		}

		#endregion

		#region Update

		private void Update()
		{
			try
			{
				using (var recipes = new Business.Recipes(Current))
				{
					if (recipes.IsValid) recipes.Save();
				}
			}
			catch (Exception e) { Log(e); }
		}
		private void UpdateAll()
		{
			try
			{
				using (var recipes = new Business.Recipes())
				{
					recipes.Save(Collection);
				}
			}
			catch (Exception e) { Log(e); }
		}

		#endregion

		#region Delete

		private void Delete()
		{
			try
			{
				using (var recipes = new Business.Recipes(Current))
				{
					if (recipes.IsValid)
					{
						if (recipes.Delete())
						{
							Collection.Remove(Current);
						}
						else
						{
							UserFriendlyError = recipes.UserFriendlyError;
						}
					}
					else
					{
						UserFriendlyError = recipes.ValidationErrors;
					}
				}
			}
			catch (Exception e) { Log(e); }
		}
		private void DeleteAll()
		{
			try
			{
				using (var recipes = new Business.Recipes())
				{
					if (recipes.DeleteAll())
					{
						Collection.Clear();
					}
					else
					{
						UserFriendlyError = !string.IsNullOrWhiteSpace(recipes.UserFriendlyError)
												? recipes.UserFriendlyError
												: "Could not successfully delete all the recipes.";
					}
				}
			}
			catch (Exception e) { Log(e); }
		}
		private void ClearUnusedCategories()
		{
			try
			{
				using (var recipes = new Business.Recipes())
				{
					if (recipes.ClearUnusedCategories())
					{

					}
				}
			}
			catch (Exception e) { Log(e); }
		}

		#endregion

		#region New

		private void ClearNewRecipe()
		{
			NewRecipe = new Recipe();
		}
		private void ClearRecipeFilterString()
		{
			FilterRecipeString = string.Empty;
		}

		#endregion

		#region Window

		private void NewRecipeWindow()
		{
			try
			{
				new AddWindow { DataContext = this }.ShowDialog();
			}
			catch (Exception e) { Log(e); }
		}

		#endregion

		#region Filters

		private bool FilterRecipe(object o)
		{
			Recipe recipe = o as Models.Recipe;
			return recipe != null && recipe.Name.Contains(_filterRecipeString);
		}
		private void AddIngredientFilter()
		{
			if(FiltersIngredient == null) FiltersIngredient = new ObservableCollection<Ingredient>();
			FiltersIngredient.Add(new Ingredient());
		}
		private void AddRecipeNameFilter()
		{
			if (FilterRecipeNames == null) FilterRecipeNames = new ObservableCollection<string>();
			FilterRecipeNames.Add(string.Empty);
		}

		#endregion

		#endregion

		private void InitCommands()
		{
			InitWindowCommands();
			InitNewCommands();
			InitAddCommands();
			InitUpdateCommands();
			InitDeleteCommands();
			LoadDataCommand = new RelayCommand(LoadData, () => DbOnline);
			CreateDatabaseCommand = new RelayCommand(CreateDatabase, () => !DbOnline);
			AddIngredientFilterCommand = new RelayCommand(AddIngredientFilter);
			AddRecipeNameFilterCommand = new RelayCommand(AddRecipeNameFilter);
		}

		private void InitData()
		{
			TimeInterval = new TimeSpan(0, 1, 0); // (0, 1, 0) is 1 minutes Time Intervals
			Current = new Recipe();
			NewRecipe = new Recipe();
			NewCategory = new Category();
			NewIngredient = new Ingredient();

			FiltersIngredient = new ObservableCollection<Ingredient>();
			FilterRecipeNames = new ObservableCollection<string>();
		}
		private void InitWindowCommands()
		{
			NewRecipeWindowCommand = new RelayCommand(NewRecipeWindow);
			CloseWindowCommand = new WindowCloseCommand();
		}
		private void InitNewCommands()
		{
			NewRecipeCommand = new RelayCommand(ClearNewRecipe);
			ClearFilterCommand = new RelayCommand(ClearRecipeFilterString);
		}
		private void InitAddCommands()
		{
			AddRecipeCommand = new RelayCommand(AddRecipe, () => DbOnline && IsValidNewRecipe);
			AddCategoryCommand = new RelayCommand(AddCategory, () => DbOnline && IsValidNewCategory);
			AddIngredientCommand = new RelayCommand(AddIngredient, () => IsValidNewIngredient);
		}
		private void InitUpdateCommands()
		{
			UpdateRecipeCommand = new RelayCommand(Update, () => DbOnline && IsValidCurrent);
			UpdateAllRecipeCommand = new RelayCommand(UpdateAll, () => DbOnline && IsValidCurrent);
		}
		private void InitDeleteCommands()
		{
			DeleteRecipeCommand = new RelayCommand(Delete, () => DbOnline && IsValidCurrent);
			DeleteAllRecipeCommand = new RelayCommand(DeleteAll, () => DbOnline);
			ClearUnusedCategoriesCommand = new RelayCommand(ClearUnusedCategories);
		}
	}
}