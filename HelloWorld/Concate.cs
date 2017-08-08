using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using System.Runtime.InteropServices;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace HelloWorld
{
    public partial class Concate : Form
    {
        /*
         * 图像拼接
         * 
         * 对应的核心函数为cvCopy
         */

        public Concate()
        {
            InitializeComponent();
        }

        int w = 100;
        int h = 200;

        Image<Gray, byte> image1;
        Image<Gray, byte> image2;

        private void Form4_Load(object sender, EventArgs e)
        {
            IntPtr srcdata = Marshal.AllocHGlobal(w * h);
            for (int i = 0; i < w * h; i++)
            {
                Random ra = new Random();
                Marshal.WriteByte(srcdata, i, (byte)ra.Next(255));
            }

            IntPtr srcdata1 = Marshal.AllocHGlobal(w * h);
            for (int i = 0; i < w * h; i++)
            {
                Random ra = new Random();
                Marshal.WriteByte(srcdata1, i, (byte)ra.Next(255));
            }

            image1 = new Image<Gray, byte>(w, h, w, srcdata);
            image2 = new Image<Gray, byte>(w, h, w, srcdata1);

            this.pictureBox1.Image = image1.ToBitmap();
            this.pictureBox2.Image = image2.ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> result1 = image1.ConcateHorizontal(image2);
            this.pictureBox3.Image = result1.ToBitmap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> result1 = image1.ConcateVertical(image2);
            this.pictureBox3.Image = result1.ToBitmap();
        }
    }
}
