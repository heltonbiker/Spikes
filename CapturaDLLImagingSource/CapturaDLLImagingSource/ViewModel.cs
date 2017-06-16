using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CapturaDLLImagingSource
{
    public class ViewModel : ViewModelBase {

        public Bitmap UltimoFrameCamera {
            get { return _ultimo_frame_camera; }
            set {
                _ultimo_frame_camera = value;
                RaisePropertyChanged(() => UltimoFrameCamera);
            }
        }
        Bitmap _ultimo_frame_camera;

        // CONSTRUTOR
        public ViewModel() {
            var service = new CameraServiceTIS();

            service.NovoFrame += new NovoFrameEventHandler(service_NovoFrame);

            service.Start();
        }




        void service_NovoFrame(object sender, NovoFrameArgs e) {
            UltimoFrameCamera = e.Frame;
        }

    }
}
