using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace filechecker_wfversion
{
    class DIR_OPT
    {
        internal static string StartupPath { get; set; } = Environment.CurrentDirectory;

        void sh_mes()
        {
            Console.WriteLine(StartupPath);
        }
    }
}
