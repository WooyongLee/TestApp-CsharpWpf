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
    /// MessageBoxUI.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MessageBoxUI : Window
    {
        public string ReturnString { get; set; }

        public MessageBoxUI()
        {
            InitializeComponent();
        }

        public MessageBoxUI(string text, MsgBoxButtonType btnType)
        {
            InitializeComponent();
            ContentTextBox.Text = text;

            switch(btnType)
            {
                case MsgBoxButtonType.Yes_No:
                    YesButton.Visibility = Visibility.Visible;
                    NoButton.Visibility = Visibility.Visible;
                    break;
                case MsgBoxButtonType.OK:
                    OkButton.Visibility = Visibility.Visible;
                    break;
                default: break;
            }
        }


        private void BarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
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
            Height = (ContentTextBox.LineCount * 27) + BarGrid.Height + 150;
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnString = ((Button)sender).Uid.ToString();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Closing -= Window_Closed;
            Close();
        }
    }
}
