using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GerenciadorDeColeta
{
	public partial class ProcedimentoDeColeta : UserControl
	{
		
        //ProjectionPattern projeção;
        
        public ProcedimentoDeColeta()
		{
			this.InitializeComponent();

            ProjectionPattern projeção = new ProjectionPattern();

            DataContext = projeção;
            
            var janela_projeção = new JanelaDeProjecao(projeção);
            janela_projeção.Show();


		}
	}
}