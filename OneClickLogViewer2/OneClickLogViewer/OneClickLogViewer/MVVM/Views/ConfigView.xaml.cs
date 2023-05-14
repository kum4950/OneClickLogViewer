using MahApps.Metro.Controls;
using OneClickLogViewer.MVVM.ViewModels;

namespace OneClickLogViewer.MVVM.Views
{
    /// <summary>
    /// ConfigView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfigView : MetroWindow
    {
        public ConfigView()
        {
            InitializeComponent();
            this.DataContext = new ConfigViewModel();
        }
    }
}
