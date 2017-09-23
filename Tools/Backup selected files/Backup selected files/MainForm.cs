using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SingleInstanceApp
{
	/// <summary>
	/// Main form for the SingleInstanceApp demo application.
	/// </summary>
	public class MainForm : Form
	{
		private IContainer components;
		private SplitContainer splitContainer1;
		private TextBox txtArgs;
		private TextBox Messages;
		public string[] Args;

		private const int backupOnSec = 2;
		private int checkChangeInterval = 1000; // 1000 = 1sec
		private Timer checkChange = new Timer();
		private int trackChange = 0;

		#region Creation and Disposal

		public MainForm()
		{
			checkChange.Interval = checkChangeInterval;
			checkChange.Tick += CheckChange_Tick; ;
			checkChange.Start();

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Add any constructor code after InitializeComponent call            
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			checkChange.Stop();
			checkChange.Dispose();
			base.Dispose(disposing);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtArgs = new System.Windows.Forms.TextBox();
			this.Messages = new System.Windows.Forms.TextBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtArgs);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.Messages);
			this.splitContainer1.Size = new System.Drawing.Size(1463, 838);
			this.splitContainer1.SplitterDistance = 788;
			this.splitContainer1.TabIndex = 4;
			// 
			// txtArgs
			// 
			this.txtArgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtArgs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtArgs.Location = new System.Drawing.Point(0, 0);
			this.txtArgs.Multiline = true;
			this.txtArgs.Name = "txtArgs";
			this.txtArgs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtArgs.Size = new System.Drawing.Size(788, 838);
			this.txtArgs.TabIndex = 3;
			// 
			// Messages
			// 
			this.Messages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Messages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Messages.Location = new System.Drawing.Point(0, 0);
			this.Messages.Multiline = true;
			this.Messages.Name = "Messages";
			this.Messages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.Messages.Size = new System.Drawing.Size(671, 838);
			this.Messages.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(1463, 838);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Backup selected files or folders";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void MainForm_Load(object sender, EventArgs e)
		{
			SetRegistry();
			// The single-instance code is going to save the command line 
			// arguments in this member variable before opening the first instance
			// of the app.
			if (Args != null)
			{
				ProcessParameters(null, Args);
				Args = null;
			}
		}

		public delegate void ProcessParametersDelegate(object sender, string[] args);
		public void ProcessParameters(object sender, string[] args)
		{
			// The form has loaded, and initialization will have been be done.

			// Add the command-line arguments to our textbox, just to confirm that
			// it reached here.
			if (args != null && args.Length != 0)
			{
				txtArgs.Text += DateTime.Now.ToString("mm:ss.ff") + " ";
				for (int i = 0; i < args.Length; i++)
				{
					txtArgs.Text += args[i] + " ";
					Global.ArgumentList.Add(args[i]);
				}
				txtArgs.Text += "\r\n";
			}
			trackChange = 0; // re-set change tracking
		}

		private void CheckChange_Tick(object sender, EventArgs e)
		{
			trackChange += checkChangeInterval;
			var sec = (trackChange / checkChangeInterval);
			if (sec >= backupOnSec)
			{
				Messages.AppendText("no change\r\n");
				Process(Global.ArgumentList.ToArray());
				Application.Exit();
			}
			else
			{
				Messages.AppendText(sec + " Sec, " + trackChange + " ticks\r\n");
			}
		}

		private void SetRegistry()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			//Windows Registry Editor Version 5.00

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
			//@="List Files...."

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
			//@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Backup selected files"))
			{
				if (key != null)
				{
					key.SetValue("", "Backup selected folders");
				}
			}
			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Backup selected files\command"))
			{
				if (key != null)
				{
					key.SetValue("", "\"" + location + "\" " + " \"%V\"");
				}
			}
			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"*\shell\Backup selected files"))
			{
				if (key != null)
				{
					key.SetValue("", "Backup selected files");
				}
			}
			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"*\shell\Backup selected files\command"))
			{
				if (key != null)
				{
					key.SetValue("", "\"" + location + "\" " + " \"%V\"");
				}
			}
		}

		private void Process(string[] args)
		{
			if (args != null && args.Any())
			{
				var isError = false;
				var now = DateTime.Now;
				string backupPath = Path.Combine("Backup", now.ToString("yyyy-MM-dd HHmm"));
				//NOTE: these files might not be in the same directory, maybe if the user used the file manager search function and selected folders, then clicked on backup selected files. 
				// get and loop over files
				foreach (var sourceFile in args.Where(i => !string.IsNullOrEmpty(i)))
				{
					try
					{
						// check if folder
						if ((File.GetAttributes(sourceFile) & FileAttributes.Directory) == FileAttributes.Directory)
						{
							// check if backup directory path exists, if not create
							var sourcePath = Path.GetDirectoryName(sourceFile);
							var destinationPath = Path.Combine(Path.Combine(sourcePath, backupPath), Path.GetFileName(sourceFile));
							// create backup folders 
							foreach (var dirPath in Directory.GetDirectories(sourceFile, "*", SearchOption.AllDirectories))
								Directory.CreateDirectory(dirPath.Replace(sourceFile, destinationPath));
							// copy source files to backup folders
							foreach (var source in Directory.GetFiles(sourceFile, "*.*", SearchOption.AllDirectories))
								File.Copy(source, source.Replace(sourceFile, destinationPath), true);
						}
						else
						{
							// check if backup directory path exists, if not create
							var fileName = Path.GetFileName(sourceFile);
							var sourcePath = sourceFile.Replace(fileName, "");
							var destinationPath = Path.Combine(sourcePath, backupPath);
							var destinationFile = Path.Combine(destinationPath, fileName);
							if (!Directory.Exists(destinationPath))
							{
								Directory.CreateDirectory(destinationPath);
							}
							// copy file
							File.Copy(sourceFile, destinationFile);
						}
					}
					catch (Exception e)
					{
						isError = true;
						Console.WriteLine(string.Format(@"{0} : Error with file : {1} : {2} : ", DateTime.Now, sourceFile, e.Message));
					}
				}
				//var path = args.FirstOrDefault();
				//if (!string.IsNullOrEmpty(path))
				//{
				//	if (System.IO.Directory.Exists(path))
				//	{
				//		try
				//		{
				//			System.IO.Directory.CreateDirectory(System.IO.Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd HHmmss")));
				//		}
				//		catch{}
				//	}
				//}
				if (isError) Console.Read();
			}
		}

	}
}
