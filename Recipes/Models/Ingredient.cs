using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class Ingredient : Base
	{
		#region Fields

		private Recipe _recipe;
		private int _count;
		private Unit _unit;

		#endregion

		#region Properties

		[StringLength(20)]
		public string Name { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Please select an ingredient.")]
		public int Count
		{
			get { return _count; }
			set { _count = value > 0 ? value : 1; }
		}

		[Range(1, int.MaxValue, ErrorMessage = "Please select a unit.")]
		public int UnitId { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Please select a recipe.")]
		public int RecipeId { get; set; }

		#endregion

		#region Navigation

		[ForeignKey("RecipeId")]
		public Recipe Recipe
		{
			get { return _recipe; }
			set { _recipe = value; if (value != null) RecipeId = value.Id; }
		}
		[ForeignKey("UnitId")]
		public Unit Unit
		{
			get { return _unit; }
			set { _unit = value; if (value != null) UnitId = value.Id; }
		}

		#endregion

		#region NotMapped

		[NotMapped]
		public string FullName
		{
			get
			{
				if (Unit != null)
				{
					return Count + " x " + Name + " " + Unit.Name;
				}
				else
				{
					return Count + " x " + Name;
				}
			}
		}

		#endregion

	}
}