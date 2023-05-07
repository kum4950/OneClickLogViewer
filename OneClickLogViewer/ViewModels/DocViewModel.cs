using AvalonDock;
using AvalonDock.Layout.Serialization;
using OneClickLogViewer.Core;
using OneClickLogViewer.Models;
using OneClickLogViewer.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace OneClickLogViewer.ViewModels
{
    class DocViewModel : ViewModelBase
    {


        public IEnumerable<ToolViewModel> Tools { get; set; } = Workspace.This.Tools;
        public ReadOnlyObservableCollection<FileViewModel> Files { get; set; } = Workspace.This.Files;
        public FileViewModel ActiveDocument { get; set; } = Workspace.This.ActiveDocument;

        public DockingManager MyDockingManager { get; set; } = Workspace.This.MyDockingManager;

        public DocViewModel()
        {



        }

    }
}
