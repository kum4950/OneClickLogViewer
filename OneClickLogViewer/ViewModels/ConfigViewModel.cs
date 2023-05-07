using OneClickLogViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickLogViewer.ViewModels
{
    internal class ConfigViewModel
    {
        public List<IDConfig> IDList { get; set; } = IDConfigs.IDConfigList;

        public ConfigViewModel()
        {
            
        }
    }
}
