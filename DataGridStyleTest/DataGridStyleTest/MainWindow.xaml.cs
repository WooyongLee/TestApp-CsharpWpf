using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGridStyleTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);

            if (brush.CanFreeze)
            {
                brush.Freeze();
            }

            // brush.Color = Colors.Black;
            // System Parameter Test
            Button btn = new Button();
            btn.Content = "SystemParameters";
            btn.FontSize = 8;
            btn.Background = SystemColors.ControlDarkDarkBrush;
            btn.Height = SystemParameters.CaptionHeight;
            btn.Width = SystemParameters.IconGridWidth;

        }

        // Thumb DragDelta 이벤트
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = e.Source as UIElement;

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        // GridSpliiter 드래그 시작 이벤트
        private void MyGridSplitter_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            LeftText.Text = "Dragging Started";
            MyGridSplitter.Background = Brushes.Red;
        }

        // GridSpliiter 드래그 종료 이벤트
        private void MyGridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            LeftText.Text = string.Format("Dragging Completed, moving {0}", e.HorizontalChange);
            MyGridSplitter.Background = Brushes.Gray;
        }

        private void MyGridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (e.HorizontalChange == 0)
            {
                RightText.Text = "Not Moving";
            }
            else if (e.HorizontalChange > 0)
                RightText.Text = "Moving Right";
            else
                RightText.Text = "Moving Left";
        }

        // Visual Tree Helper 이용해서 UI안에 Resource 찾기
        public void VisualTreeHelperTest()
        {
        }

        // Visual 객체 찾기
        public TVisual FindVisualObject<TVisual>(DependencyObject visualDependencyObject) where TVisual : class
        {
            if (visualDependencyObject is TVisual)
            {
                return visualDependencyObject as TVisual;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visualDependencyObject); i++)
            {
                TVisual childVisualDependencyObject = FindVisualObject<TVisual>(VisualTreeHelper.GetChild(visualDependencyObject, i));

                if (childVisualDependencyObject != null)
                {
                    return childVisualDependencyObject;
                }
            }
            return null;
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            // Button을 기준으로 MainGrid 찾기
            Grid g = FindVisualParentByName<Grid>(btn, "MainGrid"); // 찾는 것 확인 완료
            // g.Background = this.Resources["TempGridColor"] as Brush;

            // Button을 기준으로 ScrollViewer찾기
            // Button 내에 ScrollViewer 는 ContentTemplate을 적용한 것이라 접근 할 수가 없음
            //TextBlock sv = FindVisualChildByName<TextBlock>(btn, "InButtonTextBlock");
            TextBlock sv = FindVisualChildByName<TextBlock>(MainGrid, "InButtonTextBlock");
            if ( sv== null)
            {
                RightText.Text = "FindVisualParentByName 으로 못찾음";
            }

            // sv.Background = this.Resources["TempScrollViewerColor"] as Brush;
        }

        /// <summary>
        /// 자신의 컨트롤 한 단계 위부터 사용자정의 이름으로 검색한다.
        /// </summary>
        /// <typeparam name="T">결과값의 형식</typeparam>
        /// <param name="_Control">검색을 시작할 컨트롤</param>
        /// <param name="_FindControlName">찾고자 하는 컨트롤 이름</param>
        /// <returns></returns>
        private T FindVisualParentByName<T>(FrameworkElement _Control, string _FindControlName) where T : FrameworkElement
        {
            T t = null;
            // Object 검사
            DependencyObject obj = VisualTreeHelper.GetParent(_Control);

            // 일단 최대 100개의 Control까지 검색한다.
            for (int i = 0; i < 100; i++)
            {
                string currentName = obj.GetValue(Control.NameProperty) as string;
                if (currentName == _FindControlName)
                {
                    t = obj as T;
                    break;
                }

                // 1번 Object 부모 -> 2번 Object 불러오기 식으로 검색
                obj = VisualTreeHelper.GetParent(obj);

                // 더 이상 검사할 컨트롤이 없는 경우
                if (obj == null)
                {
                    break;
                }
            }
            return t;
        }

        /// <summary>
        /// 자신의 컨트롤 한 단계 아래부터 사용자 정의 이름으로 검색한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Control"></param>
        /// <param name="_FindControlName"></param>
        /// <returns></returns>
        private T FindVisualChildByName<T>(DependencyObject _Control, string _FindControlName) where T : DependencyObject
        {
            // 해당 컨트롤의 하위 컨트롤 갯수만큼 반복
            for ( int i = 0; i < VisualTreeHelper.GetChildrenCount(_Control); i++)
            {
                var child = VisualTreeHelper.GetChild(_Control, 1);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == _FindControlName)
                {
                    return child as T;
                }

                // 한 단계 더 내려가기
                else
                {
                    T result = FindVisualChildByName<T>(child, _FindControlName);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }

    public class MyFrameWorkElement : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext);
        }
    }

}
