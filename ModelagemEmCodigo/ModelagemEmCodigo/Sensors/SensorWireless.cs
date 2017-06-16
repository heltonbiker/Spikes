using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class SensorWireless //: IConfigurableSensor
    {

        public WirelessSensorStatus Status { get; private set; }


        // CONSTRUTOR
        public SensorWireless()
        {
            Status = WirelessSensorStatus.StandBy;
        }




        # region Coleta Etc.

        public Stack<Pacote> Buffer;

        public void IniciarTransmissão()
        {
            if (Status == WirelessSensorStatus.Ativado)
                Coletar();
            Status = WirelessSensorStatus.Transmitindo;
        }


        public void PararTransmissão()
        {
            Status = WirelessSensorStatus.StandBy;
        }

        public void PausarTransmissão()
        {
            Status = WirelessSensorStatus.Ativado;
        }

        public void RetomarTransmissão()
        {
            Status = WirelessSensorStatus.Transmitindo;
        }



        public bool TemAmostrasAtrasadas()
        {
            return Buffer.Count > 0;
        }



        void Coletar()
        {
            while (Status == WirelessSensorStatus.Transmitindo)
            {
                Buffer.Push(ReadData());
            }
        }


        void Bufferizar()
        {
            throw new NotImplementedException();
        }

        void Digitalizar()
        {
            throw new NotImplementedException();
        }

        void Transduzir()
        {
            throw new NotImplementedException();
        }

        void Transmitir()
        {
            throw new NotImplementedException();
        }



        internal Pacote ReadData()
        {
            return new Pacote();
        }

        # endregion



        # region Configuração

        int NumeroModosTransmissão { get; set; }

        IEnumerable<PacoteInfo> ModosTransmissão { get; set; }


        public bool CarregandoBateria()
        {
            return false;
        }

        public byte CargaBateria { get; set; }

        public event EventHandler CargaBateriaConcluída;


        public byte IntensidadeSinalWireless { get; set; }

        # endregion





        # region Instrumentação / Suporte

        String NumeroSerial { get; set; }


        # endregion

















    }
}
