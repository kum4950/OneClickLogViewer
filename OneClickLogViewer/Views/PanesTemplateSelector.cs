using AvalonDock.Layout;
using OneClickLogViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OneClickLogViewer.Views
{
    class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }


        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }


        public DataTemplate FileStatsViewTemplate
        {
            // C#의 자동 구현 프로퍼티 문법
            // 컴파일러가 내부적으로 필드를 생성함.
            //      get { return _fileViewTemplate; }
            //      set { _fileViewTemplate = value; }
            // 과 동일한 의미임
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            // item의 타입을 확인하고 , 해당 객체의 타입에 따라서 적절한 DataTemplate을 반환한다.
            // XAML 에서 
            //  < local:PanesTemplateSelector.FileViewTemplate >
            //      < DataTemplate >
            //          < TextBox Text = "{Binding TextContent, UpdateSourceTrigger=PropertyChanged}"
            //          BorderThickness = "0" />
            //      </ DataTemplate >
            //  </ local:PanesTemplateSelector.FileViewTemplate >
            // 
            //Binding TextContent 는  FileViewModel.TextContent 라 보면 된다.

            if (item is FileViewModel)
                return FileViewTemplate;

            if (item is FileStatsViewModel)
                return FileStatsViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
