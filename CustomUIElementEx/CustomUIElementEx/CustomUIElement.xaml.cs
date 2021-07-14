using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomUIElementEx
{
    /// <summary>
    /// CustomUIElement.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomUIElement : Window
    {
        public CustomUIElement()
        {
            InitializeComponent();

            ManipulationStarting += CustomUIElement_ManipulationStarting;
            ManipulationDelta += CustomUIElement_ManipulationDelta;
            ManipulationInertiaStarting += CustomUIElement_ManipulationInertiaStarting;
        }

        private void CustomUIElement_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            // Rectangle Speed Be Lower : (10 Inch * 96 Inch Per Pixel) / (1000^2 Ms)
            e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);

            // Rectangle Size Control Speed Be Lower : (0.1 Inch * 96 Inch Per Pixel) / (1000^2 Ms)
            e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);

            // Rectangle Rotation Speed - second per 2 rotation : (2 * 360 deg / 1000^2 ms)
            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);

            e.Handled = true;
        }

        // 윈도우 조작 델타 처리
        private void CustomUIElement_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Rectangle rectangle = e.OriginalSource as Rectangle;

            Matrix matrix = ((MatrixTransform)rectangle.RenderTransform).Matrix;

            // 조작한 점을 기준으로 회전시킴
            matrix.RotateAt
            (
                e.DeltaManipulation.Rotation,
                e.ManipulationOrigin.X,
                e.ManipulationOrigin.Y
            );

            // 최신 조작 변경 사항에 대한 Scaling 및 Origin에 대한 Scale 조정
            matrix.ScaleAt
            (
                e.DeltaManipulation.Scale.X,
                e.DeltaManipulation.Scale.X,
                e.ManipulationOrigin.X,
                e.ManipulationOrigin.Y
            );

            // 조작 변경에 대한 Offset 변환
            matrix.Translate
            (
                e.DeltaManipulation.Translation.X,
                e.DeltaManipulation.Translation.Y
            );

            // 변환행렬을 이용하여 변환 값을 설정함
            rectangle.RenderTransform = new MatrixTransform(matrix);

            // Container Rectangle에 대한 RenderSize
            Rect containerRectangle = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

            // 변환된 Rectangle에 대한 RenderSize
            Rect shapeBoundRectangle = rectangle.RenderTransform.TransformBounds(new Rect(rectangle.RenderSize));

            if ( e.IsInertial && !containerRectangle.Contains(shapeBoundRectangle))
            {
                e.Complete();
            }

            e.Handled = true;
        }

        // 윈도우 조작 시 처리
        private void CustomUIElement_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Handled = true;
        }
    }
}
