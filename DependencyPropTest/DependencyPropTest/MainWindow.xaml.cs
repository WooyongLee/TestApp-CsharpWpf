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

namespace DependencyPropTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register("TextChange", typeof(String), typeof(MainWindow), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChangePropChanged)));

        // Dependency Property의 CallBack Method
        private static void OnTextChangePropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow userNameCtrl = d as MainWindow;
            string newText = (string)e.NewValue;
            string oldText = (string)e.OldValue;

            userNameCtrl.OldTextBlock.Text = oldText;
            userNameCtrl.NewTextBlock.Text = newText;
        }

        public String UserText
        {
            get { return (String)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            UserText = textBox1.Text;
        }

        private void btnSecondPropertyWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            SecondProp secondWinodow = new SecondProp();
            secondWinodow.Show();
        }
    }
}
