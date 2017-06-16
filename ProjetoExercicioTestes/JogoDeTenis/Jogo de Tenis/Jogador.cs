using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryParaTestar.Jogo_de_Tenis
{
    public class Jogador
    {
        private String nome;
        private String scoreActual;

        public Jogador(String nome)
        {
            this.nome = nome;

        }

        public String MarcarPonto()
        {
            return scoreActual;
        }
    }
}
