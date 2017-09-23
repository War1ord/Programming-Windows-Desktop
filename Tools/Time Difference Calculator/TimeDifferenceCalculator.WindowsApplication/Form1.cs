using System;
using System.Windows.Forms;

namespace TimeDifferenceCalculator.WindowsApplication
{
	public partial class TimeDifferenceCalculator : Form
	{
		TimeSpan FromValue, ToValue;
		private bool isValidFromValue, isValidToValue;

		public TimeDifferenceCalculator()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Calulate();
		}

		private void textBoxFrom_TextChanged(object sender, EventArgs e)
		{
			isValidFromValue = TimeSpan.TryParse(textBoxFrom.Text, out FromValue);
			Calulate();
		}

		private void textBoxTo_TextChanged(object sender, EventArgs e)
		{
			isValidToValue = TimeSpan.TryParse(textBoxTo.Text, out ToValue);
			Calulate();
		}

		private void Calulate()
		{
			if (isValidFromValue && isValidToValue)
			{
				var diff = ToValue - FromValue;
				textBoxResult.Text = string.Format("Time difference: {0}  Time in numbers: {1}", diff, diff.TotalHours);
			}
			else
			{
				textBoxResult.Text = "Invalid";
			}
		}

		private void textBoxFrom_Enter(object sender, EventArgs e)
		{
			textBoxFrom.SelectAll();
		}

		private void textBoxTo_Enter(object sender, EventArgs e)
		{
			textBoxTo.SelectAll();
		}
	}
}
