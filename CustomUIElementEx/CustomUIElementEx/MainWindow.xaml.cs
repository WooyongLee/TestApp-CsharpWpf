using System;
using System.Windows;
using System.Windows.Controls;

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

        // 현재 App에서 별도의 Window를 열었을 때, Window의 Title 및 위치 정보를 가져오는 함수
        private void OpenedChildWindowsCurrentPosition()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach ( Window w in App.Current.Windows)
            {
                sb.AppendFormat("Window [{0}] is at ({1},{2}).\n", w.Title, w.Top, w.Left);
            }

            MessageBox.Show(sb.ToString(), "My Windows");
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button b2 = new Button();
            b2.AddHandler(Button.ClickEvent, new RoutedEventHandler(Myb2Click));
            // Or
            b2.Click += B2_Click;
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            // logic to handle th Click event
        }

        private void Myb2Click(object sender, RoutedEventArgs e)
        {
            // logic to handle th Click event
        }

        private void EventTestWindow_Click(object sender, RoutedEventArgs e)
        {
            EventTestWindow EtWin = new EventTestWindow();
            EtWin.Show();
        }

        private void DropTargetWindow_Click(object sender, RoutedEventArgs e)
        {
            DropTargetWindow DtWin = new DropTargetWindow();
            DtWin.Show();
        }
    }
}
