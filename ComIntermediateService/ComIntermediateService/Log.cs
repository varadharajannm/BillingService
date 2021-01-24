using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComIntermediateService
{
    static class Log
    {
        public static void Input(string msg)
        {
            string logFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            System.IO.File.AppendAllLines(logFilePath + "\\Logs\\ComIntermediateServiceLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", new List<string> { msg });
        }

        public static void Input(Exception ex)
        {
            string logFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            System.IO.File.AppendAllLines(logFilePath + "\\Logs\\ComIntermediateServiceLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", new List<string> { ex.Message });
        }
    }
}
