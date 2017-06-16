using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JogoTenis
{
    public class Jogador
    {
        private String name;
        private String actualScore;

        public String Name
        {
            get
            {
                return name;
            }
        }
        public String ActualScore
        {
            get
            { 
                return actualScore;
            }
            set 
            {
                actualScore = value;
            }
        }

        public Jogador(String name)
        {
            this.name = name;
            actualScore = "0";
        }

        public String MarcarPonto(Jogador jogador1, Jogador jogador2)
        {
           

            bool jaMudou = false;

            if(actualScore.Equals("0") && jaMudou == false)
            {
                actualScore = "15";
                jaMudou = true;
            }


            if (actualScore.Equals("15") && jaMudou == false)
            {
                actualScore = "30";
                jaMudou = true;
            }


            if (actualScore.Equals("30") && jaMudou == false)
            {
                actualScore = "40";
                jaMudou = true;
            }


            if (jogador2.ActualScore.Equals("Deuce") && jogador1.ActualScore != "Advantage "+ jogador1.Name && jaMudou == false)
            {
                jogador2.actualScore = "Advantage " + jogador2.Name;
                actualScore = "Advantage " + jogador2.Name;
                jaMudou = true;
            }

            if (jogador1.ActualScore.Equals("Deuce") && jogador2.ActualScore != "Advantage " + jogador2.Name && jaMudou == false)
            {
                jogador1.actualScore = "Advantage " + jogador1.Name;
                actualScore = "Advantage " + jogador1.Name;
                jaMudou = true;
            }

                
            if (jogador1.ActualScore.Equals("Advantage "+ jogador1.Name) && jaMudou == false)
            {
                jogador1.actualScore = "Ganhou!!";
                actualScore = "Ganhou!!";
                jaMudou = true;
            }
            if (jogador2.ActualScore.Equals("Advantage " + jogador2.Name) && jaMudou == false)
            {
                jogador2.actualScore = "Ganhou!!";
                actualScore = "Ganhou!!";
                jaMudou = true;
            }

            if (jogador1.ActualScore.Equals("40"))
            {
                if (jogador2.ActualScore.Equals("40"))
                {
                    Deuce(jogador1, jogador2);
                }
            }
            if (jogador1.ActualScore.Equals("40") && jogador2.ActualScore != "40" && jaMudou == false)
            {
                jogador1.actualScore = "Ganhou!!";
                actualScore = "Ganhou!!";
                jaMudou = true;
            }
            if (jogador2.actualScore.Equals("40") && jogador2.ActualScore != "40" && jaMudou == false)
            {
                jogador2.actualScore = "Ganhou!!";
                actualScore = "Ganhou!!";
                jaMudou = true;
            }



            return actualScore;
        }

        public bool Deuce(Jogador jogador1, Jogador jogador2)
        {
            jogador1.actualScore = "Deuce";
            jogador2.actualScore = "Deuce";
            return true;
        }

    }
}
