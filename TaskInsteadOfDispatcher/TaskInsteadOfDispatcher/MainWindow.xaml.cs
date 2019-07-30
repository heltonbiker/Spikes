using Miotec.Framework;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace TaskInsteadOfDispatcher
{
	public partial class MainWindow : Window
	{
		public ObservableCollection<string> Items { get; }
			= SynchronizeableCollection.Create<string>();


		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;
		}


		void AdicionarElemento()
		{
			Items.Add(DateTime.Now.ToString());
		}



		public ICommand ComandoAdicionar => new RelayCommand(AdicionarElemento);

	}

	public static class SynchronizeableCollection
	{
		public static ObservableCollection<T> Create<T>()
		{
			var collection = new ObservableCollection<T>();
			BindingOperations.EnableCollectionSynchronization(collection, new object());
			return collection;
		}
	}
}
