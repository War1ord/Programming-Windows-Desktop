namespace TimeDifferenceCalculator.WindowsApplication
{
	partial class TimeDifferenceCalculator
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
			this.textBoxResult = new System.Windows.Forms.TextBox();
			this.panelTop = new System.Windows.Forms.Panel();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.panelTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxResult
			// 
			this.textBoxResult.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBoxResult.Location = new System.Drawing.Point(0, 21);
			this.textBoxResult.Name = "textBoxResult";
			this.textBoxResult.ReadOnly = true;
			this.textBoxResult.Size = new System.Drawing.Size(250, 20);
			this.textBoxResult.TabIndex = 2;
			this.textBoxResult.TabStop = false;
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.textBoxTo);
			this.panelTop.Controls.Add(this.textBoxFrom);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(250, 20);
			this.panelTop.TabIndex = 3;
			// 
			// textBoxTo
			// 
			this.textBoxTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxTo.Dock = System.Windows.Forms.DockStyle.Right;
			this.textBoxTo.Location = new System.Drawing.Point(130, 0);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(120, 20);
			this.textBoxTo.TabIndex = 1;
			this.textBoxTo.TextChanged += new System.EventHandler(this.textBoxTo_TextChanged);
			this.textBoxTo.Enter += new System.EventHandler(this.textBoxTo_Enter);
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxFrom.Dock = System.Windows.Forms.DockStyle.Left;
			this.textBoxFrom.Location = new System.Drawing.Point(0, 0);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(120, 20);
			this.textBoxFrom.TabIndex = 0;
			this.textBoxFrom.TextChanged += new System.EventHandler(this.textBoxFrom_TextChanged);
			this.textBoxFrom.Enter += new System.EventHandler(this.textBoxFrom_Enter);
			// 
			// TimeDifferenceCalculator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(250, 41);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.textBoxResult);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "TimeDifferenceCalculator";
			this.Text = "Time Difference Calculator";
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxResult;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.TextBox textBoxFrom;

	}
}

