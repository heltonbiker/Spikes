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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;


namespace ParametrosNormalidade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Notes : UserControl
    {
        private enum state { expanded = 0, hidden, animating };
        state EnumState;

        public Notes()
        {
            InitializeComponent();
            EnumState = state.hidden;
        }

        public void Expand()
        {
            if (EnumState == state.hidden)
            {
                EnumState = state.animating;

                DoubleAnimation da1 = new DoubleAnimation();
                da1.Duration = TimeSpan.FromSeconds(0.6);
                da1.From = 0;
                da1.To = container.ActualHeight;
                
                DoubleAnimation da2 = new DoubleAnimation();
                da2.Duration = TimeSpan.FromSeconds(1);
                da2.From = 0.3;
                da2.To = 0.07;

                da1.Completed += new EventHandler(Expand_Completed);

                content.BeginAnimation(HeightProperty, da1);
                Gradient1.BeginAnimation(GradientStop.OffsetProperty, da2);
            }
        }

        public void Revert()
        {
            if (EnumState == state.expanded)
            {
                EnumState = state.animating;

                //contentGrid.Visibility = Visibility.Hidden;

                DoubleAnimation da1 = new DoubleAnimation();
                da1.Duration = TimeSpan.FromSeconds(0.6);
                da1.From = container.ActualHeight;
                da1.To = 0;

                DoubleAnimation da2 = new DoubleAnimation();
                da2.Duration = TimeSpan.FromSeconds(1);
                da2.From = 0.07;
                da2.To = 0.3;

                da1.Completed += new EventHandler(Revert_Completed);

                content.BeginAnimation(HeightProperty, da1);
                Gradient1.BeginAnimation(GradientStop.OffsetProperty, da2);
            }
        }

        void Expand_Completed(object sender, EventArgs e)
        {
            image1.LayoutTransform = new ScaleTransform(1, -1);
            //contentGrid.Visibility = Visibility.Visible;

            EnumState = state.expanded;
        }

        void Revert_Completed(object sender, EventArgs e)
        {
            image1.LayoutTransform = new ScaleTransform();

            EnumState = state.hidden;
        }

        private void mainBord_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EnumState == state.hidden) Expand();
            else if (EnumState == state.expanded) Revert();
        }

        private void mainBord_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.From = 0.7;
            da.To = 1;

            image1.BeginAnimation(Image.OpacityProperty, da);
        }

        private void mainBord_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.From = 1;
            da.To = 0.5;

            image1.BeginAnimation(Image.OpacityProperty, da);
        }

    }
}
