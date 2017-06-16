using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TIS.Imaging;

namespace IntegracaoColetaVVM.Model
{
    class CameraService2
    {

        ICImagingControl _camControl;

        // CONSTRUTOR
        public CameraService2()
        {
            _camControl = new ICImagingControl("ISB3200208905");
            _camControl.Device = _camControl.Devices[0];
            _camControl.LiveCaptureContinuous = true;
            _camControl.LiveDisplay = false;
            _camControl.DeviceFrameRate = 100F;
            _camControl.ImageRingBufferSize = 1;

            _camControl.ImageAvailable += new EventHandler<ICImagingControl.ImageAvailableEventArgs>(NovoFrameEventHandler);
        }

        void NovoFrameEventHandler(object sender, ICImagingControl.ImageAvailableEventArgs e)
        {
            Bitmap bmp = e.ImageBuffer.Bitmap;
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            OnNovoFrame(this, new NovoFrameArgs(bmp));
        }



        public void Start()
        {
            _camControl.LiveStart();
        }


        public void Stop()
        {
            _camControl.LiveStop();
        }


        public bool isRunning()
        {
            if (_camControl.LivePause)
                return true;
            return false;
        }






        public event NovoFrameEventHandler NovoFrame;

        protected virtual void OnNovoFrame(object sender, NovoFrameArgs e)
        {
            if (NovoFrame != null)
            {
                NovoFrame(this, e);
            }
        }

    }




}