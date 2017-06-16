using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class PC
    {        
        public void RealizaColetaComCometa()
        {
            var device = new ReceiverInspiradoNoCometa();

            int emgInstalledChanNum;

            device.getInstalledChan(out emgInstalledChanNum);

            double[] emgChanEnableVect = new double[emgInstalledChanNum];


            // desabilita todos os canais, menos o 1
            for (int i = 0; i < emgInstalledChanNum; i++)
            {
                emgChanEnableVect[i] = 0;
            }
            emgChanEnableVect[1] = 1;


            device.configure(emgChanEnableVect);

            device.activate();

            device.run();

            while (device.isRunning())
            {
                long firstSample;
                double[] data;

                device.transferData(out firstSample, out data);
            }

            device.stop();


        }
        
        
        
        public void RealizarColetaCompleta()
        {
            IniciarColeta();
            var receiver = GetReceiver();
            receiver.IniciarColeta();

            foreach (var interval in Enumerable.Range(0, 10))
            {
                List<Pacote> newdata = receiver.ReadData();
            }

            receiver.PararColeta();
        }
        

        
        public void IniciarColeta()
        {
            var i = 1;
        }

        public Receiver GetReceiver()
        {
            return new Receiver();
        }
    }
}
