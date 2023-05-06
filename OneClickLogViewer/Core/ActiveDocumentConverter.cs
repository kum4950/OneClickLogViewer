﻿
using System;
using System.Globalization;
using System.Windows.Data;
using OneClickLogViewer.ViewModels;

namespace OneClickLogViewer.Core
{
    class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FileViewModel) 
                return value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FileViewModel)
                return value;

            return Binding.DoNothing;
        }
    }
}
