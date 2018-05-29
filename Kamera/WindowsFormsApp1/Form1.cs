using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge;
using AForge.Vision.Motion;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public FilterInfoCollection VideoCaptureDevices;
        public VideoCaptureDevice FinalVideo;
        public Bitmap video;
        public int tmpI;
        public bool record;
        List<Bitmap> film = new List<Bitmap>();
        public Bitmap lastPicture;
        MotionDetector Detector1,Detector2,Detector3;
        float detectionLvl;



        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmpI = 0;
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            Detector1 = new MotionDetector(new TwoFramesDifferenceDetector(), new GridMotionAreaProcessing());
            Detector2 = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            Detector3 = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionBorderHighlighting());
            detectionLvl = 0;


            foreach (FilterInfo VideoCaptureDevice in VideoCaptureDevices)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }

            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            FinalVideo = new VideoCaptureDevice();
            timer1.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FinalVideo.IsRunning == true) FinalVideo.Stop();

            FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[comboBox1.SelectedIndex].MonikerString);
            FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
            
            FinalVideo.Start();


        }

        void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            video = (Bitmap)eventArgs.Frame.Clone();

            if (radioButton1.Checked == true)
            {

            }
            if (radioButton2.Checked == true)
            {
                detectionLvl = Detector1.ProcessFrame(video);
            }
            if (radioButton3.Checked == true)
            {
                detectionLvl = Detector2.ProcessFrame(video);
            }
            if (radioButton4.Checked == true)
            {
                detectionLvl = Detector3.ProcessFrame(video);
            }

            

            pictureBox1.Image = video;

            if (record)
            {
                film.Add(video);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalVideo.IsRunning == true) FinalVideo.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tmpI++;
            lastPicture = video;
            String path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Zdjecie_" + tmpI + ".jpg";
         //   MessageBox.Show(path);
            video.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            button5.Enabled = true;
        }

        private void MakeVideo(List<Bitmap> maps)
        {

        }
        // wyswietl film
        private void button4_Click(object sender, EventArgs e)
        {
            FinalVideo.Stop();
            foreach(Bitmap B in film)
            {
                pictureBox1.Image = B;
                pictureBox1.Refresh();
                System.Threading.Thread.Sleep(120);
            }
        }
        //nagraj film
        private void button3_Click(object sender, EventArgs e)
        {
            if (!record)
            {
                film.Clear();
                record = true;
                checkBox1.Checked = true;
            }
                else
            {
                record = false;
                checkBox1.Checked = false;
            }

            button4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FinalVideo.Stop();
            pictureBox1.Image = lastPicture;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = String.Format("{0:00.00}", detectionLvl*100) + "%";
            //textBox1.Text = detectionLvl.ToString();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
