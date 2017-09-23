using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class Recipe : Base
	{
		#region Fields

		private Category _category;
		private Book _fromBook;

		#endregion

		[StringLength(200)]
		public string Name { get; set; }
		public string Method { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Please select a count of servings for this recipe.")]
		public int Servings { get; set; }
		[DataType(DataType.Time)]
		public TimeSpan PreperationTime { get; set; }
		[DataType(DataType.Time)]
		public TimeSpan CookingTime { get; set; }
		[Required]
		public int CategoryId { get; set; }

		#region Complex Properties

		public Book FromBook
		{
			get { return _fromBook ?? (_fromBook = new Book()); }
			set { _fromBook = value; }
		}

		#endregion

		#region Navigation Properties

		[ForeignKey("CategoryId")]
		[Required]
		public Category Category
		{
			get { return _category; }
			set
			{
				_category = value;
				if (value != null) CategoryId = value.Id;
			}
		}

		#endregion

		#region Lists

		public ObservableCollection<Ingredient> Ingredients { get; set; }

		#endregion

		#region NotMapped

		[NotMapped]
		public string FullName
		{
			get { return Category != null && !string.IsNullOrWhiteSpace(Category.Name) ? Name + " (" + Category.Name + ")" : Name; }
		}
		[NotMapped]
		public DateTime? CookingDateTime
		{
			get
			{
				return CookingTime != TimeSpan.MinValue
					       ? DateTime.MinValue.Add(CookingTime)
					       : DateTime.MinValue.Add(new TimeSpan(0, 0, 0));
			}
			set { CookingTime = value != null ? value.Value.TimeOfDay : new TimeSpan(0, 0, 0); }
		}
		[NotMapped]
		public DateTime? PreperationDateTime
		{
			get
			{
				return PreperationTime != TimeSpan.MinValue
					       ? DateTime.MinValue.Add(PreperationTime)
					       : DateTime.MinValue.Add(new TimeSpan(0, 0, 0));
			}
			set { PreperationTime = value != null ? value.Value.TimeOfDay : new TimeSpan(0, 0, 0); }
		}

		#endregion
	}
}
