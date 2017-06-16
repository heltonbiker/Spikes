using System;
using System.Windows.Forms;
using System.Drawing;
namespace SensorTechnology
{
	/// <summary>
	/// CStCamera
	/// </summary>
	public class CStCamera : IDisposable
	{
		private IntPtr m_hCamera = IntPtr.Zero;


		public EventHandler OnImageSizeChanged = null;



		//---------------------------

		public CStCamera()
		{
			// 
			// 
			//
		}


		private static string m_strSettingFilePath;
		private static string m_strStillImageFilePath;
		private static string m_strVideoFilePath;
		static CStCamera()
		{
			m_strSettingFilePath = String.Format(@"{0}\{1}\{2}",
			  Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			  Application.CompanyName,
			  Application.ProductName);
			m_strStillImageFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			m_strVideoFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		}

		public static string SettingFilePath
		{
			get
			{

				//
				if (!System.IO.Directory.Exists(m_strSettingFilePath))
				{
					System.IO.Directory.CreateDirectory(m_strSettingFilePath);
				}

				return (m_strSettingFilePath);
			}
			set
			{
				m_strSettingFilePath = value;
			}
		}
		public static string StillImageFilePath
		{
			get { return (m_strStillImageFilePath); }
			set { m_strStillImageFilePath = value; }
		}
		public static string VideoFilePath
		{
			get { return (m_strVideoFilePath); }
			set { m_strVideoFilePath = value; }
		}
		public static string StartUpSettingFileName()
		{
			string fileName = SettingFilePath + "\\" + Application.ProductName + ".cfg";
			return (fileName);

		}


		//---------------------------
		#region IDisposable
		public void Dispose()
		{
			Close();
		}
		#endregion
		
		//---------------------------
		public override string ToString()
		{
			return(m_strCameraName);
		}

		//---------------------------
		#region Open/Close
		public bool Open()
		{
			bool result = true;
			Close();
			m_hCamera = StCam.Open(0);
			if(m_hCamera != IntPtr.Zero)
			{
				do
				{
					m_fps = new CFPS(100);

					//result = mGetSettings();
					result = LoadSettingFile(StartUpSettingFileName());
					if (!result) break;

					m_funcRawCallback = new StCam.fStCamRawCallbackFunc(mRawCallbackFunc);
					result = StCam.AddRawCallback(m_hCamera, m_funcRawCallback, IntPtr.Zero, out m_dwRawCallbackNo);
					if (!result) break;

					m_funcPreviewBitmapCallback = new StCam.fStCamPreviewBitmapCallbackFunc(mPreviewBitmapCallbackFunc);
					result = StCam.AddPreviewBitmapCallback(m_hCamera, m_funcPreviewBitmapCallback, IntPtr.Zero, out m_dwPreviewBitmapCallbackNo);
					if (!result) break;

					m_funcPreviewGDICallback = new StCam.fStCamPreviewGDICallbackFunc(mPreviewGDICallbackFunc);
					result = StCam.AddPreviewGDICallback(m_hCamera, m_funcPreviewGDICallback, IntPtr.Zero, out m_dwPreviewGDICallbackNo);
					if (!result) break;
				} while (false);
				if(!result)
				{
					Close();
				}
			}
			else
			{
				result = false;
			}
			return(result);
		}
		public void Close()
		{
			if (m_hCamera != IntPtr.Zero)
			{
				StCam.Close(m_hCamera);
				m_hCamera = IntPtr.Zero;
			}
		}
		#endregion Open/Close

		#region SettingFile
		public bool LoadSettingFile(string fileName)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					if (m_IsTranserStarted)
					{
						result = StCam.StopTransfer(m_hCamera);
						if (!result) break;
					}

					result = StCam.LoadSettingFile(m_hCamera, fileName);
					if (!result) break;

					result = mGetSettings();
					if (!result) break;

