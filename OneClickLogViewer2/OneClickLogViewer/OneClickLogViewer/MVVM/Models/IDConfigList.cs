﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickLogViewer.MVVM.Models
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
            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string fullPath = exePath + App.ID_PathConfigPath;

            var lines = File.ReadAllLines(fullPath);


            var list = new List<IDConfig>();

            Console.WriteLine(lines.Length);


            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(',');

                List<string> pathlist = new List<string>();
                string allpathName = "";

                Console.WriteLine(int.Parse(line[0]));

                for (int j = 1; j < line.Length; j++)
                {
                    pathlist.Add(line[j]);
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


    public class IDConfig
    {
        public int ID { get; set; }
        public string AllPathName { get; set; }
        public List<string> PathList { get; set; }
    }
}
