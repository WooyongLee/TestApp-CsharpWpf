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

namespace EventCallBackTest
{
    /// <summary>
    /// SubWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SubWindow : Window
    {
        private DataClass sData = null;
        // 대리자 정의
        public delegate void MyStringSender(DataClass data);

        // SubWindow -> MainWindow로 데이터를 넘겨주기 위한 이벤트 핸들러
        public event MyStringSender MyCallBackEvent;

        public SubWindow()
        {
            InitializeComponent();

            sData = new DataClass();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            sData.StringData = InputDataTextBox.Text;

            // CallBackEvent를 호출하여 Main에 등록된 Event를 발생시킴
            MyCallBackEvent(sData);
        }

    }

    public class DataClass
    {
        public string StringData { get; set; }
        
    }
}
