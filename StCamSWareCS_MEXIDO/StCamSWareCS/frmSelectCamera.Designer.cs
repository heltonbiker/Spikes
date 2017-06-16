namespace StCamSWareCS
{
	partial class frmSelectCamera
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
			this.components = new System.ComponentModel.Container();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.cmbCameraList = new System.Windows.Forms.ComboBox();
			this.timerCheckCamera = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(188, 44);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(108, 44);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// cmbCameraList
			// 
			this.cmbCameraList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCameraList.Location = new System.Drawing.Point(12, 12);
			this.cmbCameraList.Name = "cmbCameraList";
			this.cmbCameraList.Size = new System.Drawing.Size(256, 20);
			this.cmbCameraList.TabIndex = 3;
			// 
			// timerCheckCamera
			// 
			this.timerCheckCamera.Tick += new System.EventHandler(this.timerCheckCamera_Tick);
			// 
			// frmSelectCamera
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 78);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cmbCameraList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmSelectCamera";
			this.Text = "Camera Selection";
			this.Activated += new System.EventHandler(this.frmSelectCamera_Activated);
			this.ResumeLayout(false);

		}


		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cmbCameraList;
		private System.Windows.Forms.Timer timerCheckCamera;
	}
}