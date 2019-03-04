using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiscControl
{
    /// <summary>
    /// SliderAndProgressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SliderWindow : Window
    {
        public SliderWindow()
        {
            InitializeComponent();
        }

        // Slider 값 변경 시 이벤트
        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // RGB 값 받아서 Color 지정
            Color color = Color.FromRgb((byte)slColorR.Value, (byte)slColorG.Value, (byte)slColorB.Value);

            // StackPanel의 BackGround 속성에 지정
            this.SliderPanel.Background = new SolidColorBrush(color);
        }
    }
}
