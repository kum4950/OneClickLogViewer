using AvalonDock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickLogViewer.Models
{
    internal class IDConfigs
    {
        protected IDConfigs() { }

        static IDConfigs _this = new IDConfigs();

        public static IDConfigs This
        {
            get { return _this; }
        }

        public static List<IDConfig> IDConfigList { get; set; } = GetIDConfigs();

        public static List<IDConfig> GetIDConfigs()
        {
            var file = @"C:\ID_Path.config";
            var lines = File.ReadAllLines(file);
            var list = new List<IDConfig>();
            
            Console.WriteLine(lines.Length);


            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(',');

                List<IDtoPath> pathlist = new List<IDtoPath>();
                string allpathName = "";

                for(int j = 1; j < line.Length; j++)
                {
                    var idtopath = new IDtoPath() { PathName = line[j] };
                    pathlist.Add(idtopath);
                    allpathName += line[j] + ',';
                    Console.WriteLine(line[j]);

                }

                var idConfig = new IDConfig()
                {
                    ID = int.Parse(line[0]),
                    AllPathName = allpathName,
                    PathList = pathlist,
                };

                list.Add(idConfig);
            }
            return list;
        }
    }




    public class IDtoPath
    {
        public string? PathName { get; set; }
    }

    public class IDConfig
    {
        public int ID { get; set; }
        public string? AllPathName { get; set; }
        public List<IDtoPath>? PathList { get; set; }
    }
}
