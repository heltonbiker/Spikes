using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SensorTechnology;
using StCamSWareCS.SettingCtrl;

namespace StCamSWareCS
{
	public partial class frmSetBase : Form
	{
		public frmSetBase()
		{
			InitializeComponent();
		}
		public frmSetBase(CStCamera stCamera)
		{
			InitializeComponent();
			m_StCamera = stCamera;
		}
		protected CStCamera m_StCamera = null;

		protected virtual void UpdateDisplay(){}
		protected void SetSettingValueChangeEvent(Control.ControlCollection ctrls)
		{
			System.EventHandler eh = new System.EventHandler(this.OnUpdateSettingValue);
			foreach (Control ctrl in ctrls)
			{
				ISettingValue isv = ctrl as ISettingValue;
				if (isv != null)
				{
					isv.SettingValueChanged += eh;
				}
				else
				{
					if (0 < ctrl.Controls.Count)
					{
						SetSettingValueChangeEvent(ctrl.Controls);
					}
				}
			}
		}
		protected void InitComboBox(Control.ControlCollection ctrls)
		{
			foreach (Control ctrl in ctrls)
			{
				SettingComboBox scb = ctrl as SettingComboBox;
				if (scb != null)
				{

					CItemForComboBox[] ifcb = null;

					switch (scb.SettingID)
					{
						case (SettingCtrl.SettingIDs.DISPLAY_MODE):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("GDI", StCam.STCAM_DISPLAY_MODE_GDI), 
																					new CItemForComboBox("DirectDraw[Offscreeen]", StCam.STCAM_DISPLAY_MODE_DD_OFFSCREEN), 
																					new CItemForComboBox("DirectDraw[Overlay]", StCam.STCAM_DISPLAY_MODE_DD_OVERLAY), 
																					new CItemForComboBox("DirectDraw[Offscreen HQ]", StCam.STCAM_DISPLAY_MODE_DD_OFFSCREEN_HQ), 
																					new CItemForComboBox("DirectDraw[Overlay HQ]", StCam.STCAM_DISPLAY_MODE_DD_OVERLAY_HQ), 
																					new CItemForComboBox("DirectX", StCam.STCAM_DISPLAY_MODE_DIRECTX), 
																					new CItemForComboBox("DirectX[V Sync ON]", StCam.STCAM_DISPLAY_MODE_DIRECTX_VSYNC_ON), 
																					new CItemForComboBox("DirectX[V Sync ON 2]", StCam.STCAM_DISPLAY_MODE_DIRECTX_VSYNC_ON2)};
							break;
						case (SettingCtrl.SettingIDs.ALC_MODE):
							if (m_StCamera.HasCameraSideALC)
							{
								ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("Fixed Shutter / AGC OFF", StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF), 
																					new CItemForComboBox("Auto Shutter ON / AGC ON", StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON), 
																					new CItemForComboBox("Auto Shutter ON / AGC OFF", StCam.STCAM_ALCMODE_CAMERA_AE_ON), 
																					new CItemForComboBox("Fixed Shutter / AGC ON", StCam.STCAM_ALCMODE_CAMERA_AGC_ON)};
							}
							else
							{
								ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("Fixed Shutter / AGC OFF", StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF), 
																					new CItemForComboBox("Auto Shutter ON / AGC ON", StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON), 
																					new CItemForComboBox("Auto Shutter ON / AGC OFF", StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF), 
																					new CItemForComboBox("Fixed Shutter / AGC ON", StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON), 
																					new CItemForComboBox("Auto Shutter / AGC OneShot", StCam.STCAM_ALCMODE_AUTO_SHUTTER_AGC_ONESHOT), 
																					new CItemForComboBox("Auto Shutter OneShot / AGC OFF", StCam.STCAM_ALCMODE_AUTO_SHUTTER_ONESHOT_AGC_OFF), 
																					new CItemForComboBox("Fixed Shutter / AGC OneShot", StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ONESHOT)};
							}
							break;
						case (SettingCtrl.SettingIDs.WB_MODE):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("OFF", StCam.STCAM_WB_OFF), 
																					new CItemForComboBox("Manual", StCam.STCAM_WB_MANUAL), 
																					new CItemForComboBox("FullAuto", StCam.STCAM_WB_FULLAUTO), 
																					new CItemForComboBox("OneShot", StCam.STCAM_WB_ONESHOT)};

							break;
						case (SettingCtrl.SettingIDs.GAMMA_Y_MODE):
						case (SettingCtrl.SettingIDs.GAMMA_R_MODE):
						case (SettingCtrl.SettingIDs.GAMMA_GR_MODE):
						case (SettingCtrl.SettingIDs.GAMMA_GB_MODE):
						case (SettingCtrl.SettingIDs.GAMMA_B_MODE):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("OFF", StCam.STCAM_GAMMA_OFF), 
																					new CItemForComboBox("ON", StCam.STCAM_GAMMA_ON), 
																					new CItemForComboBox("Reverse", StCam.STCAM_GAMMA_REVERSE)};

