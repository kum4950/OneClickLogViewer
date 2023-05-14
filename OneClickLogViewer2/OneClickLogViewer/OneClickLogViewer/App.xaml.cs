using ControlzEx.Theming;
using MahApps.Metro.Theming;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OneClickLogViewer
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static string startupArg = "";
        public static string ID_PathConfigPath = @"\Config\ID_Path.config";
        private void App_Startup(object sender, StartupEventArgs e)
        {

            Console.WriteLine($"ID_PathConfigPath : {ID_PathConfigPath}");

            // 전달된 인수 출력
            foreach (string arg in e.Args)
            {
                Console.WriteLine($"Startup: {arg}");
                startupArg = arg;

                Console.WriteLine($"startupArg : {startupArg}");
            }

            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(
                new Uri("pack://application:,,,/OneClickLogViewer;component/CustomAccents/Light.Accent.xaml"),
                MahAppsLibraryThemeProvider.DefaultInstance));



            //ThemeManager.Current.ChangeTheme(this, theme);
        }
    }
}
