using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Management.Models.Service
{
	public class ServiceInterval : Base.ModelNamedBase
	{
		#region fields
		private int days;
		private int hours;
		private int minutes;
		private int seconds;
		#endregion

		[Required]
		public TimeSpan Interval { get; set; }

		#region not mapped
		[NotMapped]
		public string Display
		{
			get
			{
				return $"{Name} ({Interval.ToString()})";
			}
		}
		[NotMapped]
		public int Seconds
		{
			get
			{
				return seconds;
			}
			set
			{
				seconds = value;
				Interval = new TimeSpan(Days, Hours, Minutes, Seconds);
			}
		}
		[NotMapped]
		public int Minutes
		{
			get
			{
				return minutes;
			}
			set
			{
				minutes = value;
				Interval = new TimeSpan(Days, Hours, Minutes, Seconds);
			}
		}
		[NotMapped]
		public int Hours
		{
			get
			{
				return hours;
			}
			set
			{
				hours = value;
				Interval = new TimeSpan(Days, Hours, Minutes, Seconds);
			}
		}
		[NotMapped]
		public int Days
		{
			get
			{
				return days;
			}
			set
			{
				days = value;
				Interval = new TimeSpan(Days, Hours, Minutes, Seconds);
			}
		}
		#endregion
	}
}
