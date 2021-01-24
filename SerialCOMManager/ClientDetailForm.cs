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
    public partial class ClientDetailForm : Form
    {
        private List<string> _serialPortList = new List<string>();
        private string _configFileName = string.Empty;
        private delegate void SetClientDetailsCallBack(string[] devices);
        private bool _saveVCOMDetails = false;

        public ClientDetailForm()
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
            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Config\\COMDeviceConfig.json";
            if (!File.Exists(filePath))
                filePath = ConfigurationManager.AppSettings["ConfigFilePath"].ToString().Trim();
            return filePath;
        }

        private void ClientDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                _configFileName = GetConfigFilePath();

                DeviceSerialPort.Form = this;
                DeviceSerialPort.CallBackMethod = "CallBackDevicePortData";

                //get connected client details for the device port
                DeviceSerialPort.WriteCmdToDevicePort("AT+GETCLIENTDETAIL");

                int index = 0;
                string[] allSerialPorts = SerialPort.GetPortNames();
                allSerialPorts = allSerialPorts.OrderBy(a => a).ToArray();
                foreach (string port in allSerialPorts)
                {
                    if (port.Contains("COMA"))
                    {
                        drpVCOM1.Items.Add(port);
                        drpVCOM2.Items.Add(port);
                        drpVCOM3.Items.Add(port);
                        drpVCOM4.Items.Add(port);
                        drpVCOM5.Items.Add(port);

                        switch (index)
                        {
                            case 0: drpVCOM1.SelectedItem = port; break;
                            case 1: drpVCOM2.SelectedItem = port; break;
                            case 2: drpVCOM3.SelectedItem = port; break;
                            case 3: drpVCOM4.SelectedItem = port; break;
                            case 4: drpVCOM5.SelectedItem = port; break;
                            default: break;
                        }

                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            PopupForm popupForm = null;
            try
            {
                btnFinish.Enabled = false;
                popupForm = new PopupForm("Resetting virtual ports. please wait.");

                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (!_saveVCOMDetails)
                    {
                        MessageBox.Show(this, "Please fix errors before continue");
                        return;
                    }
                }

                popupForm.Show(this);

                COMDeviceInfo deviceInfo = new COMDeviceInfo();
                deviceInfo.DevicePortName = DeviceSerialPort.Port;

                VCOMInfo vCOMInfo = new VCOMInfo();
                if (txtDeviceName1.Text.Trim().Length > 0)
                {
                    vCOMInfo.DeviceName = txtDeviceName1.Text;
                    vCOMInfo.IPAddress = txtIPAddress1.Text;
                    vCOMInfo.VirtualPortName = drpVCOM1.SelectedItem.ToString();
                    deviceInfo.VirtualDevices.Add(vCOMInfo);
                }

                if (txtDeviceName2.Text.Trim().Length > 0)
                {
                    vCOMInfo = new VCOMInfo();
                    vCOMInfo.DeviceName = txtDeviceName2.Text;
                    vCOMInfo.IPAddress = txtIPAddress2.Text;
                    vCOMInfo.VirtualPortName = drpVCOM2.SelectedItem.ToString();
                    deviceInfo.VirtualDevices.Add(vCOMInfo);
                }

                if (txtDeviceName3.Text.Trim().Length > 0)
                {
                    vCOMInfo = new VCOMInfo();
                    vCOMInfo.DeviceName = txtDeviceName3.Text;
                    vCOMInfo.IPAddress = txtIPAddress3.Text;
                    vCOMInfo.VirtualPortName = drpVCOM3.SelectedItem.ToString();
                    deviceInfo.VirtualDevices.Add(vCOMInfo);
                }

                if (txtDeviceName4.Text.Trim().Length > 0)
                {
                    vCOMInfo = new VCOMInfo();
                    vCOMInfo.DeviceName = txtDeviceName4.Text;
                    vCOMInfo.IPAddress = txtIPAddress4.Text;
                    vCOMInfo.VirtualPortName = drpVCOM4.SelectedItem.ToString();
                    deviceInfo.VirtualDevices.Add(vCOMInfo);
                }

                if (txtDeviceName5.Text.Trim().Length > 0)
                {
                    vCOMInfo = new VCOMInfo();
                    vCOMInfo.DeviceName = txtDeviceName5.Text;
                    vCOMInfo.IPAddress = txtIPAddress5.Text;
                    vCOMInfo.VirtualPortName = drpVCOM5.SelectedItem.ToString();
                    deviceInfo.VirtualDevices.Add(vCOMInfo);
                }

                if (deviceInfo.VirtualDevices.Count > 0)
                {
                    //log COM config data into JSON file
                    File.WriteAllText(_configFileName, JsonConvert.SerializeObject(deviceInfo));

                    string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    if (File.Exists(filePath+"\\setupc.exe"))
                    {
                        try
                        {
                            int portIndex = 0;
                            foreach (VCOMInfo vCOM in deviceInfo.VirtualDevices)
                            {
                                int i = drpVCOM1.Items.IndexOf(vCOM.VirtualPortName);
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = filePath + "\\setupc.exe";
                                startInfo.Arguments = "remove " + i;
                                process.StartInfo = startInfo;
                                process.Start();
                                process.WaitForExit();
                                portIndex++;

                                process = new System.Diagnostics.Process();
                                startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = filePath + "\\setupc.exe";
                                startInfo.Arguments = "Install PortName=" + vCOM.VirtualPortName + " PortName=" + vCOM.VirtualPortName.Replace("A", "B");
                                process.StartInfo = startInfo;
                                process.Start();
                                process.WaitForExit();
                            }
                        }
                        catch(Exception ex)
                        {
                            Log.Input(ex);
                            MessageBox.Show(this, "Failed to recreate virtual ports");
                            return;
                        }
                    }

                    popupForm.Close();
                    MessageBox.Show(this, "Device setup is completed");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(this, "No devices available");
                }
                btnFinish.Enabled = true;
            }
            catch (Exception ex)
            {
                if (popupForm != null)
                    popupForm.Close();
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnFinish.Enabled = true;
            }
        }

        private void btnGetClientDetails_Click(object sender, EventArgs e)
        {
            try
            {
                //get connected client details for the device port
                DeviceSerialPort.WriteCmdToDevicePort("AT+GETCLIENTDETAIL");
            }
            catch(Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        public void CallBackDevicePortData(string data)
        {
            try
            {
                if (data != null && data.Contains("GETCLIENTDETAIL"))
                {
                    string[] devices = data.Replace("GETCLIENTDETAIL:", "").Split(',');
                    this.BeginInvoke(new SetClientDetailsCallBack(SetClientDetailValues), new object[] { devices });
                }
            }
            catch(Exception ex)
            {
                Log.Input(ex);
            }
        }

        private void SetClientDetailValues(string[] devices)
        {
            try
            {
                string[] deviceInfo;
                COMDeviceInfo deviceConfigInfo = new COMDeviceInfo();

                if (File.Exists(_configFileName))
                {
                    string configData = File.ReadAllText(_configFileName);
                    if (configData.Trim().Length > 0)
                        deviceConfigInfo = JsonConvert.DeserializeObject<COMDeviceInfo>(configData);
                }

                bool missingInformation = false;
                if (devices.Length > 0)
                {
                    deviceInfo = devices[0].Split('-');
                    if (deviceInfo.Length > 1)
                    {
                        txtDeviceName1.Text = deviceInfo[0].Trim();
                        txtIPAddress1.Text = deviceInfo[1].Trim();
                        if (deviceConfigInfo.VirtualDevices.Count > 0)
                            drpVCOM1.SelectedItem = deviceConfigInfo.VirtualDevices[0].VirtualPortName;
                    }
                    else
                    {
                        missingInformation = true;
                    }
                }

                if (devices.Length > 1)
                {
                    deviceInfo = devices[1].Split('-');
                    if (deviceInfo.Length > 1)
                    {
                        txtDeviceName2.Text = deviceInfo[0].Trim();
                        txtIPAddress2.Text = deviceInfo[1].Trim();
                        if (deviceConfigInfo.VirtualDevices.Count > 1)
                            drpVCOM2.SelectedItem = deviceConfigInfo.VirtualDevices[1].VirtualPortName;
                    }
                    else
                    {
                        missingInformation = true;
                    }
                }

                if (devices.Length > 2)
                {
                    deviceInfo = devices[2].Split('-');
                    if (deviceInfo.Length > 1)
                    {
                        txtDeviceName3.Text = deviceInfo[0].Trim();
                        txtIPAddress3.Text = deviceInfo[1].Trim();
                        if (deviceConfigInfo.VirtualDevices.Count > 2)
                            drpVCOM3.SelectedItem = deviceConfigInfo.VirtualDevices[2].VirtualPortName;
                    }
                    else
                    {
                        missingInformation = true;
                    }
                }

                if (devices.Length > 3)
                {
                    deviceInfo = devices[3].Split('-');
                    if (deviceInfo.Length > 1)
                    {
                        txtDeviceName4.Text = deviceInfo[0].Trim();
                        txtIPAddress4.Text = deviceInfo[1].Trim();
                        if (deviceConfigInfo.VirtualDevices.Count > 3)
                            drpVCOM4.SelectedItem = deviceConfigInfo.VirtualDevices[3].VirtualPortName;
                    }
                    else
                    {
                        missingInformation = true;
                    }
                }

                if (devices.Length > 4)
                {
                    deviceInfo = devices[4].Split('-');
                    if (deviceInfo.Length > 1)
                    {
                        txtDeviceName5.Text = deviceInfo[0].Trim();
                        txtIPAddress5.Text = deviceInfo[1].Trim();
                        if (deviceConfigInfo.VirtualDevices.Count > 4)
                            drpVCOM5.SelectedItem = deviceConfigInfo.VirtualDevices[4].VirtualPortName;
                    }
                    else
                    {
                        missingInformation = true;
                    }
                }

                if (missingInformation) MessageBox.Show(this, "Some of client detail is missing - " + string.Join(",", devices));
                if (devices.Length == 0) MessageBox.Show(this, "Client detail is not available");
            }
            catch(Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }

        private void ClientDetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void drpVCOM1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> selectedVCOMs = new List<string>();
                _saveVCOMDetails = true;

                if (drpVCOM1.SelectedItem != null)
                    selectedVCOMs.Add(drpVCOM1.SelectedItem.ToString());

                if (drpVCOM2.SelectedItem != null)
                {
                    if (selectedVCOMs.Contains(drpVCOM2.SelectedItem.ToString()))
                    {
                        errorProvider1.SetError(drpVCOM2, "VCOM already selected");
                        _saveVCOMDetails = false;
                    }
                    else
                    {
                        selectedVCOMs.Add(drpVCOM2.SelectedItem.ToString());
                        errorProvider1.SetError(drpVCOM2, "");
                        if (_saveVCOMDetails) _saveVCOMDetails = true;
                    }
                }

                if (drpVCOM3.SelectedItem != null)
                {
                    if (selectedVCOMs.Contains(drpVCOM3.SelectedItem.ToString()))
                    {
                        errorProvider1.SetError(drpVCOM3, "VCOM already selected");
                        _saveVCOMDetails = false;
                    }
                    else
                    {
                        selectedVCOMs.Add(drpVCOM3.SelectedItem.ToString());
                        errorProvider1.SetError(drpVCOM3, "");
                        if (_saveVCOMDetails) _saveVCOMDetails = true;
                    }
                }

                if (drpVCOM4.SelectedItem != null)
                {
                    if (selectedVCOMs.Contains(drpVCOM4.SelectedItem.ToString()))
                    {
                        errorProvider1.SetError(drpVCOM4, "VCOM already selected");
                        _saveVCOMDetails = false;
                    }
                    else
                    {
                        selectedVCOMs.Add(drpVCOM4.SelectedItem.ToString());
                        errorProvider1.SetError(drpVCOM4, "");
                        if (_saveVCOMDetails) _saveVCOMDetails = true;
                    }
                }

                if (drpVCOM5.SelectedItem != null)
                {
                    if (selectedVCOMs.Contains(drpVCOM5.SelectedItem.ToString()))
                    {
                        errorProvider1.SetError(drpVCOM5, "VCOM already selected");
                        _saveVCOMDetails = false;
                    }
                    else
                    {
                        selectedVCOMs.Add(drpVCOM5.SelectedItem.ToString());
                        errorProvider1.SetError(drpVCOM5, "");
                        if (_saveVCOMDetails) _saveVCOMDetails = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Input(ex);
                MessageBox.Show(this, ex.Message);
            }
        }
    }
}
