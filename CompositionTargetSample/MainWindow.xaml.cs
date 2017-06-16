// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)

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
using System.Diagnostics;

namespace CompositionTargetSample
{
    public partial class MainWindow : Window
    {

        Point _orbit_center;
        List<Ball> bolinhas;
        private double _lastTimeRendered;
        private const double RENDER_PERIOD_MS = 10;


        public MainWindow()
        {
            InitializeComponent();

            bolinhas = Enumerable.Range(0,300)
                                 .Select(v => Convert.ToDouble(v))
                                 .Select(val => new Ball(new Point(600, 100), new Vector(3+val/(15*3), 0)))
                                 .ToList();

            CompositionTarget.Rendering += Redraw;

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            recenter();
        }       


        protected void Redraw(object sender, EventArgs e)
        {
            RenderingEventArgs rargs = (RenderingEventArgs)e;
            //if (!((rargs.RenderingTime.TotalMilliseconds - _lastTimeRendered) > RENDER_PERIOD_MS))
            //    return;
            
            
            myCanvas.Children.Clear();

            for (var i = 0; i < bolinhas.Count(); i++)
            {
                var b = bolinhas[i];
                b.Update(_orbit_center);
                bolinhas[i] = b;
                // Should be static resource
                myCanvas.Children.Add(new Path()
                    {
                        Fill=Brushes.Red,
                        Data=new EllipseGeometry(b.Position, 3, 3),
                        IsHitTestVisible=false
                    });
            }

            _lastTimeRendered = rargs.RenderingTime.TotalMilliseconds;
        }




        public void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            _orbit_center = e.GetPosition((UIElement)sender);
        }

        private void window_MouseLeave(object sender, MouseEventArgs e)
        {
            recenter();
        }

        private void recenter()
        {
            _orbit_center = new Point(this.ActualWidth/2, this.ActualHeight/2);
        }
    }

    public class Ball
    {
        public Point Position;
        public Vector Speed;

        public Ball(Point position, Vector speed)
        {
            Position = position;
            Speed = speed;
        }


        public void Update(Point orbitCenter)
        {
            Vector posVector = Position - orbitCenter;
            Double distance = posVector.Length;
            Double gravity = Math.Min(0.01, 1/(distance*distance)) * 100;
            Vector acceleration = -posVector * gravity;
            this.Speed += acceleration;
            this.Speed *= 0.99;
            this.Position += this.Speed;
        }
    }


}