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

namespace EventCallBackTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public SubWindow SubWin;
        StringBuilder sb = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
            SubWin = new SubWindow();

            // SubWin의 이벤트를 MainWindow에 있는 콜백함수에 등록
            SubWin.MyCallBackEvent += MyCallBackFunction;
        }

        private void OpenSubWindowButton_Click(object sender, RoutedEventArgs e)
        {
            SubWin.Show();
        }

        public void MyCallBackFunction(DataClass data)
        {
            // stringbuilder 객체에 SubWin의 텍스트박스에 데이터를 추가
            sb.Append(data.StringData);
            sb.Append("\n");
            textBlock.Text = sb.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SubWin.MyCallBackEvent -= MyCallBackFunction;
        }

        // 참고 : http://www.csharpstudy.com/csharp/CSharp-delegate3.aspx
    }
}
