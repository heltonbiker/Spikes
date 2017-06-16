using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPStoPDF
{
    public class AnaliseModel {

        public string Nome { get { return "Vinicius Seganfredo" ; } }
        public string Avaliador { get { return "Helton Moraes" ; } }
        public DateTime DataRealizacao { get { return DateTime.Now; } }
        public double Idade { get { return 19; } }
        public double Peso { get {return 95 ;} }
        public double Altura { get { return 1.82; } }
        public String Genero {get {return "Masculino" ;} }

        public List<Tuple<string, string, string, string, string, string>> ListaTuple {
            get {
                var lt = new List<Tuple<string,string,string,string,string,string>>();
                Random random = new Random();
                for (int i = 0; i < 20; i++)
                {
                    if (i == 0)
                    {
                        lt.Add(Tuple.Create("C7", String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '), String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' ')));                    
                    }
                    else if (i > 0 && i < 13)
                    {
                        lt.Add(Tuple.Create("T" + i ,String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '), String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' ')));
                    }
                    else if (i > 12 && i < 18)
                    {
                        lt.Add(Tuple.Create("L" +(i-12),String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '),String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '),String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '),String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '),String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' ')));

                    }
                    else if (i > 18)
                    {
                        lt.Add(Tuple.Create("S1", String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '), String.Format("{0:0.00 cm}", random.NextDouble() * 100).PadLeft(9,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' '), String.Format("{0:0.00 º}", random.NextDouble() * 100).PadLeft(8,' ')));
                    }
                }   
               
                return lt;
            }
        }


    }
}
