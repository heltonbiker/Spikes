using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering.Forms;
using System.IO;
using ReportView;
using MigraDoc.DocumentObjectModel.IO;
using MigraDoc.Rendering;
using System.Diagnostics;
using MigraDoc.DocumentObjectModel.Tables;
using RelatorioMigradoc;


namespace RelatorioMigradoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InfoPaciente : UserControl
    {

        public InfoPaciente()
        {
            
        }
	}

    public class ExameInfo
    {
        public String Nome { get; set; }
        public String Altura { get; set; }
        public String Peso { get; set; }
        public String Genero { get; set; }
        public String CPF { get; set; }
        public String Idade { get; set; }
        public String Data { get; set; }
        public String Arranjo { get; set; }
        public String Protocolo { get; set; }
        public String Medico { get; set; }
        public String DataExame { get; set; }
    }

 }
