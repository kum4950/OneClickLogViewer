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
using System.Windows.Input;

namespace OneClickLogViewer.ViewModels
{
    // ICommand 인터페이스 
    // WPF에서 명령을 처리하기 위한 표준 인터페이스이다.
    // Execute(object Parameter) : 명령이 실행될때 호출되는 메서드
    // CanExecute(object Parameter) : 명령이 실행 가능한지 여부를 판단하는 메서드
    //  -> CanExecute 는 Delegate를 사용하여 구현되어있다.
    //      델리게이트는 메서드를 참조하는 객체로, 메서드를 인수로 전달할 수 있다.
    // CanExecuteChanged : 명령이 실행 가능 여부가 변경되었을 때 발생하는 이벤트

    // ICommand 인터페이스의 클래스는 
    // 명령이 실행될때 실행될 메서드 (Execute) 와,
    // 명령이 실행가능한지 여부를 판단하는 메서드를 델리게이트(delegate(CanExecute))를 사용하여 연결하고,
    // CanExecuteChanged 이벤트를 정의하여 명령이 실행 가능 여부가 변경될때 이벤트를 발생시키도록 구현되어 있다.

    // internal 은 C#에서 사용되는 접근 제한자이다.
    // internal 접근제한자가 붙은 클래스는 같은 Assembly 내에서만 접근할 수 있다.

    // Assembly는 .Net Framework에서 사용되는 기본단위 중 하나로,
    // .Net Framework에서 실행 가능한 코드의 패키지 이다.
    // 즉, Assembly는 .Net Framework에서 실행되는 어플래케이션을 구성하는 기본 요소 중 하나이다.
    // 대략, exe, dll 단위라고 일단 생각하자.

    internal class RelayCommand : ICommand
    {
        // region 은  코드 블럭을 묶는 전처리기 이다.
        #region Fields

        // Action 델리게이트를 사용하여 매개변수를 받는 객체 실행함수를 나타낸다.
        // readonly 키워드는 이 필드가 한번 초기화된 후 변경되지 않는 것을 보장한다.
        // 즉, 해당 필드에 대한 값을 다시 할당하려고 하면 컴파일러가 오류를 발생시킨다.

        // Action<object> 는 매개 변수가 하나인 함수를 나타내는 델리게이트 이다.
        // 이 경우, 매개 변수는 object 형식이다.
        // _execute 필드가 이 델리게이트 형식을 갖는 이유는 ICommand의 Execute 메서드가 object 형식의 매개변수를 받기 때문이다.
        // 해당 필드는 클래스 내에서 읽기 전용 필드로 선언되었으므로 객체가 생성된 이후에는 변경할 수 없다.

        // Action<object> 는 결과를 반환하지 않는 함수를 나타냄
        // Predicate<object>는 bool 형식의 결과를 반환하는 함수를 나타냄.

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors
        public RelayCommand(Action<object> execute)
          : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        //이벤트는 add 와 remove 블록을 사용하여 구현된다.
        // CommandManager.RequerySuggested 이벤트를 사용하여 CanExecuteChanged 이벤트를 구현한다.
        // CommandManager.RequerySuggested 이벤트는 WPF에서 UI 요소의 속성이 변경되었을 때 발생된다.
        // 따라서 UI 요소의 속성이 변경될때 마다 명령의 실행 가능 상태를 다시 검사하고, 이벤트핸들러가 호출된다.

        // value는 C#에서 add 또는 remove 블록안에서 사용되는 암묵적 매개 변수이다.
        // 이 매개변수는 델리게이트를 추가하거나 제거할때 전달되는 핸들러 함수를 나타낸다.
        // CommandManager.RequerySuggested += value; 는
        // CommandManager.RequerySuggested 이벤트에 CanExecuteChanged 핸들러 함수를 추가한다는 뜻이다.
        // 이렇게 추가된 핸들러 함수는 CommandManager.RequerySuggested 이벤트가 발생할 때 마다 실행된다.
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion

    }
}