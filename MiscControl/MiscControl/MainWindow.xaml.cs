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

namespace MiscControl
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public SliderWindow sliderWindow = new SliderWindow();
        public ProgressBarWindow progressWindow = new ProgressBarWindow();
        public WinFormsHost WinFormsHostWindow = new WinFormsHost();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SliderButton_Click(object sender, RoutedEventArgs e)
        {
            sliderWindow.Show();
            sliderWindow.Visibility = Visibility.Visible;
        }

        private void ProgressBarButton_Click(object sender, RoutedEventArgs e)
        {
            progressWindow.Show();
            progressWindow.Visibility = Visibility.Visible;
        }

        private void WinFormsHostButton_Click(object sender, RoutedEventArgs e)
        {
            WinFormsHostWindow.Show();
            WinFormsHostWindow.Visibility = Visibility.Visible;
        }

    }
}
