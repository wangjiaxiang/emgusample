using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace HelloWorld
{
    public partial class ImageMat : Form
    {
        int w = 800;
        int h = 600;

        byte[] data1;
        byte[] data2;
        byte[] data3;

        IntPtr pd1;
        IntPtr pd2;
        IntPtr pd3;

        public ImageMat()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            int length = w * h;
            pd1 = Marshal.AllocHGlobal(length * sizeof(float));
            for (int i = 0; i < length; i++)
            {
                Random ra = new Random();
                Marshal.WriteInt32(pd1, i * sizeof(float), 4);
            }

            pd2 = Marshal.AllocHGlobal(length * sizeof(float));
            for (int i = 0; i < length; i++)
            {
                Random ra = new Random();
                Marshal.WriteInt32(pd2, i * sizeof(float), 2);
            }

            data1 = new byte[w * h];
            for (int i = 0; i < data1.Length; i++)
            {
                Random ra = new Random();
                data1[i] = (byte)ra.Next(255);
            }

            data2 = new byte[h * w * 4];
            for (int i = 0; i < data2.Length; i++)
            {
                if (i % 4 == 0)
                {
                    Random ra = new Random();
                    data2[i] = data2[i + 1] = data2[i + 2] = (byte)ra.Next(255);
                    data2[i + 3] = 255;
                }
            }

            data3 = new byte[data1.Length];
            for (int i = 0; i < data3.Length; i++)
            {
                Random ra = new Random();
                data3[i] = (byte)ra.Next(255);
            }

            Mat m1 = new Mat(h, w, DepthType.Cv8U, 1);
            m1.SetTo<byte>(data1);

            Mat m2 = new Mat(h, w, DepthType.Cv8U, 1);
            m2.SetTo<byte>(data3);
            this.pictureBox1.Image = m1.Bitmap;
            this.pictureBox2.Image = m2.Bitmap;
        }

        /*
         * 图像构造方法
         * Mat mt = new Mat(h, w, DepthType.Cv8U, 1);
            mt.SetTo<byte>(temp1);
         * 
         * Image<Gray, byte> test1 = new Image<Gray, byte>(w, h, w, pd1);
         * 
         */

        private void button1_Click(object sender, EventArgs e)
        {
            compare();
        }

        public void compare1()
        {
            Mat m1 = new Mat(h, w, DepthType.Cv8U, 1);
            m1.SetTo<byte>(data1);

            Mat m2 = new Mat(h, w, DepthType.Cv8U, 1);
            m2.SetTo<byte>(data3);

            Mat mt = new Mat(h, w, DepthType.Cv8U, 1, pd1, w);
            mt.Save("mt.bmp");

            Mat r2 = new Mat();
            Mat r1 = new Mat();
            m1.Bitmap.Save("m1.bmp");
            m2.Bitmap.Save("m2.bmp");

            Image<Gray, byte> test1 = new Image<Gray, byte>(w, h, w, pd1);
            Image<Gray, byte> test2 = new Image<Gray, byte>(w, h, w, pd2);

            test1.Save("test1.bmp");

            CvInvoke.Compare(m1, m2, r1, CmpType.GreaterThan);
            CvInvoke.Compare(m1, m2, r2, CmpType.LessEqual);

            Mat m3 = new Mat();
            Mat m4 = new Mat();

            Mat m5 = new Mat();
            Mat m6 = new Mat();

            CvInvoke.ApplyColorMap(m2, m5, ColorMapType.Cool);
            CvInvoke.ApplyColorMap(m1, m6, ColorMapType.Hot);

            m5.Bitmap.Save("m5.bmp");
            m6.Bitmap.Save("m6.bmp");

            Mat m7 = new Mat();
            m6.CopyTo(m7, r1);
            m5.CopyTo(m7, r2);

            m7.Bitmap.Save("m7.bmp");

            this.pictureBox2.Image = m7.Bitmap;
        }

        public void compare()
        {
            Image<Gray, float> test1 = new Image<Gray, float>(w, h, w * sizeof(float), pd1);

            float[, ,] data = new float[5,5,5];
            data[0, 0, 0] = 255;
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr pd = handle.AddrOfPinnedObject();

            Image<Gray, float> test2 = new Image<Gray, float>(w, h, w * sizeof(float), pd2);

            Mat mat1 = new Mat(h, w, DepthType.Cv32F, 1, pd1, w * sizeof(float));
           // Mat result = new Mat();

            Image<Gray, float> result1 = test1.Mul(test1, 0.5);//test1*test1*0.5
            Image<Gray, float> result2 = test2.Mul(test2, 0.1);//

            //CvInvoke.Compare(test1,test2,result,CmpType.GreaterThan);
        }
    }
}
