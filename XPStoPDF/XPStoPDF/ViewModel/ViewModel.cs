using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace XPStoPDF
{
    public class ViewModel : INotifyPropertyChanged {

        public FixedDocument Relatorio { get; private set; }



        // CONSTRUTOR
        public ViewModel() {

            AnaliseModel analise = new AnaliseModel();

            Relatorio = new FixedDocument();
            Relatorio.DocumentPaginator.PageSize = new Size(mm(210), mm(297));

            // Primeira Página
            PageContent primeirapagina = new PageContent();
            Relatorio.Pages.Add(primeirapagina);

            FixedPage page1 = new FixedPage();
            primeirapagina.Child = page1;

            page1.Children.Add(new PrimeiraPagina(analise) {
                Width = Relatorio.DocumentPaginator.PageSize.Width,
                Height = Relatorio.DocumentPaginator.PageSize.Height,                        
            });


            // Segunda Página
            PageContent segundapagina = new PageContent();
            Relatorio.Pages.Add(segundapagina);

            FixedPage page2 = new FixedPage();
            segundapagina.Child = page2;

            page2.Children.Add(new SegundaPagina(analise) {
                Width = Relatorio.DocumentPaginator.PageSize.Width,
                Height = Relatorio.DocumentPaginator.PageSize.Height,                        
            });

        }


        //private FixedPage NewA4Page()
        //{
        //    return new FixedPage() {
        //        Width = mm(210),
        //        Height = mm(297)
        //    };
        //}

        private double mm(double milimetros)
        {
            return 96 * milimetros / 25.4;
        }
        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
