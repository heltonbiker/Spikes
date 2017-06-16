namespace StCamSWareCS.SettingCtrl
{
	partial class SettingComboBox
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
			// SettingComboBox
			// 
			this.SelectionChangeCommitted += new System.EventHandler(this.SettingComboBox_SelectionChangeCommitted);
			this.ResumeLayout(false);

		}

	}
}
