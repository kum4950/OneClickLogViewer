﻿
using System.Windows.Media;

namespace OneClickLogViewer.ViewModels
{
    class PaneViewModel : ViewModelBase
    {
        public PaneViewModel()
        {
        }

        #region Title

        private string? _title = null;

        public string Title
        {
            get { return _title!; }

            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        #endregion

        #region ContentId
        private string? _contentId = null;

        public string ContentId
        {
            get { return _contentId!; }
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