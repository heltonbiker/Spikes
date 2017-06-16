namespace StCamSWareCS.SettingCtrl
{
	partial class SettingNumericUpDown
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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// SettingNumericUpDown
			// 
			this.ValueChanged += new System.EventHandler(this.SettingNumericUpDown_ValueChanged);
			this.Leave += new System.EventHandler(this.SettingNumericUpDown_Leave);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

	}
}
