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
using System.ComponentModel;

namespace DrawingGraficos
{
	/// <summary>
	/// Interaction logic for ViewportControl.xaml
	/// </summary>
	public partial class ViewportControl : UserControl, INotifyPropertyChanged
	{
		
		

        public static readonly DependencyProperty LimitsProperty =
            DependencyProperty.Register("Limits",
                                        typeof(Rect),
                                        typeof(ViewportControl),
                                        new PropertyMetadata(OnLimitsChanged));

        public Rect Limits {
            get { return (Rect)GetValue(LimitsProperty); }
            set { SetValue(LimitsProperty, value); }
        }

        static void OnLimitsChanged(DependencyObject sender,
                                           DependencyPropertyChangedEventArgs e) {
            (sender as ViewportControl).OnLimitsChanged(e);
        }

        void OnLimitsChanged(DependencyPropertyChangedEventArgs args) {
            RaisePropertyChanged("Limits");
        }		
		
		
		public ViewportControl()
		{
			this.InitializeComponent();
		}




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged (String propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}