using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    /// <summary>
    /// Responsável por pegar segmentos dos sinais e multiplexá-los para envio serializado.
    /// </summary>
    public class Pacote
    {
        public List<CanalInfo> Canais { get; set; }
    }
}
