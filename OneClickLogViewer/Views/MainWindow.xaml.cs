using AvalonDock.Layout.Serialization;
using OneClickLogViewer.Core;
using OneClickLogViewer.Views;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace OneClickLogViewer
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }



        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {

        }

    }
}
