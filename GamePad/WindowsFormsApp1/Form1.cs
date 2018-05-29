using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Gaming.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("user32")]
        // [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        Timer timer = new Timer();

        Gamepad Controller1;
        String button1;
        String lastButton1;
        double LThumbstickX1;
        double LThumbstickY1;
        double RThumbstickX1;
        double RThumbstickY1;
        double RTrigger1;
        double LTrigger1;

        Gamepad Controller2;
        String button2;
        double LThumbstickX2;
        double LThumbstickY2;
        double RThumbstickX2;
        double RThumbstickY2;
        double RTrigger2;
        double LTrigger2;

        double MouseSensitive;

        Point current = new Point();
        Point last = new Point();
        Graphics g;
        Pen p = new Pen(Color.Black,5);
        Pen p2 = new Pen(Color.White, 5);
        



        public Form1()
        {
            InitializeComponent();


            g = panel1.CreateGraphics();

            panel1.BackColor = Color.White;
            



            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;


            MouseSensitive = 0;


            timer.Tick += TimerTick;
            timer.Interval = 10;
            timer.Start();

            label1.Text = ("Przycisk: null");
            label2.Text = ("Lewa gałka X: null");
            label3.Text = ("Lewa gałka Y: null");
            label4.Text = ("Prawa gałka X: null");
            label5.Text = ("Prawa gałka Y: null");
            label6.Text = ("Lewy trigger: null");
            label7.Text = ("Prawy trigger: null");
            label8.Text = ("rozłączono");

            label16.Text = ("Przycisk: null");
            label15.Text = ("Lewa gałka X: null");
            label14.Text = ("Lewa gałka Y: null");
            label13.Text = ("Prawa gałka X: null");
            label12.Text = ("Prawa gałka Y: null");
            label11.Text = ("Lewy trigger: null");
            label10.Text = ("Prawy trigger: null");
            label9.Text = ("rozłączono");

            label17.Text = ("Czułość : 0");

        }


        private void TimerTick(object sender, EventArgs e)
        {
            if (Gamepad.Gamepads.Count > 0)
            {
                Controller1 = Gamepad.Gamepads.First();
                var Reading = Controller1.GetCurrentReading();

                LThumbstickX1 = Reading.LeftThumbstickX;
                LThumbstickY1 = Reading.LeftThumbstickY;
                RThumbstickX1 = Reading.RightThumbstickX;
                RThumbstickY1 = Reading.RightThumbstickY;

                LTrigger1 = Reading.LeftTrigger;
                RTrigger1 = Reading.RightTrigger;

                lastButton1 = button1;


                switch (Reading.Buttons)
                {
                    case GamepadButtons.A:
                        button1 = "A";
                        break;
                    case GamepadButtons.B:
                        button1 = "B";
                        break;
                    case GamepadButtons.X:
                        button1 = "X";
                        break;
                    case GamepadButtons.Y:
                        button1 = "Y";
                        break;
                    default:
                        button1 = "null";
                        break;

                }





                label1.Text = ("Przycisk: " + button1);
                label2.Text = ("Lewa gałka X: " + LThumbstickX1.ToString("F3"));
                label3.Text = ("Lewa gałka Y: " + LThumbstickY1.ToString("F3"));
                label4.Text = ("Prawa gałka X: " + RThumbstickX1.ToString("F3"));
                label5.Text = ("Prawa gałka Y: " + RThumbstickY1.ToString("F3"));
                label6.Text = ("Lewy trigger: " + LTrigger1.ToString("F3"));
                label7.Text = ("Prawy trigger: " + RTrigger1.ToString("F3"));
                label8.Text = ("podłączono");

            }
            else
            {
                label1.Text = ("Przycisk: null");
                label2.Text = ("Lewa gałka X: null");
                label3.Text = ("Lewa gałka Y: null");
                label4.Text = ("Prawa gałka X: null");
                label5.Text = ("Prawa gałka Y: null");
                label6.Text = ("Lewy trigger: null");
                label7.Text = ("Prawy trigger: null");
                label8.Text = ("rozłączono");
            }

            if (Gamepad.Gamepads.Count > 1)
            {
                Controller2 = Gamepad.Gamepads.Last();
                var Reading = Controller2.GetCurrentReading();

                LThumbstickX2 = Reading.LeftThumbstickX;
                LThumbstickY2 = Reading.LeftThumbstickY;
                RThumbstickX2 = Reading.RightThumbstickX;
                RThumbstickY2 = Reading.RightThumbstickY;

                LTrigger2 = Reading.LeftTrigger;
                RTrigger2 = Reading.RightTrigger;


                switch (Reading.Buttons)
                {
                    case GamepadButtons.A:
                        button2 = "A";
                        break;
                    case GamepadButtons.B:
                        button2 = "B";
                        break;
                    case GamepadButtons.X:
                        button2 = "X";
                        break;
                    case GamepadButtons.Y:
                        button2 = "Y";
                        break;
                    default:
                        button2 = "null";
                        break;

                }

                label16.Text = ("Przycisk: " + button2);
                label15.Text = ("Lewa gałka X: " + LThumbstickX2.ToString("F3"));
                label14.Text = ("Lewa gałka Y: " + LThumbstickY2.ToString("F3"));
                label13.Text = ("Prawa gałka X: " + RThumbstickX2.ToString("F3"));
                label12.Text = ("Prawa gałka Y: " + RThumbstickY2.ToString("F3"));
                label11.Text = ("Lewy trigger: " + LTrigger2.ToString("F3"));
                label10.Text = ("Prawy trigger: " + RTrigger2.ToString("F3"));
                label9.Text = ("podłączono");

            }
            else
            {
                label16.Text = ("Przycisk: null");
                label15.Text = ("Lewa gałka X: null");
                label14.Text = ("Lewa gałka Y: null");
                label13.Text = ("Prawa gałka X: null");
                label12.Text = ("Prawa gałka Y: null");
                label11.Text = ("Lewy trigger: null");
                label10.Text = ("Prawy trigger: null");
                label9.Text = ("rozłączono");
            }

            if (checkBox1.Checked)
            {
                MouseMove((int)(LThumbstickX1 * MouseSensitive), (int)(-LThumbstickY1 * MouseSensitive), 0, 0);
                MouseClick();
                MouseScroll((int)(LThumbstickY1 * 100));
                changeSize(RThumbstickY1);

                if (button1 == "X")
                    g.Clear(Color.White);

            }
        }

        private async void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            await Log("Controller added");
        }

        private async void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            await Log("Controller removed");
        }

        private async Task Log(string txt)
        {
            Task t = Task.Run(() =>
            {
                Debug.WriteLine(DateTime.Now.ToShortTimeString() + ": " + txt);
            });

            await t;
        }

        private void MouseMove(int Lx, int Ly, int Px, int Py)
        {
            Cursor.Position = new Point(Cursor.Position.X + Lx, Cursor.Position.Y + Ly);
        }

        private void MouseClick()
        {
            

            if (lastButton1 == "null" && button1 == "A")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_LEFTDOWN , X, Y, 0, 0);
            }
            if (lastButton1 == "null" && button1 == "B")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_RIGHTDOWN , X, Y, 0, 0);
            }
            if (lastButton1 == "A" && button1 == "null")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            }
            if (lastButton1 == "B" && button1 == "null")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
            }

        }

        private void MouseScroll(int Y)
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, Y, 0);



        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            MouseSensitive = trackBar1.Value;
            label17.Text = ("Czułość : " + MouseSensitive.ToString());
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            last = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                current = e.Location;
                g.DrawLine(p, last, current);
                last = current;
            }

            if (e.Button == MouseButtons.Right)
            {
                current = e.Location;
                g.DrawLine(p2, last, current);
                last = current;
            }
        }

        void changeSize(double Y)
        {
            p.Width += (float)Y;  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }
    }
}
