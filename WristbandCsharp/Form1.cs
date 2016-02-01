using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace WristbandCsharp
{
    public partial class Form1 : Form
    {

        int intensityPercent = 100;
        int durationPercent = 50;

        int camera = 1;

        Capture cap;
        Image<Bgr, Byte> image;
        Arduino arduino = null;

        // Glove Configuration
        // The entries in this list determine what code will be sent to the 
        //  glove when trying to send the following motor commands (in this order):
        // Left, down, right, up, all
        // Note that the code for "all" should not change from 5 glove-to-glove,
        //  but the arrangement of the first four numbers will change depending
        //  on how the glove is wired.
        int[] gloveConfig = {0,1,2,3,5};



        public Form1()
        {

            // So that key presses go to the form first.
            this.KeyPreview = true;

            InitializeComponent();

            RefreshSerialPortList();

            RefreshCameraList();

            this.KeyPress += new KeyPressEventHandler( Form1_OnKeyDown);

        }

        private void RefreshCameraList()
        {
            
        }

        private void StartCameraFromUserChoice(int cameraChoice)
        {

            try
            {

                // Remove old event handler as we set up new camera.
                Application.Idle -= new EventHandler(ShowFromCam);

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                cap = new Capture(cameraChoice);


                // 896x504
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 504.0);
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 896.0);
                // 928x522
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 522.0);
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 928.0);
                //// 1024x576
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 576.0);
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1024.0);
                // 1664x936
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 936.0);
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1664.0);
                // 1152x648
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 648.0);
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1152.0);
                // 1920x1080
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 1080.0);
                //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1920.0);

                // Add new event handler.
                Application.Idle += new EventHandler(ShowFromCam);

            }
            catch (Exception e)
            {

            }
        }

        void ShowFromCam(object sender, EventArgs e)
        {

            Mat frameMat = new Mat();

            try
            {
                if (!cap.Retrieve(frameMat, 0))
                    return;

                //image = null;
                //while (image == null) image = cap;
                Image<Bgr, byte> returnimage = frameMat.ToImage<Bgr, byte>();


                pictureBox1.Image = returnimage.ToBitmap();
                //Image<Bgr, byte> frame = frameMat.ToImage<Bgr, byte>();
            }
            catch
            {

            }
            // Get image.

            

        }

        private void RefreshSerialPortList()
        {
            // Combo box 2 - Arduino selection
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (arduino!= null)
            {
                arduino.SendPacket(gloveConfig[0], intensityPercent, durationPercent);


            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(gloveConfig[2], intensityPercent, durationPercent);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(gloveConfig[1], intensityPercent, durationPercent);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(gloveConfig[3], intensityPercent, durationPercent);
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(gloveConfig[4], intensityPercent, durationPercent);
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected void Form1_OnKeyDown(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                    if (arduino != null)
                    {
                        arduino.SendPacket(gloveConfig[0], intensityPercent, durationPercent);
                        
                    }
                    break;
                case 'd':
                    if (arduino != null)
                    {
                        arduino.SendPacket(gloveConfig[2], intensityPercent, durationPercent);
                    }
                    break;
                case 'w':
                    if (arduino != null)
                    {
                        arduino.SendPacket(gloveConfig[3], intensityPercent, durationPercent);
                    }
                    break;
                case 's':
                    if (arduino != null)
                    {
                        arduino.SendPacket(gloveConfig[1], intensityPercent, durationPercent);
                    }
                    break;
                case 'e':
                    if (arduino != null)
                    {
                        arduino.SendPacket(gloveConfig[5], intensityPercent, durationPercent);
                    }
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(ShowFromCam);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            StartCameraFromUserChoice(comboBox1.SelectedIndex);
        }

    }
}
