using System.Windows;

namespace ModalSettings
{
	/// <summary>
	/// Interaction logic for ModalSettingsWindow.xaml
	/// </summary>
	public partial class ModalSettingsWindow : Window
	{

        public ModalSettingsWindow()
        {            
			this.InitializeComponent();
        }

        private void OK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.DialogResult = true;
        }
	}
}