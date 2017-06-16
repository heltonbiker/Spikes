using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MeuWriteableBitmap
{
    class Program
    {
        static void Main(string[] args)
        {
            var coiso = new Coiso();
        }
    }

    public class Coiso
    {
        private WriteableBitmap writeableBmp;
        private static Random rand = new Random();
        private int frameCounter = 0;


        private void Init()
        {
            writeableBmp = new WriteableBitmap(800, 600, 96.0, 96.0, PixelFormats.Pbgra32, null);
        }

        private unsafe void DrawShapes()
        {
            // Wrap updates in a GetContext call, to prevent invalidation and nested locking/unlocking during this block
            using (var bitmapContext = writeableBmp.GetBitmapContext())
            {
                // Init some size vars
                int w = this.writeableBmp.PixelWidth - 2;
                int h = this.writeableBmp.PixelHeight - 2;
                int wh = w >> 1;
                int hh = h >> 1;

                // Clear 
                writeableBmp.Clear();

                // Draw Shapes and use refs for faster access which speeds up a lot.
                int wbmp = writeableBmp.PixelWidth;
                int hbmp = writeableBmp.PixelHeight;
                var pixels = bitmapContext.Pixels;
                var shapeCount = 36;
                for (int i = 0; i < shapeCount/6; i++)
                {
                    // Standard shapes
                    WriteableBitmapExtensions.DrawLine(bitmapContext, wbmp, hbmp, rand.Next(w), rand.Next(h), rand.Next(w),
                                                       rand.Next(h), GetRandomColor());
                    writeableBmp.DrawTriangle(rand.Next(w), rand.Next(h), rand.Next(w), rand.Next(h), rand.Next(w),
                                              rand.Next(h), GetRandomColor());
                    writeableBmp.DrawQuad(rand.Next(w), rand.Next(h), rand.Next(w), rand.Next(h), rand.Next(w),
                                          rand.Next(h), rand.Next(w), rand.Next(h), GetRandomColor());
                    writeableBmp.DrawRectangle(rand.Next(wh), rand.Next(hh), rand.Next(wh, w), rand.Next(hh, h),
                                               GetRandomColor());
                    writeableBmp.DrawEllipse(rand.Next(wh), rand.Next(hh), rand.Next(wh, w), rand.Next(hh, h),
                                             GetRandomColor());

                    // Random polyline
                    int[] p = new int[rand.Next(5, 10)*2];
                    for (int j = 0; j < p.Length; j += 2)
                    {
                        p[j] = rand.Next(w);
                        p[j + 1] = rand.Next(h);
                    }
                    writeableBmp.DrawPolyline(p, GetRandomColor());
                }

                // Invalidates on end of using block
            }
        }

        private void DrawEllipses()
        {
            // Init some size vars
            int w = this.writeableBmp.PixelWidth - 2;
            int h = this.writeableBmp.PixelHeight - 2;
            int wh = w >> 1;
            int hh = h >> 1;

            // Wrap updates in a GetContext call, to prevent invalidation and nested locking/unlocking during this block
            using (writeableBmp.GetBitmapContext())
            {
                // Clear 
                writeableBmp.Clear();

                // Draw Ellipses
                var shapeCount = 36;
                for (int i = 0; i < shapeCount; i++)
                {
                    writeableBmp.DrawEllipse(rand.Next(wh), rand.Next(hh), rand.Next(wh, w), rand.Next(hh, h),
                                             GetRandomColor());
                }

                // Invalidates on exit of using block
            }
        }
    
        private void DrawEllipsesFlower()
        {
            if (writeableBmp == null)
                return;

            // Init some size vars
            int w = this.writeableBmp.PixelWidth - 2;
            int h = this.writeableBmp.PixelHeight - 2;

            // Wrap updates in a GetContext call, to prevent invalidation and nested locking/unlocking during this block
            using (writeableBmp.GetBitmapContext())
            {
                // Increment frame counter
                if (++frameCounter >= int.MaxValue || frameCounter < 1)
                {
                    frameCounter = 1;
                }
                double s = Math.Sin(frameCounter*0.01);
                if (s < 0)
                {
                    s *= -1;
                }

                // Clear 
                writeableBmp.Clear();

                // Draw center circle
                int xc = w >> 1;
                int yc = h >> 1;
                // Animate base size with sine
                int r0 = (int)((w + h)*0.07*s) + 10;
                writeableBmp.DrawEllipseCentered(xc, yc, r0, r0, Colors.Brown);

                // Draw outer circles
                int dec = (int)((w + h)*0.0045f);
                int r = (int)((w + h)*0.025f);
                int offset = r0 + r;
                for (int i = 1; i < 6 && r > 1; i++)
                {
                    for (double f = 1; f < 7; f += 0.7)
                    {
                        // Calc postion based on unit circle
                        int xc2 = (int)(Math.Sin(frameCounter*0.002*i + f)*offset + xc);
                        int yc2 = (int)(Math.Cos(frameCounter*0.002*i + f)*offset + yc);
                        int col = (int)(0xFFFF0000 | (uint)(0x1A*i) << 8 | (uint)(0x20*f));
                        writeableBmp.DrawEllipseCentered(xc2, yc2, r, r, col);
                    }
                    // Next ring
                    offset += r;
                    r -= dec;
                    offset += r;
                }

                // Invalidates on exit of using block
            }
        }

        private static int GetRandomColor()
        {
            return (int)(0xFF000000 | (uint)rand.Next(0xFFFFFF));
        }


    }


}
