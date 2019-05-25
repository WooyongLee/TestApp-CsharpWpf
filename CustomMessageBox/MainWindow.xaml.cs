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

namespace CustomMessageBox
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

        // MsgBox1
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxControl.Show(InputTextBox.Text, MsgBoxButtonType.Yes_No);
        }

        // Msgbox2
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxControl.Show(InputTextBox.Text, MsgBoxButtonType.OK);
        }

        // Notification
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NotificationBoxControl.Show(InputTextBox.Text);
        }
    }
}
