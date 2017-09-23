using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Password_Generator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Generate_Click(object sender, RoutedEventArgs e)
		{
			GeneratedPassword.Text = KeyGenerator.GeneratePassword((int)Length.Value, GetCharSet().ToArray(), GetCaseType());
		}

		private IEnumerable<CharSet> GetCharSet()
		{
			if ((SpecialChar.IsChecked.HasValue && SpecialChar.IsChecked.Value) ||
				(Alpha.IsChecked.HasValue && Alpha.IsChecked.Value) ||
				(Numeric.IsChecked.HasValue && Numeric.IsChecked.Value))
			{
				if (SpecialChar.IsChecked.HasValue && SpecialChar.IsChecked.Value)
				{
					yield return CharSet.SpecialChar;
				}
				if (Alpha.IsChecked.HasValue && Alpha.IsChecked.Value)
				{
					yield return CharSet.Alpha;
				}
				if (Numeric.IsChecked.HasValue && Numeric.IsChecked.Value)
				{
					yield return CharSet.Numeric;
				}
			}
			else
			{
				yield return default(CharSet);
			}
		}

		private CaseType GetCaseType()
		{
			if (Mixed_Case.IsChecked.HasValue && Mixed_Case.IsChecked.Value)
			{
				return CaseType.Mixed;
			}
			else if (Case_All_Lower.IsChecked.HasValue && Case_All_Lower.IsChecked.Value)
			{
				return CaseType.Lower;
			}
			else if (Case_All_Upper.IsChecked.HasValue && Case_All_Upper.IsChecked.Value)
			{
				return CaseType.Upper;
			}
			else
			{
				return default(CaseType);
			}
		}

		private void Length_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (LengthValue != null)
				LengthValue.Text = ((int)e.NewValue).ToString();
		}
	}
}
