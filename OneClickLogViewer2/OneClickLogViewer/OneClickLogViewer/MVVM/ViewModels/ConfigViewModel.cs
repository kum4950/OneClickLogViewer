using OneClickLogViewer.MVVM.Models;
using System.Collections.Generic;

namespace OneClickLogViewer.MVVM.ViewModels
{
    internal class ConfigViewModel
    {
        public List<IDConfig> IDList { get; set; } = IDConfigs.IDConfigList;

        public ConfigViewModel()
        {

        }
    }
}