							break;
						case (SettingIDs.SHARPNESS_MODE):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("OFF", StCam.STCAM_SHARPNESS_OFF), 
																					new CItemForComboBox("ON", StCam.STCAM_SHARPNESS_ON)};

							break;
						case (SettingIDs.HUE_SATURATION_MODE):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("OFF", StCam.STCAM_HUE_SATURATION_OFF), 
																					new CItemForComboBox("ON", StCam.STCAM_HUE_SATURATION_ON)};

							break;
						case (SettingIDs.SCAN_MODE):
							ifcb = new CItemForComboBox[m_StCamera.EnableScanModeCount];
							{
								ushort wEnableScanMode = m_StCamera.EnableScanMode;
								int index = 0;
								ifcb[index++] = new CItemForComboBox("Normal", StCam.STCAM_SCAN_MODE_NORMAL);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_PARTIAL_1) != 0) ifcb[index++] = new CItemForComboBox("1/1 Partial", StCam.STCAM_SCAN_MODE_PARTIAL_1);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_PARTIAL_2) != 0) ifcb[index++] = new CItemForComboBox("1/2 Partial", StCam.STCAM_SCAN_MODE_PARTIAL_2);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_PARTIAL_4) != 0) ifcb[index++] = new CItemForComboBox("1/4 Partial", StCam.STCAM_SCAN_MODE_PARTIAL_4);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_VARIABLE_PARTIAL) != 0) ifcb[index++] = new CItemForComboBox("Variable Partial", StCam.STCAM_SCAN_MODE_VARIABLE_PARTIAL);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_BINNING) != 0) ifcb[index++] = new CItemForComboBox("Binning", StCam.STCAM_SCAN_MODE_BINNING);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_1) != 0) ifcb[index++] = new CItemForComboBox("Binning 1/1 Partial", StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_1);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_2) != 0) ifcb[index++] = new CItemForComboBox("Binning 1/2 Partial", StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_2);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_4) != 0) ifcb[index++] = new CItemForComboBox("Binning 1/4 Partial", StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_4);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_BINNING_VARIABLE_PARTIAL) != 0) ifcb[index++] = new CItemForComboBox("Binning Variable Partial", StCam.STCAM_SCAN_MODE_BINNING_VARIABLE_PARTIAL);
								if ((wEnableScanMode & StCam.STCAM_SCAN_MODE_AOI) != 0) ifcb[index++] = new CItemForComboBox("AOI", StCam.STCAM_SCAN_MODE_AOI);
							}
							break;
						case (SettingIDs.CLOCK_MODE):
							ifcb = new CItemForComboBox[m_StCamera.EnableClockModeCount];
							{
								uint dwEnableClockMode = m_StCamera.EnableClockMode;
								int index = 0;
								ifcb[index++] = new CItemForComboBox("Normal", (int)StCam.STCAM_CLOCK_MODE_NORMAL);
								if ((dwEnableClockMode & StCam.STCAM_CLOCK_MODE_DIV_2) != 0) ifcb[index++] = new CItemForComboBox("1/2", (int)StCam.STCAM_CLOCK_MODE_DIV_2);
								if ((dwEnableClockMode & StCam.STCAM_CLOCK_MODE_DIV_4) != 0) ifcb[index++] = new CItemForComboBox("1/4", (int)StCam.STCAM_CLOCK_MODE_DIV_4);
								if ((dwEnableClockMode & StCam.STCAM_CLOCK_MODE_VGA_90FPS) != 0) ifcb[index++] = new CItemForComboBox("90FPS", (int)StCam.STCAM_CLOCK_MODE_VGA_90FPS);
							}
							break;
						case(SettingIDs.H_BIN_SKIP):
							switch (m_StCamera.ProductID)
							{
								case (StCam.STCAM_USBPID_STC_MBA5MUSB3):
								case (StCam.STCAM_USBPID_STC_MCA5MUSB3):
									ifcb = new CItemForComboBox[]
										{
										new CItemForComboBox("1/1", 0x0000),
										new CItemForComboBox("1/2", 0x0001),
										new CItemForComboBox("2/2", 0x0101),
										new CItemForComboBox("1/3", 0x0002),
										new CItemForComboBox("1/4", 0x0003),
										new CItemForComboBox("2/4", 0x0103),
										new CItemForComboBox("4/4", 0x0203),
										new CItemForComboBox("1/5", 0x0004),
										new CItemForComboBox("1/6", 0x0005),
										new CItemForComboBox("2/6", 0x0105),
										new CItemForComboBox("1/7", 0x0006),
										};
									break;
								case (StCam.STCAM_USBPID_STC_MBE132U3V):
								case (StCam.STCAM_USBPID_STC_MCE132U3V):
									ifcb = new CItemForComboBox[]
										{
										new CItemForComboBox("1/1", 0x0101),
										new CItemForComboBox("1/2", 0x0102),
										new CItemForComboBox("1/4", 0x0104),
										new CItemForComboBox("2/1", 0x0201),
										new CItemForComboBox("2/2", 0x0202),
										new CItemForComboBox("2/4", 0x0204),
										};
									break;

							}
							break;
						case (SettingIDs.V_BIN_SKIP):
							switch (m_StCamera.ProductID)
							{
								case (StCam.STCAM_USBPID_STC_MBA5MUSB3):
								case (StCam.STCAM_USBPID_STC_MCA5MUSB3):
									ifcb = new CItemForComboBox[]
										{
										new CItemForComboBox("1/1", 0x0000),
										new CItemForComboBox("1/2", 0x0001),
										new CItemForComboBox("2/2", 0x0101),
										new CItemForComboBox("1/3", 0x0002),
										new CItemForComboBox("1/4", 0x0003),
										new CItemForComboBox("2/4", 0x0103),
										new CItemForComboBox("4/4", 0x0203),
										new CItemForComboBox("1/5", 0x0004),
										new CItemForComboBox("1/6", 0x0005),
										new CItemForComboBox("2/6", 0x0105),
										new CItemForComboBox("1/7", 0x0006),
										new CItemForComboBox("1/8", 0x0007),
										new CItemForComboBox("2/8", 0x0107),
										new CItemForComboBox("4/8", 0x0207),
										};
									break;
								case (StCam.STCAM_USBPID_STC_MBE132U3V):
								case (StCam.STCAM_USBPID_STC_MCE132U3V):
									ifcb = new CItemForComboBox[]
										{
										new CItemForComboBox("1/1", 0x0101),
										new CItemForComboBox("1/2", 0x0102),
										new CItemForComboBox("1/4", 0x0104),
										new CItemForComboBox("2/1", 0x0201),
										new CItemForComboBox("2/2", 0x0202),
										new CItemForComboBox("2/4", 0x0204),
										};
									break;

							}
							break;
						case (SettingIDs.H_BIN_SUM):
							ifcb = new CItemForComboBox[]
								{
								new CItemForComboBox("OFF", StCam.STCAM_BINNING_SUM_MODE_OFF),
								new CItemForComboBox("ON", StCam.STCAM_BINNING_SUM_MODE_H),
								};
							break;
						case (SettingIDs.V_BIN_SUM):
							ifcb = new CItemForComboBox[]
								{
								new CItemForComboBox("OFF", StCam.STCAM_BINNING_SUM_MODE_OFF),
								new CItemForComboBox("ON", StCam.STCAM_BINNING_SUM_MODE_V),
								};
							break;

						#region PixelFormat
						case (SettingIDs.PIXEL_FORMAT):
							ifcb = new CItemForComboBox[m_StCamera.EnablePixelFormatCount];
							{

								uint dwEnablePixelFormat = m_StCamera.EnablePixelFormat;
								int index = 0;
								if ((dwEnablePixelFormat & StCam.STCAM_PIXEL_FORMAT_08_MONO_OR_RAW) != 0) ifcb[index++] = new CItemForComboBox("GRAY8", (int)StCam.STCAM_PIXEL_FORMAT_08_MONO_OR_RAW);
								if ((dwEnablePixelFormat & StCam.STCAM_PIXEL_FORMAT_24_BGR) != 0) ifcb[index++] = new CItemForComboBox("BGR24", (int)StCam.STCAM_PIXEL_FORMAT_24_BGR);
								if ((dwEnablePixelFormat & StCam.STCAM_PIXEL_FORMAT_32_BGR) != 0) ifcb[index++] = new CItemForComboBox("BGR32", (int)StCam.STCAM_PIXEL_FORMAT_32_BGR);
							}
							break;
						#endregion PixelFormat
						#region ColorInterpolation
						case (SettingIDs.COLOR_INTERPOLATION):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("OFF(Mono)", (int)StCam.STCAM_COLOR_INTERPOLATION_NONE_MONO), 
																					new CItemForComboBox("OFF(Color)", (int)StCam.STCAM_COLOR_INTERPOLATION_NONE_COLOR), 
																					new CItemForComboBox("Nearest Neighbor Replication", (int)StCam.STCAM_COLOR_INTERPOLATION_NEAREST_NEIGHBOR), 
																					new CItemForComboBox("Bilinear", (int)StCam.STCAM_COLOR_INTERPOLATION_BILINEAR), 
																					new CItemForComboBox("Bilinear with false color reduction", (int)StCam.STCAM_COLOR_INTERPOLATION_BILINEAR_FALSE_COLOR_REDUCTION), 
																					new CItemForComboBox("BiCubic", (int)StCam.STCAM_COLOR_INTERPOLATION_BICUBIC)};
							break;
						#endregion ColorInterpolation
						#region MirrorRotation
						case (SettingIDs.MIRROR_MODE):
							ifcb = new CItemForComboBox[m_StCamera.EnableMirrorModeCount];
							{
								byte byteEnableMirrorMode = m_StCamera.EnableMirrorMode;
								int index = 0;
								ifcb[index++] = new CItemForComboBox("OFF(Normal)", (int)StCam.STCAM_MIRROR_OFF);
								if ((byteEnableMirrorMode & StCam.STCAM_MIRROR_HORIZONTAL) != 0) ifcb[index++] = new CItemForComboBox("Horizontal", StCam.STCAM_MIRROR_HORIZONTAL);
								if ((byteEnableMirrorMode & StCam.STCAM_MIRROR_VERTICAL) != 0)
								{
									ifcb[index++] = new CItemForComboBox("Vertical", StCam.STCAM_MIRROR_VERTICAL);
									if ((byteEnableMirrorMode & StCam.STCAM_MIRROR_HORIZONTAL) != 0)
									{
										ifcb[index++] = new CItemForComboBox("Horizontal/Vertical", StCam.STCAM_MIRROR_HORIZONTAL_VERTICAL);
									}
								}
								if ((byteEnableMirrorMode & StCam.STCAM_MIRROR_HORIZONTAL_CAMERA) != 0) ifcb[index++] = new CItemForComboBox("Horizontal[Camera]", StCam.STCAM_MIRROR_HORIZONTAL_CAMERA);
								if ((byteEnableMirrorMode & StCam.STCAM_MIRROR_VERTICAL_CAMERA) != 0)
								{
									ifcb[index++] = new CItemForComboBox("Vertical[Camera]", StCam.STCAM_MIRROR_VERTICAL_CAMERA);
									if ((byteEnableMirrorMode & StCam.STCAM_MIRROR_HORIZONTAL_CAMERA) != 0)
									{
										ifcb[index++] = new CItemForComboBox("Horizontal/Vertical[Camera]", StCam.STCAM_MIRROR_HORIZONTAL_CAMERA | StCam.STCAM_MIRROR_VERTICAL_CAMERA);
									}
								}
							}
							break;
						case (SettingIDs.ROTATION_MODE):
							ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("Normal", (int)StCam.STCAM_ROTATION_OFF), 
																					new CItemForComboBox("CLOCKWISE_90", (int)StCam.STCAM_ROTATION_CLOCKWISE_90), 
																					new CItemForComboBox("COUNTERCLOCKWISE_90", (int)StCam.STCAM_ROTATION_COUNTERCLOCKWISE_90)};
							break;
						#endregion MirrorRotation

						#region AVI
						case (SettingIDs.AVI_COMPRESSOR):
							bool IsMP42FileExist = m_StCamera.IsMP42FileExist;
							if (IsMP42FileExist)
							{
								ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("Uncompressed", (int)StCam.STCAM_AVI_COMPRESSOR_UNCOMPRESSED), 
																					new CItemForComboBox("Motion JPEG", (int)StCam.STCAM_AVI_COMPRESSOR_MJPG), 
																					new CItemForComboBox("MS-MPEG4 V1", (int)StCam.STCAM_AVI_COMPRESSOR_MPV4), 
																					new CItemForComboBox("MS-MPEG4 V2", (int)StCam.STCAM_AVI_COMPRESSOR_MP42)};
							}
							else
							{
								ifcb = new CItemForComboBox[]{ 
																					new CItemForComboBox("Uncompressed", (int)StCam.STCAM_AVI_COMPRESSOR_UNCOMPRESSED), 
																					new CItemForComboBox("Motion JPEG", (int)StCam.STCAM_AVI_COMPRESSOR_MJPG)};
							}

							break;
						
						#endregion AVI

						case (SettingIDs.DEFECT_PIXEL_CORRECTION_MODE):
							ifcb = new CItemForComboBox[]
								{
								new CItemForComboBox("OFF", StCam.STCAM_DEFECT_PIXEL_CORRECTION_OFF),
								new CItemForComboBox("ON", StCam.STCAM_DEFECT_PIXEL_CORRECTION_ON),
								};
							break;

					}

					if (ifcb != null)
					{
						scb.Items.Clear();
						scb.Items.AddRange(ifcb);
						scb.SelectedIndex = 0;
					}
					else
					{
						MessageBox.Show("[frmSetBase::InitComboBox] Item list is not exist for " + scb.SettingID.ToString());
					}

				}
				else
				{
					if (0 < ctrl.Controls.Count)
					{
						InitComboBox(ctrl.Controls);
					}

				}


			}

		}

		protected void OnUpdateSettingValue(object sender, System.EventArgs e)
		{
			ISettingValue isv = sender as ISettingValue;

			System.Diagnostics.Debug.WriteLine("frmSetting::OnUpdateSettingValue : " + isv.SettingID.ToString() + "->" + isv.SettingValue.ToString());

			switch (isv.SettingID)
			{
				#region PreviewWindow
				case (SettingIDs.DISPLAY_MODE):
					m_StCamera.DisplayMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.ASPECT_MODE):
					m_StCamera.AspectMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.PW_OFFSETX):
					m_StCamera.PreviewWindowOffsetX = isv.SettingValue;
					break;
				case (SettingIDs.PW_OFFSETY):
					m_StCamera.PreviewWindowOffsetY = isv.SettingValue;
					break;
				case (SettingIDs.PW_WIDTH):
					m_StCamera.PreviewWindowWidth = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PW_HEIGHT):
					m_StCamera.PreviewWindowHeight = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PM_OFFSETX):
					m_StCamera.PreviewMaskOffsetX = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PM_OFFSETY):
					m_StCamera.PreviewMaskOffsetY = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PM_WIDTH):
					m_StCamera.PreviewMaskWidth = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PM_HEIGHT):
					m_StCamera.PreviewMaskHeight = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PD_OFFSETX):
					m_StCamera.PreviewDestOffsetX = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PD_OFFSETY):
					m_StCamera.PreviewDestOffsetY = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PD_WIDTH):
					m_StCamera.PreviewDestWidth = (uint)isv.SettingValue;
					break;
				case (SettingIDs.PD_HEIGHT):
					m_StCamera.PreviewDestHeight = (uint)isv.SettingValue;
					break;
				#endregion PreviewWindow
				#region Shutter/Gain
				case (SettingIDs.ALC_MODE):
					m_StCamera.ALCMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.GAIN):
					m_StCamera.Gain = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.SHUTTER):
					m_StCamera.ExposureClock = (uint)isv.SettingValue;
					break;
				case (SettingIDs.ALC_TARGET):
					m_StCamera.ALCTarget = (byte)isv.SettingValue;
					break;
				case (SettingIDs.ALC_TOLERANCE):
					m_StCamera.ALCTolerance = (byte)isv.SettingValue;
					break;
				case (SettingIDs.ALC_THRESHOLD):
					m_StCamera.ALCThreshold = (byte)isv.SettingValue;
					break;

				case (SettingIDs.AGC_CHECK):
					m_StCamera.IsAGCOn = (isv.SettingValue != 0);
					break;
				case (SettingIDs.AEE_CHECK):
					m_StCamera.IsAEEOn = (isv.SettingValue != 0);
					break;
				case (SettingIDs.SHUTTER_BTN):
					if (isv.SettingValue == 0)
					{
						m_StCamera.ExposureClock = 0;
					}
					else
					{
						m_StCamera.ExposureTime = (float)(1.0 / (float)isv.SettingValue);
					}
					break;
				case (SettingIDs.AEE_RANGE):
					{
						ISettingRange2 isr2 = sender as ISettingRange2;
						if (isr2 != null)
						{
							m_StCamera.AEExposureClockRange = new uint[] { (uint)isr2.SettingValue, (uint)isr2.SettingValue2 };
						}
					}
					break;
				case (SettingIDs.AEE_RANGE_MIN):
					m_StCamera.AEMinExposureClock = (uint)(isv.SettingValue);
					break;
				case (SettingIDs.AEE_RANGE_MAX):
					m_StCamera.AEMaxExposureClock = (uint)(isv.SettingValue);
					break;
				case (SettingIDs.AGC_RANGE):
					{
						ISettingRange2 isr2 = sender as ISettingRange2;
						if (isr2 != null)
						{
							m_StCamera.AGCGainRange = new ushort[] { (ushort)isr2.SettingValue, (ushort)isr2.SettingValue2 };
						}
					}
					break;
				case (SettingIDs.AGC_RANGE_MIN):
					m_StCamera.AGCMinGain = (ushort)(isv.SettingValue);
					break;
				case (SettingIDs.AGC_RANGE_MAX):
					m_StCamera.AGCMaxGain = (ushort)(isv.SettingValue);
					break;
				case (SettingIDs.DIGITAL_GAIN):
					m_StCamera.DigitalGain = (ushort)(isv.SettingValue);
					break;


				case (SettingIDs.ALC_WEIGHT_00):
				case (SettingIDs.ALC_WEIGHT_01):
				case (SettingIDs.ALC_WEIGHT_02):
				case (SettingIDs.ALC_WEIGHT_03):
				case (SettingIDs.ALC_WEIGHT_04):
				case (SettingIDs.ALC_WEIGHT_05):
				case (SettingIDs.ALC_WEIGHT_06):
				case (SettingIDs.ALC_WEIGHT_07):
				case (SettingIDs.ALC_WEIGHT_08):
				case (SettingIDs.ALC_WEIGHT_09):
				case (SettingIDs.ALC_WEIGHT_10):
				case (SettingIDs.ALC_WEIGHT_11):
				case (SettingIDs.ALC_WEIGHT_12):
				case (SettingIDs.ALC_WEIGHT_13):
				case (SettingIDs.ALC_WEIGHT_14):
				case (SettingIDs.ALC_WEIGHT_15):
					{
						byte[] pbyteWeight = m_StCamera.ALCWeight;
						pbyteWeight[isv.SettingID - SettingIDs.ALC_WEIGHT_00] = (byte)isv.SettingValue;
						m_StCamera.ALCWeight = pbyteWeight;
					}
					break;

				case (SettingIDs.CONTROL_AREA_X_0):
				case (SettingIDs.CONTROL_AREA_X_1):
				case (SettingIDs.CONTROL_AREA_X_2):
					{
						ushort[] pwArea = m_StCamera.ControlAreaX;
						pwArea[isv.SettingID - SettingIDs.CONTROL_AREA_X_0] = (ushort)isv.SettingValue;
						m_StCamera.ControlAreaX = pwArea;
					}
					break;

				case (SettingIDs.CONTROL_AREA_Y_0):
				case (SettingIDs.CONTROL_AREA_Y_1):
				case (SettingIDs.CONTROL_AREA_Y_2):
					{
						ushort[] pwArea = m_StCamera.ControlAreaY;
						pwArea[isv.SettingID - SettingIDs.CONTROL_AREA_Y_0] = (ushort)isv.SettingValue;
						m_StCamera.ControlAreaY = pwArea;
					}
					break;

				#endregion Shutter/Gain

				#region WB
				case (SettingIDs.WB_MODE):
					m_StCamera.WBMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.AWB_CHECK):
					m_StCamera.IsAWBOn = (isv.SettingValue != 0);
					break;
				case (SettingIDs.WB_GAIN_R):
					m_StCamera.WBGainR = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.WB_GAIN_GR):
					m_StCamera.WBGainGr = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.WB_GAIN_GB):
					m_StCamera.WBGainGb = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.WB_GAIN_B):
					m_StCamera.WBGainB = (ushort)isv.SettingValue;
					break;

				case (SettingIDs.WB_TARGET_R):
					m_StCamera.WBTargetR = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.WB_TARGET_B):
					m_StCamera.WBTargetB = (ushort)isv.SettingValue;
					break;
				#endregion WB
				#region Clamp
				case (SettingIDs.DIGITAL_CLAMP):
					m_StCamera.DigitalClamp = (ushort)isv.SettingValue;
					break;
				#endregion //Clamp
				#region Gamma
				case (SettingIDs.GAMMA_Y_MODE):
					m_StCamera.GammaYMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_Y_VALUE):
					m_StCamera.GammaYValue = (ushort)isv.SettingValue;
					break;
				case(SettingIDs.CAMERA_GAMMA_VALUE):
					m_StCamera.CameraGamma = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_CHECK):
					m_StCamera.IsGammaYOn = (isv.SettingValue != 0);
					break;
				case (SettingIDs.GAMMA_R_MODE):
					m_StCamera.GammaRMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_R_VALUE):
					m_StCamera.GammaRValue = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_GR_MODE):
					m_StCamera.GammaGrMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_GR_VALUE):
					m_StCamera.GammaGrValue = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_GB_MODE):
					m_StCamera.GammaGbMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_GB_VALUE):
					m_StCamera.GammaGbValue = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_B_MODE):
					m_StCamera.GammaBMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.GAMMA_B_VALUE):
					m_StCamera.GammaBValue = (ushort)isv.SettingValue;
					break;
				#endregion Gamma
				#region Sharpness
				case (SettingIDs.SHARPNESS_MODE):
					m_StCamera.SharpnessMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.SHARPNESS_CHECK):
					m_StCamera.IsSharpnessOn = (isv.SettingValue != 0);
					break;
				case (SettingIDs.SHARPNESS_GAIN):
					m_StCamera.SharpnessGain = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.SHARPNESS_CORING):
					m_StCamera.SharpnessCoring = (byte)isv.SettingValue;
					break;
				#endregion Sharpness
				#region Hue/Saturation
				case (SettingIDs.HUE_SATURATION_MODE):
					m_StCamera.HueSaturationMode = (byte)isv.SettingValue;
					break;
				case (SettingIDs.HUESATURATION_CHECK):
					m_StCamera.IsHueSaturationOn = (isv.SettingValue != 0);
					break;
				case (SettingIDs.HUE):
					m_StCamera.Hue = (short)isv.SettingValue;
					break;
				case (SettingIDs.SATURATION):
					m_StCamera.Saturation = (ushort)isv.SettingValue;
					break;
				#endregion Hue/Saturation
				#region ScanMode
				case (SettingIDs.SCAN_MODE):
					m_StCamera.ScanMode = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.SCAN_OFFSET_Y):
					m_StCamera.OrgImageOffsetY = (uint)isv.SettingValue;
					break;
				case (SettingIDs.SCAN_HEIGHT):
					m_StCamera.OrgImageHeight = (uint)isv.SettingValue;
					break;
				case (SettingIDs.SCAN_OFFSET_X):
					m_StCamera.OrgImageOffsetX = (uint)isv.SettingValue;
					break;
				case (SettingIDs.SCAN_WIDTH):
					m_StCamera.OrgImageWidth = (uint)isv.SettingValue;
					break;
				case(SettingIDs.H_BIN_SKIP):
					m_StCamera.HBinningSkipping = (ushort)isv.SettingValue;
					break;
				case(SettingIDs.V_BIN_SKIP):
					m_StCamera.VBinningSkipping = (ushort)isv.SettingValue;
					break;
				case (SettingIDs.H_BIN_SUM):
					m_StCamera.BinningSumMode = (ushort)((m_StCamera.BinningSumMode & 0xFF00) | (ushort)isv.SettingValue);
					break;
				case (SettingIDs.V_BIN_SUM):
					m_StCamera.BinningSumMode = (ushort)((m_StCamera.BinningSumMode & 0x00FF) | (ushort)isv.SettingValue);
					break;
				#endregion ScanMode
				#region Clock
				case (SettingIDs.CLOCK_MODE):
					m_StCamera.ClockMode = (uint)isv.SettingValue;
					break;
				case(SettingIDs.VBLANKING_FOR_FPS):
					m_StCamera.VBlankingForFPS = (uint)isv.SettingValue;
					break;
				#endregion Clock
				#region PixelFormat
				case (SettingIDs.PIXEL_FORMAT):
					m_StCamera.PixelFormat = (uint)isv.SettingValue;
					break;
				#endregion PixelFormat
				#region ColorInterpolation
				case (SettingIDs.COLOR_INTERPOLATION):
					m_StCamera.ColorInterpolation = (byte)isv.SettingValue;
					break;
				#endregion ColorInterpolation
				#region MirrorRotation
				case (SettingIDs.MIRROR_MODE):
					m_StCamera.Mirror = (byte)isv.SettingValue;
					break;
				case (SettingIDs.ROTATION_MODE):
					m_StCamera.Rotation = (byte)isv.SettingValue;
					break;
				#endregion MirrorRotation
				#region ColorMatrix
				case (SettingIDs.COLOR_MATRIX_00):
				case (SettingIDs.COLOR_MATRIX_01):
				case (SettingIDs.COLOR_MATRIX_02):
				case (SettingIDs.COLOR_MATRIX_03):
				case (SettingIDs.COLOR_MATRIX_10):
				case (SettingIDs.COLOR_MATRIX_11):
				case (SettingIDs.COLOR_MATRIX_12):
				case (SettingIDs.COLOR_MATRIX_13):
				case (SettingIDs.COLOR_MATRIX_20):
				case (SettingIDs.COLOR_MATRIX_21):
				case (SettingIDs.COLOR_MATRIX_22):
				case (SettingIDs.COLOR_MATRIX_23):
					{
						short[] pshtMat = m_StCamera.ColorMatrix;
						pshtMat[isv.SettingID - SettingIDs.COLOR_MATRIX_00] = (short)isv.SettingValue;
						m_StCamera.ColorMatrix = pshtMat;
					}
					break;
				#endregion ColorMatrix
				#region AVI
				case(SettingIDs.AVI_FILE_FORMAT):
					m_StCamera.AVIFileFormat = (uint)isv.SettingValue;
					break;
				case (SettingIDs.AVI_COMPRESSOR):
					m_StCamera.AVICompressor = (uint)isv.SettingValue;
					break;
				case (SettingIDs.AVI_QUALITY):
					m_StCamera.AVIQuality = (uint)isv.SettingValue;
					break;
				case (SettingIDs.AVI_LENGTH):
					m_StCamera.AVILength = (uint)isv.SettingValue;
					break;

				#endregion AVI
				case(SettingIDs.CAMERA_SEETING_BTN):
					m_StCamera.CameraSetting((ushort)isv.SettingValue);
					break;
				#region DefectPixelCorrection
				case (SettingIDs.DEFECT_PIXEL_CORRECTION_MODE):
					m_StCamera.DefectPixelCorrectionMode = (ushort)isv.SettingValue;
					break;

				#endregion DefectPixelCorrection
				default:
					#region DefectPixelCorrection
					if ((SettingIDs.DEFECT_PIXEL_POS_X_00 <= isv.SettingID) && (isv.SettingID <= SettingIDs.DEFECT_PIXEL_POS_Y_LAST))
					{
						int nTmp = (int)(isv.SettingID - SettingIDs.DEFECT_PIXEL_POS_X_00);
						ushort wIndex = (ushort)(nTmp / 2);
						uint x = 0;
						uint y = 0;
						m_StCamera.GetDefectPixelCorrectionPosition(wIndex, out x, out y);
						if (nTmp % 2 == 0)
						{
							x = (ushort)isv.SettingValue;
						}
						else
						{
							y = (ushort)isv.SettingValue;
						}
						m_StCamera.SetDefectPixelCorrectionPosition(wIndex, x, y);
					}
					#endregion DefectPixelCorrection
					else
					{
						MessageBox.Show("[frmSetting::OnUpdateSettingValue] Item list is not exist for " + isv.SettingID.ToString());
					}
					break;
			}


			UpdateDisplay();
		}
		protected void UpdateSettingCtrls(Control.ControlCollection ctrls)
		{
			foreach (Control ctrl in ctrls)
			{
				#region foreach
				ISettingCtrl isc = ctrl as ISettingCtrl;
				if (isc != null)
				{
					const int maxSizeH = 1600;
					const int maxSizeV = 1600;
					int val = 0;
					int val2 = 100;
					int max = int.MaxValue;
					int min = int.MinValue;
					string strLabel = null;
					bool enabled = true;
					bool visible = true;

					//val , max
					switch (isc.SettingID)
					{
						#region PreviewWindow
						case (SettingIDs.ASPECT_MODE):
							val = m_StCamera.AspectMode;
							break;
						case (SettingIDs.DISPLAY_MODE):
							val = m_StCamera.DisplayMode;
							break;
						case (SettingIDs.PW_OFFSETX):
							max = maxSizeH;
							val = m_StCamera.PreviewWindowOffsetX;
							break;
						case (SettingIDs.PW_OFFSETY):
							max = maxSizeV;
							val = m_StCamera.PreviewWindowOffsetY;
							break;
						case (SettingIDs.PW_WIDTH):
							max = maxSizeH;
							val = (int)m_StCamera.PreviewWindowWidth;
							break;
						case (SettingIDs.PW_HEIGHT):
							max = maxSizeV;
							val = (int)m_StCamera.PreviewWindowHeight;
							break;
						case (SettingIDs.PM_OFFSETX):
							max = (int)m_StCamera.OrgImageWidth - (int)m_StCamera.PreviewMaskWidth;
							val = (int)m_StCamera.PreviewMaskOffsetX;
							break;
						case (SettingIDs.PM_OFFSETY):
							max = (int)m_StCamera.OrgImageHeight - (int)m_StCamera.PreviewMaskHeight;
							val = (int)m_StCamera.PreviewMaskOffsetY;
							break;
						case (SettingIDs.PM_WIDTH):
							max = (int)m_StCamera.OrgImageWidth - (int)m_StCamera.PreviewMaskOffsetX;
							val = (int)m_StCamera.PreviewMaskWidth;
							break;
						case (SettingIDs.PM_HEIGHT):
							max = (int)m_StCamera.OrgImageHeight - (int)m_StCamera.PreviewMaskOffsetY;
							val = (int)m_StCamera.PreviewMaskHeight;
							break;
						case (SettingIDs.PD_OFFSETX):
							max = maxSizeH;
							val = (int)m_StCamera.PreviewDestOffsetX;
							break;
						case (SettingIDs.PD_OFFSETY):
							max = maxSizeV;
							val = (int)m_StCamera.PreviewDestOffsetY;
							break;
						case (SettingIDs.PD_WIDTH):
							max = maxSizeH;
							val = (int)m_StCamera.PreviewDestWidth;
							break;
						case (SettingIDs.PD_HEIGHT):
							max = maxSizeV;
							val = (int)m_StCamera.PreviewDestHeight;
							break;
						#endregion PreviewWindow
						#region Shutter/Gain
						case (SettingIDs.ALC_MODE):
							val = (int)m_StCamera.ALCMode;
							break;
						case (SettingIDs.GAIN):
							max = m_StCamera.MaxGain;
							min = m_StCamera.MinGain;
							val = (int)m_StCamera.Gain;
							strLabel = m_StCamera.GainDB.ToString("0.00") + "dB";
							enabled = !m_StCamera.IsAGCOn;
							break;
						case (SettingIDs.SHUTTER):
							max = (int)m_StCamera.MaxExposureClock;
							min = 0;
							val = (int)m_StCamera.ExposureClock;
							double exposureTime = m_StCamera.ExposureTime;
							if (exposureTime <= 0.5)
							{
								double invExposureTime = 1.0 / exposureTime;
								strLabel = "1/" + invExposureTime.ToString("0.00") + "[s]";
							}
							else
							{
								strLabel = exposureTime.ToString("0.00") + "[s]";
							}
							enabled = !m_StCamera.IsAEEOn;
							break;
						case (SettingIDs.SHUTTER_BTN):
							if (m_StCamera.IsAEEOn)
							{
								enabled = false;
							}
							else
							{
								SettingButton sb = ctrl as SettingButton;
								if (sb != null)
								{
									if (sb.SettingValue == 0)
									{
										enabled = true;
									}
									else
									{
										double invMin = 1.0 / m_StCamera.MaxExposureTime;
										if (sb.SettingValue < invMin)
										{
											enabled = false;
										}
										else
										{
											enabled = true;
										}
									}
								}
							}
							break;
						case (SettingIDs.ALC_TARGET):
							max = 255;
							min = 0;
							val = (int)m_StCamera.ALCTarget;
							enabled = m_StCamera.IsAEEOn || m_StCamera.IsAGCOn;
							break;
						case (SettingIDs.ALC_TOLERANCE):
							max = 255;
							min = 0;
							val = (int)m_StCamera.ALCTolerance;
							enabled = m_StCamera.IsAEEOn || m_StCamera.IsAGCOn;
							visible = !m_StCamera.HasCameraSideALC;
							break;
						case (SettingIDs.ALC_THRESHOLD):
							max = 255;
							min = 0;
							val = (int)m_StCamera.ALCThreshold;
							enabled = m_StCamera.IsAEEOn || m_StCamera.IsAGCOn;
							visible = !m_StCamera.HasCameraSideALC;
							break;
						case (SettingIDs.AGC_CHECK):
							val = (m_StCamera.IsAGCOn ? -1 : 0);
							break;
						case (SettingIDs.AEE_CHECK):
							val = (m_StCamera.IsAEEOn ? -1 : 0);
							break;
						case (SettingIDs.AEE_RANGE):
							max = (int)m_StCamera.MaxExposureClock;
							min = 1;
							val = (int)m_StCamera.AEMinExposureClock;
							val2 = (int)m_StCamera.AEMaxExposureClock;
							enabled = m_StCamera.IsAEEOn;
							break;
						case (SettingIDs.AEE_RANGE_MIN):
							max = (int)m_StCamera.MaxExposureClock;
							min = 1;
							val = (int)m_StCamera.AEMinExposureClock;
							enabled = m_StCamera.IsAEEOn;
							double exposureMinTime = m_StCamera.AEEMinExposureTime;
							if (exposureMinTime <= 0.5)
							{
								double invExposureMinTime = 1.0 / exposureMinTime;
								strLabel = "1/" + invExposureMinTime.ToString("0.00") + "[s]";
							}
							else
							{
								strLabel = exposureMinTime.ToString("0.00") + "[s]";
							}
							break;
						case (SettingIDs.AEE_RANGE_MAX):
							max = (int)m_StCamera.MaxExposureClock;
							min = 1;
							val = (int)m_StCamera.AEMaxExposureClock;
							enabled = m_StCamera.IsAEEOn;
							double exposureMaxTime = m_StCamera.AEEMaxExposureTime;
							if (exposureMaxTime <= 0.5)
							{
								double invExposureMaxTime = 1.0 / exposureMaxTime;
								strLabel = "1/" + invExposureMaxTime.ToString("0.00") + "[s]";
							}
							else
							{
								strLabel = exposureMaxTime.ToString("0.00") + "[s]";
							}
							break;
						case (SettingIDs.AGC_RANGE):
							max = m_StCamera.MaxGain;
							min = m_StCamera.MinGain;
							val = m_StCamera.AGCMinGain;
							val2 = m_StCamera.AGCMaxGain;
							enabled = m_StCamera.IsAGCOn;
							break;
						case (SettingIDs.AGC_RANGE_MIN):
							max = m_StCamera.MaxGain;
							min = m_StCamera.MinGain;
							val = m_StCamera.AGCMinGain;
							enabled = m_StCamera.IsAGCOn;
							strLabel = m_StCamera.AGCMinGainDB.ToString("0.00") + "dB";
							break;
						case (SettingIDs.AGC_RANGE_MAX):
							max = m_StCamera.MaxGain;
							min = m_StCamera.MinGain;
							val = m_StCamera.AGCMaxGain;
							enabled = m_StCamera.IsAGCOn;
							strLabel = m_StCamera.AGCMaxGainDB.ToString("0.00") + "dB";
							break;
						case (SettingIDs.DIGITAL_GAIN):
							max = m_StCamera.MaxDigitalGain;
							min = m_StCamera.MinDigitalGain;
							val = m_StCamera.DigitalGain;
							strLabel = "x" + m_StCamera.DigitalGainPower.ToString("0.00");
							enabled = m_StCamera.HasDigitalGain;
							break;
						case (SettingIDs.ALC_WEIGHT_00):
						case (SettingIDs.ALC_WEIGHT_01):
						case (SettingIDs.ALC_WEIGHT_02):
						case (SettingIDs.ALC_WEIGHT_03):
						case (SettingIDs.ALC_WEIGHT_04):
						case (SettingIDs.ALC_WEIGHT_05):
						case (SettingIDs.ALC_WEIGHT_06):
						case (SettingIDs.ALC_WEIGHT_07):
						case (SettingIDs.ALC_WEIGHT_08):
						case (SettingIDs.ALC_WEIGHT_09):
						case (SettingIDs.ALC_WEIGHT_10):
						case (SettingIDs.ALC_WEIGHT_11):
						case (SettingIDs.ALC_WEIGHT_12):
						case (SettingIDs.ALC_WEIGHT_13):
						case (SettingIDs.ALC_WEIGHT_14):
						case (SettingIDs.ALC_WEIGHT_15):
							{
								max = 255;
								min = 0;
								byte[] pbyteWeight = m_StCamera.ALCWeight;
								val = pbyteWeight[isc.SettingID - SettingIDs.ALC_WEIGHT_00];
							}
							break;

						case (SettingIDs.CONTROL_AREA_X_0):
						case (SettingIDs.CONTROL_AREA_X_1):
						case (SettingIDs.CONTROL_AREA_X_2):
							{
								max = 10000;
								min = 0;
								ushort[] pwArea = m_StCamera.ControlAreaX;
								val = pwArea[isc.SettingID - SettingIDs.CONTROL_AREA_X_0];
								strLabel = ((double)(val / 100.0)).ToString("0.00") + "%";
							}
							break;

						case (SettingIDs.CONTROL_AREA_Y_0):
						case (SettingIDs.CONTROL_AREA_Y_1):
						case (SettingIDs.CONTROL_AREA_Y_2):
							{
								max = 10000;
								min = 0;
								ushort[] pwArea = m_StCamera.ControlAreaY;
								val = pwArea[isc.SettingID - SettingIDs.CONTROL_AREA_Y_0];
								strLabel = ((double)(val / 100.0)).ToString("0.00") + "%";
							}
							break;
						#endregion Shutter/Gain
						#region WB
						case (SettingIDs.WB_MODE):
							val = m_StCamera.WBMode;
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.AWB_CHECK):
							val = (m_StCamera.IsAWBOn ? -1 : 0);
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.WB_GAIN_R):
							max = 511;
							min = 128;
							val = m_StCamera.WBGainR;
							strLabel = "x" + ((double)(val / 128.0)).ToString("0.00");
							enabled = (!m_StCamera.IsAWBOn) && m_StCamera.IsColor;
							break;
						case (SettingIDs.WB_GAIN_GR):
							max = 511;
							min = 128;
							val = m_StCamera.WBGainGr;
							strLabel = "x" + ((double)(val / 128.0)).ToString("0.00");
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.WB_GAIN_GB):
							max = 511;
							min = 128;
							val = m_StCamera.WBGainGb;
							strLabel = "x" + ((double)(val / 128.0)).ToString("0.00");
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.WB_GAIN_B):
							max = 511;
							min = 128;
							val = m_StCamera.WBGainB;
							strLabel = "x" + ((double)(val / 128.0)).ToString("0.00");
							enabled = (!m_StCamera.IsAWBOn) && m_StCamera.IsColor;
							break;

						case (SettingIDs.WB_TARGET_R):
							max = 512;
							min = 0;
							val = m_StCamera.WBTargetR;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsAWBOn && m_StCamera.IsColor;
							visible = !m_StCamera.HasCameraSideWB;
							break;
						case (SettingIDs.WB_TARGET_B):
							max = 512;
							min = 0;
							val = m_StCamera.WBTargetB;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsAWBOn && m_StCamera.IsColor;
							visible = !m_StCamera.HasCameraSideWB;
							break;
						#endregion WB
						#region Clamp
						case (SettingIDs.DIGITAL_CLAMP):
							max = 255;
							min = 0;
							val = m_StCamera.DigitalClamp;
							enabled = visible = m_StCamera.HasDigitalClamp;
							break;
						#endregion //Clamp

						#region Gamma
						case (SettingIDs.GAMMA_Y_MODE):
							val = m_StCamera.GammaYMode;
							break;
						case (SettingIDs.GAMMA_Y_VALUE):
							max = 500;
							min = 1;
							val = m_StCamera.GammaYValue;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsGammaYOn;
							break;
						case (SettingIDs.GAMMA_CHECK):
							val = (m_StCamera.IsGammaYOn ? -1 : 0);
							break;
						case (SettingIDs.CAMERA_GAMMA_VALUE):
							max = 40;
							min = 0;
							val = m_StCamera.CameraGamma;
							strLabel = (val == 0) ? "OFF" : ((double)(val / 10.0)).ToString("0.0");
							enabled = visible = m_StCamera.HasCameraSideGamma;
							break;

						case (SettingIDs.GAMMA_R_MODE):
							val = m_StCamera.GammaRMode;
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_R_VALUE):
							max = 500;
							min = 1;
							val = m_StCamera.GammaRValue;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsGammaROn && m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_GR_MODE):
							val = m_StCamera.GammaGrMode;
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_GR_VALUE):
							max = 500;
							min = 1;
							val = m_StCamera.GammaGrValue;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsGammaGrOn && m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_GB_MODE):
							val = m_StCamera.GammaGbMode;
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_GB_VALUE):
							max = 500;
							min = 1;
							val = m_StCamera.GammaGbValue;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsGammaGbOn && m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_B_MODE):
							val = m_StCamera.GammaBMode;
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.GAMMA_B_VALUE):
							max = 500;
							min = 1;
							val = m_StCamera.GammaBValue;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsGammaBOn && m_StCamera.IsColor;
							break;
						#endregion Gamma

						#region Sharpness
						case (SettingIDs.SHARPNESS_MODE):
							val = m_StCamera.SharpnessMode;
							break;
						case (SettingIDs.SHARPNESS_CHECK):
							val = (m_StCamera.IsSharpnessOn ? -1 : 0);
							break;
						case (SettingIDs.SHARPNESS_GAIN):
							max = 500;
							min = 0;
							val = m_StCamera.SharpnessGain;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsSharpnessOn;
							break;
						case (SettingIDs.SHARPNESS_CORING):
							max = 255;
							min = 0;
							val = m_StCamera.SharpnessCoring;
							enabled = m_StCamera.IsSharpnessOn;
							break;
						#endregion Sharpness
						#region Hue/Saturation
						case (SettingIDs.HUE_SATURATION_MODE):
							val = m_StCamera.HueSaturationMode;
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.HUESATURATION_CHECK):
							val = (m_StCamera.IsHueSaturationOn ? -1 : 0);
							enabled = m_StCamera.IsColor;
							break;
						case (SettingIDs.HUE):
							max = 1800;
							min = -1800;
							val = m_StCamera.Hue;
							strLabel = ((double)(val / 10.0)).ToString("0.00");
							enabled = m_StCamera.IsHueSaturationOn && m_StCamera.IsColor;
							break;
						case (SettingIDs.SATURATION):
							max = 200;
							min = 0;
							val = m_StCamera.Saturation;
							strLabel = "x" + ((double)(val / 100.0)).ToString("0.00");
							enabled = m_StCamera.IsHueSaturationOn && m_StCamera.IsColor;
							break;
						#endregion Hue/Saturation
						#region ScanMode
						case (SettingIDs.SCAN_MODE):
							val = m_StCamera.ScanMode;
							break;
						case (SettingIDs.SCAN_OFFSET_Y):
							max = (int)m_StCamera.MaxImageHeight - 2;
							min = 0;
							val = (int)m_StCamera.OrgImageOffsetY;
							enabled = (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_VARIABLE_PARTIAL) || (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_BINNING_VARIABLE_PARTIAL) || (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							break;
						case (SettingIDs.SCAN_HEIGHT):
							max = (int)m_StCamera.MaxImageHeight;
							min = 2;
							val = (int)m_StCamera.OrgImageHeight;
							enabled = (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_VARIABLE_PARTIAL) || (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_BINNING_VARIABLE_PARTIAL) || (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							break;
						case (SettingIDs.SCAN_OFFSET_X):
							max = (int)m_StCamera.MaxImageWidth - 2;
							min = 0;
							val = (int)m_StCamera.OrgImageOffsetX;
							enabled = (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							break;
						case (SettingIDs.SCAN_WIDTH):
							max = (int)m_StCamera.MaxImageWidth;
							min = 2;
							val = (int)m_StCamera.OrgImageWidth;
							enabled = (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							break;
						case (SettingIDs.H_BIN_SKIP):
							val = m_StCamera.HBinningSkipping;
							enabled = m_StCamera.EnableBinningSkipping & (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							visible = m_StCamera.EnableBinningSkipping;
							break;
						case (SettingIDs.V_BIN_SKIP):
							val = m_StCamera.VBinningSkipping;
							enabled = m_StCamera.EnableBinningSkipping & (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							visible = m_StCamera.EnableBinningSkipping;
							break;
						case (SettingIDs.H_BIN_SUM):
							val = m_StCamera.BinningSumMode & 0x00FF;
							enabled = m_StCamera.EnableBinningSkipping & (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							visible = m_StCamera.HasHBinningSum;
							break;
						case (SettingIDs.V_BIN_SUM):
							val = m_StCamera.BinningSumMode & 0xFF00;
							enabled = m_StCamera.EnableBinningSkipping & (m_StCamera.ScanMode == StCam.STCAM_SCAN_MODE_AOI);
							visible = m_StCamera.HasVBinningSum;
							break;
						#endregion ScanMode
						#region Clock
						case (SettingIDs.CLOCK_MODE):
							val = (int)m_StCamera.ClockMode;
							if (m_StCamera.EnableClockModeCount < 2)
							{
								visible = false;
								enabled = false;
							}
							break;
						case(SettingIDs.VBLANKING_FOR_FPS):
							max = (int)m_StCamera.MaxVBlankingSetting;
							min = 0;
							val = (int)m_StCamera.VBlankingForFPS;
							visible = enabled = m_StCamera.EnableVBlaningSetting;
							break;
						#endregion Clock
						#region PixelFormat
						case (SettingIDs.PIXEL_FORMAT):
							val = (int)m_StCamera.PixelFormat;
							break;
						#endregion PixelFormat
						#region ColorInterpolation
						case (SettingIDs.COLOR_INTERPOLATION):
							val = m_StCamera.ColorInterpolation;
							enabled = m_StCamera.IsColor;
							break;
						#endregion ColorInterpolation
						#region MirrorRotation
						case (SettingIDs.MIRROR_MODE):
							val = m_StCamera.Mirror;
							break;
						case (SettingIDs.ROTATION_MODE):
							val = m_StCamera.Rotation;
							break;
						#endregion MirrorRotation


						#region ColorMatrix
						case (SettingIDs.COLOR_MATRIX_00):
						case (SettingIDs.COLOR_MATRIX_01):
						case (SettingIDs.COLOR_MATRIX_02):
						case (SettingIDs.COLOR_MATRIX_03):
						case (SettingIDs.COLOR_MATRIX_10):
						case (SettingIDs.COLOR_MATRIX_11):
						case (SettingIDs.COLOR_MATRIX_12):
						case (SettingIDs.COLOR_MATRIX_13):
						case (SettingIDs.COLOR_MATRIX_20):
						case (SettingIDs.COLOR_MATRIX_21):
						case (SettingIDs.COLOR_MATRIX_22):
						case (SettingIDs.COLOR_MATRIX_23):
							{
								max = short.MaxValue;
								min = short.MinValue;
								short[] pshtMat = m_StCamera.ColorMatrix;
								val = pshtMat[isc.SettingID - SettingIDs.COLOR_MATRIX_00];
							}
							break;
						#endregion ColorMatrix
						#region AVI
						case(SettingIDs.AVI_FILE_FORMAT):
							switch(m_StCamera.AVICompressor)
							{
								case(StCam.STCAM_AVI_COMPRESSOR_UNCOMPRESSED):
								case(StCam.STCAM_AVI_COMPRESSOR_MJPG):
									break;
								default:
									visible = false;
									break;
							}
							val = (int)m_StCamera.AVIFileFormat;
							break;
						case (SettingIDs.AVI_COMPRESSOR):
							val = (int)m_StCamera.AVICompressor;
							break;
						case (SettingIDs.AVI_QUALITY):
							max = 100;
							min = 1;
							val = (int)m_StCamera.AVIQuality;
							break;
						case (SettingIDs.AVI_LENGTH):
							max = (int)m_StCamera.AVIMaxLength;
							min = 1;
							val = (int)m_StCamera.AVILength;
							break;

						#endregion AVI
						case (SettingIDs.OUTPUT_FPS):
							strLabel = m_StCamera.OutputFPS.ToString("0.00") + "FPS";
							break;
						case (SettingIDs.CAMERA_SEETING_BTN):
							
							break;
						#region DefectPixelCorrection
						case(SettingIDs.DEFECT_PIXEL_CORRECTION_MODE):
							val = m_StCamera.DefectPixelCorrectionMode;
							break;
						#endregion DefectPixelCorrection
						default:
							#region DefectPixelCorrection
							if ((SettingIDs.DEFECT_PIXEL_POS_X_00 <= isc.SettingID) && (isc.SettingID <= SettingIDs.DEFECT_PIXEL_POS_Y_LAST))
							{
								int nTmp = (int)(isc.SettingID - SettingIDs.DEFECT_PIXEL_POS_X_00);
								ushort wIndex = (ushort)(nTmp / 2);
								uint x = 0;
								uint y = 0;
								m_StCamera.GetDefectPixelCorrectionPosition(wIndex, out x, out y);
								if (nTmp % 2 == 0)
								{
									val = (int)x;
								}
								else
								{
									val = (int)y;
								}
								max = 0xFFF;
								min = 0;
							}
							#endregion DefectPixelCorrection
							else
							{
								MessageBox.Show("[frmSetBase::UpdateSettingCtrls] Item list is not exist for " + isc.SettingID.ToString());
							}
							break;
					}

					//Enabled
					if ((max <= min) || (val < min) || (max < val))
					{
						enabled = false;
					}
					else
					{
						switch (isc.SettingID)
						{
							case (SettingIDs.PD_OFFSETX):
							case (SettingIDs.PD_OFFSETY):
							case (SettingIDs.PD_WIDTH):
							case (SettingIDs.PD_HEIGHT):
								byte byteAspectMode = m_StCamera.AspectMode;
								if ((byteAspectMode != StCam.STCAM_ASPECT_MODE_CUSTOM_OFFSET) && (byteAspectMode != StCam.STCAM_ASPECT_MODE_CUSTOM))
								{
									enabled = false;
								}
								break;
						}



					}

					ISettingRange2 isr2 = ctrl as ISettingRange2;
					if (isr2 != null)
					{
						isr2.SettingMin = min;
						isr2.SettingMax = max;
						isr2.SettingValue = val;
						isr2.SettingValue2 = val2;
						ctrl.Enabled = enabled;
						ctrl.Visible = visible;
					}
					else
					{
						ISettingRange isr = ctrl as ISettingRange;
						if (isr != null)
						{
							isr.SettingMin = min;
							isr.SettingMax = max;
							isr.SettingValue = val;

							ctrl.Enabled = enabled;
							ctrl.Visible = visible;

							TrackBar tb = ctrl as TrackBar;
							if (tb != null)
							{
								tb.TickFrequency = (max - min) / 10;
							}
						}
						else
						{
							ISettingValue isv = ctrl as ISettingValue;
							if (isv != null)
							{
								if (ctrl is SettingButton)
								{

								}
								else
								{
									isv.SettingValue = val;
								}
								ctrl.Enabled = enabled;
								ctrl.Visible = visible;
							}
							else
							{
								SettingLabel sl = ctrl as SettingLabel;
								if (sl != null)
								{
									if (strLabel == null)
									{
										strLabel = val.ToString();
									}
									sl.Text = strLabel;
									sl.Enabled = enabled;
									sl.Visible = visible;
								}
								else
								{
									SettingNameLabel snl = ctrl as SettingNameLabel;
									if (snl != null)
									{
										snl.Enabled = enabled;
										snl.Visible = visible;
									}
								}
							}
						}
					}
				}
				if (0 < ctrl.Controls.Count)
				{
					UpdateSettingCtrls(ctrl.Controls);
				}
				#endregion foreach
			}
		}
	}
}