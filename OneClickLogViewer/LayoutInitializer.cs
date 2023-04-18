using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickLogViewer
{
    // ILayoutUpdateStrategy는 LayoutRoot를 기반으로 하는 레이아웃과 관련된 작업에서 호출되는 메서드의 집합을 정의한다.
    class LayoutInitializer : ILayoutUpdateStrategy
    {
        //LayoutAnchorable 이 삽입되기 전 호출
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            //AD wants to add the anchorable into destinationContainer
            //just for test provide a new anchorablepane 
            //if the pane is floating let the manager go ahead
            LayoutAnchorablePane destPane = destinationContainer as LayoutAnchorablePane;
            if (destinationContainer != null &&
                destinationContainer.FindParent<LayoutFloatingWindow>() != null)
                return false;

            // FirstOrDefault()는 조건에 맞는 첫번째 요소를 반환하거나, 조건에 맞는 요소가 없으면 기본값(NUll)을 반환
            // layout.Descendents() 는 layout의 하위요소들을 열거하는 메서드이다.
            //OfType<LayoutAnchrablePan>() 는 이 열거된 요소 중 LayoutAnchorablePane 형식에 해당하는 것들만 선택하는 쿼리이다.
            var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == "ToolsPane");
            if (toolsPane != null)
            {
                toolsPane.Children.Add(anchorableToShow);
                return true;
            }

            return false;

        }

        //LayoutAnchorable 이 삽입된 후 호출
        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
        }


        //LayoutDocument를 삽입할 때 호출
        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }

        //LayoutDocument를 삽입후 호출
        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {

        }
    }
}
