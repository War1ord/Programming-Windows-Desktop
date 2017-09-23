namespace WebView.UI
{
    partial class FormCreateLink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateLink));
            this.lblSaveTo = new System.Windows.Forms.Label();
            this.txtSaveTo = new System.Windows.Forms.TextBox();
            this.btnBrowseDestinationFolder = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtWebLink = new System.Windows.Forms.TextBox();
            this.lblWebLink = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSaveTo
            // 
            this.lblSaveTo.AutoSize = true;
            this.lblSaveTo.Location = new System.Drawing.Point(28, 9);
            this.lblSaveTo.Name = "lblSaveTo";
            this.lblSaveTo.Size = new System.Drawing.Size(44, 13);
            this.lblSaveTo.TabIndex = 0;
            this.lblSaveTo.Text = "Save to";
            // 
            // txtSaveTo
            // 
            this.txtSaveTo.Location = new System.Drawing.Point(78, 6);
            this.txtSaveTo.Name = "txtSaveTo";
            this.txtSaveTo.Size = new System.Drawing.Size(294, 20);
            this.txtSaveTo.TabIndex = 1;
            this.txtSaveTo.WordWrap = false;
            // 
            // btnBrowseDestinationFolder
            // 
            this.btnBrowseDestinationFolder.Location = new System.Drawing.Point(378, 6);
            this.btnBrowseDestinationFolder.Name = "btnBrowseDestinationFolder";
            this.btnBrowseDestinationFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDestinationFolder.TabIndex = 2;
            this.btnBrowseDestinationFolder.Text = "Browse";
            this.btnBrowseDestinationFolder.UseVisualStyleBackColor = true;
            this.btnBrowseDestinationFolder.Click += new System.EventHandler(this.btnBrowseDestinationFolder_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(78, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(294, 20);
            this.txtName.TabIndex = 4;
            this.txtName.WordWrap = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(37, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            // 
            // txtWebLink
            // 
            this.txtWebLink.Location = new System.Drawing.Point(78, 53);
            this.txtWebLink.Name = "txtWebLink";
            this.txtWebLink.Size = new System.Drawing.Size(294, 20);
            this.txtWebLink.TabIndex = 6;
            this.txtWebLink.WordWrap = false;
            // 
            // lblWebLink
            // 
            this.lblWebLink.AutoSize = true;
            this.lblWebLink.Location = new System.Drawing.Point(19, 56);
            this.lblWebLink.Name = "lblWebLink";
            this.lblWebLink.Size = new System.Drawing.Size(53, 13);
            this.lblWebLink.TabIndex = 5;
            this.lblWebLink.Text = "Web Link";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(378, 51);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // FormCreateLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 84);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtWebLink);
            this.Controls.Add(this.lblWebLink);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnBrowseDestinationFolder);
            this.Controls.Add(this.txtSaveTo);
            this.Controls.Add(this.lblSaveTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormCreateLink";
            this.Text = "Create a Web View Link";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSaveTo;
        private System.Windows.Forms.TextBox txtSaveTo;
        private System.Windows.Forms.Button btnBrowseDestinationFolder;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtWebLink;
        private System.Windows.Forms.Label lblWebLink;
        private System.Windows.Forms.Button btnCreate;
    }
}

