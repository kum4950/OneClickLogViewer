/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System.Windows.Media;

namespace OneClickLogViewer.ViewModels
{
    // Template에서 FileViewMode, ToolViewModel를 모두사용할 수있게 PaneViewModel을 만듬
    class PaneViewModel : ViewModelBase
    {
        public PaneViewModel()
        {
        }

        #region Title

        // 백필드 선언
        // 백필드를 선언안해도 되지만, 백필드를 명시적으로 선언함으로써,
        // 이름을 개발자가 정할 수 있기 때문에, 가독성 측면에서 더욱 좋다.
        // 자동 구현 프로퍼티를 사용하면 컴파일러가 백필드를 생성함으로, 완전히 백필드를 사용하지 않는것은 아니다.
        private string? _title = null;

        public string Title
        {
            get { return _title; }

            set
            {
                if (_title != value)
                {
                    _title = value;
                    // nameof 변수명이 문자열 리터럴로 변경됨 -> "Title"
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        #endregion

        #region ContentId
        // ? 는 NULL 값을 가질수있다는것을 명시하는것이다.
        private string? _contentId = null;

        public string ContentId
        {
            get { return _contentId; }
            set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    RaisePropertyChanged(nameof(ContentId));
                }
            }
        }

        #endregion

        #region IsSelected
        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(nameof(IsSelected));
                }
            }
        }
        #endregion

        #region IsActive
        private bool _isActive = false;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }
        #endregion

    }
}