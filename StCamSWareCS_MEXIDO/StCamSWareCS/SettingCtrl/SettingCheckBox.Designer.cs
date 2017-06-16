namespace StCamSWareCS.SettingCtrl
{
	partial class SettingCheckBox
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

			this.CheckedChanged += new System.EventHandler(this.SettingCheckBox_CheckedChanged);
			this.ResumeLayout(false);
		}

	}
}
