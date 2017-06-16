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
using System.Windows.Shapes;


namespace RenderingEventDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        public Window1()
        {
            InitializeComponent();
            CompositionTarget.Rendering += DancingChildren;
        }

        private void DancingChildren(object sender, EventArgs e)
        {
            Canvas root = this.Content as Canvas;
            Point center = new Point(this.ActualWidth / 2.0, this.ActualHeight / 2.0);
            foreach (FrameworkElement child in root.Children)
            {
                string[] tag = ((string)child.Tag).Split(';');
                Point follow = GetLocation((FrameworkElement)FindName(tag[0]));
                Point avoid = GetLocation((FrameworkElement)FindName(tag[1]));
                Point me = GetLocation(child);

                // impulse's tweaked to come close to an orbit around the center
                Vector attract = (follow - me) * 0.1;
                Vector repel = (me - avoid) * 0.1555;
                Vector toCenter = (center - me) * 0.099;

                SetLocation(child, me + attract + repel + toCenter);
            }
        }

        private void SetLocation(FrameworkElement child, Point point)
        {
            Canvas.SetLeft(child, point.X);
            Canvas.SetTop(child, point.Y);
        }

        private Point GetLocation(FrameworkElement frameworkElement)
        {
            return new Point(Canvas.GetLeft(frameworkElement), Canvas.GetTop(frameworkElement));
        }
    }
}