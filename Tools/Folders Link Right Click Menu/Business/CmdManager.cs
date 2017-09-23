using Common.Helpers;
using System.Diagnostics;
using System.Windows.Input;

namespace Business
{
	public class CmdManager
	{
		#region Properties

		public string CmdString { get; set; }

		public bool IsCmdStringValid
		{
			get { return !string.IsNullOrWhiteSpace(CmdString); }
		}

		public string CmdReturn { get; set; }

		public bool IsCmdReturnValid
		{
			get { return !string.IsNullOrWhiteSpace(CmdReturn); }
		}

		public string fileName { get { return "cmd.exe"; } }

		public string Arguments { get { return IsCmdStringValid ? string.Format("/C {0}", CmdString) : string.Empty; } }

		#endregion

		public ICommand Action
		{
			get { return new Action(Execute, () => IsCmdStringValid); }
		}

		private void Execute()
		{
			if (IsCmdStringValid)
			{
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo
				{
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden,
					FileName = fileName,
					Arguments = Arguments,
					UseShellExecute = false,
					RedirectStandardOutput = true,
				};
				process.Start();
				CmdReturn = process.StandardOutput.ReadToEnd();
				process.Close();
			}
		}
	}
}