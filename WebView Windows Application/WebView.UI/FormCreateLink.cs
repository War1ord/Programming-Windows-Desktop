using System;
using System.Text;
using System.Windows.Forms;

namespace WebView.UI
{
    public partial class FormCreateLink : Form
    {
        public FormCreateLink()
        {
            InitializeComponent();
        }

        private void btnBrowseDestinationFolder_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog
            {
                Description = @"Select the folder you want to save this Web Link to",
                ShowNewFolderButton = true,
            };
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                txtSaveTo.Text = dialog.SelectedPath;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var istxtSaveToValid = !string.IsNullOrEmpty(txtSaveTo.Text);
            var istxtNameValid = !string.IsNullOrEmpty(txtName.Text);
            var istxtWebLinkValid = !string.IsNullOrEmpty(txtWebLink.Text);
            if (istxtSaveToValid && istxtNameValid && istxtWebLinkValid)
            {
	            var success = CreateWebLink();
	            if (success)
	            {
					MessageBox.Show(@"Shortcut created successfully", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
	            else
	            {
					MessageBox.Show(@"Unexpected error occurred!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
	            }
			}
            else
            {
                var sb = new StringBuilder();
                if (!istxtSaveToValid)
                {
                    sb.Append("Please select a folder to save the web link to.").AppendLine();
                }
                if (!istxtNameValid)
                {
                    sb.Append("Please enter a name of the web link.").AppendLine();
                }
                if (!istxtWebLinkValid)
                {
                    sb.Append("Please enter the web link you want to use.").AppendLine();
                }
                MessageBox.Show(sb.ToString(), @"Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private bool CreateWebLink()
        {
	        var shortcut = new IWshRuntimeLibrary.WshShellClass().CreateShortcut(
				System.IO.Path.Combine(txtSaveTo.Text, txtName.Text + ".lnk")) as IWshRuntimeLibrary.IWshShortcut;
			if (shortcut != null)
			{
				try
				{
					shortcut.Arguments = txtWebLink.Text;
					var executingAssemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
					shortcut.TargetPath = executingAssemblyLocation;
					// not sure about what this is for
					shortcut.WindowStyle = 3; // Normal Window 1, Minimized 2, Maximized 3
					shortcut.Description = txtName.Text; // Comments in the link
					shortcut.WorkingDirectory = new System.IO.FileInfo(executingAssemblyLocation).Directory.FullName;
					/*shortcut.IconLocation = "specify icon location";*/
					shortcut.Save();
					return true;
				}
				catch (Exception e)
				{
					MessageBox.Show(@"Unexpected error occurred!" + Environment.NewLine + @"Error Message: " + e.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
			return false;
		}
    }
}
