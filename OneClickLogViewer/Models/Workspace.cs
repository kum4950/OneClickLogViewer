﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.IO;
using System.Linq;
using OneClickLogViewer.ViewModels;
using OneClickLogViewer.Core;
using AvalonDock;

namespace OneClickLogViewer.Models
{
    class Workspace : ViewModelBase
    {
        protected Workspace() { MyDockingManager = new DockingManager(); }

        static Workspace _this = new Workspace();

        public static Workspace This
        {
            get { return _this; }
        }

        public DockingManager MyDockingManager { get; set; }

        ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();
        ReadOnlyObservableCollection<FileViewModel> _readonyFiles = null!;
        public ReadOnlyObservableCollection<FileViewModel> Files
        {
            get
            {
                if (_readonyFiles == null)
                    _readonyFiles = new ReadOnlyObservableCollection<FileViewModel>(_files);

                return _readonyFiles;
            }
        }

        ToolViewModel[] _tools = null!;

        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (_tools == null)
                    _tools = new ToolViewModel[] { FileStats };
                return _tools;
            }
        }

        FileStatsViewModel _fileStats = null!;
        public FileStatsViewModel FileStats
        {
            get
            {
                if (_fileStats == null)
                    _fileStats = new FileStatsViewModel();

                return _fileStats;
            }
        }

        #region OpenCommand

        RelayCommand _openCommand = null!;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand((p) => OnOpen(p), (p) => CanOpen(p));
                }

                return _openCommand;
            }
        }

        private bool CanOpen(object parameter)
        {
            return true;
        }

        private void OnOpen(object parameter)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                var fileViewModel = Open(dlg.FileName);
                ActiveDocument = fileViewModel;
            }
        }

        public FileViewModel Open(string filepath)
        {
            var fileViewModel = _files.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;

            fileViewModel = new FileViewModel(filepath);
            _files.Add(fileViewModel);
            return fileViewModel;
        }

        #endregion 

        #region NewCommand
        RelayCommand _newCommand = null;
        public ICommand NewCommand
        {
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new RelayCommand((p) => OnNew(p), (p) => CanNew(p));
                }

                return _newCommand;
            }
        }

        private bool CanNew(object parameter)
        {
            return true;
        }

        private void OnNew(object parameter)
        {
            _files.Add(new FileViewModel());
            ActiveDocument = _files.Last();
        }

        #endregion 

        #region ActiveDocument

        private FileViewModel _activeDocument = null;
        public FileViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    RaisePropertyChanged("ActiveDocument");
                    if (ActiveDocumentChanged != null)
                        ActiveDocumentChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ActiveDocumentChanged;

        #endregion


        internal void Close(FileViewModel fileToClose)
        {
            if (fileToClose.IsDirty)
            {
                var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", fileToClose.FileName), "OnClickLogViewer Test App", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Cancel)
                    return;
                if (res == MessageBoxResult.Yes)
                {
                    Save(fileToClose);
                }
            }

            _files.Remove(fileToClose);
        }

        internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
        {
            if (fileToSave.FilePath == null || saveAsFlag)
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog().GetValueOrDefault())
                    fileToSave.FilePath = dlg.SafeFileName;
            }

            File.WriteAllText(fileToSave.FilePath!, fileToSave.TextContent);
            ActiveDocument.IsDirty = false;
        }

    }
}