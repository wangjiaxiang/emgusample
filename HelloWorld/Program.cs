//----------------------------------------------------------------------------
//  Copyright (C) 2004-2016 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace HelloWorld
{
   class Program
   {

       [STAThread]
       static void Main(string[] args)
      {
        //for (int i = 80; i < 200; i++)
        // {
        //     Mat temp = img.Row(i);
        //     temp.SetTo(new Rgb(0, 255, 0).MCvScalar);
        // }

        // for (int i = 0; i < 100; i++)
        // {
        //     Mat temp = img.Col(i);
        //     temp.SetTo(new Rgb(0, 0, 0).MCvScalar);
        // }
         //Draw "Hello, world." on the image using the specific font
         //CvInvoke.PutText(
         //   img, 
         //   ">", 
         //   new System.Drawing.Point(10, 80), 
         //   FontFace.HersheyComplex, 
         //   1.0, 
         //   new Bgr(0, 255, 0).MCvScalar);

        // CvInvoke.LinearPolar(img, img, new Point(img.Width / 2, img.Height/2), img.Height, Inter.Linear, Warp.InverseMap);
         Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new BackgroundDetection());
      }
   }
}
