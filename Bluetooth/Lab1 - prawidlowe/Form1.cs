using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Windows.Forms;


namespace Lab1___prawidlowe
{
    public partial class Form1 : Form
    {

        List<Device> devices = new List<Device>();
        Device selectedDevice = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            devices.Clear();
            listBox1.Items.Clear();
            listBox1.Items.Add("Szukam . . .");
            listBox1.Refresh();

            using (var bluetoothClient = new BluetoothClient())
            {
                var array = bluetoothClient.DiscoverDevices();
                var count = array.Length;
                for (var i = 0; i < count; i++)
                {
                    devices.Add(new Device(array[i]));
                }
            }



            listBox1.Items.Clear();
            foreach (Device D in devices)
                listBox1.Items.Add(D.DeviceName);



        }

        private void button2_Click(object sender, EventArgs e)
        {

            int tmpIndex = listBox1.SelectedIndex;

            textBox1.Text = devices[tmpIndex].DeviceInfo.DeviceAddress.ToString();
            textBox3.Text = devices[tmpIndex].DeviceName;
            checkBox1.Checked = devices[tmpIndex].IsConnected;
            if (devices[tmpIndex].IsAuthenticated)
            {
                button5.Enabled = false;
                checkBox2.Checked = devices[tmpIndex].IsAuthenticated;

            }
            else
                button5.Enabled = true;
            selectedDevice = devices[tmpIndex];

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            SelectBluetoothDeviceDialog dialog = new SelectBluetoothDeviceDialog();

            //    dialog.ShowAuthenticated = true;

            dialog.ShowRemembered = true;

            dialog.ShowUnknown = true;

            OpenFileDialog ofd = new OpenFileDialog();


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.Uri uri = new Uri("obex://" + selectedDevice.DeviceInfo.DeviceAddress + "/" + ofd.FileName);

                ObexWebRequest request = new ObexWebRequest(uri);

                request.ReadFile(ofd.FileName);


                ObexWebResponse response = (ObexWebResponse)request.GetResponse();

                MessageBox.Show(response.StatusCode.ToString());

                response.Close();

                Cursor.Current = Cursors.Default;

            }
            else
            {
                MessageBox.Show("File Not Selected");
            }






            /*
            string FileName = textBox2.Text;

            SelectBluetoothDeviceDialog dialog = new SelectBluetoothDeviceDialog();

            //    dialog.ShowAuthenticated = true;

            dialog.ShowRemembered = true;

            dialog.ShowUnknown = true;

            OpenFileDialog ofd = new OpenFileDialog();

                    System.Uri uri = new Uri("obex://" + selectedDevice.DeviceInfo.DeviceAddress + "/" + FileName);

                    ObexWebRequest request = new ObexWebRequest(uri);

                    request.ReadFile(FileName);


                    ObexWebResponse response = (ObexWebResponse)request.GetResponse();

                    MessageBox.Show(response.StatusCode.ToString());

                    response.Close();

                    Cursor.Current = Cursors.Default;




            /*

            string tmpWiadomosc = textBox2.Text;

            if (selectedDevice == null)
            {
                MessageBox.Show("Nie znaleziono urządzenia !");
            }
            else
            if (string.IsNullOrEmpty(tmpWiadomosc))
            {
                MessageBox.Show("Nie wprowadzono tekstu !");
            }
            else
            {
                Guid _serviceClassId = new Guid("9bde4762-89a6-418e-bacf-fcd82f1e0677");
                //Guid _serviceClassId = new Guid("00000000-0000-1000-8000-00805F9B34FB");
                //Guid _serviceClassId = ;



                var bluetoothClient = new BluetoothClient();
                var ep = new BluetoothEndPoint(selectedDevice.DeviceInfo.DeviceAddress, _serviceClassId);



                // connecting
                bluetoothClient.Connect(ep);



                // get stream for send the data
                var bluetoothStream = bluetoothClient.GetStream();

                // if all is ok to send
                if (bluetoothClient.Connected && bluetoothStream != null)
                {
                    // write the data in the stream
                    var buffer = System.Text.Encoding.UTF8.GetBytes(tmpWiadomosc);
                    bluetoothStream.Write(buffer, 0, buffer.Length);
                    bluetoothStream.Flush();
                    bluetoothStream.Close();

                }
                else
                    MessageBox.Show("Wystąpił problem z połączeniem...");



            }
            */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (BluetoothSecurity.PairRequest(selectedDevice.DeviceInfo.DeviceAddress, "123456"))
                checkBox2.Checked = true;
            else
                MessageBox.Show("Nie udało się parować.");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectBluetoothDeviceDialog dialog = new SelectBluetoothDeviceDialog();

            //    dialog.ShowAuthenticated = true;

            dialog.ShowRemembered = true;

            dialog.ShowUnknown = true;

            OpenFileDialog ofd = new OpenFileDialog();


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.Uri uri = new Uri("obex://" + selectedDevice.DeviceInfo.DeviceAddress + "/" + ofd.FileName);

                ObexWebRequest request = new ObexWebRequest(uri);

                request.ReadFile(ofd.FileName);


                ObexWebResponse response = (ObexWebResponse)request.GetResponse();

                MessageBox.Show(response.StatusCode.ToString());

                response.Close();

                Cursor.Current = Cursors.Default;

            }
            else
            {
                MessageBox.Show("File Not Selected");
            }
        }


    }
}




