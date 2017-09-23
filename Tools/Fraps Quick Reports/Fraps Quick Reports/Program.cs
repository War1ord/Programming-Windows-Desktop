namespace Fraps_Quick_Reports
{
	using Models.Core;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Windows.Forms;
	using System.Windows.Forms.DataVisualization.Charting;

	class Program
	{
		public Program() { }
		public Program(string[] args) { this.args = args; }

		static void Main(string[] args)
		{
			new Program(args).Run();
		}

		string[] args;

		private static string[] search_patterns = new string[] { "*frametimes.csv", "*fps.csv", "*minmaxavg.csv" };
		private const string split_by = " ";

		public void Run()
		{
			// 1. Get folder path from user
			var path = (args != null && args.Length > 0) ? args.First(arg => !string.IsNullOrWhiteSpace(arg)) : GetFolderPathFromUser();
			if (string.IsNullOrWhiteSpace(path)) return;
			// 2. Retrieve a list of all files, recursive search. (maybe just for *.csv, or list of types)
			var files = SearchFiles(path);
			// 3 + 4 + 5. Group files by common text. --- Pull files into data structure(s) (POCO class instances). --- Build Reporting data structure(s)
			var data = BuildData(files);
			// 6. Generate and save report files.
			foreach (var item in data)
			{
				switch (item.Type)
				{
					case FileStructType.frametimes:
						BuildFrameTimesChart(item, path);
						break;
					case FileStructType.fps:
						BuildFPSChart(item, path);
						break;
					case FileStructType.minmaxavg:
						BuildMinMaxAvgChart(item, path);
						break;
					case FileStructType.unknown:
					default:
						break;
				}
			}
		}

		#region charts
		private static void BuildFrameTimesChart(FileStruct file, string path)
		{
			const int page_size = 10000;
			int iteration_count = file.FrameTimes.Length / page_size;
			if ((file.FrameTimes.Length % page_size) != 0) iteration_count++;
			for (int i = 0; i < iteration_count; i++)
			{
				int page = i + 1;
				string title = $@"{file.SubGrouping} {file.Type} page {page}";

				//prepare chart control...
				var chart = new Chart
				{
					Titles = { title },
					DataSource = file.FrameTimes.Skip(page_size * i).Take(page_size),
					Width = 1024 * 2,
					Height = 1024,
					Series =
					{
						new Series
						{
							Name = "Series1",
							XValueMember = nameof(FrameTimes.Frame),
							YValueMembers = nameof(FrameTimes.Delta),
							Color = Color.Black,
							BorderColor = Color.Gray,
							ChartType = SeriesChartType.Line,
							BorderDashStyle = ChartDashStyle.Dot,
							BorderWidth = 2,
							ShadowColor = Color.Gray,
							ShadowOffset = 1,
							Font = new Font("Tahoma", 8.0f),
							BackSecondaryColor = Color.Gray,
							LabelForeColor = Color.Gray,
						},
					},
					ChartAreas =
					{
						new ChartArea
						{
							Name = "ChartArea1",
							BackColor = Color.White,
							BorderColor = Color.Gray,
							BorderWidth = 0,
							BorderDashStyle = ChartDashStyle.Solid,
							AxisX = new Axis(){ },
							AxisY = new Axis(){ Maximum = 60 },
						},
					},
				};
				//databind...
				chart.DataBind();
				//save result...
				chart.SaveImage(Path.Combine(path, $@"{title}.png"), ChartImageFormat.Png);
			}
		}
		private static void BuildFPSChart(FileStruct file, string path)
		{
			const int page_size = 10000;
			int iteration_count = file.FPS.Length / page_size;
			if ((file.FPS.Length % page_size) != 0) iteration_count++;
			for (int i = 0; i < iteration_count; i++)
			{
				int page = i + 1;
				string title = $@"{file.SubGrouping} {file.Type} page {page}";

				//prepare chart control...
				var chart = new Chart
				{
					Titles = { title },
					DataSource = file.FPS.Select((item, index) => new { item.FramesPerSecond, interval = index + 1 }).Skip(page_size * i).Take(page_size),
					Width = 1024 * 2,
					Height = 1024,
					Series =
					{
						new Series
						{
							XValueMember = "interval",
							YValueMembers = nameof(FPS.FramesPerSecond),
							Color = Color.Black,
							BorderColor = Color.Gray,
							ChartType = SeriesChartType.Area,
							BorderDashStyle = ChartDashStyle.Dot,
							BorderWidth = 2,
							ShadowColor = Color.Gray,
							ShadowOffset = 1,
							Font = new Font("Tahoma", 8.0f),
							BackSecondaryColor = Color.Gray,
							LabelForeColor = Color.Gray,
						},
					},
					ChartAreas =
					{
						new ChartArea
						{
							BackColor = Color.White,
							BorderColor = Color.Gray,
							BorderWidth = 0,
							BorderDashStyle = ChartDashStyle.Solid,
							AxisX = new Axis(){ },
							AxisY = new Axis(){ Maximum = 100 },
						},
					},
				};
				//databind...
				chart.DataBind();
				//save result...
				chart.SaveImage(Path.Combine(path, $@"{title}.png"), ChartImageFormat.Png);
			}
		}
		private static void BuildMinMaxAvgChart(FileStruct file, string path)
		{
			string title = $@"{file.SubGrouping} {file.Type}";

			//prepare chart control...
			var chart = new Chart
			{
				Titles = { title },
				DataSource = new object[]
				{
					new { Name = nameof(file.MinMaxAvg.Avg), Value = file.MinMaxAvg.Avg, },
					new { Name = nameof(file.MinMaxAvg.Max), Value = file.MinMaxAvg.Max, },
					new { Name = nameof(file.MinMaxAvg.Min), Value = file.MinMaxAvg.Min, },
					//new { Name = nameof(file.MinMaxAvg.TotalFrames), Value = file.MinMaxAvg.TotalFrames, },
					//new { Name = nameof(file.MinMaxAvg.Time), Value = file.MinMaxAvg.Time, },
				},
				Width = 1024 * 2,
				Height = 1024,
				Series =
				{
					new Series
					{
						XValueMember = "Name",
						YValueMembers = "Value",
						IsValueShownAsLabel = true,
						Color = Color.Black,
						BorderColor = Color.Black,
						ChartType = SeriesChartType.Column,
						BorderDashStyle = ChartDashStyle.Solid,
						BorderWidth = 2,
						ShadowColor = Color.Black,
						ShadowOffset = 1,
						Font = new Font("Tahoma", 11.0f),
						BackSecondaryColor = Color.Gray,
						LabelForeColor = Color.Black,
					},
				},
				ChartAreas =
				{
					new ChartArea
					{
						BackColor = Color.White,
						BorderColor = Color.Gray,
						BorderWidth = 0,
						BorderDashStyle = ChartDashStyle.Solid,
						AxisX = new Axis(){ },
						AxisY = new Axis(){ Maximum = 300 },
					},
				},
			};
			//databind...
			chart.DataBind();
			//save result...
			chart.SaveImage(Path.Combine(path, $@"{title}.png"), ChartImageFormat.Png);
		}
		#endregion

		#region helpers - private
		private static FileStruct[] SearchFiles(string path)
		{
			return search_patterns.SelectMany(search_pattern => Directory.GetFiles(path, search_pattern, SearchOption.AllDirectories)
				.Select(file => new FileStruct
				{
					Type = FindFileStructType(search_pattern),
					FullName = file,
					ShortName = file.Replace(path, "")
							.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries)
							.Last()
				})
				.ToArray()).ToArray();
		}

		private static FileStruct[] BuildData(FileStruct[] files)
		{
			return files.Select(file =>
			{
				var words = file.ShortName.Split(new string[] { split_by }, StringSplitOptions.RemoveEmptyEntries);

				FindMainGrouping(file, files, words);
				FindSubGrouping(file, files, words);

				file.Content = File.ReadAllText(file.FullName);

				switch (file.Type)
				{
					case FileStructType.frametimes:
						file.FrameTimes = BuildFrameTimes(file.Content);
						BuildFrameTimesDelta(file);
						break;
					case FileStructType.fps:
						file.FPS = BuildFPS(file.Content);
						break;
					case FileStructType.minmaxavg:
						file.MinMaxAvg = BuildMinMaxAvg(file.Content);
						break;
					default:
						break;
				}
				return file;
			})
			//.GroupBy(i => i.SubGrouping)
			.ToArray();
		}

		private static FrameTimes[] BuildFrameTimes(string content)
		{
			var results = new List<FrameTimes>();
			foreach (var item in ReadContent(content))
			{
				var rows = item.Split(',').Select(i => i.Trim()).ToArray();
				results.Add(new FrameTimes
				{
					Frame = long.Parse(rows[0]),
					Time = float.Parse(rows[1], CultureInfo.InvariantCulture),
				});
			}
			return results.ToArray();
		}

		private static void BuildFrameTimesDelta(FileStruct file)
		{
			if (file != null && file.FrameTimes != null)
			{
				for (int i = 0; i < file.FrameTimes.Length; i++)
				{
					if (i > 0)
					{
						var previous_frame = file.FrameTimes[i - 1];
						var frame = file.FrameTimes[i];
						frame.Delta = frame.Time - previous_frame.Time;
					}
				}
			}
		}

		private static FPS[] BuildFPS(string content)
		{
			var results = new List<FPS>();
			foreach (var item in ReadContent(content))
			{
				var rows = item.Split(',').Select(i => i.Trim()).ToArray();
				results.Add(new FPS
				{
					FramesPerSecond = int.Parse(rows[0]),
				});
			}
			return results.ToArray();
		}

		private static MinMaxAvg BuildMinMaxAvg(string content)
		{
			var item = ReadContent(content).FirstOrDefault();
			if (!string.IsNullOrWhiteSpace(item))
			{
				var rows = item.Split(',').Select(i => i.Trim()).ToArray();

				return new MinMaxAvg
				{
					TotalFrames = long.Parse(rows[0], CultureInfo.InvariantCulture),
					Time = long.Parse(rows[1], CultureInfo.InvariantCulture),
					Min = int.Parse(rows[2], CultureInfo.InvariantCulture),
					Max = int.Parse(rows[3], CultureInfo.InvariantCulture),
					Avg = float.Parse(rows[4], CultureInfo.InvariantCulture),
				};
			}
			else
			{
				return new MinMaxAvg();
			}
		}

		private static string[] ReadContent(string content, bool skipFirst = true)
		{
			return content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Skip(skipFirst ? 1 : 0).ToArray();
		}

		private static FileStructType FindFileStructType(string search_pattern)
		{
			if (!string.IsNullOrWhiteSpace(search_pattern))
			{
				var value = search_pattern.Replace(@"*", "").Split('.').FirstOrDefault();

				return Enum.TryParse<FileStructType>(value, out var result) ? result : default(FileStructType);
			}
			else
			{
				return default(FileStructType);
			}
		}

		private static void FindMainGrouping(FileStruct file, FileStruct[] files, string[] words)
		{
			if (words != null && words.Length > 0)
			{
				foreach (var word in words)
				{
					if (ExistsIn(file, files, word))
					{
						file.MainGrouping = word;
						break;
					}
				}
			}
		}

		private static void FindSubGrouping(FileStruct file, FileStruct[] files, string[] words)
		{
			if (words != null && words.Length > 0)
			{
				if (words.Length == 1)
				{
					var only_word = words.First();
					if (!string.IsNullOrWhiteSpace(only_word))
					{
						if (ExistsIn(file, files, only_word))
						{
							file.SubGrouping = only_word;
							return;
						}
					}
				}
				else
				{
					var results = new List<string>();

					for (int w = 0; w < words.Length; w++)
					{
						for (int l = 0; l < words.Length - w; l++)
						{
							var full_word_string = string.Join(split_by, words.Skip(w).Take(l + 1).ToArray());

							if (ExistsIn(file, files, full_word_string))
							{
								results.Add(full_word_string);
							}
						}
					}

					file.SubGrouping = results.OrderByDescending(i => i.Length).FirstOrDefault();
				}
			}
		}

		private static bool ExistsIn(FileStruct file, FileStruct[] files, string full_word_string)
		{
			return files.Any(i => i.ShortName.Contains(full_word_string) && file.ShortName != i.ShortName);
		}

		private static string GetFolderPathFromUser()
		{
			using (var dialog = new FolderBrowserDialog())
			{
				return (dialog.ShowDialog() == DialogResult.OK) ? dialog.SelectedPath : null;
			}
		}

		private static DataTable GetDataTableSample()
		{
			//populate dataset with some demo data..
			DataTable dt = new DataTable();
			dt.Columns.Add("Name", typeof(string));
			dt.Columns.Add("Counter", typeof(int));
			DataRow r1 = dt.NewRow();
			r1[0] = "Demo";
			r1[1] = 8;
			dt.Rows.Add(r1);
			DataRow r2 = dt.NewRow();
			r2[0] = "Second";
			r2[1] = 15;
			dt.Rows.Add(r2);
			return dt;
		}

		#endregion

	}
}

namespace Models.Core
{
	using System;

	class FileStruct
	{
		public FileStructType Type { get; set; }
		public string FullName { get; set; }
		public string ShortName { get; set; }

		public string MainGrouping { get; set; }
		public string SubGrouping { get; set; }

		public string Content { get; set; }

		public FrameTimes[] FrameTimes { get; set; }
		public FPS[] FPS { get; set; }
		public MinMaxAvg MinMaxAvg { get; set; }
	}
	enum FileStructType
	{
		unknown = 0,
		frametimes,
		fps,
		minmaxavg,
	}
	class FrameTimes
	{
		public Int64 Frame { get; set; }
		public float Time { get; set; }
		public float Delta { get; set; }
	}
	class FPS
	{
		public int FramesPerSecond { get; set; }
	}
	class MinMaxAvg
	{
		public Int64 TotalFrames { get; set; }
		public long Time { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }
		public float Avg { get; set; }
	}
}
