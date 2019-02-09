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

namespace WrappedCallBackTest
{
    /// <summary>
    /// TargetWindow1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TargetWindow1 : Window
    {
        public event EventHandler<CallBackArgs> OnReceive;

        public TargetWindow1( )
        {
            InitializeComponent();
        }

        // 이벤트 핸들러를 매개변수로 한 생성자. 다른 클래스에서 객체를 생성할 때 핸들러를 넘길 수도 있음
        public TargetWindow1(EventHandler<CallBackArgs> Handler)
        {
            OnReceive = Handler;
        }

        public void SetDatatFromReceoveCallBack()
        {
            
        }
    }
}
