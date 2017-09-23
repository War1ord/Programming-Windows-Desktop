﻿namespace WebView.UI
{
	partial class FormOpenLinks
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOpenLinks));
			this.fWebViewLinks = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// fWebViewLinks
			// 
			this.fWebViewLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fWebViewLinks.Location = new System.Drawing.Point(0, 0);
			this.fWebViewLinks.Name = "fWebViewLinks";
			this.fWebViewLinks.Size = new System.Drawing.Size(870, 517);
			this.fWebViewLinks.TabIndex = 0;
			// 
			// FormOpenLinks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 517);
			this.Controls.Add(this.fWebViewLinks);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormOpenLinks";
			this.Text = "Open a Link";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel fWebViewLinks;
	}
}