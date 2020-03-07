using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace PdfSample01
{
    public class CSiteUtility
    {
        public static void WriteLog(string title, string contents = "")
        {
            DateTime logTimeReal = DateTime.Now;
            string timeString = logTimeReal.ToString("HH:mm:ss.fff");

            StreamWriter output = new StreamWriter(File.Open(@"C:\Web\donus-saas-test4\__log\debug\PdfSample01.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
            output.WriteLine("[" + timeString + "] " + title
                + (contents != "" ? "\r\n" + Regex.Replace(contents, "(?<!\r)\n", "\r\n") + "\r\n" : ""));
            output.Close();
        }

    }
}
