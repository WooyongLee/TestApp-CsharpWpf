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
using System.Windows.Shapes;

namespace DelegateCallBackTest
{
    /// <summary>
    /// DelegateTarget.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DelegateTarget : Window
    {
        public int targetButtonClick = 0;
        public string targetString = "";

        public event EventHandler FirstSomthingHappened;
        //public event EventHandler SecondSomethingHappend;
        //public event EventHandler ThirdSomethingHappend;

        public DelegateTarget()
        {
            InitializeComponent();
        }

        private void CallBack_1_Click(object sender, RoutedEventArgs e)
        {
            targetButtonClick = 1;
            Result_1.Content = targetString;
        }

        private void CallBack_2_Click(object sender, RoutedEventArgs e)
        {
            targetButtonClick = 2;
            Result_2.Content = targetString;
        }

        private void CallBack_3_Click(object sender, RoutedEventArgs e)
        {
            targetButtonClick = 3;
            Result_3.Content = targetString;
        }
    }
}
