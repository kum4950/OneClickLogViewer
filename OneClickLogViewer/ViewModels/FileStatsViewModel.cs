/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using OneClickLogViewer.Models;
using System.IO;
using System.Windows.Media.Imaging;
using System;

namespace OneClickLogViewer.ViewModels
{
    class FileStatsViewModel : ToolViewModel
    {
        // base 키워드는 부모 클래스인 ToolViewModel 클래스의 생성자를 호출하는데 사용된다.
        // base 키워드를 사용하여 부모클래스의 생성자를 호출하면,
        // 자식 클래스에서 필요한 초기화 작업을 수행하기 전에 먼저 부모클래스에서 필요한 초기화작업을 수행한다.

        public FileStatsViewModel()
            : base("File Stats")
        {
            Workspace.This.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
            ContentId = ToolContentId;
        }

        public const string ToolContentId = "FileStatsTool";

        void OnActiveDocumentChanged(object sender, EventArgs e)
        {
            if (Workspace.This.ActiveDocument != null &&
                Workspace.This.ActiveDocument.FilePath != null &&
                File.Exists(Workspace.This.ActiveDocument.FilePath))
            {
                var fi = new FileInfo(Workspace.This.ActiveDocument.FilePath);
                FileSize = fi.Length;
                LastModified = fi.LastWriteTime;
            }
            else
            {
                FileSize = 0;
                LastModified = DateTime.MinValue;
            }
        }

        #region FileSize

        private long _fileSize;
        public long FileSize
        {
            get { return _fileSize; }
            set
            {
                if (_fileSize != value)
                {
                    _fileSize = value;
                    RaisePropertyChanged("FileSize");
                }
            }
        }

        #endregion

        #region LastModified

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get { return _lastModified; }
            set
            {
                if (_lastModified != value)
                {
                    _lastModified = value;
                    RaisePropertyChanged("LastModified");
                }
            }
        }

        #endregion
    }
}