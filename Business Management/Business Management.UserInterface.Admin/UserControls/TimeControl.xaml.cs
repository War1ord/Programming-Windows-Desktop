using Business_Management.Business.Extentions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Business_Management.UserInterface.Admin.UserControls
{
	/// <summary>
	/// Interaction logic for TimeControl.xaml
	/// </summary>
	public partial class TimeControl : UserControl
	{
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimeControl), new UIPropertyMetadata(DateTime.Now.TimeOfDay, new PropertyChangedCallback(OnValueChanged)));
		public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days", typeof(int), typeof(TimeControl), new UIPropertyMetadata(0, new PropertyChangedCallback(OnTimeChanged)));
		public static readonly DependencyProperty HoursProperty = DependencyProperty.Register("Hours", typeof(int), typeof(TimeControl), new UIPropertyMetadata(0, new PropertyChangedCallback(OnTimeChanged)));
		public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register("Minutes", typeof(int), typeof(TimeControl), new UIPropertyMetadata(0, new PropertyChangedCallback(OnTimeChanged)));
		public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register("Seconds", typeof(int), typeof(TimeControl), new UIPropertyMetadata(0, new PropertyChangedCallback(OnTimeChanged)));

		public TimeControl()
		{
			InitializeComponent();
		}

		public TimeSpan Value
		{
			get { return (TimeSpan)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public int Days
		{
			get { return (int)GetValue(DaysProperty); }
			set { SetValue(DaysProperty, value); }
		}
		public int Hours
		{
			get { return (int)GetValue(HoursProperty); }
			set { SetValue(HoursProperty, value); }
		}
		public int Minutes
		{
			get { return (int)GetValue(MinutesProperty); }
			set { SetValue(MinutesProperty, value); }
		}
		public int Seconds
		{
			get { return (int)GetValue(SecondsProperty); }
			set { SetValue(SecondsProperty, value); }
		}

		private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			TimeControl control = obj as TimeControl;
			if (control.IsSet())
			{
				control.Days = ((TimeSpan)e.NewValue).Days;
				control.Hours = ((TimeSpan)e.NewValue).Hours;
				control.Minutes = ((TimeSpan)e.NewValue).Minutes;
				control.Seconds = ((TimeSpan)e.NewValue).Seconds;
			}
		}
		private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			TimeControl control = obj as TimeControl;
			if (control.IsSet())
			{
				control.Value = new TimeSpan(control.Days, control.Hours, control.Minutes, control.Seconds);
			}
		}
		private void Down(object sender, KeyEventArgs args)
		{
			//var numberKeys = new[] { Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9 };
			switch (((TextBox)sender).Name)
			{
				case "second":
					if (args.Key == Key.Up)
						Seconds++;
					if (args.Key == Key.Down)
						Seconds--;
					//if (numberKeys.Contains(args.Key))
					//	Seconds = ((int)args.Key) - 34;
					break;
				case "minute":
					if (args.Key == Key.Up)
						Minutes++;
					if (args.Key == Key.Down)
						Minutes--;
					//if (numberKeys.Contains(args.Key))
					//	Minutes = ((int)args.Key) - 34;
					break;
				case "hour":
					if (args.Key == Key.Up)
						Hours++;
					if (args.Key == Key.Down)
						Hours--;
					//if (numberKeys.Contains(args.Key))
					//	Hours = ((int)args.Key) - 34;
					break;
				case "day":
					if (args.Key == Key.Up)
						Days++;
					if (args.Key == Key.Down)
						Days--;
					//if (numberKeys.Contains(args.Key))
					//	Days = ((int)args.Key) - 34;
					break;

			}
		}

	}
}
