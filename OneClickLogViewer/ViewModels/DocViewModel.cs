using OneClickLogViewer.Core;
using OneClickLogViewer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneClickLogViewer.ViewModels
{
    class DocViewModel : ViewModelBase
    {
        public RelayCommand NewCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }

        public IEnumerable<ToolViewModel> Tools { get; set; } = Workspace.This.Tools;
        public ReadOnlyObservableCollection<FileViewModel> Files { get; set; } = Workspace.This.Files;
        public FileViewModel ActiveDocument { get; set; } = Workspace.This.ActiveDocument;

        public DocViewModel()
        {
            NewCommand = new RelayCommand(Workspace.This.NewCommand.Execute);
            OpenCommand = new RelayCommand(Workspace.This.OpenCommand.Execute);
        }

    }
}
