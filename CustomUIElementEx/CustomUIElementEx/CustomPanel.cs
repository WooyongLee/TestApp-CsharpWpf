using System;
using System.Windows;
using System.Windows.Controls;

namespace CustomUIElementEx
{
    // 하위 항목을 두 열로 정렬 후, 두 열이 모두 채워진 후 다음 열로 이동하는 패널
    // 하위 요소에 포함 하는 패널의 총 크기에 따라 균일한 공간을 만들어주는 로직
    public class TwoColUniformPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size childSize = CalcUniformChildSize(availableSize);

            foreach (UIElement elem in InternalChildren)
                elem.Measure(childSize);

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size childSize = CalcUniformChildSize(finalSize);

            double top = 0.0;
            double left = 0.0;

            for (int i = 0; i < InternalChildren.Count; i++)
            {
                Rect r = new Rect(new Point(left, top), childSize);

                InternalChildren[i].Arrange(r);

                // Next row
                if (left > 0.0)
                    top += childSize.Height;

                // Alternate column
                left = (left > 0.0) ? 0.0 : (finalSize.Width / 2.0);
            }

            return finalSize;
        }

        private Size CalcUniformChildSize(Size availableSize)
        {
            // Row 갯수는 Child Element 갯수의 절반
            int numRows = (int)Math.Ceiling(InternalChildren.Count / 2.0);
            return new Size(availableSize.Width / 2.0, availableSize.Height / numRows);
        }
    }

    public class MyPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            // 기본적으로 패널 컨트롤은 하위요소의 패널을 따름(레이아웃 ZIndex의 값)
            // 해당 패널에서는 하위 요소들 겹치기 가능

            // Child Size를 결정하는데, 넉넉하게
            // InternalChildren.Count :: 하위 UIElement의 갯수
            Size childSize = new Size(availableSize.Width, double.PositiveInfinity); // Height Size 무한대로 확장
            // Size childSize = new Size(availableSize.Width, availableSize.Height / InternalChildren.Count);
            // Size childSize = new Size(availableSize.Width / InternalChildren.Count, availableSize.Height);

            // 모든 Children에 대한 element들의 Desired Size를 Update함
            foreach (UIElement elem in InternalChildren)
                elem.Measure(childSize);

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // Child의 크기 설정함
            double childHeight = finalSize.Height / InternalChildren.Count;
            Size childSize; // = new Size(finalSize.Width, childHeight);

            double top = 0.0;
            // double left = 0.0;
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                // Child Ful Width
                childSize = new Size(finalSize.Width, InternalChildren[i].DesiredSize.Height);
                Rect r = new Rect(new Point(0.0, top), childSize);
                InternalChildren[i].Arrange(r);
                top += childSize.Height * 0.5; // 다음 Child Element의 0.5배 Height만큼 겹침
                // top += childSize.Height;

                // Child의 크기를 최종 Size에 맞게 Arrange함
                //childSize = new Size(InternalChildren[i].DesiredSize.Width, finalSize.Height);
                //Rect r = new Rect(new Point(left, 0.0), childSize);
                //InternalChildren[i].Arrange(r);
                //left += childSize.Width;
            }

            return finalSize;
        }
    }

    public class HorizontalOrientationPanel : MyPanel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            // Child Size를 결정하는데, 넉넉하게
            Size childSize = new Size(availableSize.Width / InternalChildren.Count, availableSize.Height);

            // 모든 Children에 대한 element들의 Desired Size를 Update함
            // 자식요소들의 크기, Panel위치
            foreach (UIElement elem in InternalChildren)
                elem.Measure(childSize);

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double childHeight = finalSize.Height / InternalChildren.Count;
            Size childSize; // = new Size(finalSize.Width, childHeight);

            double left = 0.0;
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                childSize = new Size(InternalChildren[i].DesiredSize.Width, finalSize.Height);
                Rect r = new Rect(new Point(left, 0.0), childSize);
                InternalChildren[i].Arrange(r);
                left += childSize.Width;
            }

            return finalSize;
        }

    }

    public class VerticalOrientationPanel : MyPanel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            // Child Size를 결정하는데, 넉넉하게
            Size childSize = new Size(availableSize.Width, availableSize.Height / InternalChildren.Count);

            // 모든 Children에 대한 element들의 Desired Size를 Update함
            // 자식요소들의 크기, Panel위치
            foreach (UIElement elem in InternalChildren)
                elem.Measure(childSize);

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double childHeight = finalSize.Height / InternalChildren.Count;
            Size childSize; // = new Size(finalSize.Width, childHeight);

            double top = 0.0;
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                // Child Ful Width
                childSize = new Size(finalSize.Width, InternalChildren[i].DesiredSize.Height);
                Rect r = new Rect(new Point(0.0, top), childSize);
                InternalChildren[i].Arrange(r);
                top += childSize.Height;
            }

            return finalSize;
        }
    }
}
