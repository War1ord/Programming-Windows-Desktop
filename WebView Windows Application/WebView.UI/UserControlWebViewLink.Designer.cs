namespace WebView.UI
{
	partial class UserControlWebViewLink
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.linkUrl = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// webBrowser
			// 
			this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser.Location = new System.Drawing.Point(0, 13);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.Size = new System.Drawing.Size(227, 158);
			this.webBrowser.TabIndex = 0;
			// 
			// linkUrl
			// 
			this.linkUrl.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkUrl.Location = new System.Drawing.Point(0, 0);
			this.linkUrl.Name = "linkUrl";
			this.linkUrl.Size = new System.Drawing.Size(227, 13);
			this.linkUrl.TabIndex = 1;
			this.linkUrl.TabStop = true;
			this.linkUrl.Text = "linkLabel1";
			this.linkUrl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// UserControlWebViewLink
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.webBrowser);
			this.Controls.Add(this.linkUrl);
			this.Name = "UserControlWebViewLink";
			this.Size = new System.Drawing.Size(227, 171);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser;
		private System.Windows.Forms.LinkLabel linkUrl;
	}
}
