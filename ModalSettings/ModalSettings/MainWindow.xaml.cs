using System.ComponentModel;
using System.Windows;

namespace ModalSettings
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		
		public int Valor
		{
            get
            {
                return _valor;
            }
			set
            {
                _valor = value;
                RaisePropertyChanged("Valor");
            }
		}
        int _valor;
		
		public MainWindow()
		{
			
			DataContext = this;
			
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var modal = new ModalSettingsWindow() 
            {
                Owner = this,
                DataContext = this.DataContext
            };
            modal.ShowDialog();
		}






        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}