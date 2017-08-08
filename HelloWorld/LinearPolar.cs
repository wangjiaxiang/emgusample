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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace HelloWorld
{
    public partial class LinearPolar : Form
    {
        Mat img;
        public LinearPolar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Mat temp = new Mat();
            //int Radius = Convert.ToInt32(this.textBox1.Text);
            //CvInvoke.LinearPolar(img, temp, new Point(img.Width / 2, 50), Radius, Inter.Lanczos4/*差值算法*/, Warp.InverseMap);
            
            //byte[] data = new byte[600*800*4];
            //for (int i = 0; i < data.Length; i++)
            //{
            //    if (i % 4 == 0)
            //    {
            //        Random ra = new Random();
            //        data[i] = data[i + 1] = data[i + 2] = (byte)ra.Next(255);
            //        data[i + 3] = 255;
            //    }
            //}

            byte[] data = new byte[600 * 800];
            for (int i = 0; i < data.Length; i++)
            {
                Random ra = new Random();
                data[i] = (byte)ra.Next(255);       
            }
            byte[] a = new byte[600*800];
            data.CopyTo(a, 0);
            Stopwatch sw =new Stopwatch();
            sw.Start();
            //GCHandle hObject = GCHandle.Alloc(data, GCHandleType.Pinned);
            //IntPtr ptemp = hObject.AddrOfPinnedObject();
            Mat image = new Mat(600, 800, DepthType.Cv8U, 1);
            image.SetTo<byte>(data);
            //CvInvoke.LinearPolar(img, temp, new PointF((float)img.Width / 2, 50),(double)(depth+radius)*800/depth, Inter.Lanczos4/*差值算法*/, Warp.InverseMap);
            //if (hObject.IsAllocated)
            //    hObject.Free();
            sw.Stop();
            System.Console.WriteLine("1:" + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            Mat po = GetAffineTransform(image, 0, 30, 60);
            sw.Stop();
            System.Console.WriteLine("4:" + sw.ElapsedMilliseconds);
            po.Bitmap.Save("polar.bmp");
            this.pictureBox1.Image = image.Bitmap;
            this.pictureBox2.Image = po.Bitmap;
            //Mat mat = new Mat(600,800,DepthType.Cv8U,3);
            //mat.Col(10).Row(10).SetTo(new Rgb(0,255,0).MCvScalar);
            image.Save("test.bmp");

            image.Bitmap.Save("test.bmp");
            sw.Reset();
            sw.Start();
           // Bitmap imag = show(a);
            sw.Stop();
            System.Console.WriteLine("2:" + sw.ElapsedMilliseconds);
            //imag.Save("test1.bmp");
            sw.Reset();
            sw.Start();
            Image<Gray, Byte> mimage = new Image<Gray, byte>(800, 600, new Gray(0));
            //for (int x = 0; x < mimage.Width; x++)
            //{
            //    for (int y = 0; y < mimage.Height; y++)
            //    {
            //        int index = 800 * y + x;
            //        byte temp = a[index];
            //        mimage[y, x] = new Gray(temp);
            //    }
            //}

            sw.Stop();
            System.Console.WriteLine("3:" + sw.ElapsedMilliseconds);
            //mimage.Save("test3.bmp");

            //Mat mattest = CvInvoke.Imread("test3.bmp",ImreadModes.Grayscale);
            //bool iscommom = mattest.Equals(image);
            //bool isSame = mimage.Bitmap.Equals(mimage.Bitmap);
        }

        public Mat GetAffineTransform(Mat src,double sr,double depth,double angle)
        {
            //int w = (int)((sr + depth) * src.Width / depth);
            //int h = (int)(360.0 * src.Height / angle);
            Mat temp = new Mat();
            //Mat temp = new Mat(h, w, src.Depth, src.NumberOfChannels);
            //Rectangle rect = new Rectangle(new Point((int)(sr * src.Width / depth), (int)((90 - angle / 2) * src.Height / angle)), new Size(src.Width, src.Height));
            //Mat temp2 = new Mat(temp, rect);
            //src.CopyTo(temp2);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            CvInvoke.CopyMakeBorder(src, temp, (int)((90 - angle / 2) * src.Height / angle), (int)((270 - angle / 2) * src.Height / angle), (int)(sr * src.Width / depth), 0, BorderType.Constant);
            sw.Stop();
            System.Console.WriteLine("5:"+sw.ElapsedMilliseconds);
            //temp.Bitmap.Save("cart.bmp");
            Mat result = new Mat();
            //result.SetTo(new Rgba(0, 0, 0,0).MCvScalar);
            double y1 = sr * Math.Cos(angle * Math.PI / 360) * src.Width / depth;
            double width = (sr + depth) * 2 * src.Width / depth * Math.Sin(angle * Math.PI / 360);
            double height = (sr + depth) * src.Width / depth - y1;
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            sw.Reset();
            sw.Start();
            CvInvoke.LinearPolar(temp, result, new PointF((float)(temp.Width / 2), 0), temp.Width, Inter.Lanczos4/*差值算法*/, Warp.InverseMap);
            sw.Stop();
            System.Console.WriteLine("linearpolar:" + sw.ElapsedMilliseconds);
            return new Mat(result, new Rectangle((int)((result.Width - width) / 2), (int)Math.Floor(y1), (int)Math.Floor(width), (int)Math.Floor(height)));
        }

        public Bitmap show(byte[] data)
        {
            Bitmap curbitmap = new Bitmap(800, 600, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            for (int x = 0; x < 800;x++)
            {
                for (int y = 0; y < 600; y++)
                {
                    int index = 800 * y + x;
                    Color c = Color.FromArgb(data[4 * index+3], data[4 * index +2], data[4 * index +1], data[4 * index]);
                    curbitmap.SetPixel(x, y, Color.FromArgb(data[4 * index + 3], data[4 * index + 2], data[4 * index + 1], data[4 * index]));
                }
            }
            return curbitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            img = new Mat(600, 800, DepthType.Cv8U, 3); //Create a 3 channel image of 400x200
            img.SetTo(new Rgb(0, 0, 0).MCvScalar); // set it to Blue color
            
            Mat img2 = new Mat(img, new Rectangle(50, 0, 200, 600));
            img2.SetTo(new Rgb(0, 0, 255).MCvScalar);

            Mat img3 = new Mat(img, new Rectangle(0, 0, 50, 50));
            img3.SetTo(new Rgb(0, 255, 0).MCvScalar);

            this.pictureBox1.Image = img.Bitmap;            
        }
    }
}
