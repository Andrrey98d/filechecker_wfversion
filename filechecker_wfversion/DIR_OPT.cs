using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace filechecker_wfversion
{
    public class DIR_OPT
    {
        internal static string StartupPath { get; set; } = Environment.CurrentDirectory;
        public List<string> header_values_ = new List<string>();
       
        public void Main()
        {
            ARR_FILL();
        }
        static void sh_mes()
        {
            Console.WriteLine(StartupPath);
        }

        public void ARR_FILL()
        {
            string[] header = {
                "Cигнатура",
                "Использование",
                "Порт/протокол"
            };
                header_values_.AddRange(header);
            
        }
        internal void Notify_error()
        {
            Console.WriteLine("error");
        }
    }
}
