using Newtonsoft.Json;
using SerialManagerCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialCOMManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            try
            {
                PopupForm popupForm = new PopupForm("Initializing settings for the first time. This may take some time");
                popupForm.Show();
                InstallPorts();
                popupForm.Close();

                //stop COM intermediate service for the appliation if it is running.
                ServiceController service = new ServiceController("ComIntermediateService");
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                }
            }
            catch (Exception ex)
            {
                Log.Input(ex);
            }

            Application.Run(new MainForm());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                string configFileName = GetConfigFilePath();
                if (File.Exists(configFileName))
                {
                    string configData = File.ReadAllText(configFileName);
                    if (configData.Trim().Length > 0)
                    {
                        COMDeviceInfo deviceConfigInfo = JsonConvert.DeserializeObject<COMDeviceInfo>(configData);
                        if (deviceConfigInfo.VirtualDevices != null && deviceConfigInfo.VirtualDevices.Count > 0)
                        {
                            //start COM intermediate service for the appliation once the configuration settings are completed
                            ServiceController service = new ServiceController("ComIntermediateService");
                            if (service.Status != ServiceControllerStatus.Running)
                            {
                                service.Start();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Log.Input(ex);
            }
        }

        private static void InstallPorts()
        {
            string[] allSerialPorts = SerialPort.GetPortNames();
            if (!allSerialPorts.Contains("COMA1"))
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                if (File.Exists(filePath + "\\setupc.exe"))
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = filePath + "\\setupc.exe";
                        startInfo.Arguments = "Install PortName=COMA" + i + " PortName=COMB" + i;
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();
                    }
                }
            }
        }

        private static string GetConfigFilePath()
        {
            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string[] folders = filePath.Split("\\".ToCharArray()).Reverse().ToArray();
            foreach (string folder in folders)
            {
                int index = filePath.IndexOf(folder);
                string folderPath = filePath.Substring(0, index + folder.Length);
                string[] subFolders = Directory.GetDirectories(folderPath);
                foreach (string subFolder in subFolders)
                {
                    if (subFolder.Contains("Config"))
                    {
                        filePath = subFolder + "\\COMDeviceConfig.json";
                        return filePath;
                    }
                }
            }
            return filePath;
        }
    }
}
