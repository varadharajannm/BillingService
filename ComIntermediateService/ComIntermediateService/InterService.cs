using Newtonsoft.Json;
using SerialManagerCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ComIntermediateService
{
    public partial class InterService : ServiceBase
    {
        private SerialPort _devicePort;
        private List<SerialPort> _virtualPorts;
        private List<VirtualPortInfo> _virtualPortInfoList;
        private string _configFileName = string.Empty;

        public InterService()
        {
            InitializeComponent();
        }

        private string GetConfigFilePath()
        {
            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Config\\COMDeviceConfig.json";
            if (!File.Exists(filePath))
                filePath = ConfigurationManager.AppSettings["ConfigFilePath"].ToString().Trim();
            return filePath;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _configFileName = GetConfigFilePath();
                if (System.IO.File.Exists(_configFileName))
                {
                    string configData = System.IO.File.ReadAllText(_configFileName);
                    if(configData.Trim().Length > 0)
                    {
                        COMDeviceInfo deviceConfigInfo = JsonConvert.DeserializeObject<COMDeviceInfo>(configData);

                        _devicePort = new SerialPort(deviceConfigInfo.DevicePortName, 9600, Parity.None, 8, StopBits.One);
                        _devicePort.Open();
                        _devicePort.DataReceived += new SerialDataReceivedEventHandler((sender, e) => DevicePortDataReceived(sender, e));

                        SerialPort virtualSerialPort = null;
                        _virtualPorts = new List<SerialPort>();
                        _virtualPortInfoList = new List<VirtualPortInfo>();

                        foreach (VCOMInfo com in deviceConfigInfo.VirtualDevices)
                        {
                            string vCOMPort = com.VirtualPortName.Replace("A", "B");
                            virtualSerialPort = new SerialPort(vCOMPort, 9600, Parity.None, 8, StopBits.One);
                            virtualSerialPort.Open();
                            virtualSerialPort.DataReceived += new SerialDataReceivedEventHandler((sender, e) => VirtualSerialPortDataReceived(sender, e, com.IPAddress));
                            _virtualPorts.Add(virtualSerialPort);

                            _virtualPortInfoList.Add(new VirtualPortInfo(vCOMPort, com.IPAddress));
                        }
                    }
                }
                else
                {
                    Log.Input("Config file doesn't exist");
                }
            }
            catch(Exception ex)
            {
                Log.Input(ex);
            }
        }

        private void VirtualSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e, string ipAddress)
        {
            try
            {
                SerialPort sp = sender as SerialPort;
                int bytes = sp.BytesToRead;
                byte[] buffer = new byte[bytes];
                sp.Read(buffer, 0, bytes);
                string data = Encoding.UTF8.GetString(buffer);

                byte[] outputBuffer = Encoding.UTF8.GetBytes(ipAddress + "-" + data);
                _devicePort.Write(outputBuffer, 0, outputBuffer.Length);
            }
            catch(Exception)
            {

            }
        }

        private void DevicePortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = sender as SerialPort;
                int bytes = sp.BytesToRead;
                byte[] buffer = new byte[bytes];
                sp.Read(buffer, 0, bytes);
                string data = Encoding.UTF8.GetString(buffer);

                string[] deviceData = data.Split('-');
                if (deviceData.Length > 0)
                {
                    string ipAddress = deviceData[0].Trim();
                    var portInfo = _virtualPortInfoList.Where(a => a.IPAddress == ipAddress).FirstOrDefault();
                    if (portInfo != null)
                    {
                        var virtualPort = _virtualPorts.Where(a => a.PortName == portInfo.PortName).FirstOrDefault();
                        if (virtualPort != null)
                        {
                            byte[] outputBuffer = Encoding.UTF8.GetBytes(deviceData[deviceData.Length - 1].Trim());
                            virtualPort.Write(outputBuffer, 0, outputBuffer.Length);
                        }
                    }
                }
            }
            catch(Exception)
            {

            }
        }

        protected override void OnStop()
        {
            try
            {
                if (_devicePort != null && _devicePort.IsOpen)
                    _devicePort.Close();

                foreach (SerialPort sp in _virtualPorts)
                {
                    if (sp != null && sp.IsOpen)
                        sp.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Input(ex);
            }
        }
    }

    public class VirtualPortInfo
    {
        public string PortName { get; set; }
        public string IPAddress { get; set; }

        public VirtualPortInfo(string portName, string ipAddress)
        {
            this.PortName = portName;
            this.IPAddress = ipAddress;
        }
    }
}
