using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialCOMManager
{
    public static class DeviceSerialPort
    {
        private static SerialPort _devicePort = null;
        public static string CallBackMethod = string.Empty;
        public static Form Form = null;
        public static string Port = string.Empty;

        public static void Open(string portName, int baudRate)
        {
            try
            {
                if (_devicePort == null || Port != portName)
                {
                    if (_devicePort != null && _devicePort.IsOpen)
                        _devicePort.Close();

                    Port = portName;
                    _devicePort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                    _devicePort.Open();
                    _devicePort.DataReceived += new SerialDataReceivedEventHandler((sender, e) => DevicePortDataReceived(sender, e));
                }
            }
            catch (Exception)
            {
                _devicePort = null;
                throw;
            }
        }

        public static bool WriteCmdToDevicePort(string msg)
        {
            if (_devicePort.IsOpen)
            {
                byte[] outputBuffer = Encoding.UTF8.GetBytes(msg);
                _devicePort.Write(outputBuffer, 0, outputBuffer.Length);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void DevicePortDataReceived(object sender, SerialDataReceivedEventArgs e)
        { 
            SerialPort sp = sender as SerialPort;
            int bytes = sp.BytesToRead;
            byte[] buffer = new byte[bytes];
            sp.Read(buffer, 0, bytes);
            string data = Encoding.UTF8.GetString(buffer);

            MethodInfo method = Form.GetType().GetMethod(CallBackMethod);
            method.Invoke(Form, new List<object>() { data }.ToArray());
        }
    }
}