					if (m_IsTranserStarted)
					{
						result = StCam.StartTransfer(m_hCamera);
						if (!result) break;
					}
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				else
				{
					if (OnImageSizeChanged != null)
					{
						OnImageSizeChanged(this, new EventArgs());
					}
				}
			}
			return (result);
		}
		public bool SaveSettingFile(string fileName)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					if (m_IsTranserStarted)
					{
						result = StCam.StopTransfer(m_hCamera);
						if (!result) break;
					}

					result = StCam.SaveSettingFile(m_hCamera, fileName);
					if (!result) break;

					if (m_IsTranserStarted)
					{
						result = StCam.StartTransfer(m_hCamera);
						if (!result) break;
					}
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public bool ResetSetting()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					if (m_IsTranserStarted)
					{
						result = StCam.StopTransfer(m_hCamera);
						if (!result) break;
					}

					result = StCam.ResetSetting(m_hCamera);
					if (!result) break;

					result = StCam.LoadSettingFile(m_hCamera, StartUpSettingFileName());
					if (!result) break;

					result = mGetSettings();
					if (!result) break;

					if (m_IsTranserStarted)
					{
						result = StCam.StartTransfer(m_hCamera);
						if (!result) break;
					}
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				else
				{
					if (OnImageSizeChanged != null)
					{
						OnImageSizeChanged(this, new EventArgs());
					}
				}
			}
			return (result);
		}
		public bool CameraSetting(ushort wMode)
		{
			bool result = false;

			do
			{
				if (m_IsTranserStarted)
				{
					result = StCam.StopTransfer(m_hCamera);
					if (!result) break;
				}

				result = StCam.CameraSetting(m_hCamera, wMode);
				if (!result) break;

				result = mGetSettings();
				if (!result) break;

				if (m_IsTranserStarted)
				{
					result = StCam.StartTransfer(m_hCamera);
					if (!result) break;
				}
			} while (false);
			if (!result)
			{
				uint errorNo = GetLastError();
				if (OnError != null)
				{
					OnError(this, errorNo);
				}
			}
			else
			{
				if (OnImageSizeChanged != null)
				{
					OnImageSizeChanged(this, new EventArgs());
				}
			}
			
			return (result);
		}
		#endregion SettingFile
		//---------------------------
		#region Callback
		//callabck
		public delegate void PreviewBitmapEvent(System.IntPtr pbyteBitmap, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, uint dwPreviewPixelFormat);
		public delegate void RawEvent(System.IntPtr pbyteBuffer, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, ushort wColorArray, uint dwTransferBitsPerPixel);
		public delegate void PreviewGDIEvent(System.IntPtr hDC, uint dwWidth, uint dwHeight, uint dwFrameNo);

		public event PreviewBitmapEvent OnPreviewBitmap = null;
		public event RawEvent OnRawCallback = null;
		public event PreviewGDIEvent OnPreviewGDI = null;

		private StCam.fStCamRawCallbackFunc m_funcRawCallback = null;
		private StCam.fStCamPreviewBitmapCallbackFunc m_funcPreviewBitmapCallback = null;
		private StCam.fStCamPreviewGDICallbackFunc m_funcPreviewGDICallback = null;

		private uint m_dwRawCallbackNo = 0;
		private uint m_dwPreviewBitmapCallbackNo = 0;
		private uint m_dwPreviewGDICallbackNo = 0;

		private bool m_IsFirstTimeRcvImage = true;
		private uint m_dwLastRcvFrameNo = 0;
		public uint LastRcvFrameNo
		{
			get { return (m_dwLastRcvFrameNo); }
		}
		private uint m_dwRcvFrameCount = 0;
		public uint RcvFrameCount
		{
			get { return (m_dwRcvFrameCount); }
		}
		private uint m_dwDropCount = 0;
		public uint DropCount
		{
			get { return (m_dwDropCount); }
		}
		public double FPS
		{
			get { return (m_fps.GetFPS()); }
		}

		private CFPS m_fps = null;
		private void mRawCallbackFunc(System.IntPtr pbyteBuffer, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, ushort wColorArray, uint dwTransferBitsPerPixel, System.IntPtr lpContext, System.IntPtr lpReserved)
		{
			try
			{
				if (m_IsFirstTimeRcvImage)
				{
					m_IsFirstTimeRcvImage = false;
				}
				else
				{
					if (m_dwLastRcvFrameNo + 1 != dwFrameNo)
					{
						if (m_dwLastRcvFrameNo < dwFrameNo)
						{
							m_dwDropCount += dwFrameNo - m_dwLastRcvFrameNo - 1;
						}
						else
						{
							m_dwDropCount += uint.MaxValue - m_dwLastRcvFrameNo;
							m_dwDropCount += dwFrameNo;
						}
					}
				}
				m_dwLastRcvFrameNo = dwFrameNo;
				m_dwRcvFrameCount++;
				m_fps.Check();

				if (OnRawCallback != null)
				{
					//OnRawCallback.Invoke(pbyteBuffer, dwBufferSize, dwWidth, dwHeight, dwFrameNo, wColorArray, dwTransferBitsPerPixel);
					OnRawCallback(pbyteBuffer, dwBufferSize, dwWidth, dwHeight, dwFrameNo, wColorArray, dwTransferBitsPerPixel);
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.ToString());
			}
		}
		private void mPreviewBitmapCallbackFunc(System.IntPtr pbyteBitmap, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, uint dwPreviewPixelFormat, System.IntPtr lpContext, System.IntPtr lpReserved)
		{
			if (OnPreviewBitmap != null)
			{
				//OnPreviewBitmap.Invoke(pbyteBitmap, dwBufferSize, dwWidth, dwHeight, dwFrameNo, dwPreviewPixelFormat);
				OnPreviewBitmap(pbyteBitmap, dwBufferSize, dwWidth, dwHeight, dwFrameNo, dwPreviewPixelFormat);
			}
		}
		private void mPreviewGDICallbackFunc(System.IntPtr hDC, uint dwWidth, uint dwHeight, uint dwFrameNo, System.IntPtr lpContext, System.IntPtr lpReserved)
		{
			if (OnRawCallback != null)
			{
				//OnPreviewGDI.Invoke(hDC, dwWidth, dwHeight, dwFrameNo);
				OnPreviewGDI(hDC, dwWidth, dwHeight, dwFrameNo);
			}
		}
		public delegate void fStCamPreviewBitmapCallbackFunc(System.IntPtr pbyteBitmap, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, uint dwPreviewPixelFormat, System.IntPtr lpContext, System.IntPtr lpReserved);
		public delegate void fStCamRawCallbackFunc(System.IntPtr pbyteBuffer, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, ushort wColorArray, uint dwTransferBitsPerPixel, System.IntPtr lpContext, System.IntPtr lpReserved);
		public delegate void fStCamPreviewGDICallbackFunc(System.IntPtr hDC, uint dwWidth, uint dwHeight, uint dwFrameNo, System.IntPtr lpContext, System.IntPtr lpReserved);
		#endregion Callback
		//---------------------------
		private bool mGetSettings()
		{
			bool result = true;

			do
			{
				result = mGetCameraName();
				if(!result) break;

				result = mGetCameraVersion();
				if (!result) break;

				result = mGetImageSize();
				if(!result) break;

				result = mGetSkippingBinning();
				if (!result) break;

				result = mGetDisplayMode();
				if(!result) break;

				result = mGetAspectMode();
				if(!result) break;

				result = mGetPreviewMaskSize();
				if(!result) break;

				result = GetPreviewDestSize();
				if(!result) break;

				result = GetGain();
				if (!result) break;

				result = GetDigitalGain();
				if (!result) break;

				result = GetClock();
				if (!result) break;

				result = GetExposure();
				if (!result) break;

				result = GetALC();
				if (!result) break;

				result = GetWB();
				if (!result) break;

				result = GetGamma();
				if (!result) break;

				result = GetSharpness();
				if (!result) break;

				result = GetHueSaturation();
				if (!result) break;

				result = GetPixelFormat();
				if (!result) break;

				result = GetColorInterpolation();
				if (!result) break;

				result = GetMirrorRotation();
				if (!result) break;

				result = GetColorMatrix();
				if (!result) break;

				result = GetAVIQuality();
				if (!result) break;
			}while(false);
			return(result);
		}

		//---------------------------
		private IntPtr m_hPreviewParentWnd = IntPtr.Zero;
		public bool CreateWindow(IntPtr hWnd)
		{
			bool result = true;
			UInt32 dwStyle = StCam.WS_VISIBLE | StCam.WS_HSCROLL | StCam.WS_VSCROLL;

			if(m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				if(hWnd == IntPtr.Zero)
				{
					//Overlapped Window
					dwStyle |= StCam.WS_OVERLAPPEDWINDOW;
					m_hPreviewParentWnd = IntPtr.Zero;
				}
				else
				{
					//Child Window
					dwStyle |= StCam.WS_CHILDWINDOW;
					m_hPreviewParentWnd = hWnd;
				}

				result =StCam.CreatePreviewWindow(m_hCamera, "Preview", dwStyle,
					m_lPreviewWindowOffsetX, m_lPreviewWindowOffsetY, m_dwPreviewWindowWidth, m_dwPreviewWindowHeight,
					hWnd, IntPtr.Zero, false);
				if (result)
				{
					result = mGetPreviewWindowSize();
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}		
											 
			return(result);
		}

		//---------------------------
		#region Start/StopTransfer
		private bool m_IsTranserStarted = false;
		public bool StartTransfer()
		{
			bool result = true;

			if(m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.StartTransfer(m_hCamera);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				else
				{
					m_IsTranserStarted = true;
				}
			}
			return(result);
		}
		public bool StopTransfer()
		{
			bool result = true;

			if(m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.StopTransfer(m_hCamera);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				else
				{
					m_IsTranserStarted = false;
				}
			}
			return(result);
		}
		#endregion Start/StopTransfer
		//---------------------------
		#region CameraVersion
		private ushort m_wUSBVendorID = 0;
		private ushort m_wUSBProductID = 0;
		private ushort m_wFPGAVersion = 0;
		private ushort m_wFirmVersion = 0;
		private bool mGetCameraVersion()
		{
			bool result = true;

			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetCameraVersion(m_hCamera, out m_wUSBVendorID, out m_wUSBProductID, out m_wFPGAVersion, out m_wFirmVersion);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}

			return (result);
		}
		public bool IsColor
		{
			get
			{
				bool isColor = false;
				switch (m_wUSBProductID)
				{
					case (StCam.STCAM_USBPID_STC_C33USB):
					case (StCam.STCAM_USBPID_STC_C83USB):
					case (StCam.STCAM_USBPID_STC_TC33USB):
					case (StCam.STCAM_USBPID_STC_TC83USB):
					case (StCam.STCAM_USBPID_STC_TC133USB):
					case (StCam.STCAM_USBPID_STC_TC152USB):
					case (StCam.STCAM_USBPID_STC_TC202USB):
					case (StCam.STCAM_USBPID_STC_MC33USB):
					case (StCam.STCAM_USBPID_STC_MC83USB):
					case (StCam.STCAM_USBPID_STC_MC133USB):
					case (StCam.STCAM_USBPID_STC_MC152USB):
					case (StCam.STCAM_USBPID_STC_MC202USB):
					case(StCam.STCAM_USBPID_STC_MCA5MUSB3):
						isColor = true;
						break;
				}
				return (isColor);
			}
		}
		public bool IsMinGain32
		{
			get
			{
				bool value = false;
				switch (m_wUSBProductID)
				{
					case (StCam.STCAM_USBPID_STC_C33USB):
					case (StCam.STCAM_USBPID_STC_B33USB):
					case (StCam.STCAM_USBPID_STC_C83USB):
					case (StCam.STCAM_USBPID_STC_B83USB):
					case (StCam.STCAM_USBPID_STC_TC33USB):
					case (StCam.STCAM_USBPID_STC_TB33USB):
					case (StCam.STCAM_USBPID_STC_TC83USB):
					case (StCam.STCAM_USBPID_STC_TB83USB):
					case (StCam.STCAM_USBPID_STC_TC133USB):
					case (StCam.STCAM_USBPID_STC_TB133USB):
					case (StCam.STCAM_USBPID_STC_TC152USB):
					case (StCam.STCAM_USBPID_STC_TB152USB):
					case (StCam.STCAM_USBPID_STC_TC202USB):
					case (StCam.STCAM_USBPID_STC_TB202USB):
						value = true;
						break;
				}
				return (value);
			}
		}
		public bool IsMSeries
		{
			get
			{
				bool value = false;
				switch (m_wUSBProductID)
				{
					case (StCam.STCAM_USBPID_STC_MC33USB):
					case (StCam.STCAM_USBPID_STC_MB33USB):
					case (StCam.STCAM_USBPID_STC_MC83USB):
					case (StCam.STCAM_USBPID_STC_MB83USB):
					case (StCam.STCAM_USBPID_STC_MC133USB):
					case (StCam.STCAM_USBPID_STC_MB133USB):
					case (StCam.STCAM_USBPID_STC_MC152USB):
					case (StCam.STCAM_USBPID_STC_MB152USB):
					case (StCam.STCAM_USBPID_STC_MC202USB):
					case (StCam.STCAM_USBPID_STC_MB202USB):
						value = true;
						break;
				}
				return (value);
			}
		}
		public bool HasDigitalGain
		{
			get
			{
				bool value = true;
				switch (m_wUSBProductID)
				{
					case (StCam.STCAM_USBPID_STC_B33USB):
					case (StCam.STCAM_USBPID_STC_B83USB):
					case (StCam.STCAM_USBPID_STC_C33USB):
					case (StCam.STCAM_USBPID_STC_C83USB):
						value = true;
						break;
				}
				return (value);
			}
		}
		public bool HasVGA90FPSFunction
		{
			get
			{
				bool value = false;
				switch (m_wUSBProductID)
				{
					case (StCam.STCAM_USBPID_STC_MC33USB):
					case (StCam.STCAM_USBPID_STC_MB33USB):
						value = true;
						break;
				}
				return (value);
			}
		}
		public bool IsUSB30Camera
		{
			get
			{
				bool value = false;
				switch (m_wUSBProductID)
				{
					case (StCam.STCAM_USBPID_STC_MBA5MUSB3):
					case (StCam.STCAM_USBPID_STC_MCA5MUSB3):
						value = true;
						break;
				}
				return (value);
			}
		}
		public bool HasCameraSideALC
		{
			get{return (IsUSB30Camera);}
		}
		public bool HasCameraSideWB
		{
			get { return (IsUSB30Camera); }
		}
		public bool HasCameraSideGamma
		{
			get { return (IsUSB30Camera); }
		}
		public bool HasDigitalClamp
		{
			get { return (IsUSB30Camera); }
		}
		public bool HasStoreCameraSetting
		{
			get { return (IsUSB30Camera); }
		}
		public ushort ProductID
		{
			get { return (m_wUSBProductID); }
		}
		#endregion CameraVersion
		//---------------------------
		#region CameraName
		//Camera ID
		private uint m_nCameraID = 0;
		private string m_strCameraName = "";
		private bool mGetCameraName()
		{
			bool result = true;


			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				uint dwCameraID = 0;
				System.Text.StringBuilder sb = new System.Text.StringBuilder(255);

				result = StCam.ReadCameraUserID(m_hCamera, out dwCameraID, sb, (uint)sb.Capacity);
				if(result)
				{
					m_nCameraID = dwCameraID;
					m_strCameraName = sb.ToString();
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		public string CameraName
		{
			get{return(m_strCameraName);}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					uint len = (uint)System.Text.Encoding.Unicode.GetByteCount(value);
					result = StCam.WriteCameraUserID(m_hCamera, m_nCameraID, value, len);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}

				}
				m_strCameraName = value;
			}
		}
		#endregion CameraName
		//---------------------------
		#region ImageSize
		private ushort m_wEnableScanMode = 0;

		//
		private uint m_dwImageSizeMode = StCam.STCAM_IMAGE_SIZE_MODE_UXGA;
		private ushort m_wScanMode = StCam.STCAM_SCAN_MODE_NORMAL;
		private uint m_dwOrgImageOffsetX = 0;
		private uint m_dwOrgImageOffsetY = 0;
		private uint m_dwOrgImageWidth = 0;
		private uint m_dwOrgImageHeight = 0;
		private bool mGetImageSize()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					uint dwReserved = 0;
					result = StCam.GetEnableImageSize(m_hCamera, out dwReserved, out m_wEnableScanMode);
					if (!result) break;
					result = StCam.GetImageSize(m_hCamera, out m_dwImageSizeMode, out m_wScanMode, out m_dwOrgImageOffsetX, out m_dwOrgImageOffsetY, out m_dwOrgImageWidth, out m_dwOrgImageHeight);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}

			}
			return(result);
		}
		private bool mSetImageSize()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					if (m_IsTranserStarted)
					{
						result = StCam.StopTransfer(m_hCamera);
						if (!result) break;
					}
					result = StCam.SetImageSize(m_hCamera, m_dwImageSizeMode, m_wScanMode, m_dwOrgImageOffsetX, m_dwOrgImageOffsetY, m_dwOrgImageWidth, m_dwOrgImageHeight);
					if (!result) break;

					if (m_IsTranserStarted)
					{
						result = StCam.StartTransfer(m_hCamera);
						if (!result) break;
					}
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				else
				{
					result = mGetSkippingBinning();
					result = mGetImageSize();
					if (OnImageSizeChanged != null)
					{
						OnImageSizeChanged(this, new EventArgs());
					}
				}
			}


			return (result);
		}
		public ushort EnableScanMode
		{
			get { return (m_wEnableScanMode); }
		}
		public uint EnableScanModeCount
		{
			get
			{
				uint count = 1;
				ushort tmp = m_wEnableScanMode;
				while (0 < tmp)
				{
					if ((tmp & 1) != 0)
					{
						count++;
					}
					tmp >>= 1;
				}
				return (count);

			}

		}
		public uint MaxImageWidth
		{
			get
			{
				uint maxWidth = 0;
				uint maxHeight = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					StCam.GetMaximumImageSize(m_hCamera, out maxWidth, out maxHeight);
				}
				return (maxWidth);
			}
		}
		public uint MaxImageHeight
		{
			get
			{
				uint maxWidth = 0;
				uint maxHeight = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					StCam.GetMaximumImageSize(m_hCamera, out maxWidth, out maxHeight);
				}
				return (maxHeight);
			}
		}
		public ushort ScanMode
		{
			get { return (m_wScanMode); }
			set
			{
				m_wScanMode = value;
				mSetImageSize();
			}
		}
		public uint OrgImageWidth
		{
			get { return (m_dwOrgImageWidth); }
			set
			{
				m_dwOrgImageWidth = value;
				mSetImageSize();
			}
		}
		public uint OrgImageHeight
		{
			get { return (m_dwOrgImageHeight); }
			set
			{
				m_dwOrgImageHeight = value;
				mSetImageSize();
			}
		}
		public uint OrgImageOffsetX
		{
			get { return (m_dwOrgImageOffsetX); }
			set
			{
				m_dwOrgImageOffsetX = value;
				mSetImageSize();
			}
		}
		public uint OrgImageOffsetY
		{
			get { return (m_dwOrgImageOffsetY); }
			set
			{
				m_dwOrgImageOffsetY = value;
				mSetImageSize();
			}
		}

		protected byte m_byteHSkipping = 0;
		protected byte m_byteVSkipping = 0;
		protected byte m_byteHBinning = 0;
		protected byte m_byteVBinning = 0;
		private bool mGetSkippingBinning()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetSkippingAndBinning(m_hCamera, out m_byteHSkipping, out m_byteVSkipping, out m_byteHBinning, out m_byteVBinning);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}

			}
			return (result);
		}
		private bool mSetSkippingBinning()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					if (m_IsTranserStarted)
					{
						result = StCam.StopTransfer(m_hCamera);
						if (!result) break;
					}
					result = StCam.SetSkippingAndBinning(m_hCamera, m_byteHSkipping, m_byteVSkipping, m_byteHBinning, m_byteVBinning);
					if (!result) break;

					if (m_IsTranserStarted)
					{
						result = StCam.StartTransfer(m_hCamera);
						if (!result) break;
					}
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				else
				{
					result = mGetSkippingBinning();
					result = mGetImageSize();
					if (OnImageSizeChanged != null)
					{
						OnImageSizeChanged(this, new EventArgs());
					}
				}
			}


			return (result);
		}
		public bool EnableBinningSkipping
		{
			get
			{
				return ((m_wEnableScanMode & StCam.STCAM_SCAN_MODE_AOI) != 0);
			}
		}
		public bool HasHBinningSum
		{
			get
			{
				bool has = false;
				switch (ProductID)
				{
					case (StCam.STCAM_USBPID_STC_MBA5MUSB3):
					case (StCam.STCAM_USBPID_STC_MCA5MUSB3):
					case (StCam.STCAM_USBPID_STC_MBE132U3V):
					case (StCam.STCAM_USBPID_STC_MCE132U3V):
						has = true;
						break;
				}
				return (has);
			}
		}
		public bool HasVBinningSum
		{
			get
			{
				bool has = false;
				switch (ProductID)
				{
					case (StCam.STCAM_USBPID_STC_MBE132U3V):
					case (StCam.STCAM_USBPID_STC_MCE132U3V):
						has = true;
						break;
				}
				return (has);
			}
		}
		public ushort HBinningSkipping
		{
			get
			{
				return ((ushort)((m_byteHBinning << 8) + m_byteHSkipping));
			}
			set
			{
				if (m_hCamera != IntPtr.Zero)
				{
					m_byteHSkipping = (byte)(value & 0xFF);
					m_byteHBinning = (byte)(value >> 8);
					mSetSkippingBinning();
				}
			}
		}
		public ushort VBinningSkipping
		{
			get
			{
				return ((ushort)((m_byteVBinning << 8) + m_byteVSkipping));
			}
			set
			{
				if (m_hCamera != IntPtr.Zero)
				{
					m_byteVSkipping = (byte)(value & 0xFF);
					m_byteVBinning = (byte)(value >> 8);
					mSetSkippingBinning();
				}
			}
		}
		public ushort BinningSumMode
		{
			get
			{
				ushort value = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					bool reval = StCam.GetBinningSumMode(m_hCamera, out value);
				}
				return (value);
			}
			set
			{
				if (m_hCamera != IntPtr.Zero)
				{
					bool reval = StCam.SetBinningSumMode(m_hCamera, value);
				}
			}
		}

		#endregion ImageSize
		//---------------------------
		#region DisplayMode
		//Display Mode
		private byte m_byteDisplayMode = StCam.STCAM_DISPLAY_MODE_GDI;

		private bool mGetDisplayMode()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetDisplayMode(m_hCamera, out m_byteDisplayMode);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}

			}

			return(result);
		}
		private bool mSetDisplayMode(byte byteDisplayMode)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.DestroyPreviewWindow(m_hCamera);
					if(!result) break;

					result = StCam.SetDisplayMode(m_hCamera, byteDisplayMode);
					if(!result) break;

					m_byteDisplayMode = byteDisplayMode;

					result = CreateWindow(m_hPreviewParentWnd);
					if(!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		public byte DisplayMode
		{
			get{return(m_byteDisplayMode);}
			set{mSetDisplayMode(value);}
		}	
		#endregion DisplayMode

		//---------------------------
		#region AspectMode
		//Aspect Mode
		private byte m_byteAspectMode = StCam.STCAM_ASPECT_MODE_FIXED;
		private bool mGetAspectMode()
		{
			bool result = true;
			result = StCam.GetAspectMode(m_hCamera, out m_byteAspectMode);
			return(result);
		}
		private bool mSetAspectMode(byte byteAspectMode)
		{
			bool result = true;
			if(m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.SetAspectMode(m_hCamera, byteAspectMode);
					if (!result) break;
					
					m_byteAspectMode = byteAspectMode;

					result = mGetSettings();
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}

			return(result);
		}
		public byte AspectMode
		{
			get{return(m_byteAspectMode);}
			set{mSetAspectMode(value);}
		}
		#endregion AspectMode

		//---------------------------
		#region PreviewWindowSize
		//Preview Window
		private int m_lPreviewWindowOffsetX = 0;
		private int m_lPreviewWindowOffsetY = 0;
		private uint m_dwPreviewWindowWidth = 0;
		private uint m_dwPreviewWindowHeight = 0;
		private bool mGetPreviewWindowSize()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetPreviewWindowSize(m_hCamera, out m_lPreviewWindowOffsetX, out m_lPreviewWindowOffsetY, out  m_dwPreviewWindowWidth, out m_dwPreviewWindowHeight);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		private bool mSetPreviewWindowSize(int nOffsetX, int nOffsetY, UInt32 dwWidth, UInt32 dwHeight)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.SetPreviewWindowSize(m_hCamera, nOffsetX, nOffsetY, dwWidth, dwHeight);
				if(result)
				{
					m_lPreviewWindowOffsetX = nOffsetX;
					m_lPreviewWindowOffsetY = nOffsetY;
					m_dwPreviewWindowWidth = dwWidth;
					m_dwPreviewWindowHeight = dwHeight;
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}

			}
			return(result);
		}
		public int PreviewWindowOffsetX
		{
			get{return(m_lPreviewWindowOffsetX);}
			set{mSetPreviewWindowSize(value, m_lPreviewWindowOffsetY, m_dwPreviewWindowWidth, m_dwPreviewWindowHeight);}
		}
		public int PreviewWindowOffsetY
		{
			get{return(m_lPreviewWindowOffsetY);}
			set{mSetPreviewWindowSize(m_lPreviewWindowOffsetX, value, m_dwPreviewWindowWidth, m_dwPreviewWindowHeight);}
		}
		public uint PreviewWindowWidth
		{
			get{return(m_dwPreviewWindowWidth);}
			set
			{
				mSetPreviewWindowSize(m_lPreviewWindowOffsetX, m_lPreviewWindowOffsetY, value, m_dwPreviewWindowHeight);
			}
		}
		public uint PreviewWindowHeight
		{
			get{return(m_dwPreviewWindowHeight);}
			set
			{
				mSetPreviewWindowSize(m_lPreviewWindowOffsetX, m_lPreviewWindowOffsetY, m_dwPreviewWindowWidth, value);
			}
		}
		#endregion PreviewWindowSize

		//---------------------------
		#region PreviewMaskSize
		//Preview Mask
		private uint m_dwPreviewMaskOffsetX = 0;
		private uint m_dwPreviewMaskOffsetY = 0;
		private uint m_dwPreviewMaskWidth = 0;
		private uint m_dwPreviewMaskHeight = 0;
		private bool mGetPreviewMaskSize()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetPreviewMaskSize(m_hCamera, out m_dwPreviewMaskOffsetX, out m_dwPreviewMaskOffsetY, out m_dwPreviewMaskWidth, out m_dwPreviewMaskHeight);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		private bool mSetPreviewMaskSize(UInt32 nOffsetX, UInt32 nOffsetY, UInt32 dwWidth, UInt32 dwHeight)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.SetPreviewMaskSize(m_hCamera, nOffsetX, nOffsetY, dwWidth, dwHeight);
				if(result)
				{
					m_dwPreviewMaskOffsetX = nOffsetX;
					m_dwPreviewMaskOffsetY = nOffsetY;
					m_dwPreviewMaskWidth = dwWidth;
					m_dwPreviewMaskHeight = dwHeight;
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		public UInt32 PreviewMaskOffsetX
		{
			get{return(m_dwPreviewMaskOffsetX);}
			set{mSetPreviewMaskSize(value, m_dwPreviewMaskOffsetY, m_dwPreviewMaskWidth, m_dwPreviewMaskHeight);}
		}
		public UInt32 PreviewMaskOffsetY
		{
			get{return(m_dwPreviewMaskOffsetY);}
			set{mSetPreviewMaskSize(m_dwPreviewMaskOffsetX, value, m_dwPreviewMaskWidth, m_dwPreviewMaskHeight);}
		}
		public uint PreviewMaskWidth
		{
			get{return(m_dwPreviewMaskWidth);}
			set{mSetPreviewMaskSize(m_dwPreviewMaskOffsetX, m_dwPreviewMaskOffsetY, value, m_dwPreviewMaskHeight);}
		}
		public uint PreviewMaskHeight
		{
			get{return(m_dwPreviewMaskHeight);}
			set{mSetPreviewMaskSize(m_dwPreviewMaskOffsetX, m_dwPreviewMaskOffsetY, m_dwPreviewMaskWidth, value);}
		}
		#endregion PreviewMaskSize

		//---------------------------
		#region PreviewDestSize
		//Preview Dest
		private uint m_dwPreviewDestOffsetX = 0;
		private uint m_dwPreviewDestOffsetY = 0;
		private uint m_dwPreviewDestWidth = 0;
		private uint m_dwPreviewDestHeight = 0;
		public bool GetPreviewDestSize()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetPreviewDestSize(m_hCamera, out  m_dwPreviewDestOffsetX, out m_dwPreviewDestOffsetY, out m_dwPreviewDestWidth, out m_dwPreviewDestHeight);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		private bool mSetPreviewDestSize(UInt32 nOffsetX, UInt32 nOffsetY, UInt32 dwWidth, UInt32 dwHeight)
		{
			bool result = true;

			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.SetPreviewDestSize(m_hCamera, nOffsetX, nOffsetY, dwWidth, dwHeight);
				if(result)
				{
					m_dwPreviewDestOffsetX = nOffsetX;
					m_dwPreviewDestOffsetY = nOffsetY;
					m_dwPreviewDestWidth = dwWidth;
					m_dwPreviewDestHeight = dwHeight;
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return(result);
		}
		public UInt32 PreviewDestOffsetX
		{
			get{return(m_dwPreviewDestOffsetX);}
			set{mSetPreviewDestSize(value, m_dwPreviewDestOffsetY, m_dwPreviewDestWidth, m_dwPreviewDestHeight);}
		}
		public UInt32 PreviewDestOffsetY
		{
			get{return(m_dwPreviewDestOffsetY);}
			set{mSetPreviewDestSize(m_dwPreviewDestOffsetX, value, m_dwPreviewDestWidth, m_dwPreviewDestHeight);}
		}
		public uint PreviewDestWidth
		{
			get{return(m_dwPreviewDestWidth);}
			set{mSetPreviewDestSize(m_dwPreviewDestOffsetX, m_dwPreviewDestOffsetY, value, m_dwPreviewDestHeight);}
		}
		public uint PreviewDestHeight
		{
			get{return(m_dwPreviewDestHeight);}
			set{mSetPreviewDestSize(m_dwPreviewDestOffsetX, m_dwPreviewDestOffsetY, m_dwPreviewDestWidth, value);}
		}
		#endregion PreviewDestSize
		//---------------------------
		#region Gain
		//Gain
		private ushort m_wGain = 0;
		private bool GetGain()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetGain(m_hCamera, out m_wGain);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public ushort Gain
		{
			get { return (m_wGain); }
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetGain(m_hCamera, value);
					if (result)
					{
						m_wGain = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public ushort MinGain
		{
			get
			{
				if (IsMinGain32)
				{
					return (32);
				}
				else
				{
					return (0);
				}
			}
		}
		public ushort MaxGain
		{
			get
			{
				ushort value = 0;
				StCam.GetMaxGain(m_hCamera, out value);
				return (value);
			}
		}
		public float GainDB
		{
			get { return (GainValueToDB(m_wGain)); }
		}
		public float GainValueToDB(ushort wGain)
		{
			float fValue = 0;
			StCam.GetGainDBFromSettingValue(m_hCamera, wGain, out fValue);
			return (fValue);
		}
		#endregion Gain

		#region DigitalGain
		//DigitalGain
		private ushort m_wDigitalGain = 0;
		private bool GetDigitalGain()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				if (HasDigitalGain)
				{
					result = StCam.GetDigitalGain(m_hCamera, out m_wDigitalGain);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
			return (result);
		}
		public ushort DigitalGain
		{
			get { return (m_wDigitalGain); }
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					if (HasDigitalGain)
					{
						result = StCam.SetDigitalGain(m_hCamera, value);
						if (result)
						{
							m_wDigitalGain = value;
						}
						else
						{
							uint errorNo = GetLastError();
							if (OnError != null)
							{
								OnError(this, errorNo);
							}
						}
					}
				}
			}
		}
		public ushort MinDigitalGain
		{
			get { return (64); }
		}
		public ushort MaxDigitalGain
		{
			get { return (255); }
		}
		public double DigitalGainPower
		{
			get { return ((double)m_wDigitalGain / 64.0); }
		}
		#endregion DigitalGain
		#region Clock
		private uint m_dwEnableClockMode = 0;
		private uint m_dwClockMode = 0;
		private uint m_dwClockFreq = 0;
		private ushort m_wLinesPerFrame = 0;
		private ushort m_wClocksPerLine = 0;
		public uint EnableClockMode
		{
			get { return (m_dwEnableClockMode); }
		}
		public uint EnableClockModeCount
		{
			get
			{
				uint count = 1;
				uint tmp = m_dwEnableClockMode;
				while (0 < tmp)
				{
					if ((tmp & 1) != 0)
					{
						count++;
					}
					tmp >>= 1;
				}
				return (count);

			}

		}
		public uint ClockMode
		{
			get { return (m_dwClockMode); }
			set
			{
				bool result = true;
				do
				{
					result = StCam.SetClock(m_hCamera, value, m_dwClockFreq);
					if (!result) break;

					result = StCam.GetClock(m_hCamera, out m_dwClockMode, out m_dwClockFreq);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
		}
		private bool GetClock()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					uint dwStandardClock = 0;
					uint dwMinimumClock = 0;
					uint dwmaximumClock = 0;
					result = StCam.GetEnableClock(m_hCamera, out m_dwEnableClockMode, out dwStandardClock, out dwMinimumClock, out dwmaximumClock);
					if (!result) break;

					result = StCam.GetClock(m_hCamera, out m_dwClockMode, out m_dwClockFreq);
					if (!result) break;

					result = StCam.GetFrameClock(m_hCamera, out m_wLinesPerFrame, out m_wClocksPerLine);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}

			}
			return (result);
		}
		public float OutputFPS
		{
			get
			{
				float fFPS = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					bool result = StCam.GetOutputFPS(m_hCamera, out fFPS);
				}
				return (fFPS);
			}
		}
		public bool EnableVBlaningSetting
		{
			get
			{
				return (0 < MaxVBlankingSetting);
			}
		}
		public uint MaxVBlankingSetting
		{
			get
			{
				uint value = 0;
				StCam.GetMaxVBlankForFPS(m_hCamera, out value);
				return (value);
			}
		}
		public uint VBlankingForFPS
		{
			get
			{
				uint value = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					bool result = StCam.GetVBlankForFPS(m_hCamera, out value);
				}
				return (value);
			}
			set
			{
				if (EnableVBlaningSetting)
				{
					if (m_hCamera != IntPtr.Zero)
					{
						bool result = StCam.SetVBlankForFPS(m_hCamera, value);
					}
				}
			}

		}
		#endregion Clock
		#region Shutter
		private uint m_dwExposureClock = 0;

		private bool GetExposure()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetExposureClock(m_hCamera, out m_dwExposureClock);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public uint MaxShortExposureClock
		{
			get
			{
				bool result = true;
				uint dwMaxExposureClock = 0;
				result = StCam.GetMaxShortExposureClock(m_hCamera, out dwMaxExposureClock);
				return (dwMaxExposureClock);
			}
		}
		public uint MaxLongExposureClock
		{
			get
			{
				bool result = true;
				uint dwMaxExposureClock = 0;
				result = StCam.GetMaxLongExposureClock(m_hCamera, out dwMaxExposureClock);
				return (dwMaxExposureClock);
			}
		}
		public uint MaxExposureClock
		{
			get
			{
				uint dwMaxExposureClock = 0;
				switch (m_wScanMode)
				{
					case (StCam.STCAM_SCAN_MODE_PARTIAL_1):
					case (StCam.STCAM_SCAN_MODE_PARTIAL_2):
					case (StCam.STCAM_SCAN_MODE_PARTIAL_4):
					case (StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_1):
					case (StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_2):
					case (StCam.STCAM_SCAN_MODE_BINNING_PARTIAL_4):
					case (StCam.STCAM_SCAN_MODE_AOI):
						dwMaxExposureClock = MaxLongExposureClock;
						break;
					default:
						dwMaxExposureClock = MaxShortExposureClock;
						break;
				}
				return (dwMaxExposureClock); 
			}
		}
		public float MaxExposureTime
		{
			get
			{
				float fExpTime = 0;
				bool result = StCam.GetExposureTimeFromClock(m_hCamera, MaxExposureClock, out fExpTime);
				return (fExpTime);
			}
		}
		public float ExposureTime
		{
			get
			{
				float fExpTime = 0;
				bool result = StCam.GetExposureTimeFromClock(m_hCamera, m_dwExposureClock, out fExpTime);
				return (fExpTime);
			}
			set
			{
				uint dwExposureClock = 0;
				bool result = StCam.GetExposureClockFromTime(m_hCamera, value, out dwExposureClock);
				ExposureClock = dwExposureClock;
			}
		}
		public uint ExposureClock
		{
			get
			{
				return (m_dwExposureClock);
			
			}
			set
			{

				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetExposureClock(m_hCamera, value);
					if (result)
					{
						m_dwExposureClock = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}

		#endregion Shutter

		public bool ReadAutoControlSettingValue()
		{
			bool result = true;

			do
			{
				result = ReadALCSettingValue();
				if (!result) break;

				result = ReadAWBSettingValue();
			} while (false);
			return (result);
		}
		private bool ReadALCSettingValue()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				//ALC
				switch (m_byteALCMode)
				{
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_AGC_ONESHOT):
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ONESHOT_AGC_OFF):
					case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ONESHOT):
						result = StCam.GetALCMode(m_hCamera, out m_byteALCMode);
						break;
				}
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
				do
				{
					result = GetGain();
					if (!result) break;
					result = GetExposure();
					if (!result) break;
				} while (false);
			}
			return (result);
		}

		
		#region ALC
		//ALC
		private byte m_byteALCMode = StCam.STCAM_ALCMODE_ALC_FIXED_AGC_OFF;
		private byte m_byteALCTarget = 128;
		private byte m_byteALCTolerance = 10;
		private byte m_byteALCThreshold = 10;
		private byte[] m_pbyteALCWeigth = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		private ushort[] m_pwControlAreaX = new ushort[] { 2500, 5000, 7500 };
		private ushort[] m_pwControlAreaY = new ushort[] { 2500, 5000, 7500 };
		private uint m_dwAEMinExposureClock = 1;
		private uint m_dwAEMaxExposureClock = 0;

		private ushort m_wAGCMinGain = 0;
		private ushort m_wAGCMaxGain = 255;
		public bool GetALC()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetALCMode(m_hCamera, out m_byteALCMode);
					if (!result) break;

					result = StCam.GetTargetBrightness(m_hCamera, out m_byteALCTarget, out m_byteALCTolerance, out m_byteALCThreshold);
					if (!result) break;

					result = StCam.GetALCWeight(m_hCamera, m_pbyteALCWeigth);
					if (!result) break;

					result = StCam.GetControlArea(m_hCamera, m_pwControlAreaX, m_pwControlAreaY);
					if (!result) break;

					result = StCam.GetAEMaxExposureClock(m_hCamera, out m_dwAEMaxExposureClock);
					if (!result) break;

					result = StCam.GetAEMinExposureClock(m_hCamera, out m_dwAEMinExposureClock);
					if (!result) break;

					result = StCam.GetGainControlRange(m_hCamera, out m_wAGCMinGain, out m_wAGCMaxGain);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public byte ALCMode
		{
			get
			{
				return (m_byteALCMode);
			}
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						result = ReadALCSettingValue();
						if (!result) break;

						result = StCam.SetALCMode(m_hCamera, value);
						if (!result) break;
					} while (false);
					if (result)
					{
						m_byteALCMode = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}

				}
			}
		}
		public bool IsAGCOn
		{
			get
			{
				bool auto = false;
				switch (m_byteALCMode)
				{
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON): 
					case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON):
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_AGC_ONESHOT): 
					case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ONESHOT):
					case (StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON):
					case (StCam.STCAM_ALCMODE_CAMERA_AGC_ON):
						auto = true;
						break;
				}
				return(auto);
			}
			set
			{
				byte alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF;
				if (HasCameraSideALC)
				{
					switch (m_byteALCMode)
					{
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF):
						case (StCam.STCAM_ALCMODE_CAMERA_AGC_ON):
							if (value) alcMode = StCam.STCAM_ALCMODE_CAMERA_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF;
							break;
						case (StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON):
						case (StCam.STCAM_ALCMODE_CAMERA_AE_ON):
							if (value) alcMode = StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_CAMERA_AE_ON;
							break;
					}
				}
				else
				{
					switch (m_byteALCMode)
					{
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF):
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON):
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ONESHOT):
							if (value) alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF;
							break;
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON):
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF):
							if (value) alcMode = StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF;
							break;
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_AGC_ONESHOT):
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ONESHOT_AGC_OFF):
							if (value) alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_AUTO_SHUTTER_ONESHOT_AGC_OFF;
							break;
					}
				}
				ALCMode = alcMode;
			}
		}
		public bool IsAEEOn
		{
			get
			{
				bool auto = false;
				switch (m_byteALCMode)
				{
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON): 
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF): 
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_AGC_ONESHOT): 
					case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ONESHOT_AGC_OFF):
					case (StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON):
					case (StCam.STCAM_ALCMODE_CAMERA_AE_ON):
						auto = true;
						break;
				}
				return(auto);
			}
			set
			{
				byte alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF;
				if (HasCameraSideALC)
				{
					switch (m_byteALCMode)
					{
						case (StCam.STCAM_ALCMODE_CAMERA_AE_ON):
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF):
							if (value) alcMode = StCam.STCAM_ALCMODE_CAMERA_AE_ON;
							else alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF;
							break;
						case (StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON):
						case (StCam.STCAM_ALCMODE_CAMERA_AGC_ON):
							if (value) alcMode = StCam.STCAM_ALCMODE_CAMERA_AE_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_CAMERA_AGC_ON;
							break;
					}
				}
				else
				{
					switch (m_byteALCMode)
					{
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF):
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF):
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ONESHOT_AGC_OFF):
							if (value) alcMode = StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF;
							else alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_OFF;
							break;
						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON):
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON):
							if (value) alcMode = StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_ON;
							else alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ON;
							break;

						case (StCam.STCAM_ALCMODE_AUTO_SHUTTER_AGC_ONESHOT):
						case (StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ONESHOT):
							if (value) alcMode = StCam.STCAM_ALCMODE_AUTO_SHUTTER_ON_AGC_OFF;
							else alcMode = StCam.STCAM_ALCMODE_FIXED_SHUTTER_AGC_ONESHOT;
							break;
					}
				}
				ALCMode = alcMode;
			}
		}
		public byte ALCTarget
		{
			get { return (m_byteALCTarget); }
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetTargetBrightness(m_hCamera, value, m_byteALCTolerance, m_byteALCThreshold);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
					else
					{
						m_byteALCTarget = value;
					}
				}
			}
		}
		public byte ALCTolerance
		{
			get { return (m_byteALCTolerance); }
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetTargetBrightness(m_hCamera, m_byteALCTarget, value, m_byteALCThreshold);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
					else
					{
						m_byteALCTolerance = value;
					}
				}
			}
		}
		public byte ALCThreshold
		{
			get { return (m_byteALCThreshold); }
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetTargetBrightness(m_hCamera, m_byteALCTarget, m_byteALCTolerance, value);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
					else
					{
						m_byteALCThreshold = value;
					}
				}
			}
		}


		public uint AEMinExposureClock
		{
			get
			{
				uint value = m_dwAEMinExposureClock;
				if (value == 0)
				{
					value = MaxShortExposureClock;
				}
				else if (MaxExposureClock < value)
				{
					value = MaxExposureClock;
				}
				return (value);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					if (MaxShortExposureClock == value)
					{
						value = 0;
					}
					else if(MaxLongExposureClock < value)
					{
						value = MaxLongExposureClock;
					}

					result = StCam.SetAEMinExposureClock(m_hCamera, value);
					if (result)
					{
						m_dwAEMinExposureClock = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public uint AEMaxExposureClock
		{
			get
			{
				uint value = m_dwAEMaxExposureClock;
				if (value == 0)
				{
					value = MaxShortExposureClock;
				}
				else if (MaxExposureClock < value)
				{
					value = MaxExposureClock;
				}
				return (value);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					if (MaxShortExposureClock == value)
					{
						value = 0;
					}
					else if (MaxLongExposureClock < value)
					{
						value = MaxLongExposureClock;
					}

					result = StCam.SetAEMaxExposureClock(m_hCamera, value);
					if (result)
					{
						m_dwAEMaxExposureClock = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public uint[] AEExposureClockRange
		{
			get
			{
				return (new uint[] { m_dwAEMinExposureClock, m_dwAEMaxExposureClock });
			}
			set
			{
				if (m_hCamera != IntPtr.Zero)
				{
					AEMinExposureClock = value[0];
					AEMaxExposureClock = value[1];
				}

			}
		}

		public float AEEMinExposureTime
		{
			get
			{
				float fExpTime = 0;
				bool result = StCam.GetExposureTimeFromClock(m_hCamera, m_dwAEMinExposureClock, out fExpTime);
				return (fExpTime);
			}
			set
			{
				uint dwExposureClock = 0;
				bool result = StCam.GetExposureClockFromTime(m_hCamera, value, out dwExposureClock);
				AEMinExposureClock = dwExposureClock;
			}
		}
		public float AEEMaxExposureTime
		{
			get
			{
				float fExpTime = 0;
				bool result = StCam.GetExposureTimeFromClock(m_hCamera, m_dwAEMaxExposureClock, out fExpTime);
				return (fExpTime);
			}
			set
			{
				uint dwExposureClock = 0;
				bool result = StCam.GetExposureClockFromTime(m_hCamera, value, out dwExposureClock);
				AEMaxExposureClock = dwExposureClock;
			}
		}

		public ushort AGCMinGain
		{

			get
			{
				return (m_wAGCMinGain);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetGainControlRange(m_hCamera, value, m_wAGCMaxGain);
					if (result)
					{
						m_wAGCMinGain = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public ushort AGCMaxGain
		{

			get
			{
				return (m_wAGCMaxGain);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetGainControlRange(m_hCamera, m_wAGCMinGain, value);
					if (result)
					{
						m_wAGCMaxGain = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public ushort[] AGCGainRange
		{
			get
			{
				return (new ushort[] { m_wAGCMinGain, m_wAGCMaxGain });
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetGainControlRange(m_hCamera, value[0], value[1]);
					if (result)
					{
						m_wAGCMinGain = value[0];
						m_wAGCMaxGain = value[1];
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}

			}
		}


		public float AGCMinGainDB
		{
			get { return (GainValueToDB(m_wAGCMinGain)); }
		}
		public float AGCMaxGainDB
		{
			get { return (GainValueToDB(m_wAGCMaxGain)); }
		}

		public byte[] ALCWeight
		{
			get
			{
				return (m_pbyteALCWeigth);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetALCWeight(m_hCamera, value);
					if (result)
					{
						m_pbyteALCWeigth = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}

			}
		}

		public ushort[] ControlAreaX
		{
			get
			{
				return (m_pwControlAreaX);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetControlArea(m_hCamera, value, m_pwControlAreaY);
					if (result)
					{
						m_pwControlAreaX = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}

		public ushort[] ControlAreaY
		{
			get
			{
				return (m_pwControlAreaY);
			}
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = StCam.SetControlArea(m_hCamera, m_pwControlAreaX, value);
					if (result)
					{
						m_pwControlAreaY = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}

		#endregion ALC

		//---------------------------
		#region WB
		private byte m_byteWBMode = StCam.STCAM_WB_OFF;
		private ushort[] m_pwWBGain = new ushort[] {128, 128, 128, 128};
		private ushort[] m_pwWBTarget = new ushort[] {100, 100};
		private bool ReadAWBSettingValue()
		{
			bool result = true;
			if(m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do{
					if(m_byteWBMode == StCam.STCAM_WB_ONESHOT)
					{
						result = StCam.GetWhiteBalanceMode(m_hCamera, out m_byteWBMode);
						if(!result) break;
					}

					result = StCam.GetWhiteBalanceGain(m_hCamera, out m_pwWBGain[0], out m_pwWBGain[1], out m_pwWBGain[2], out m_pwWBGain[3]);
				}while(false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		private bool GetWB()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetWhiteBalanceMode(m_hCamera, out m_byteWBMode);
					if (!result) break;

					result = StCam.GetWhiteBalanceGain(m_hCamera, out m_pwWBGain[0], out m_pwWBGain[1], out m_pwWBGain[2], out m_pwWBGain[3]);
					if (!result) break;

					result = StCam.GetWhiteBalanceTarget(m_hCamera, out m_pwWBTarget[0], out m_pwWBTarget[1]);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public byte WBMode
		{
			get
			{
				return (m_byteWBMode);
			}
			set
			{
				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						result = ReadAWBSettingValue();
						if (!result) break;

						result = StCam.SetWhiteBalanceMode(m_hCamera, value);
						if (!result) break;
					} while (false);
					if (result)
					{
						m_byteWBMode = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}

				}
			}
		}
		public bool IsAWBOn
		{
			get { return ((m_byteWBMode == StCam.STCAM_WB_FULLAUTO) || (m_byteWBMode == StCam.STCAM_WB_ONESHOT)); }
			set { WBMode = value?StCam.STCAM_WB_FULLAUTO:StCam.STCAM_WB_MANUAL; }
		}
		public ushort WBGainR
		{
			get { return (m_pwWBGain[0]); }
			set { mSetWBGain(0, value); }
		}
		public ushort WBGainGr
		{
			get { return (m_pwWBGain[1]); }
			set { mSetWBGain(1, value); }
		}
		public ushort WBGainGb
		{
			get { return (m_pwWBGain[2]); }
			set { mSetWBGain(2, value); }
		}
		public ushort WBGainB
		{
			get { return (m_pwWBGain[3]); }
			set { mSetWBGain(3, value); }
		}
		private void mSetWBGain(int index, ushort value)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				m_pwWBGain[index] = value;

				result = StCam.SetWhiteBalanceGain(m_hCamera, m_pwWBGain[0], m_pwWBGain[1], m_pwWBGain[2], m_pwWBGain[3]);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
		}

		public ushort WBTargetR
		{
			get { return (m_pwWBTarget[0]); }
			set { mSetWBTarget(0, value); }
		}

		public ushort WBTargetB
		{
			get { return (m_pwWBTarget[1]); }
			set { mSetWBTarget(1, value); }
		}
		private void mSetWBTarget(int index, ushort value)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				m_pwWBTarget[index] = value;

				result = StCam.SetWhiteBalanceTarget(m_hCamera, m_pwWBTarget[0], m_pwWBTarget[1]);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
		}


		#endregion WB
		#region Gamma
		private byte[] m_pbyteGammaMode = new byte[]{
			StCam.STCAM_GAMMA_OFF, StCam.STCAM_GAMMA_OFF, StCam.STCAM_GAMMA_OFF, 
			StCam.STCAM_GAMMA_OFF, StCam.STCAM_GAMMA_OFF};
		private ushort[] m_pwGammaValue = new ushort[] { 100, 100, 100, 100, 100 };
		private short[] m_pshtGammaBrightness = new short[] { 0, 0, 0, 0, 0 };
		private byte[] m_pbyteGammaContrast = new byte[] { 0, 0, 0, 0, 0 };
		private byte[][] m_ppbyteGammaTable = new byte[][]{
			new byte[256], new byte[256], new byte[256], new byte[256], new byte[256]};

		private bool GetGamma()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				for (byte byteGammaTarget = StCam.STCAM_GAMMA_TARGET_Y; byteGammaTarget <= StCam.STCAM_GAMMA_TARGET_B; byteGammaTarget++)
				{
					result = StCam.GetGammaModeEx(m_hCamera, byteGammaTarget, out m_pbyteGammaMode[byteGammaTarget],
										out m_pwGammaValue[byteGammaTarget], out m_pshtGammaBrightness[byteGammaTarget],
										out m_pbyteGammaContrast[byteGammaTarget], m_ppbyteGammaTable[byteGammaTarget]);
					if (!result) break;
				}
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}

		public byte GammaYMode
		{
			get{return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_Y]);}
			set{mSetGammaMode(StCam.STCAM_GAMMA_TARGET_Y, value);}
		}
		public byte GammaRMode
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_R]); }
			set { mSetGammaMode(StCam.STCAM_GAMMA_TARGET_R, value); }
		}
		public byte GammaGrMode
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_GR]); }
			set { mSetGammaMode(StCam.STCAM_GAMMA_TARGET_GR, value); }
		}
		public byte GammaGbMode
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_GB]); }
			set { mSetGammaMode(StCam.STCAM_GAMMA_TARGET_GB, value); }
		}
		public byte GammaBMode
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_B]); }
			set { mSetGammaMode(StCam.STCAM_GAMMA_TARGET_B, value); }
		}
		public bool IsGammaYOn
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_Y] != StCam.STCAM_GAMMA_OFF); }
			set	{GammaYMode = value?StCam.STCAM_GAMMA_ON:StCam.STCAM_GAMMA_OFF;	}
		}
		public bool IsGammaROn
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_R] != StCam.STCAM_GAMMA_OFF); }
			set { GammaRMode = value ? StCam.STCAM_GAMMA_ON : StCam.STCAM_GAMMA_OFF; }
		}
		public bool IsGammaGrOn
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_GR] != StCam.STCAM_GAMMA_OFF); }
			set { GammaGrMode = value ? StCam.STCAM_GAMMA_ON : StCam.STCAM_GAMMA_OFF; }
		}
		public bool IsGammaGbOn
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_GB] != StCam.STCAM_GAMMA_OFF); }
			set { GammaGbMode = value ? StCam.STCAM_GAMMA_ON : StCam.STCAM_GAMMA_OFF; }
		}
		public bool IsGammaBOn
		{
			get { return (m_pbyteGammaMode[StCam.STCAM_GAMMA_TARGET_B] != StCam.STCAM_GAMMA_OFF); }
			set { GammaBMode = value ? StCam.STCAM_GAMMA_ON : StCam.STCAM_GAMMA_OFF; }
		}
		private bool mSetGammaMode(byte byteGammaTarget, byte byteGammaMode)
		{
			bool result = true;

			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{

				result = StCam.SetGammaModeEx(m_hCamera, byteGammaTarget, byteGammaMode,
											m_pwGammaValue[byteGammaTarget], m_pshtGammaBrightness[byteGammaTarget],
											m_pbyteGammaContrast[byteGammaTarget], m_ppbyteGammaTable[byteGammaTarget]);

				if (result)
				{
					m_pbyteGammaMode[byteGammaTarget] = byteGammaMode;
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public ushort GammaYValue
		{
			get { return (m_pwGammaValue[StCam.STCAM_GAMMA_TARGET_Y]); }
			set{mSetGammaValue(StCam.STCAM_GAMMA_TARGET_Y, value);}
		}
		public ushort GammaRValue
		{
			get { return (m_pwGammaValue[StCam.STCAM_GAMMA_TARGET_R]); }
			set { mSetGammaValue(StCam.STCAM_GAMMA_TARGET_R, value); }
		}
		public ushort GammaGrValue
		{
			get { return (m_pwGammaValue[StCam.STCAM_GAMMA_TARGET_GR]); }
			set { mSetGammaValue(StCam.STCAM_GAMMA_TARGET_GR, value); }
		}
		public ushort GammaGbValue
		{
			get { return (m_pwGammaValue[StCam.STCAM_GAMMA_TARGET_GB]); }
			set { mSetGammaValue(StCam.STCAM_GAMMA_TARGET_GB, value); }
		}
		public ushort GammaBValue
		{
			get { return (m_pwGammaValue[StCam.STCAM_GAMMA_TARGET_B]); }
			set { mSetGammaValue(StCam.STCAM_GAMMA_TARGET_B, value); }
		}
		private bool mSetGammaValue(byte byteGammaTarget, ushort byteGammaValue)
		{
			bool result = true;

			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{

				result = StCam.SetGammaModeEx(m_hCamera, byteGammaTarget, m_pbyteGammaMode[byteGammaTarget],
											byteGammaValue, m_pshtGammaBrightness[byteGammaTarget],
											m_pbyteGammaContrast[byteGammaTarget], m_ppbyteGammaTable[byteGammaTarget]);

				if (result)
				{
					m_pwGammaValue[byteGammaTarget] = byteGammaValue;
				}
				else
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public ushort CameraGamma
		{
			get
			{
				ushort value = 0;
				if (HasCameraSideGamma)
				{
					if (m_hCamera != IntPtr.Zero)
					{
						bool result = StCam.GetCameraGammaValue(m_hCamera, out value);
					}
				}
				return (value);
			}
			set
			{
				if (HasCameraSideGamma)
				{
					if (m_hCamera != IntPtr.Zero)
					{
						bool result = StCam.SetCameraGammaValue(m_hCamera, value);
					}
				}
			}
		}

		#endregion Gamma
		#region Clamp
		public ushort DigitalClamp
		{
			get
			{
				ushort value = 0;
				if (HasDigitalClamp)
				{
					if (m_hCamera != IntPtr.Zero)
					{
						bool result = StCam.GetDigitalClamp(m_hCamera, out value);
					}
				}
				return (value);
			}
			set
			{
				if (HasDigitalClamp)
				{
					if (m_hCamera != IntPtr.Zero)
					{
						bool result = StCam.SetDigitalClamp(m_hCamera, value);
					}
				}
			}
		}
		#endregion //Clamp

		#region Sharpness
		private byte m_byteSharpnessMode = StCam.STCAM_SHARPNESS_OFF;
		private ushort m_wSharpnessGain = 0;
		private byte m_byteSharpnessCoring = 0;
		public bool GetSharpness()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetSharpnessMode(m_hCamera, out m_byteSharpnessMode, out m_wSharpnessGain, out m_byteSharpnessCoring);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public bool IsSharpnessOn
		{
			get { return (m_byteSharpnessMode != StCam.STCAM_SHARPNESS_OFF); }
			set { SharpnessMode = value ? StCam.STCAM_SHARPNESS_ON : StCam.STCAM_SHARPNESS_OFF; }
		}
		public byte SharpnessMode
		{
			get { return (m_byteSharpnessMode); }
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{

					result = StCam.SetSharpnessMode(m_hCamera, value, m_wSharpnessGain, m_byteSharpnessCoring);

					if (result)
					{
						m_byteSharpnessMode = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public ushort SharpnessGain
		{
			get { return (m_wSharpnessGain); }
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{

					result = StCam.SetSharpnessMode(m_hCamera, m_byteSharpnessMode, value, m_byteSharpnessCoring);

					if (result)
					{
						m_wSharpnessGain = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public byte SharpnessCoring
		{
			get { return (m_byteSharpnessCoring); }
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{

					result = StCam.SetSharpnessMode(m_hCamera, m_byteSharpnessMode, m_wSharpnessGain, value);

					if (result)
					{
						m_byteSharpnessCoring = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		#endregion Sharpness
		#region Hue/Saturation
		private byte m_byteHueSaturationMode = StCam.STCAM_HUE_SATURATION_OFF;
		private short m_shtHue = 0;
		private ushort m_wSaturation = 100;
		public bool GetHueSaturation()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetHueSaturationMode(m_hCamera, out m_byteHueSaturationMode, out m_shtHue, out m_wSaturation);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public bool IsHueSaturationOn
		{
			get { return (m_byteHueSaturationMode != StCam.STCAM_HUE_SATURATION_OFF); }
			set { HueSaturationMode = value ? StCam.STCAM_HUE_SATURATION_ON : StCam.STCAM_HUE_SATURATION_OFF; }
		}
		public byte HueSaturationMode
		{
			get { return (m_byteHueSaturationMode); }
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{

					result = StCam.SetHueSaturationMode(m_hCamera, value, m_shtHue, m_wSaturation);

					if (result)
					{
						m_byteHueSaturationMode = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public short Hue
		{
			get { return (m_shtHue); }
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{

					result = StCam.SetHueSaturationMode(m_hCamera, m_byteHueSaturationMode, value, m_wSaturation);

					if (result)
					{
						m_shtHue = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public ushort Saturation
		{
			get { return (m_wSaturation); }
			set
			{
				bool result = true;

				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{

					result = StCam.SetHueSaturationMode(m_hCamera, m_byteHueSaturationMode, m_shtHue, value);

					if (result)
					{
						m_wSaturation = value;
					}
					else
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}


		#endregion Hue/Saturation
		#region PixelFormat
		private uint m_dwEnablePixelFormat = 0;
		private uint m_dwPixelFormat = StCam.STCAM_PIXEL_FORMAT_24_BGR;
		public bool GetPixelFormat()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetEnablePreviewPixelFormat(m_hCamera, out m_dwEnablePixelFormat);
					if (!result) break;

					result = StCam.GetPreviewPixelFormat(m_hCamera, out m_dwPixelFormat);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public uint EnablePixelFormat
		{
			get { return (m_dwEnablePixelFormat); }
		}
		public uint EnablePixelFormatCount
		{
			get
			{
				uint count = 0;
				uint tmp = m_dwEnablePixelFormat;
				while (0 < tmp)
				{
					if ((tmp & 1) != 0)
					{
						count++;
					}
					tmp >>= 1;
				}
				return (count);
			}
		}
		public uint PixelFormat
		{
			get { return (m_dwPixelFormat); }
			set
			{

				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						if (m_IsTranserStarted)
						{
							result = StCam.StopTransfer(m_hCamera);
							if (!result) break;
						}

						result = StCam.SetPreviewPixelFormat(m_hCamera, value);
						if (!result) break;

						m_dwPixelFormat = value;

						if (m_IsTranserStarted)
						{
							result = StCam.StartTransfer(m_hCamera);
							if (!result) break;
						}
					} while (false);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
					else
					{
						result = mGetImageSize();
					}
				}
			}
		}
		#endregion PixelFormat
		#region ColorInterpolation
		private byte m_byteColorInterpolation = StCam.STCAM_COLOR_INTERPOLATION_BILINEAR;
		public bool GetColorInterpolation()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetColorInterpolationMethod(m_hCamera, out m_byteColorInterpolation);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public byte ColorInterpolation
		{
			get { return (m_byteColorInterpolation); }
			set
			{

				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						result = StCam.SetColorInterpolationMethod(m_hCamera, value);
						if (!result) break;

						m_byteColorInterpolation = value;
					} while (false);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		#endregion ColorInterpolation


		#region MirrorRotation
		private byte m_byteEnableMirrroMode = 0;
		private byte m_byteMirrorMode = StCam.STCAM_MIRROR_OFF;
		private byte m_byteRotationMode = StCam.STCAM_ROTATION_OFF;
		public bool GetMirrorRotation()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetEnableMirrorMode(m_hCamera, out m_byteEnableMirrroMode);
					if (!result) break;
					result = StCam.GetMirrorMode(m_hCamera, out m_byteMirrorMode);
					if (!result) break;
					result = StCam.GetRotationMode(m_hCamera, out m_byteRotationMode);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}

		public byte Mirror
		{
			get { return (m_byteMirrorMode); }
			set
			{

				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						if (m_IsTranserStarted)
						{
							result = StCam.StopTransfer(m_hCamera);
							if (!result) break;
						}

						result = StCam.SetMirrorMode(m_hCamera, value);
						if (!result) break;

						m_byteMirrorMode = value;

						if (m_IsTranserStarted)
						{
							result = StCam.StartTransfer(m_hCamera);
							if (!result) break;
						}
					} while (false);
					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}

		public byte Rotation
		{
			get { return (m_byteRotationMode); }
			set
			{

				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						result = StCam.SetRotationMode(m_hCamera, value);
						if (!result) break;

						m_byteRotationMode = value;
					} while (false);

					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
					else
					{
						if (OnImageSizeChanged != null)
						{
							OnImageSizeChanged(this, new EventArgs());
						}
					}
				}
			}
		}
		public byte EnableMirrorMode
		{
			get{return(m_byteEnableMirrroMode);}
		}
		public uint EnableMirrorModeCount
		{
			get
			{
				uint count = 1;
				byte tmp = m_byteEnableMirrroMode;
				if ((tmp & StCam.STCAM_MIRROR_HORIZONTAL_VERTICAL) == StCam.STCAM_MIRROR_HORIZONTAL_VERTICAL)
				{
					count++;
				}
				if ((tmp & (StCam.STCAM_MIRROR_HORIZONTAL_CAMERA | StCam.STCAM_MIRROR_VERTICAL_CAMERA)) == (StCam.STCAM_MIRROR_HORIZONTAL_CAMERA | StCam.STCAM_MIRROR_VERTICAL_CAMERA))
				{
					count++;
				}
				while (0 < tmp)
				{
					if ((tmp & 1) != 0)
					{
						count++;
					}
					tmp >>= 1;
				}
				
				return (count);

			}

		}

		#endregion MirrorRotation

		#region ColorMatrix
		private short[] m_pshtColorMatrix = new short[] { 100, 0, 0, 0, 0, 100, 0, 0, 0, 0, 100, 0 };
		public bool GetColorMatrix()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					byte byteColorMatrixMode = StCam.STCAM_COLOR_MATRIX_OFF;
					result = StCam.GetColorMatrix(m_hCamera, out byteColorMatrixMode, m_pshtColorMatrix);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public short[] ColorMatrix
		{
			get { return (m_pshtColorMatrix); }
			set
			{

				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						byte byteColorMatrixMode = StCam.STCAM_COLOR_MATRIX_OFF;

						for (int i = 0; i < 12; i++)
						{
							if ((i == 0) || (i == 5) || (i == 10))
							{
								if (value[i] != 100)
								{
									byteColorMatrixMode = StCam.STCAM_COLOR_MATRIX_CUSTOM;
									break;
								}
							}
							else
							{
								if (value[i] != 0)
								{
									byteColorMatrixMode = StCam.STCAM_COLOR_MATRIX_CUSTOM;
									break;
								}
							}
						}

						result = StCam.SetColorMatrix(m_hCamera, byteColorMatrixMode, value);
						if (!result) break;

						m_pshtColorMatrix = value;
					} while (false);

					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}

		}

		#endregion ColorMatrix
		#region AVI
		private uint m_dwAVIFileFormat = StCam.STCAM_AVI_FILE_FORMAT_AVI1;
		public uint AVIFileFormat
		{
			get { return (m_dwAVIFileFormat); }
			set { m_dwAVIFileFormat = value; }
		}
		private uint m_dwAVICompressor = StCam.STCAM_AVI_COMPRESSOR_MJPG;
		public uint AVICompressor
		{
			get
			{
				uint max = AVIMaxLength;
				if (max < m_dwAVILength)
				{
					m_dwAVILength = max;
				}
				return (m_dwAVICompressor); 
			}
			set
			{
				m_dwAVICompressor = value;
				if ((value != StCam.STCAM_AVI_COMPRESSOR_UNCOMPRESSED) && (value != StCam.STCAM_AVI_COMPRESSOR_MJPG))
				{
					m_dwAVIFileFormat = StCam.STCAM_AVI_FILE_FORMAT_AVI1;
				}
			}
		}
		private uint m_dwAVILength = 300;
		public uint AVILength
		{
			get {
				uint max = AVIMaxLength;
				if (max < m_dwAVILength)
				{
					m_dwAVILength = max;
				}
	
				return (m_dwAVILength);
			
			}
			set { m_dwAVILength = value; }
		}
		private uint m_dwAVIQuality = 75;
		private bool GetAVIQuality()
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.GetAVIQuality(m_hCamera, out m_dwAVIQuality);
					if (!result) break;
				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public uint AVIQuality
		{
			get { return (m_dwAVIQuality); }
			set
			{

				bool result = true;
				if (m_hCamera == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					do
					{
						result = StCam.SetAVIQuality(m_hCamera, value);
						if (!result) break;

						m_dwAVIQuality = value;
					} while (false);

					if (!result)
					{
						uint errorNo = GetLastError();
						if (OnError != null)
						{
							OnError(this, errorNo);
						}
					}
				}
			}
		}
		public bool SaveAVI(string fileName)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					result = StCam.SetAVIPriorityFileFormat(m_hCamera, m_dwAVIFileFormat);
					if (!result) break;

					result = StCam.SaveAVI(m_hCamera, fileName, m_dwAVICompressor, m_dwAVILength, IntPtr.Zero);
					if (!result) break;

				} while (false);

				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}
		public bool IsMP42FileExist
		{
			get
			{
				string fileName = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\mpg4c32.dll";
				return (System.IO.File.Exists(fileName));
			}
		}
		public uint FrameDataSize
		{
			get
			{
				uint frameDataSize = m_dwOrgImageWidth * m_dwOrgImageHeight;
				switch (m_dwPixelFormat)
				{
					case (StCam.STCAM_PIXEL_FORMAT_24_BGR):
						frameDataSize *= 3;
						break;
					case (StCam.STCAM_PIXEL_FORMAT_32_BGR):
						frameDataSize *= 4;
						break;
				}
				return (frameDataSize);
			}
		}
		public ulong AVIFileSize
		{
			get
			{
				ulong fileSize = FrameDataSize + 24;
				fileSize *= m_dwAVILength;
				fileSize += 2056 + 512;
				fileSize &= ~(ulong)0x1FF;
				
				return (fileSize);
			}
		}
		public uint AVIMaxLength
		{

			get
			{
				uint maxLength = 0;
				if (m_dwAVICompressor == StCam.STCAM_AVI_COMPRESSOR_UNCOMPRESSED)
				{
					if (m_dwAVIFileFormat != StCam.STCAM_AVI_FILE_FORMAT_AVI2)
					{
						maxLength = (0x80000000 - 2056 - 512) / (FrameDataSize + 24);
					}
					else
					{
						UInt64 ulTmp = (0x10000000000 - 2056 - 512) / (UInt64)(FrameDataSize + 24);
						maxLength = (uint.MaxValue < ulTmp) ? uint.MaxValue : (uint)ulTmp;
					}
				}
				else
				{
					const double dblLimit = 30.0 * 60.0;	//30.0[min]
					uint maxShutterClock = MaxExposureClock;
					if(0 < maxShutterClock)
					{
						maxLength = (uint)(dblLimit * m_dwClockFreq / maxShutterClock);
					}
				}
				return (maxLength);
			}
		}
		public double AVIFileTime
		{
			get
			{
				double second = 0.0;
				if (0 < m_dwClockFreq)
				{
					second = (double)m_dwAVILength * (double)MaxExposureClock / (double)m_dwClockFreq;
				}
				return (second);
			}
		}
		#endregion AVI
		#region DefectPixelCorrection
		public ushort EnableDefectPixelCorrectionCount
		{
			get
			{
				ushort value = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					bool result = StCam.GetEnableDefectPixelCorrectionCount(m_hCamera, out value);
				}
				return (value);
			}
		}
		public ushort DefectPixelCorrectionMode
		{
			set
			{
				if (m_hCamera != IntPtr.Zero)
				{
					bool result = StCam.SetDefectPixelCorrectionMode(m_hCamera, value);
				}
			}
			get
			{
				ushort value = 0;
				if (m_hCamera != IntPtr.Zero)
				{
					bool result = StCam.GetDefectPixelCorrectionMode(m_hCamera, out value);
				}
				return (value);
			}
		}
		public bool SetDefectPixelCorrectionPosition(ushort wIndex, uint x, uint y)
		{
			bool result = false;
			if (m_hCamera != IntPtr.Zero)
			{
				result = StCam.SetDefectPixelCorrectionPosition(m_hCamera, wIndex, x, y);
			}
			return (result);
		}
		public bool GetDefectPixelCorrectionPosition(ushort wIndex, out uint x, out uint y)
		{
			bool result = false;
			x = 0xFFF;
			y = 0xFFF;
			if (m_hCamera != IntPtr.Zero)
			{
				result = StCam.GetDefectPixelCorrectionPosition(m_hCamera, wIndex, out x, out y);
			}
			return (result);
		}
		public bool DetectDefectPixel(ushort wThreshold)
		{
			bool result = true;
			do
			{
				DefectPixelCorrectionMode = StCam.STCAM_DEFECT_PIXEL_CORRECTION_OFF;
				Mirror = StCam.STCAM_MIRROR_OFF;
				ScanMode = StCam.STCAM_SCAN_MODE_NORMAL;

				//
				MessageBox.Show("Please click on the OK button in the state, protected from light.");

				byte[] pbyteRaw = new byte[OrgImageWidth * OrgImageHeight];

				System.Runtime.InteropServices.GCHandle gch = System.Runtime.InteropServices.GCHandle.Alloc(pbyteRaw, System.Runtime.InteropServices.GCHandleType.Pinned);
				do
				{
					System.IntPtr pvTmpRaw = gch.AddrOfPinnedObject();
					uint dwNumberOfByteTrans = 0;
					uint[] pdwFrameNo = new uint[] { 0 };
					result = StCam.TakeRawSnapShot(m_hCamera, pvTmpRaw, (uint)pbyteRaw.GetLength(0), out dwNumberOfByteTrans, pdwFrameNo, 1000);
					if (!result) break;

					//detect
					result = StCam.DetectDefectPixel(m_hCamera, OrgImageWidth, OrgImageHeight, pvTmpRaw, wThreshold);
					if (!result) break;
				} while (false);
				gch.Free();
				if (!result) break;

			} while (false);
			return (result);
		}
		#endregion DefectPixelCorrection
		#region PreviewSnapShot
		public bool TakePreviewSnapShot(out Bitmap bitmap, out uint frameNo)
		{
			bool result = true;
			bitmap = null;
			frameNo = 0;
			int nWidth = (int)m_dwOrgImageWidth;
			int nHeight = (int)m_dwOrgImageHeight;

			if (m_byteRotationMode != StCam.STCAM_ROTATION_OFF)
			{
				nWidth = (int)m_dwOrgImageHeight;
				nHeight = (int)m_dwOrgImageWidth;
			}
			do
			{
				System.Drawing.Imaging.PixelFormat pixelFormat = System.Drawing.Imaging.PixelFormat.Format8bppIndexed;
				switch (m_dwPixelFormat)
				{
					case (StCam.STCAM_PIXEL_FORMAT_08_MONO_OR_RAW):
						break;
					case (StCam.STCAM_PIXEL_FORMAT_24_BGR):
						pixelFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
						break;
					case (StCam.STCAM_PIXEL_FORMAT_32_BGR):
						pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppRgb;
						break;
				}

				bitmap = new Bitmap(nWidth, nHeight, pixelFormat);
				if (bitmap == null)
				{
					result = false;
					break;
				}

				if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
				{
					System.Drawing.Imaging.ColorPalette colorPalette = bitmap.Palette;
					
					for (int i = 0; i <= 255; i++)
					{
						colorPalette.Entries[i] = Color.FromArgb(i, i, i);
					}
					bitmap.Palette = colorPalette;
				}
				
				uint frameDataSize = FrameDataSize;
				uint dwTransferredSize = 0;
				uint[] pdwFrameNo = new uint[1]{0};
				uint timeout = 5000;
				System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, nWidth, nHeight), System.Drawing.Imaging.ImageLockMode.WriteOnly, pixelFormat);
				result = StCam.TakePreviewSnapShot(m_hCamera, bitmapData.Scan0, frameDataSize, out dwTransferredSize, pdwFrameNo, timeout);
				bitmap.UnlockBits(bitmapData);
				frameNo = pdwFrameNo[0];

			} while (false);
			return (result);
		}

		public delegate void SnapShotEvent(object sender, SnapShotEventArgs e);
		public event SnapShotEvent OnSnapShot = null;

		public bool SnapShot()
		{
			bool result = true;
			if (OnSnapShot != null)
			{

				Bitmap bitmap = null;
				uint frameNo = 0;
				result = TakePreviewSnapShot(out bitmap, out frameNo);
				if (result)
				{
					OnSnapShot(this, new SnapShotEventArgs(bitmap, frameNo));
				}


			}
			return (result);
		}

		#endregion PreviewSnapShot
		//---------------------------
		public bool SetRcvMsgWnd(IntPtr hWnd)
		{
			bool result = true;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.SetReceiveMsgWindow(m_hCamera, hWnd);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}
			return (result);
		}

		//---------------------------
		public bool AdjustWindowSize(Form frm, bool bResetPosition, int nOffsetX, int nOffsetY, int nAddWidth, int nAddHeight)
		{
			bool reval = true;

			int nTargetClientWidth = (int)m_dwPreviewDestOffsetX + (int)m_dwPreviewDestWidth + nOffsetX + nAddWidth;
			int nTargetClientHeight = (int)m_dwPreviewDestOffsetY + (int)m_dwPreviewDestHeight + nOffsetY + nAddHeight;

			System.Drawing.Rectangle rectDesktop = Screen.GetWorkingArea(frm);


			System.Drawing.Rectangle rectWnd = frm.Bounds;
			System.Drawing.Rectangle rectClient = frm.ClientRectangle;

			rectWnd.Width += nTargetClientWidth - rectClient.Width;
			rectWnd.Height += nTargetClientHeight - rectClient.Height;

			if (rectDesktop.Width < rectWnd.Width)
			{
				rectWnd.Width -= rectWnd.Width - rectDesktop.Width;
			}

			if (rectDesktop.Height < rectWnd.Height)
			{
				rectWnd.Height -= rectWnd.Height - rectDesktop.Height;
			}

			frm.Bounds = rectWnd;

			return (reval);
		}
		//---------------------------
		private int m_PreviewSizeShiftCount = 0;
		public int PreviewSizeShiftCount
		{
			get { return (m_PreviewSizeShiftCount); }
			set
			{
				m_PreviewSizeShiftCount = value;
				bSetPreviewSize();
			}
		}
		public bool bSetPreviewSize()
		{
			bool reval = true;
			int nSiftCount = m_PreviewSizeShiftCount;
			int nOrgWidth = 0;
			int nOrgHeight = 0;

			if (StCam.STCAM_ROTATION_OFF != m_byteRotationMode)
			{
				nOrgWidth = (int)m_dwOrgImageHeight;
				nOrgHeight = (int)m_dwOrgImageWidth;
			}
			else
			{
				nOrgHeight = (int)m_dwOrgImageHeight;
				nOrgWidth = (int)m_dwOrgImageWidth;
			}

			int nWidth = nOrgWidth;
			int nHeight = nOrgHeight;

			if (nSiftCount < 0)
			{
				nWidth >>= -nSiftCount;
				nHeight >>= -nSiftCount;
			}
			else if (0 < nSiftCount)
			{
				nWidth <<= nSiftCount;
				nHeight <<= nSiftCount;
			}

			do
			{
				AspectMode = StCam.STCAM_ASPECT_MODE_CUSTOM;

				PreviewMaskWidth = (uint)nOrgWidth;
				PreviewMaskHeight = (uint)nOrgHeight;

				PreviewDestOffsetX = 0;
				PreviewDestOffsetY = 0;
				PreviewDestWidth = (uint)nWidth;
				PreviewDestHeight = (uint)nHeight;

			} while (false);
			return (reval);
		}
		public Point GetImagePos(Point wndPt)
		{
			bool result = true;
			Point imagePt = new Point(wndPt.X, wndPt.Y);
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				do
				{
					IntPtr previewWnd = IntPtr.Zero;

					if (m_hCamera == IntPtr.Zero) break;

					result = StCam.GetPreviewWnd(m_hCamera, out previewWnd);
					if (!result) break;

					if (previewWnd == IntPtr.Zero) break;

					imagePt.Offset(
						Native.GetScrollPos(previewWnd, Native.SB_HORZ) - (int)m_dwPreviewDestOffsetX,
						Native.GetScrollPos(previewWnd, Native.SB_VERT) - (int)m_dwPreviewDestOffsetY);

					if (0 < m_PreviewSizeShiftCount)
					{
						imagePt.X >>= m_PreviewSizeShiftCount;
						imagePt.Y >>= m_PreviewSizeShiftCount;
					}
					else if (m_PreviewSizeShiftCount < 0)
					{
						imagePt.X <<= -m_PreviewSizeShiftCount;
						imagePt.Y <<= -m_PreviewSizeShiftCount;
					}

				} while (false);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}
			}

			return (imagePt);
		}
		public System.Drawing.Color GetImageColor(Point imagePt)
		{
			bool result = true;
			uint dwColor = 0;
			if (m_hCamera == IntPtr.Zero)
			{
				result = false;
			}
			else
			{
				result = StCam.GetPreviewImagePixelValue(m_hCamera, (uint)imagePt.X, (uint)imagePt.Y, out dwColor);
				if (!result)
				{
					uint errorNo = GetLastError();
					if (OnError != null)
					{
						OnError(this, errorNo);
					}
				}

			}
			System.Drawing.Color color = Color.FromArgb((int)(dwColor & 0xFF), (int)((dwColor >> 8) & 0xFF), (int)((dwColor >> 16) & 0xFF));
			return (color);
		}

		

		#region Error
		public uint GetLastError()
		{
			uint error = 0;
			if (m_hCamera != IntPtr.Zero)
			{
				error = StCam.GetLastError(m_hCamera);
			}
			return (error);

		}

		public delegate void ErrorEvent(object sender, uint errorNo);
		public event ErrorEvent OnError = null;
		#endregion Error

	}
	public class SnapShotEventArgs : EventArgs
	{
		public SnapShotEventArgs(Bitmap bitmap, uint frameNo)
		{
			m_Bitmap = bitmap;
			m_dwFrameNo = frameNo;
		}
		private Bitmap m_Bitmap;
		private uint m_dwFrameNo;
		public Bitmap Bitmap
		{
			get { return (m_Bitmap); }
		}
		public uint FrameNo
		{
			get { return (m_dwFrameNo); }
		}
	}
}
