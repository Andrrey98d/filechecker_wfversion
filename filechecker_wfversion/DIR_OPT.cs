using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace filechecker_wfversion
{
    public class DIR_OPT : Form1
    {
        internal static string StartupPath { get; set; } = Environment.CurrentDirectory;
        public List<string> header_values_ = new List<string>();
        public void Main()
        {
          
        }
        static void sh_mes()
        {
            Console.WriteLine(StartupPath);
        }

      
        internal void Notify_error()
        {
            Console.WriteLine("error");
        }
 

    }
}



