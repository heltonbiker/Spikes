
namespace Miotec.Vert3d.DomainModel
{
    public class Marcadores
    {

        // REFACTOR: esses nomes estão "crípticos",
        // devem ser substituídos por nomes mais claros e informativos:
        // C7, S1(ou S2??), EIPSE, EIPSD (??).

        public PontoEstereometria VP { get; set; }
        public PontoEstereometria SP { get; set; }
        public PontoEstereometria DL { get; set; }
        public PontoEstereometria DR { get; set; }

    }
}
