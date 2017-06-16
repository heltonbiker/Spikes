using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace DoisRelatorios
{
    public class ViewModel : ViewModelBase {

        FixedDocument documento;

        public ViewModel() {
            documento = new FixedDocument();
            //Section cabecalho = new Section();
            
            documento.Pages.Add(new PageContent());
            
            //Paragraph identificacao = new Paragraph();

        }


    }
}
