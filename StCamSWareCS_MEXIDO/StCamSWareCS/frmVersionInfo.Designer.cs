namespace StCamSWareCS
{
	partial class frmVersionInfo
	{

		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.btnOK = new System.Windows.Forms.Button();
			this.labelProductName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(205, 42);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// labelProductName
			// 
			this.labelProductName.AutoSize = true;
			this.labelProductName.Location = new System.Drawing.Point(22, 20);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(126, 12);
			this.labelProductName.TabIndex = 1;
			this.labelProductName.Text = "StCamSWareCS vx.x.x.x";
			// 
			// frmVersionInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 77);
			this.Controls.Add(this.labelProductName);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmVersionInfo";
			this.Text = "About StCamSWareCS";
			this.Load += new System.EventHandler(this.frmVersionInfo_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label labelProductName;
	}
}