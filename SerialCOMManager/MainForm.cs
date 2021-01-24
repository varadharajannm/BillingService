using Newtonsoft.Json;
using SerialManagerCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialCOMManager
{
    public partial class MainForm : Form
    {
        private string[] _serialPortList;
        private string _configFileName = string.Empty;
        delegate void ShowTextCallBack(string text);

        public MainForm()
        {
            try
            {
                InitializeComponent();
                this.Text = ConfigurationManager.AppSettings["ApplicationName"].ToString().Trim();
                string iconPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\favicon.ico";
                if (File.Exists(iconPath))
                    this.Icon = new Icon(iconPath);
            }
            catch(Exception ex)
            {
                Log.Input(ex);
            }
        }

        private string GetConfigFilePath()
        {
            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+ "\\Config\\COMDeviceConfig.json";
            if (!File.Exists(filePath))
                filePath = ConfigurationManager.AppSettings["ConfigFilePath"].ToString().Trim();
            return filePath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _configFileName = GetConfigFilePath();

                drpDeviceBaudRate.SelectedItem = "9600";
                _serialPortList = SerialPort.GetPortNames();

                DeviceSerialPort.Form = this;
                DeviceSerialPort.CallBackMethod = "CallBackDevicePortData";

                if (System.IO.File.Exists(_configFileName))
                {
                    string configData = System.IO.File.ReadAllText(_configFileName);
                    if (configData.Trim().Length > 0)
                    {
                        COMDeviceInfo deviceConfigInfo = JsonConvert.DeserializeObject<COMDeviceInfo>(configData);
                        txtDeviceCOM.Text = deviceConfigInfo.DevicePortName;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDevicePortAndOpen())
                {
                    DeviceSerialPort.WriteCmdToDevicePort("AT+RST");
                }
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDevicePortAndOpen())
                {
                    DeviceInfoForm deviceInfoForm = new DeviceInfoForm(this, false);
                    deviceInfoForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDevicePortAndOpen())
                {
                    DeviceInfoForm deviceInfoForm = new DeviceInfoForm(this, true);
                    deviceInfoForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private bool ValidateDevicePortAndOpen()
        {
            string devicePortName = txtDeviceCOM.Text.Trim();
            int baudRate = int.Parse(drpDeviceBaudRate.SelectedItem.ToString());

            if (devicePortName.Length > 0 && _serialPortList.Contains(devicePortName, StringComparer.OrdinalIgnoreCase))
            {
                DeviceSerialPort.Open(devicePortName, baudRate);
                return true;
            }
            else
            {
                MessageBox.Show(this, "Please enter valid device COMM");
                return false;
            }
        }

        public void CallBackDevicePortData(string data)
        {
            try
            {
                if (data != null && data.Contains("RST"))
                {
                    this.BeginInvoke(new ShowTextCallBack(ShowMessageBoxText), new object[] { "Device has been reset." });
                }
            }
            catch(Exception ex)
            {
                Log.Input(ex);
            }
        }

        private void ShowMessageBoxText(string text)
        {
            MessageBox.Show(this, text);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
