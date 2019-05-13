using System.Windows;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System;
using System.Windows.Threading;

namespace NetworkConnectCheck
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Network Config
        private const string LocalIP = "192.168.2.46";
        private const string LoopbackAdapterIP = "169.254.205.52";
        #endregion

        public NetworkChecker NetChecker = new NetworkChecker();

        public DispatcherTimer ltimer = new DispatcherTimer();
        public TimeSpan timervalue2 = new TimeSpan(6400000); // 1.5625Hz

        public MainWindow()
        {
            InitializeComponent();

            // 네트워크 상태 체크 타이머 시작
            ltimer.Interval = timervalue2;
            ltimer.Tick += new EventHandler(DeviceStatusChecker);
            ltimer.Start();
        }

        private void DeviceStatusChecker(object sender, EventArgs e)
        {
            if (NetChecker.IsConnectionExists(LocalIP))
            {
                Dispatcher.BeginInvoke(new Action(() =>
                    {
                        FirstNetworkTextBlock.Text = "연결";
                    }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() =>
                    {
                        FirstNetworkTextBlock.Text = "미연결";
                    }));
            }

            if (NetChecker.IsConnectionExists(LoopbackAdapterIP))
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                      SecondNetworkTextBlock.Text = "연결";
                }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() =>
          {
              SecondNetworkTextBlock.Text = "미연결";
          }));
            }

            // 가상의 네트워크
            if (NetChecker.IsConnectionExists("192.160.2.41"))
            {
                Dispatcher.BeginInvoke(new Action(() =>
          {
              VirtualNetworkTextBlock.Text = "연결";
          }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() =>
          {
              VirtualNetworkTextBlock.Text = "미연결";
          }));
            }
        }
    }

    // 네트워크 상태 확인 클래스
    public class NetworkChecker
    {
        private PerformanceCounterCategory performanceCounterCategory;
        private int NetworkLength = 0;
        public bool IsNetworkConnected = false;

        public NetworkChecker()
        {
            // 네트워크 리소스 가져오기
            performanceCounterCategory = new PerformanceCounterCategory("NetWork Interface");

            // 시스템의 장착되어있는 랜카드 인스턴스를 가져옴
            NetworkLength = performanceCounterCategory.GetInstanceNames().Length;

            // 로컬 네트워크 상에만 있는 경우, 네트워크가 연결되었다고 판단한다
            IsNetworkConnected = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public bool IsConnectionExists(string paramIP)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(paramIP);

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
