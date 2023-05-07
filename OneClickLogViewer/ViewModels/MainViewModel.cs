using AvalonDock.Layout.Serialization;
using OneClickLogViewer.Core;
using OneClickLogViewer.Models;
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


        public RelayCommand NewCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }
        public RelayCommand LoadLayoutCommand { get; set; }
        public RelayCommand SaveLayoutCommand { get; set; }

        public RelayCommand Settings_Configuration_Command { get; set; }


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

            NewCommand = new RelayCommand(Workspace.This.NewCommand.Execute, Workspace.This.NewCommand.CanExecute);
            OpenCommand = new RelayCommand(Workspace.This.OpenCommand.Execute, Workspace.This.OpenCommand.CanExecute);
            
            LoadLayoutCommand = new RelayCommand(o => {
                var layoutSerializer = new XmlLayoutSerializer(Workspace.This.MyDockingManager);

                layoutSerializer.LayoutSerializationCallback += (s, e) =>
                {

                };
                layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
            },
            o => { 
                return File.Exists(@".\AvalonDock.Layout.config"); 
            });

            SaveLayoutCommand = new RelayCommand(o =>
            {
                var layoutSerializer = new XmlLayoutSerializer(Workspace.This.MyDockingManager);
                layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
            });

            Settings_Configuration_Command = new RelayCommand(o => {
                var configView = new ConfigView();
                configView.ShowDialog();
            });


        }
    }
}
