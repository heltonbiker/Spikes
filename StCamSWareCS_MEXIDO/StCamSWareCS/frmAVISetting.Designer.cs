namespace StCamSWareCS
{
	partial class frmAVISetting
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label51 = new System.Windows.Forms.Label();
			this.settingComboBox16 = new StCamSWareCS.SettingCtrl.SettingComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.settingNumericUpDown2 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.settingNumericUpDown1 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.settingTrackBar1 = new StCamSWareCS.SettingCtrl.SettingTrackBar();
			this.settingTrackBar2 = new StCamSWareCS.SettingCtrl.SettingTrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbFileSize = new System.Windows.Forms.TextBox();
			this.tbRecordingTime = new System.Windows.Forms.TextBox();
			this.settingCheckBox1 = new StCamSWareCS.SettingCtrl.SettingCheckBox();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingTrackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingTrackBar2)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(338, 182);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 12;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(419, 182);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 13;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Location = new System.Drawing.Point(12, 9);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(66, 12);
			this.label51.TabIndex = 0;
			this.label51.Text = "Compressor";
			// 
			// settingComboBox16
			// 
			this.settingComboBox16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.settingComboBox16.Location = new System.Drawing.Point(148, 9);
			this.settingComboBox16.Name = "settingComboBox16";
			this.settingComboBox16.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.AVI_COMPRESSOR;
			this.settingComboBox16.SettingValue = 0;
			this.settingComboBox16.Size = new System.Drawing.Size(240, 20);
			this.settingComboBox16.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "Frame Count";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 38);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "Quality";
			// 
			// settingNumericUpDown2
			// 
			this.settingNumericUpDown2.Location = new System.Drawing.Point(431, 63);
			this.settingNumericUpDown2.Name = "settingNumericUpDown2";
			this.settingNumericUpDown2.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.AVI_LENGTH;
			this.settingNumericUpDown2.SettingMax = 100;
			this.settingNumericUpDown2.SettingMin = 0;
			this.settingNumericUpDown2.SettingValue = 0;
			this.settingNumericUpDown2.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown2.TabIndex = 7;
			this.settingNumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// settingNumericUpDown1
			// 
			this.settingNumericUpDown1.Location = new System.Drawing.Point(431, 35);
			this.settingNumericUpDown1.Name = "settingNumericUpDown1";
			this.settingNumericUpDown1.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.AVI_QUALITY;
			this.settingNumericUpDown1.SettingMax = 100;
			this.settingNumericUpDown1.SettingMin = 0;
			this.settingNumericUpDown1.SettingValue = 0;
			this.settingNumericUpDown1.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown1.TabIndex = 4;
			this.settingNumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// settingTrackBar1
			// 
			this.settingTrackBar1.AutoSize = false;
			this.settingTrackBar1.Location = new System.Drawing.Point(148, 63);
			this.settingTrackBar1.Name = "settingTrackBar1";
			this.settingTrackBar1.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.AVI_LENGTH;
			this.settingTrackBar1.SettingMax = 10;
			this.settingTrackBar1.SettingMin = 0;
			this.settingTrackBar1.SettingValue = 0;
			this.settingTrackBar1.Size = new System.Drawing.Size(277, 19);
			this.settingTrackBar1.TabIndex = 6;
			// 
			// settingTrackBar2
			// 
			this.settingTrackBar2.AutoSize = false;
			this.settingTrackBar2.Location = new System.Drawing.Point(148, 35);
			this.settingTrackBar2.Name = "settingTrackBar2";
			this.settingTrackBar2.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.AVI_QUALITY;
			this.settingTrackBar2.SettingMax = 10;
			this.settingTrackBar2.SettingMin = 0;
			this.settingTrackBar2.SettingValue = 0;
			this.settingTrackBar2.Size = new System.Drawing.Size(277, 19);
			this.settingTrackBar2.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(224, 135);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(132, 12);
			this.label1.TabIndex = 8;
			this.label1.Text = "File Size(Uncompressed)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(226, 160);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 12);
			this.label2.TabIndex = 10;
			this.label2.Text = "Recording Time";
			// 
			// tbFileSize
			// 
			this.tbFileSize.Location = new System.Drawing.Point(362, 135);
			this.tbFileSize.Name = "tbFileSize";
			this.tbFileSize.ReadOnly = true;
			this.tbFileSize.Size = new System.Drawing.Size(132, 19);
			this.tbFileSize.TabIndex = 9;
			this.tbFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tbRecordingTime
			// 
			this.tbRecordingTime.Location = new System.Drawing.Point(362, 157);
			this.tbRecordingTime.Name = "tbRecordingTime";
			this.tbRecordingTime.ReadOnly = true;
			this.tbRecordingTime.Size = new System.Drawing.Size(132, 19);
			this.tbRecordingTime.TabIndex = 11;
			this.tbRecordingTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// settingCheckBox1
			// 
			this.settingCheckBox1.AutoSize = true;
			this.settingCheckBox1.CheckedValue = 1;
			this.settingCheckBox1.Location = new System.Drawing.Point(421, 11);
			this.settingCheckBox1.Name = "settingCheckBox1";
			this.settingCheckBox1.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.AVI_FILE_FORMAT;
			this.settingCheckBox1.SettingValue = 0;
			this.settingCheckBox1.Size = new System.Drawing.Size(73, 16);
			this.settingCheckBox1.TabIndex = 15;
			this.settingCheckBox1.Text = "OpenDML";
			this.settingCheckBox1.UseVisualStyleBackColor = true;
			// 
			// frmAVISetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(509, 217);
			this.Controls.Add(this.settingCheckBox1);
			this.Controls.Add(this.tbRecordingTime);
			this.Controls.Add(this.tbFileSize);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.settingNumericUpDown2);
			this.Controls.Add(this.settingNumericUpDown1);
			this.Controls.Add(this.settingTrackBar1);
			this.Controls.Add(this.settingTrackBar2);
			this.Controls.Add(this.label51);
			this.Controls.Add(this.settingComboBox16);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmAVISetting";
			this.Text = "Recording Setting";
			this.Load += new System.EventHandler(this.frmAVISetting_Load);
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingTrackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingTrackBar2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label51;
		private StCamSWareCS.SettingCtrl.SettingComboBox settingComboBox16;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown2;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown1;
		private StCamSWareCS.SettingCtrl.SettingTrackBar settingTrackBar1;
		private StCamSWareCS.SettingCtrl.SettingTrackBar settingTrackBar2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbFileSize;
		private System.Windows.Forms.TextBox tbRecordingTime;
		private StCamSWareCS.SettingCtrl.SettingCheckBox settingCheckBox1;
	}
}
