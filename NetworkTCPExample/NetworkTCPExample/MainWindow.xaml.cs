using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace NetworkTCPExample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsServerStop = true;
        public ObservableCollection<TCPDataItem> DataList; // WPF DataGrid에 붙이기 위한 List
        public TCPserverManager ServerManager;
        public Thread ServerThread;

        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();
            ServerManager = new TCPserverManager();

            ServerThread = new Thread(new ThreadStart(Listen));
            DataList = new ObservableCollection<TCPDataItem>();

            ServerManager.DataReceiveEvent += ServerManager_DataReceiveEvent;

            this.ServerDataGrid.ItemsSource = DataList;
        }


        private void ServerStart_Click(object sender, RoutedEventArgs e)
        {
            if (IsServerStop)
            {
                ServerManager.BindIP = IPtextBox.Text;
                ServerManager.BindPort = int.Parse(PORTtextBox.Text);

                ServerThread.Start();
                
                ServerStart.Content = "서버 작동중";
                ServerStart.IsEnabled = false;
                IsServerStop = false;
            }
            
            else
            {
                // ServerThread.Join();
                ServerStart.Content = "서버 작동중";
                IsServerStop = true;
            }
        }

        // DataGrid에 데이터를 등록함
        private void ServerManager_DataReceiveEvent(string receiveData)
        {
            string time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"); // 현재시각

            // 순번, 시간, Client로 부터 들어온 데이터를 등록함
            index++;

            TCPDataItem addItem = new TCPDataItem(index, time, receiveData);

            // DataList에 등록
            //foreach (TCPDataItem item in DataList)
            //{
            //    App.Current.Dispatcher.Invoke((Action)delegate
            //    {
            //        DataList.Add(addItem);
            //    });
            //}

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                DataList.Add(addItem);
            });
        }

        private void Listen()
        {
            ServerManager.ServerStart();
        }

        // 로컬 IP 설정
        public string GetLocalIP()
        {
            string localIP = "Not available, please check your network seetings!";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }

    public class TCPserverManager
    {
        // 루프백 주소, 컴퓨터 네트워크의 입출력 기능을 시험하기 위해 가상으로 할당한 주소
        // 이를 활용하면 자기 자신에게 패킷을 보내게 됨
        public string BindIP { get; set; }
        public int BindPort { get; set; }
        public TcpListener server = null; // System.Net.Socket
        public TcpClient client = null;
        public IPEndPoint localAddress; // System.Net
        Thread ReceiveThread;
        private bool isConnected = false;
        private StreamReader reader;

        // 네트워크 스트림 객체 생성. 데이터의 입출력 처리의 중간자 역할
        //// 데이터,패킷,비트 등의 일련의 연속성을 갖는 흐름을 의미
        public NetworkStream stream;

        public delegate void DataReceiveDelegate(string _data);
        public event DataReceiveDelegate DataReceiveEvent;
        

        public TCPserverManager()
        {
            
        }

        public void ServerStart()
        {
            try
            {
                // EndPoint :: 어떠한 소프트웨어나 제품에 최종목적지인 사용자, 
                // 그 예로는 PC나 노트북, 핸드폰등 유저가 사용하는 devices등
                localAddress = new IPEndPoint(IPAddress.Parse(BindIP), BindPort);

                server = new TcpListener(localAddress);

                // 서버 시작
                server.Start();

                ReceiveThread = new Thread(ReceiveFunc);
                ReceiveThread.Start();

                client = server.AcceptTcpClient();
                isConnected = true;
                reader = new StreamReader(stream);

                while (true)
                {
                    stream = client.GetStream();
                    ManagedStream();
                }
            }

            catch (SocketException ex)
            {

            }
        }

        private void ReceiveFunc(object obj)
        {
            while (isConnected)
            {
                Thread.Sleep(1);
                if (stream.CanRead)
                {
                    string str = reader.ReadLine();
                    this.DataReceiveEvent(str);
                }
            }
        }

        public void ServerExit()
        {
            if (client != null)
                client.Close();

            if (server != null)
                server.Stop();
        }

        public void ManagedStream()
        {
            try
            {
                int length;
                string data = null;
                byte[] bytes = new byte[256];

                // if ( stream. == null)
                

                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // 문자인코딩을 통해 byte형태로 들어오는 것을 string으로 변환
                    data = Encoding.Default.GetString(bytes, 0, length);
                    
                    byte[] msg = Encoding.Default.GetBytes(data);

                    // 데이터 송신
                    // message 내용, 쓸 위치(offset), message 길이
                    stream.Write(msg, 0, msg.Length);
                }
                // Client로부터 들어온 데이터를 처리하는 TextBlock에 string data 뿌려줌
                DataReceiveEvent(data);

                stream.Close();
                // client.Close();
            }
            catch (SocketException e)
            {

            }
        }

    }

    public class TCPDataItem
    {
        public int Index { get; set; }
        public string Date { get; set; }
        public string Data { get; set; }


        public TCPDataItem()
        {

        }

        public TCPDataItem(int _Index, string _Date, string _Data)
        {
            Index = _Index;
            Date = _Date;
            Data = _Data;
        }
    }

}
