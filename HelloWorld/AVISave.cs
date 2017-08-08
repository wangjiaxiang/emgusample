using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;

namespace HelloWorld
{
    public partial class AVISave : Form
    {

        private int w = 200;
        private int h = 200;

        public AVISave()
        {
            InitializeComponent();
        }

        public void ImagetoVideo()
        {
            int length = w * h;
            IntPtr pd1 = Marshal.AllocHGlobal(length);
            for (int i = 0; i < length; i++)
            {
                Random ra = new Random();
                Marshal.WriteByte(pd1, i, (byte)ra.Next(255));
            }
            VideoWriter vw = new VideoWriter("test.avi", VideoWriter.Fourcc('M', 'J', 'P', 'G'), 25, new Size(200, 200), false);

            Image<Gray, byte> test1 = new Image<Gray, byte>(w, h, w, pd1);
            test1.Save("test1.bmp");
            vw.Write(test1.Mat);
            vw.Dispose();
        }

        private void videoplay_Load(object sender, EventArgs e)
        {
            ImagetoVideo();
        }

    }
}
