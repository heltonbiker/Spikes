using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIS.Imaging;
using System.Drawing;

namespace CapturaDLLImagingSource
{
    public class CameraServiceTIS {

        ICImagingControl _camControl;

        private TIS.Imaging.VCDHelpers.VCDSimpleProperty VCDProp; 


        public int maxGain { get { return VCDProp.RangeMax(VCDIDs.VCDID_Gain) - 20; } }
        public int minGain { get { return VCDProp.RangeMin(VCDIDs.VCDID_Gain) + 20; } }
        public int maxBrightness { get { return VCDProp.RangeMax(VCDIDs.VCDID_Gamma); } } //máximo de brilho
        public int minBrightness { get { return VCDProp.RangeMin(VCDIDs.VCDID_Gamma); } } //mínimo de brilho

        public int defaultGain { get { return 500; } }
        public int defaultBrightness { get { return 51; } } // Regula o brilho.

        public int Gain
        {
            get
            {
                return VCDProp.RangeValue[VCDIDs.VCDID_Gain];
            }
            set
            {
                if (value < VCDProp.RangeMax(VCDIDs.VCDID_Gain) && value > VCDProp.RangeMin(VCDIDs.VCDID_Gain))
                    VCDProp.RangeValue[VCDIDs.VCDID_Gain] = value;
            }
        }
        // CONSTRUTOR
        public CameraServiceTIS() {
            _camControl = new ICImagingControl("ISB3200208905");
            _camControl.Device = _camControl.Devices[0];
            _camControl.LiveCaptureContinuous = true;
            _camControl.LiveDisplay = false;
            _camControl.DeviceFrameRate = 10F; // Frame rate, o máximo no mac mini é 10F
            _camControl.ImageRingBufferSize = 1;

            VCDProp = TIS.Imaging.VCDHelpers.VCDSimpleModule.GetSimplePropertyContainer(_camControl.VCDPropertyItems);

            VCDProp.RangeValue[VCDIDs.VCDID_Exposure] = -7; // Exposure

            VCDProp.RangeValue[VCDIDs.VCDID_Gain] = defaultGain;
            VCDProp.RangeValue[VCDIDs.VCDID_Gamma] = defaultBrightness;

            _camControl.ImageAvailable += new EventHandler<ICImagingControl.ImageAvailableEventArgs>(NovoFrameEventHandler);
        }

        void NovoFrameEventHandler(object sender, ICImagingControl.ImageAvailableEventArgs e) {
            Bitmap bmp = e.ImageBuffer.Bitmap;
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            OnNovoFrame(this, new NovoFrameArgs(bmp));
        }



        public void Start() {
            _camControl.LiveStart();
        }


        public void Stop() {
            throw new NotImplementedException();
        }


        public bool IsRunning() {
            throw new NotImplementedException();
            return true;
        }






        public event NovoFrameEventHandler NovoFrame;

        protected virtual void OnNovoFrame (object sender, NovoFrameArgs e) {
            if (NovoFrame != null) {
                NovoFrame(this, e);
            }
        }

    }


    public delegate void NovoFrameEventHandler(object sender, NovoFrameArgs e);

    public class NovoFrameArgs : EventArgs {

        public Bitmap Frame { get; private set; }

        public NovoFrameArgs(Bitmap fr) {
            Frame = fr;
        }
    }

}
