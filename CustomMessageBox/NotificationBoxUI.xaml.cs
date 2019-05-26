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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomMessageBox
{
    /// <summary>
    /// NotificationBoxUI.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NotificationBoxUI : Window
    {
        public NotificationBoxUI(string text)
        {
            InitializeComponent();
            NotifyTextBox.Text = text;
        }


        DoubleAnimation DAnim;
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closed;
            e.Cancel = true;
            DAnim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            DAnim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, DAnim);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             Height = (NotifyTextBox.LineCount * 27) + BodyGrid.Height +  150;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // this.FastClose();
        }

        public void FastClose()
        {
            Closing -= Window_Closed;
            Close();
            //DAnim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            //DAnim.Completed += (s, _) => this.Close();
            //this.BeginAnimation(UIElement.OpacityProperty, DAnim);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.FastClose();
        }
    }
}
