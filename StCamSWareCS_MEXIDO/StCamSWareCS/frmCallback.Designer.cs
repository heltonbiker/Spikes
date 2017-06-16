namespace StCamSWareCS
{
	partial class frmCallback
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
			this.listBoxEnabled = new System.Windows.Forms.ListBox();
			this.listBoxDisabled = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnEnabled = new System.Windows.Forms.Button();
			this.btnDisabled = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBoxEnabled
			// 
			this.listBoxEnabled.FormattingEnabled = true;
			this.listBoxEnabled.ItemHeight = 12;
			this.listBoxEnabled.Location = new System.Drawing.Point(12, 30);
			this.listBoxEnabled.Name = "listBoxEnabled";
			this.listBoxEnabled.Size = new System.Drawing.Size(120, 160);
			this.listBoxEnabled.TabIndex = 0;
			// 
			// listBoxDisabled
			// 
			this.listBoxDisabled.FormattingEnabled = true;
			this.listBoxDisabled.ItemHeight = 12;
			this.listBoxDisabled.Location = new System.Drawing.Point(253, 30);
			this.listBoxDisabled.Name = "listBoxDisabled";
			this.listBoxDisabled.Size = new System.Drawing.Size(120, 160);
			this.listBoxDisabled.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "Enabled";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(294, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "Disabled";
			// 
			// btnEnabled
			// 
			this.btnEnabled.Location = new System.Drawing.Point(158, 77);
			this.btnEnabled.Name = "btnEnabled";
			this.btnEnabled.Size = new System.Drawing.Size(75, 23);
			this.btnEnabled.TabIndex = 4;
			this.btnEnabled.Text = "<-Enabled";
			this.btnEnabled.UseVisualStyleBackColor = true;
			this.btnEnabled.Click += new System.EventHandler(this.btnEnabled_Click);
			// 
			// btnDisabled
			// 
			this.btnDisabled.Location = new System.Drawing.Point(158, 106);
			this.btnDisabled.Name = "btnDisabled";
			this.btnDisabled.Size = new System.Drawing.Size(75, 23);
			this.btnDisabled.TabIndex = 5;
			this.btnDisabled.Text = "Disabled->";
			this.btnDisabled.UseVisualStyleBackColor = true;
			this.btnDisabled.Click += new System.EventHandler(this.btnDisabled_Click);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(298, 196);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// frmCallback
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 229);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnDisabled);
			this.Controls.Add(this.btnEnabled);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBoxDisabled);
			this.Controls.Add(this.listBoxEnabled);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmCallback";
			this.Text = "Callback Functions";
			this.Load += new System.EventHandler(this.frmCallback_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		private System.Windows.Forms.ListBox listBoxEnabled;
		private System.Windows.Forms.ListBox listBoxDisabled;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnEnabled;
		private System.Windows.Forms.Button btnDisabled;
		private System.Windows.Forms.Button btnOK;
	}
}