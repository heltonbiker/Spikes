using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

namespace RelatorioWpf
{
    public class RelatorioViewModel : ViewModelBase
    {
        public String Nome { get { return "Vinicius"; } }
        public String CPF { get { return "03634022094"; } }
        public String Solicitante { get { return "Dr. Fulano"; } }
        public int Idade{ get { return 19;}}
        public String Altura { get { return "1,83 m"; } }
        public String Genero { get { return "Masculino"; } }
        public String Peso { get { return "100 kg"; } }


                
        public RelatorioViewModel()
        {
            List blocos = new List();

            Paragraph itemDaLista1 = new Paragraph(new Run("Braquiorradial Direito (EMG)"));
            Paragraph itemDaLista2 = new Paragraph(new Run("Braquiorradial Esquerdo (EMG)"));
            Paragraph itemDaLista3 = new Paragraph(new Run("Membro Superior Esquerdo (Dinamometria)"));

            blocos.ListItems.Add(new ListItem(itemDaLista1));
            blocos.ListItems.Add(new ListItem(itemDaLista2));
            blocos.ListItems.Add(new ListItem(itemDaLista3));

            FlowDocument flowdocument = new FlowDocument();
            flowdocument.Blocks.Add(blocos);

            FlowDocumentReader flowdocumentreader = new FlowDocumentReader();
            flowdocumentreader.Document = flowdocument;

        }


        
    }
}
