using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialCOMManager
{
    public partial class DeviceInfoForm : Form
    {
        private bool _setConfig = true;
        private Form _backForm;
        private bool _readOnlyMode = false;
        private bool _backClick = false;

        private delegate void ShowTextCallBack(string text);
        private delegate void SetDeviceInfoCallBack(string[] deviceInfo);

        public DeviceInfoForm(Form backForm, bool readOnlyMode)
        {
            try
            {
                InitializeComponent();
                this.Text = ConfigurationManager.AppSettings["ApplicationName"].ToString().Trim();
                string iconPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\favicon.ico";
                if (File.Exists(iconPath))
                    this.Icon = new Icon(iconPath);
                _backForm = backForm;
                _readOnlyMode = readOnlyMode;
            }
            catch(Exception ex)
            {
                Log.Input(ex);
            }
        }

        private void DeviceInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                drpDeviceBaudRate.SelectedItem = "9600";
                drpDeviceMode.SelectedItem = "Server";

                DeviceSerialPort.Form = this;
                DeviceSerialPort.CallBackMethod = "CallBackDevicePortData";

                //Get device config details on load
                DeviceSerialPort.WriteCmdToDevicePort("AT+GETCONFIG");

                if (_readOnlyMode)
                {
                    btnSetConfig.Visible = false;
                    btnGetConfig.Visible = false;
                    btnNext.Visible = false;
                    drpDeviceBaudRate.Enabled = false;
                    drpDeviceMode.Enabled = false;
                    txtDeviceName.Enabled = false;
                    txtDeviceIPAddress.Enabled = false;
                    txtDeviceSSID.Enabled = false;
                    txtDevicePassword.Enabled = false;
                    txtDeviceBufferSize.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnGetConfig_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceSerialPort.WriteCmdToDevicePort("AT+GETCONFIG");
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnSetConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (!_setConfig)
                    {
                        MessageBox.Show(this, "Please fix errors before continue");
                        return;
                    }
                }

                string deviceName = txtDeviceName.Text.Trim();
                string deviceBaudRate = drpDeviceBaudRate.SelectedItem.ToString();
                string deviceMode = drpDeviceMode.SelectedItem.ToString();
                string ipAddress = txtDeviceIPAddress.Text.Trim();
                string SSID = txtDeviceSSID.Text.Trim();
                string password = txtDevicePassword.Text.Trim();
                string bufferSize = txtDeviceBufferSize.Text.Trim();

                string data = deviceName + "," + deviceBaudRate + "," + deviceMode + "," + ipAddress + "," + SSID + "," + password + "," + bufferSize;
                DeviceSerialPort.WriteCmdToDevicePort("AT+SETCONFIG:" + data);
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Text == "Next")
            {
                ClientDetailForm clientDetailForm = new ClientDetailForm();
                clientDetailForm.Show();
                this.Hide();
            }
            else
            {
                Application.Exit();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _backClick = true;
            _backForm.Show();
            this.Close();
        }

        private void drpDeviceMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpDeviceMode.SelectedItem.ToString() == "Server")
            {
                btnNext.Text = "Next";
            }
            else
            {
                btnNext.Text = "Finish";
            }
        }

        public void CallBackDevicePortData(string data)
        {
            try
            {
                if (data != null)
                {
                    if (data.Contains("GETCONFIG"))
                    {
                        string[] deviceInfo = data.Replace("GETCONFIG:", "").Split(',');
                        this.BeginInvoke(new SetDeviceInfoCallBack(SetDeviceInfoValues), new object[] { deviceInfo });
                    }
                    else if (data.Contains("SETCONFIG"))
                    {
                        this.BeginInvoke(new ShowTextCallBack(ShowMessageBoxText), new object[] { "Device configuration has been set." });
                    }
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

        private void SetDeviceInfoValues(string[] deviceInfo)
        {
            try
            {
                int inputLength = deviceInfo.Length;
                if (inputLength > 0)
                    txtDeviceName.Text = deviceInfo[0].Trim(); //device name
                if (inputLength > 1)
                    drpDeviceBaudRate.SelectedItem = deviceInfo[1].Trim(); //baud rate
                if (inputLength > 2)
                    drpDeviceMode.SelectedItem = deviceInfo[2].Trim(); //device mode
                if (inputLength > 3)
                    txtDeviceIPAddress.Text = deviceInfo[3].Trim(); //device IP address
                if (inputLength > 4)
                    txtDeviceSSID.Text = deviceInfo[4].Trim(); //device SSID
                if (inputLength > 5)
                    txtDevicePassword.Text = deviceInfo[5].Trim(); //device password
                if (inputLength > 6)
                    txtDeviceBufferSize.Text = deviceInfo[6].Trim(); //device buffer size

                if (inputLength <= 6) MessageBox.Show(this, "Some of device information is missing - " + string.Join(",", deviceInfo));
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void DeviceInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!_backClick)
                Application.Exit();
        }

        private void txtDeviceName_TextChanged(object sender, EventArgs e)
        {
            if (txtDeviceName.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDeviceName, "Missing Device Name");
                _setConfig = false;
            }
            else
            {
                errorProvider1.SetError(txtDeviceName, "");
                _setConfig = true;
            }
        }

        private void txtDeviceIPAddress_TextChanged(object sender, EventArgs e)
        {
            if (txtDeviceIPAddress.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDeviceIPAddress, "Missing IP Address");
                _setConfig = false;
            }
            else
            {
                errorProvider1.SetError(txtDeviceIPAddress, "");
                _setConfig = true;
            }
        }

        private void txtDeviceSSID_TextChanged(object sender, EventArgs e)
        {
            if (txtDeviceSSID.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDeviceSSID, "Missing SSID");
                _setConfig = false;
            }
            else
            {
                errorProvider1.SetError(txtDeviceSSID, "");
                _setConfig = true;
            }
        }

        private void txtDevicePassword_TextChanged(object sender, EventArgs e)
        {
            if (txtDevicePassword.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDevicePassword, "Missing Password");
                _setConfig = false;
            }
            else
            {
                errorProvider1.SetError(txtDevicePassword, "");
                _setConfig = true;
            }
        }

        private void txtDeviceBufferSize_TextChanged(object sender, EventArgs e)
        {
            if (txtDeviceBufferSize.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDeviceBufferSize, "Missing Buffer size");
                _setConfig = false;
            }
            else
            {
                errorProvider1.SetError(txtDeviceBufferSize, "");
                _setConfig = true;
            }
        }
    }
}
