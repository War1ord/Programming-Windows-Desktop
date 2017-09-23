using System.Diagnostics;
using System.Windows.Input;
using Common.Helpers;

namespace Business.Managers
{
    public class Cmd
    {
        private Cmd() { }
        public Cmd(string cmdString)
        {
            CmdString = cmdString;
            CmdReturn = string.Empty;
        }

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
        public string FileName
        {
            get { return CommandFileName; }
        }
        private string CommandFileName
        {
            get { return "cmd.exe"; }
        }
        public string Arguments
        {
            get { return IsCmdStringValid ? string.Format("/C {0}", CmdString) : string.Empty; }
        }

        public ICommand Action
        {
            get { return new Action(Execute, () => IsCmdStringValid); }
        }
        private void Execute()
        {
            if (IsCmdStringValid)
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = FileName,
                        Arguments = Arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                    }
                };
                process.Start();
                CmdReturn = process.StandardOutput.ReadToEnd();
                process.Close();
            }
        }
    }
}