using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class ReceiverInspiradoNoCometa : ReceiverBase
    {
        const int INSTALLED_CHAN = 8;
        private bool _is_running;

        internal void getInstalledChan(out int emgInstalledChanNum)
        {
            emgInstalledChanNum = INSTALLED_CHAN;
        }

        internal void configure(double[] emgChanEnableVect)
        {
            int i = 1;
        }

        internal void activate()
        {
            int i = 1;
        }

        internal void run()
        {
            _is_running = true;
        }

        internal bool isRunning()
        {
            return _is_running;
        }

        internal void transferData(out long firstSample, out double[] data)
        {
            firstSample = 1;
            data = new double[INSTALLED_CHAN];
        }

        internal void stop()
        {
            _is_running = false;
        }
    }
}
