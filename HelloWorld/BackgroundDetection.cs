using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;

namespace HelloWorld
{
    public partial class BackgroundDetection : Form
    {
        public BackgroundDetection()
        {
            InitializeComponent();
        }
        
        Image<Bgr, byte> image;

        private void BackgroundDetection_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = new Image<Bgr,byte>(this.openFileDialog1.FileName);
                this.pictureBox1.Image = image.ToBitmap();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int history = 200;
            double dist2Threshold = 2.5*2.5;
            bool detectShadows = false;
            
            Mat fgmask = new Mat();
            BackgroundSubtractorKNN bg_knn = new BackgroundSubtractorKNN(history, dist2Threshold, detectShadows);
            BackgroundSubtractorMOG2 bg_mod = new BackgroundSubtractorMOG2();

            bg_mod.Apply(image,fgmask);
            //bg_knn.Apply(image,fgmask);

            this.pictureBox2.Image = fgmask.Bitmap;
        }
    }
}
