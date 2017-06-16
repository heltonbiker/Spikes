using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miotec.Vert3d.DomainModel
{
    
    // REFACTOR: Esta classe pode perfeitamente encapsular o padrão de projeção,
    // suas regras e seus métodos, aceitando os ajustes do código cliente (início e fim),
    // gerando a projeção propriamente dita (ConstruirProjecao())
    // e armazenando os parâmetros para uso futuro

    // TODO: definir melhor a classe Projecao e o ProcedimentoColeta, pois
    // a idéia é de que a projeção seja ajustável pelo usuário.
    // Em especial, ONDE É QUE FICA vivendo esta classe durante a coleta?
    // (diria que é na TelaColetaViewModel)



    /// <summary>
    /// Descreve os ângulos de cada franja conforme projetada durante a coleta,
    /// bem como o índice da franja principal.
    /// </summary>
    public class Projecao {

        private int _indice_franja_principal;

        public Projecao() {
                _indice_franja_principal = 49;
        }

        public List<Double> Angulos {
            get {
                return new List<double> {
                    -0.248779358033,
                    -0.240409363585,
                    -0.232004879467,
                    -0.22356688657,
                    -0.215096392708,
                    -0.206594432123,
                    -0.198062064944,
                    -0.189500376605,
                    -0.1809104772,
                    -0.172293500802,
                    -0.163650604721,
                    -0.154982968729,
                    -0.146291794222,
                    -0.137578303356,
                    -0.128843738117,
                    -0.120089359368,
                    -0.111316445842,
                    -0.102526293102,
                    -0.0937202124601,
                    -0.0848995298623,
                    -0.0760655847429,
                    -0.0672197288439,
                    -0.0583633250088,
                    -0.0494977459497,
                    -0.040624372992,
                    -0.0317445947985,
                    -0.022859806077,
                    -0.0139714062726,
                    -0.00508079825004,
                    0.003810613033,
                    0.0127014218571,
                    0.0215902230741,
                    0.030475613439,
                    0.0393561929371,
                    0.0482305661038,
                    0.0570973433329,
                    0.0659551421725,
                    0.0748025886026,
                    0.0836383182938,
                    0.0924609778436,
                    0.101269225987,
                    0.11006173478,
                    0.11883719075,
                    0.127594296017,
                    0.136331769381,
                    0.145048347363,
                    0.15374278522,
                    0.162413857906,
                    0.171060361004,
                    0.181524952972,
                    0.191949500955,
                    0.200502924029,
                    0.209026713273,
                    0.217519790421,
                    0.225981102682,
                    0.234409623311,
                    0.24280435214,
                    0.251164316051,
                    0.2594885694,
                    0.267776194398,
                    0.276026301436,
                    0.284238029365,
                    0.29241054573,
                    0.300543046955,
                    0.308634758478,
                    0.316684934853,
                    0.324692859793,
                    0.332657846185,
                    0.34057923605,
                    0.348456400475,
                    0.3562887395,
                    0.364075681966,
                    0.371816685337,
                    0.379511235475,
                    0.387158846397,
                    0.394759059987,
                    0.402311445692,
                    0.409815600179,
                    0.417271146978,
                    0.424677736092,
                    0.432035043587,
                    0.439342771166,
                    0.446600645716,
                    0.453808418847,
                    0.460965866405,
                    0.468072787978,
                    0.475129006389,
                    0.482134367175,
                    0.489088738055,
                    0.495992008395,
                    0.502844088659,
                    0.509644909863,
                    0.516394423015,
                    0.523092598557,
                    0.529739425806,
                    0.536334912388,
                    0.542879083679,
                    0.549371982242,
                    0.555813667267,
                };
            }
        }

        /// <summary>
        /// Índice onde se encontra a franja principal.
        /// </summary>
        public int IndiceFranjaPrincipal {
            get { return _indice_franja_principal; }
        }

    }
}
