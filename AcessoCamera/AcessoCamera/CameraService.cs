using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;
using System.Drawing;

namespace AcessoCamera
{
    public class CameraService {

        VideoCaptureDevice _video_source;

        public void Start()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                _video_source = new VideoCaptureDevice(videoDevices[0].MonikerString);

                _video_source.NewFrame += new NewFrameEventHandler(NewFrameHandler);

                _video_source.Start();
            }
        }


        public event NovoFrameEventHandler NovoFrame;

        private void NewFrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            var bmp = (Bitmap)eventArgs.Frame.Clone();
            OnNovoFrame(new NovoFrameArgs(bmp));
        }

        protected void OnNovoFrame(NovoFrameArgs e)
        {
            if (NovoFrame != null)
                NovoFrame(this, e);
        }

        public void Stop()
        {
            if (_video_source != null)
            {
                _video_source.Stop();
            }
        }

        public bool isRunning()
        {
            if (_video_source != null)
            {
                return _video_source.IsRunning;
            }
            return false;
        }

    }

    public delegate void NovoFrameEventHandler(object sender, NovoFrameArgs e);

    public class NovoFrameArgs : EventArgs {

        Bitmap _frame;

        public NovoFrameArgs(Bitmap fr)
        {
            this._frame = fr;
        }

        public Bitmap Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }
    }
}
