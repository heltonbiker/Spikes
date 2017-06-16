using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IntegracaoColetaVVM.Model
{
    public delegate void NovoFrameEventHandler(object sender, NovoFrameArgs e);

    public class NovoFrameArgs : EventArgs
    {

        public Bitmap Frame { get; private set; }

        public NovoFrameArgs(Bitmap fr)
        {
            Frame = fr;
        }

    }

}
