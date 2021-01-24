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
    public partial class Form2 : Form
    {
        SerialPort sp = null;
        delegate void SetTextCallback(string text);

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void SetText1(string text)
        {
            this.richTextBox1.AppendText(text + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sp = new SerialPort(textBox1.Text.Trim(), 9600, Parity.None, 8, StopBits.One);
            sp.Open();

            sp.DataReceived += Sp_DataReceived;

            button1.Enabled = false;
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            int bytes = sp.BytesToRead;
            byte[] buffer = new byte[bytes];
            sp.Read(buffer, 0, bytes);
            string data = Encoding.UTF8.GetString(buffer);

            this.BeginInvoke(new SetTextCallback(SetText1), new object[] { "From Server: " + data });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] outputBuffer = Encoding.UTF8.GetBytes(textBox2.Text.Trim());
            sp.Write(outputBuffer, 0, outputBuffer.Length);
            this.BeginInvoke(new SetTextCallback(SetText1), new object[] { textBox2.Text.Trim() });
        }
    }
}
