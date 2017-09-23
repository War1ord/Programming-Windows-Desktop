namespace HTML2JSON
{
	partial class frmHTMLToJsonConverter
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.wbMain = new System.Windows.Forms.WebBrowser();
			this.panel1 = new System.Windows.Forms.Panel();
			this.rbFromWebAddress = new System.Windows.Forms.RadioButton();
			this.rbFromHtml = new System.Windows.Forms.RadioButton();
			this.btnConvert = new System.Windows.Forms.Button();
			this.txtJson = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.Location = new System.Drawing.Point(0, 0);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.panel2);
			this.scMain.Panel1.Controls.Add(this.wbMain);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.panel1);
			this.scMain.Panel2.Controls.Add(this.txtJson);
			this.scMain.Size = new System.Drawing.Size(1382, 800);
			this.scMain.SplitterDistance = 660;
			this.scMain.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnBrowse);
			this.panel2.Controls.Add(this.txtAddress);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(660, 23);
			this.panel2.TabIndex = 9;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowse.Location = new System.Drawing.Point(585, 0);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(0);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 10;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtAddress
			// 
			this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.txtAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
			this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAddress.Location = new System.Drawing.Point(1, 3);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(582, 16);
			this.txtAddress.TabIndex = 9;
			// 
			// wbMain
			// 
			this.wbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wbMain.Location = new System.Drawing.Point(3, 26);
			this.wbMain.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbMain.Name = "wbMain";
			this.wbMain.Size = new System.Drawing.Size(654, 771);
			this.wbMain.TabIndex = 6;
			this.wbMain.Url = new System.Uri("", System.UriKind.Relative);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.rbFromWebAddress);
			this.panel1.Controls.Add(this.rbFromHtml);
			this.panel1.Controls.Add(this.btnConvert);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(718, 26);
			this.panel1.TabIndex = 4;
			// 
			// rbFromWebAddress
			// 
			this.rbFromWebAddress.AutoSize = true;
			this.rbFromWebAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rbFromWebAddress.Location = new System.Drawing.Point(75, 3);
			this.rbFromWebAddress.Name = "rbFromWebAddress";
			this.rbFromWebAddress.Size = new System.Drawing.Size(114, 17);
			this.rbFromWebAddress.TabIndex = 7;
			this.rbFromWebAddress.Text = "From Web Address";
			this.rbFromWebAddress.UseVisualStyleBackColor = true;
			// 
			// rbFromHtml
			// 
			this.rbFromHtml.AutoSize = true;
			this.rbFromHtml.Checked = true;
			this.rbFromHtml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rbFromHtml.Location = new System.Drawing.Point(1, 3);
			this.rbFromHtml.Name = "rbFromHtml";
			this.rbFromHtml.Size = new System.Drawing.Size(71, 17);
			this.rbFromHtml.TabIndex = 6;
			this.rbFromHtml.TabStop = true;
			this.rbFromHtml.Text = "From Html";
			this.rbFromHtml.UseVisualStyleBackColor = true;
			// 
			// btnConvert
			// 
			this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConvert.Location = new System.Drawing.Point(192, 0);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(111, 23);
			this.btnConvert.TabIndex = 5;
			this.btnConvert.Text = "Convert To Json";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// txtJson
			// 
			this.txtJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtJson.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtJson.Location = new System.Drawing.Point(0, 25);
			this.txtJson.Multiline = true;
			this.txtJson.Name = "txtJson";
			this.txtJson.Size = new System.Drawing.Size(715, 772);
			this.txtJson.TabIndex = 3;
			// 
			// frmHTMLToJsonConverter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1382, 800);
			this.Controls.Add(this.scMain);
			this.Name = "frmHTMLToJsonConverter";
			this.Text = "Html to Json Converter";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			this.scMain.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
			this.scMain.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.WebBrowser wbMain;
		private System.Windows.Forms.TextBox txtJson;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rbFromWebAddress;
		private System.Windows.Forms.RadioButton rbFromHtml;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtAddress;
	}
}

