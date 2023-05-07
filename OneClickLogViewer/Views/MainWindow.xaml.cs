using AvalonDock.Layout.Serialization;
using OneClickLogViewer.Core;
using OneClickLogViewer.Views;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Windwo Close");
            // UserControl의 Unloaded 이벤트 호출

        }
    }
}
