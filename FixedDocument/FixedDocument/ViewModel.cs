using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Controls;

namespace FixDoc
{
    public class ViewModel : INotifyPropertyChanged {

        public FixedDocument Relatorio { get; private set; }


        // CONSTRUTOR
        public ViewModel() {
            FixedPage page = new FixedPage();

            page.Children.Add(new TextBox(){Text = "teste"});

            PageContent primeirapagina = new PageContent();
            primeirapagina.Child = page;

            Relatorio = new FixedDocument();
            Relatorio.Pages.Add(primeirapagina);
        }
        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
