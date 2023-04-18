/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System.ComponentModel;

namespace OneClickLogViewer.ViewModels
{
    // INotifyPropertyChanged 인터페이스는 .Net Framework에서 제공하는 인터페이스 중 하나로, 
    // 데이터 바인딩에 사용된다.
    // 이 Interface는 객체가 프로퍼티 값이 변경되었음을 알리는 기능을 제공한다.
    // INotifyPropertyChanged Interface를 구현한 ViewModel은 자동으로 데이터변경 이벤트를 발생 시킬수있다. 
    // 이 이벤트를 사용하면, ViewModel에서 UI를 명시적으로 참조할 필요없이, 데이터 바인딩을 통해 UI 업테이트를 수행할 수 있다.

    // C#에서 Property 는 클래스 멤버변수를 의미하는데 , Get Set 접근자를 사용할수있다.
    // 예를들어 Set 접근자를 정의를 안한다면, 읽기 전용으로 만 사용이 가능하다.
    // 사용 방법은 객체 인스턴스를 생성하고, 객체.property = A 이렇게 set 접근자를 사용할 수 있다.
    // 만약 접근자를 설정안하고 프로퍼티값을 설정하려고 하면 컴파일 에러가 발생한다.
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // 보통 Set 접근자 쪽에서 RaisePropertyChanged()를 사용하여, 프로퍼티가 변경되었음을 UI한테 알려서, UI를 업데이트 시킨다.
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            //PropertyChanged 는 INotifyPropertyChanged 에 선언된 PropertyChangedEventHandler이다.
            // 해당 객체의 string property가 변화되었음을 알린다.
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}