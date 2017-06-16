namespace StCamSWareCS.SettingCtrl
{
	partial class SettingRange
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
			this.SuspendLayout();
			// 
			// SettingRange
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "SettingRange";
			this.Size = new System.Drawing.Size(219, 28);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingRange_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SettingRange_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingRange_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SettingRange_MouseUp);
			this.ResumeLayout(false);

		}

	}
}
