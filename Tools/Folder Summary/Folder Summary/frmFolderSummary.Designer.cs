namespace Folder_Summary
{
    partial class frmFolderSummary
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
			this.lblTotal = new System.Windows.Forms.Label();
			this.lblTotalValue = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// lblTotal
			// 
			this.lblTotal.AutoSize = true;
			this.lblTotal.Location = new System.Drawing.Point(8, 6);
			this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.Size = new System.Drawing.Size(119, 13);
			this.lblTotal.TabIndex = 0;
			this.lblTotal.Text = "Total Size && File Count :";
			// 
			// lblTotalValue
			// 
			this.lblTotalValue.AutoSize = true;
			this.lblTotalValue.Location = new System.Drawing.Point(125, 6);
			this.lblTotalValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblTotalValue.Name = "lblTotalValue";
			this.lblTotalValue.Size = new System.Drawing.Size(76, 13);
			this.lblTotalValue.TabIndex = 1;
			this.lblTotalValue.Text = "{lblTotalValue}";
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listView1.Location = new System.Drawing.Point(0, 36);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(834, 415);
			this.listView1.TabIndex = 4;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// frmFolderSummary
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 451);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.lblTotalValue);
			this.Controls.Add(this.lblTotal);
			this.Name = "frmFolderSummary";
			this.Text = "Folder Summary";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.Label lblTotalValue;
		private System.Windows.Forms.ListView listView1;
	}
}

