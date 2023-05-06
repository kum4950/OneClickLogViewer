using AvalonDock.Layout.Serialization;
using OneClickLogViewer.Core;
using OneClickLogViewer.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OneClickLogViewer.ViewModels
{
    class MainViewModel : ViewModelBase
    {

        /*Command*/
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutdownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }


        public RelayCommand LoadLayoutCommand { get; set; }

        public RelayCommand NewCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }




        public object CurrentView { get; set; }
        public DocViewModel DocVM { get; set; }

        public MainViewModel()
        {
            DocVM = new DocViewModel();
            CurrentView = DocVM;


            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutdownWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            MaximizeWindowCommand = new RelayCommand(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });
            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });

            NewCommand = new RelayCommand(DocVM.NewCommand.Execute,DocVM.NewCommand.CanExecute);
            OpenCommand = new RelayCommand(DocVM.OpenCommand.Execute, DocVM.OpenCommand.CanExecute);




        }
    }
}
