using System.Windows;

namespace CustomUIElementEx
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void WidthHeightChecked_Checked(object sender, RoutedEventArgs e)
        {
            // UI는 Abstract하게 되지 않는듯...
            if ((bool)WidthHeightChecked.IsChecked)
            {
                this.MyCustomPanel = new HorizontalOrientationPanel();
            }

            else
            {
                this.MyCustomPanel = new VerticalOrientationPanel();
            }
        }

        private void CustomUIElementBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomUIElement CusUIelement = new CustomUIElement();
            CusUIelement.Show();
        }
    }
}
