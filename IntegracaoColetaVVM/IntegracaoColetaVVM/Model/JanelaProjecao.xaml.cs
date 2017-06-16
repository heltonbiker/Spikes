using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IntegracaoColetaVVM.Model
{
    /// <summary>
    /// Interaction logic for JanelaProjecao.xaml
    /// </summary>
    public partial class JanelaProjecao : Window
    {        

        public JanelaProjecao()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EnviaParaProjetor);
        }

        void EnviaParaProjetor(object sender, RoutedEventArgs e)
        {
            var Telas = System.Windows.Forms.Screen.AllScreens.ToList();

            var TelaProjetor = Telas.OrderBy(tela => tela.Bounds.X).Last();

            this.Left = TelaProjetor.Bounds.X + 100;
            this.Top = TelaProjetor.Bounds.Y + 100;

            this.WindowState = System.Windows.WindowState.Maximized;
        }
    }
}
