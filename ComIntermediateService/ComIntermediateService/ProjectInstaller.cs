using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace ComIntermediateService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            // Determine whether the environment variable exists.
            if (Environment.GetEnvironmentVariable("CNC_INSTALL_START_MENU_SHORTCUTS") == null)
            {
                // If it doesn't exist, create it.
                Environment.SetEnvironmentVariable("CNC_INSTALL_START_MENU_SHORTCUTS", "NO");
            }

            string targetDir = this.Context.Parameters["targetdir"];
            System.IO.Directory.CreateDirectory(targetDir + "Config");
            System.IO.Directory.CreateDirectory(targetDir + "Logs");

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C \"" + targetDir + "setup_com0com_W7_x64_signed.exe\" /S /D=" + targetDir;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            string targetDir = this.Context.Parameters["targetdir"];
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C \"" + targetDir + "uninstall.exe\" /S";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            string configFolder = targetDir + "Config";
            if (System.IO.Directory.Exists(configFolder))
            {
                System.IO.Directory.Delete(configFolder);
            }

            string logFolder = targetDir + "Logs";
            if (System.IO.Directory.Exists(logFolder))
            {
                System.IO.Directory.Delete(logFolder);
            }

            base.Uninstall(savedState);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }
    }
}
