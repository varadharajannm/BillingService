using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPortCommunicationTest
{
    public partial class Form1 : Form
    {
        private SerialPort port1 = new SerialPort("COM1",
      9600, Parity.None, 8, StopBits.One);

        private SerialPort port2 = new SerialPort("COM2",
      9600, Parity.None, 8, StopBits.One);

        List<SerialPort> serialPorts = new List<SerialPort>();
        string[] vCOMList;
        bool comEvent = false;

        delegate void SetTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] portAvailable = SerialPort.GetPortNames();

            vCOMList = System.IO.File.ReadAllLines(@"C:\Users\Varadharajan\Desktop\VCOMList.csv");
            foreach(string vCOMInfo in vCOMList)
            {
                string[] vCOM = vCOMInfo.Split(',');

                SerialPort sp = new SerialPort(vCOM[0], 9600, Parity.None, 8, StopBits.One);
                sp.Open();
                sp.DataReceived += new SerialDataReceivedEventHandler((a, b) => SerialPortDataReceived(a, b, vCOM[2], vCOM[3]));
                serialPorts.Add(sp);

                sp = new SerialPort(vCOM[1], 9600, Parity.None, 8, StopBits.One);
                sp.Open();
                serialPorts.Add(sp);

                comboBox1.Items.Add(vCOM[3]);
            }

            port1.Open();
            port2.Open();

            port2.DataReceived += Port2_DataReceived;
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e, string ip, string vCOMDisplayName)
        {
            SerialPort sp = sender as SerialPort;
            string data = sp.ReadLine();
            this.BeginInvoke(new SetTextCallback(SetText1), new object[] { vCOMDisplayName + ":" + data });
            if(!comEvent)
                port1.WriteLine(ip + "-" + data.Length + "-" + data);
        }

        private void Port2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port2.ReadLine();
            this.BeginInvoke(new SetTextCallback(SetText2), new object[] { data });
            if(data.IndexOf('-') != -1 && comEvent)
            {
                var selectedItem = vCOMList.Where(a => a.Contains(data.Split('-')[0])).FirstOrDefault();
                if (selectedItem != null)
                {
                    var selectedPort = serialPorts.Where(a => a.PortName == selectedItem.Split(',')[1]).FirstOrDefault();
                    if (selectedPort != null)
                        selectedPort.WriteLine(data.Replace(selectedItem.Split(',')[2], "").Replace("-",""));
                }
            }
        }

        private void SetText1(string text)
        {
            this.richTextBox1.AppendText(text + "\n");
        }

        private void SetText2(string text)
        {
            this.richTextBox2.AppendText(text + "\n");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            port1.Close();
            port2.Close();       }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = textBox1.Text.Trim();
            var selectedItem = vCOMList.Where(a => a.Contains(comboBox1.SelectedItem.ToString())).FirstOrDefault();
            var selectedPort = serialPorts.Where(a => a.PortName == selectedItem.Split(',')[1]).FirstOrDefault();
            if (selectedPort != null)
            {
                selectedPort.WriteLine(data);
            }
            comEvent = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data = textBox1.Text.Trim();
            var selectedItem = vCOMList.Where(a=>a.Contains(comboBox1.SelectedItem.ToString())).FirstOrDefault();
            if (selectedItem != null)
            {
                port1.WriteLine(selectedItem.Split(',')[2] + "-" + data);
            }
            comEvent = true;
        }
    }
}
