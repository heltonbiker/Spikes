using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class Receiver : ReceiverBase
    {
        
        List<SensorWireless> _sensores;

        public void CarregarSensores()
        {
            _sensores = new List<SensorWireless>() { new SensorWireless() };
        }
        

        public void IniciarColeta()
        {
            CarregarSensores();
            foreach (var sensor in _sensores)
            { 
                sensor.IniciarTransmissão();
            }
        }


        internal List<Pacote> ReadData()
        {
            var result = new List<Pacote>();
            foreach (var sensor in _sensores)
            {
                Pacote newSensorData = sensor.ReadData();
                result.Add(newSensorData);
            }
            return result;
        }

        internal void PararColeta()
        {
            throw new NotImplementedException();
        }
    }
}
