using System;
using System.Windows;

namespace WrappedCallBackTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public WrappedManager WManager;
        public TargetWindow1 TWIndow1;
        public msgType EmsgType;
        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();
            WManager = new WrappedManager();
            TWIndow1 = new TargetWindow1();

            TWIndow1.OnReceive += onReceiveData;
        }

        public void onReceiveData(Object Sender, CallBackArgs e, msgType EmsgT)
        {
           switch (EmsgType)
            {
                case msgType.Type1: { WManager.ReceiveData1(); } break;
                case msgType.Type2: { WManager.ReceiveData2(); } break;
                case msgType.Type3: { WManager.ReceiveData3(); } break;
                default: break;
            }
        }

        public void setData1()
        {
            WManager.Data1 = index.ToString() + "111";
            WManager.Data4 = index++;
        }

        public void setData2()
        {
            WManager.Data2 = index.ToString() + "222";
            WManager.Data4 = index++;
        }

        public void setData3()
        {
            WManager.Data3 = index.ToString() + "333";
            WManager.Data4 = index++;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TWIndow1.OnReceive -= onReceiveData;
        }

        private void TypeToggleButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }

    public enum msgType
    {
        Type1, Type2, Type3
    };
}

//1.Main에 Onlog Method(Event Function) 정의
//Ex) OnLog(Object Sender, CallBackArgs e) { }
//    형식

//2. Target Class에 EventHandler 선언
// Ex) public event EventHandler<CallBackArgs> OnLog;

//3. Target Class의 생성자나 멤버로 EventHandler<CallBackArgs>[이벤트핸들러변수명] 을 넘기던지 만들어놓던지 함
//Ex) public TargetClass(EventHandler<CallBackArgs> MyEventHandler

//4. CallBackArgs 클래스 생성, EventArgs를 상속받음

//Ex) public class CallBackArgs : EventArgs { }

//5. TargetClass에서 마음껏 사용하면 됨(참고로 MainWindow의 OnLog 내에서 기능을 열심히 구현 해 놓으면 될듯


//-> Wrapper에 적용
//1. Main에 Event Function으로 쓸 Method 정의
// Ex) WrappedCallBackFunc
//      {
//	ExData1 data1;
//    ExData2 data2;
//    ExData3 data3; ... 

//	// 각 Enum별로 분기 및 Receive 코드들 작성	
//	switch( )
//	{

//	}

//      }

//2. Target Class에 EventHandler 선언
// Ex) public event EventHandler<CallBackArgs> OnWrapped;

//3. Target Class의 생성자로 EventHandler<CallBackArgs> [이름]을 만들어 놓음
// Ex) public DLPScenario(EventHandler<CallBackArgs> WrappedEventHandler

//4. CallBackArgs 클래스 생성, EventArgs를 상속받음
//  Ex) public class CallBackArgs : EventArgs

//5. Target Class에서 사용하기

