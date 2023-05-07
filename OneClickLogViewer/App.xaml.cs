using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ModernWpf;

namespace OneClickLogViewer
{
    public partial class App : Application
    {
        public static string startupArg = "";
        public static string DockconfigPath = @"\Config\AvalonDock.config";
        public static string ID_PathConfigPath = @"\Config\ID_Path.config";
        private void App_Startup(object sender, StartupEventArgs e)
        {

            Console.WriteLine($"DockconfigPath : {DockconfigPath}");
            Console.WriteLine($"ID_PathConfigPath : {ID_PathConfigPath}");

            // 전달된 인수 출력
            foreach (string arg in e.Args)
            {
                Console.WriteLine($"Startup: {arg}");
                startupArg = arg;

                Console.WriteLine($"startupArg : {startupArg}");

            }
        }
    }
}
