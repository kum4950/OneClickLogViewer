/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Globalization;
using System.Windows.Data;
using OneClickLogViewer.ViewModels;

namespace OneClickLogViewer
{
    // IValueConverter 는 WPF에서 데이터바인딩을 위해 제공되는 인터페이스 중 하나이다.
    // IValueConverter 인터페이스를 구현하면 데이터 바인딩에서 값을 변환하는 작업을 할 수 있다.
    // -. Convert : 소스 값에서 대상 값으로 변환을 수행한다.
    // -. ConvertBack : 대상 값에서 소스값으로 변환을 수행한다.
    // 이 인터페이스를 구현하여 데이터 바인딩 시 값을 변환하거나, 역으로 변환할 수 있다.
    // 이를 통해 데이터 바인딩을 더욱 유연하게 사용할 수 있다.
    class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FileViewModel) 
                return value;

            // DoNothing은 데이터 바인딩에서 사용되는 특수한 값을 나타내는 열거형 상수 이다.
            // DoNothing 값은 업데이트 프로세스를 중단하고 대상 속성을 이전 상태로 유지한다.
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
