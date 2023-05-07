using ControlzEx.Standard;
using OneClickLogViewer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OneClickLogViewer.Views
{
    /// <summary>
    /// DocView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DocView : UserControl
    {
        public DocView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DocView_Loaded);
            this.Unloaded += new RoutedEventHandler(DocView_Unloaded);
        }

        void DocView_Loaded(object sender, RoutedEventArgs e)
        {

            if (App.startupArg.Length > 0)
            {
                string findstr = App.startupArg;

                var list= findstr.Split(',');

                int id = int.Parse(list[0]);
                Console.WriteLine(id);


                DateTime result;
                string input = list[1] +' '+ list[2];

                string format = "yy/MM/dd HH:mm:ss.fff";
                

                // DateTime.ParseExact() 메서드 사용
                result = DateTime.ParseExact(input, format, null);

                // DateTime.TryParseExact() 메서드 사용
                bool success = DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out result);

                if (success)
                {
                    Console.WriteLine(result.ToString(format)); // 출력 결과: 2023-04-27 20:15:44.209
                }
                else
                {
                    Console.WriteLine("변환 실패");
                }


                IDConfig idConfig = IDConfigs.IDConfigList.FirstOrDefault(c => c.ID == id);

                if (idConfig != null)
                {
                    Console.WriteLine(idConfig.ID);
                    Console.WriteLine(idConfig.AllPathName);

                    // path 처리
                    List<string> pathlist = new List<string>();
                    
                    for(int i=0; i<pathlist.Count; i++)
                    {
                        string s = pathlist[i];
                        //....
                    }

                    
                }
                else
                {
                    Console.WriteLine("ID Not Found");

                }

                //var file = @"C:\ID_Path.config";
                //var lines = File.ReadAllLines(file);
                //var list = new List<IDConfig>();

                //Console.WriteLine(lines.Length);

            }
            else
            {
                Console.WriteLine("loaded");
                var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);

                serializer.LayoutSerializationCallback += (s, args) =>
                {
                    args.Content = args.Content;
                };

                string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string fullPath = exePath + App.DockconfigPath;

                Console.WriteLine($"fullpath : {fullPath}");

                if (File.Exists(fullPath))
                {

                    Console.WriteLine("Loaded Serial");
                    serializer.Deserialize(fullPath);
                }
                else
                {
                    Console.WriteLine($"exePath : {exePath}");
                    Console.WriteLine("NO File");
                }
            }



            Window window = Window.GetWindow(this);
            window.Closing += window_Closing;

        }
        void window_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            //Save your settings here
            Console.WriteLine("window unloaded");

            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string fullPath = exePath + App.DockconfigPath;

            Console.WriteLine($"fullpath : {fullPath}");

            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(fullPath);
        }
        void DocView_Unloaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("unloaded");
            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(App.DockconfigPath);
        }
    }
}
