using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace HelloWorld
{
    public partial class Remap : Form
    {

        /*
         * 图像变形
         * */

        /*
         * 
         * Image<TColor, TDepth>:
         * 
         * TColor类型:
         * 1、Gray
         * 2、Bgr (Blue Green Red)	 
         * 3、Bgra (Blue Green Red Alpha)
         * 4、Hsv (Hue Saturation Value)
         * 5、Hls (Hue Lightness Saturation)	
         * 6、Lab (CIE L*a*b*)	 
         * 7、Luv (CIE L*u*v*)	 
         * 8、Xyz (CIE XYZ.Rec 709 with D65 white point)	 
         * 9、Ycc (YCrCb JPEG)
         * 
         * TDepth类型: Byte SByte Single (float) Double UInt16 Int16 Int32
         */
        Mat src, dst;
        Mat map_x, map_y;

        public Remap()
        {
            InitializeComponent();
            src = CvInvoke.Imread("test3.bmp", ImreadModes.Grayscale);
        }

        public unsafe static void updateMap(Mat src,Mat dst,float[] datax,float[] datay,float sr,float depth,float sa,float angle)
        {
            //for (int i = 0; i < datax.Length; i++)
            //{
            //    datax[i] = i % dst.Width;
            //    datax[i] = i % dst.Width;
            //}
            //Task[] tasklist = new Task[datax.Length];  
            int c = dst.Cols;
            int rS = src.Rows;
            int w = dst.Width;
            PointF mCenterP = new PointF(w / 2, 0);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Parallel.For(0, datax.Length, i =>
            //    {
            //        //Stopwatch sw = new Stopwatch();
            //        //sw.Start();

            //        int x = i % c;
            //        int y = i / c;

            //        float X = (float)Math.Sqrt(Math.Pow((x - mCenterP.X), 2) + Math.Pow((y - mCenterP.Y), 2));
            //        float Y = (float)(Math.Acos(((x - mCenterP.X) / X)) * 180 / Math.PI);

            //        float xr = (float)(X - sr);
            //        float yr = (Y - sa) * (rS - 1) / angle;
            //        datax[i] = xr;
            //        datay[i] = yr;
            //        //sw.Stop();
            //        //System.Console.WriteLine(sw.ElapsedMilliseconds);
            //    });
            sw.Stop();
            System.Console.WriteLine("Parallel" + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            //Task[] tasklist = new Task[datax.Length];
            //for (int i = 0; i < datax.Length; i++)
            //{
            //    int x = i % c;
            //    int y = i / c;
            //    Task task = new Task(() =>
            //        {
            //            PointF s = PointToSector(new PointF(x, y), mCenterP);
            //        });
            //    tasklist[i] = task;
            //    task.Start();
            //}
            //Task.WaitAll(tasklist);
            sw.Stop();
            System.Console.WriteLine("Task" + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();

            for (int i = 0; i < datax.Length; i++)
            {
                int x = i % c;
                int y = i / c;
                PointF s = PointToSector(new PointF(x, y), mCenterP);

                datax[i] = (float)(s.X - sr*c*depth);
                datay[i] = (s.Y - sa) * (rS - 1) / angle;
            }
            sw.Stop();
            System.Console.WriteLine("for" + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            //Thread t1 = new Thread(() =>
            //    {
            //        for (int i = 0; i < datax.Length/3; i++)
            //        {
            //            int x = i % c;
            //            int y = i / c;
            //            PointF s = PointToSector(new PointF(x, y), mCenterP);

            //            datax[i] = (float)(s.X - sr);
            //            datay[i] = (s.Y - sa) * (rS - 1) / angle;
            //        }
            //    });
            //    t1.Start();

            //Thread t2 = new Thread(() =>
            //{
            //    for (int i = datax.Length / 3; i < datax.Length*2/3; i++)
            //    {
            //        int x = i % c;
            //        int y = i / c;
            //        PointF s = PointToSector(new PointF(x, y), mCenterP);

            //        datax[i] = (float)(s.X - sr);
            //        datay[i] = (s.Y - sa) * (rS - 1) / angle;
            //    }
            //});
            //t2.Start();

            //Thread t3 = new Thread(() =>
            //{
            //    for (int i = datax.Length * 2 / 3; i < datax.Length; i++)
            //    {
            //        int x = i % c;
            //        int y = i / c;
            //        PointF s = PointToSector(new PointF(x, y), mCenterP);

            //        datax[i] = (float)(s.X - sr);
            //        datay[i] = (s.Y - sa) * (rS - 1) / angle;
            //    }
            //});
            //t3.Start();

            //Thread t4 = new Thread(() =>
            //{
            //    for (int i = datax.Length * 3 / 4; i < datax.Length; i++)
            //    {
            //        int x = i % c;
            //        int y = i / c;
            //        PointF s = PointToSector(new PointF(x, y), mCenterP);

            //        datax[i] = (float)(s.X - sr);
            //        datay[i] = (s.Y - sa) * (rS - 1) / angle;
            //    }
            //});
            //t4.Start();

            //while (t1.ThreadState == System.Threading.ThreadState.Running || t2.ThreadState == System.Threading.ThreadState.Running || t3.ThreadState == System.Threading.ThreadState.Running /*|| t4.ThreadState == System.Threading.ThreadState.Running*/)
            //{
            //    Thread.Sleep(1);
            //}

            sw.Stop();
            System.Console.WriteLine("thread" + sw.ElapsedMilliseconds);

        }


        public static PointF PointToSector(PointF p,PointF mCenterP)
        {
            PointF s = new PointF();
            if (p.Equals(mCenterP))
            {
                s.X = 0;
                s.Y = 0;
            }
            s.X = (float)Math.Sqrt(Math.Pow((p.X - mCenterP.X), 2) + Math.Pow((p.Y - mCenterP.Y), 2));
            if (mCenterP.Y > p.Y)
            {
                s.Y = (float)Math.Abs(Math.Acos(((p.X - mCenterP.X) / s.X)) * 180 / Math.PI - 180) + 180;
            }
            else
            {
                s.Y = (float)(Math.Acos(((p.X - mCenterP.X) / s.X)) * 180 / Math.PI);
            }
            return s;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            float sr = 2;
            float depth = 10;
            float angle = 90;

            float er = sr + depth;
            float sa = 90 - angle / 2;
            float ea = 90 + angle / 2;

            double Er = er * src.Width / depth;
            double Sr = sr * src.Width / depth;
            double y1 = sr * Math.Cos(angle * Math.PI / 360) * src.Width / depth;
            double width = er * Math.Sin(angle * Math.PI / 360) * 2 * src.Width / depth;
            double height = Er - y1;

            dst = new Mat((int)Math.Round(height), (int)Math.Round(width), src.Depth, src.NumberOfChannels);
            PointF mCenterP = new PointF(dst.Width / 2, (float)-y1);

            int c = dst.Cols;

            map_x = new Mat(dst.Rows, dst.Cols, DepthType.Cv32F, 1);
            map_y = new Mat(dst.Rows, dst.Cols, DepthType.Cv32F, 1);

            float[] datax = new float[dst.Width * dst.Height];
            float[] datay = new float[dst.Width * dst.Height];

            Stopwatch sw = new Stopwatch();
            sw.Start();
           // updateMap(src, dst, datax, datay, sr,depth, sa, angle);

            for (int i = 0; i < datax.Length; i++)
            {
                int x = i % c;
                int y = i / c;
                PointF s = PointToSector(new PointF(x, y), mCenterP);

                datax[i] = (float)(s.X - Sr);
                datay[i] = (s.Y - sa) * (src.Rows - 1) / angle;
            }

            sw.Stop();
            System.Console.WriteLine("1:" + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            map_x.SetTo<float>(datax);
            map_y.SetTo<float>(datay);
            sw.Stop();
            System.Console.WriteLine("2:" + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            CvInvoke.Remap(src, dst, map_x, map_y, Inter.Nearest);
            sw.Stop();
            System.Console.WriteLine("3(ticks):" + sw.ElapsedTicks);
            Bitmap temp = dst.Bitmap;
            temp.Save("result.bmp");
            this.BackgroundImage = temp;
        }
    }
}
