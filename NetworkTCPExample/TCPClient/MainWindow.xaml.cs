using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace TCPClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // 연결상태 - 처음은 stop이 True
        public bool IsClientStop = true;

        public TCPclientManager ClientManager;

        public MainWindow()
        {
            InitializeComponent();

          //  IPtextBox.Text = GetLocalIP();

            ClientManager = new TCPclientManager();
        }

        // Client Start
        private void ClientStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsClientStop)
            {
                ClientManager.BindIP = IPtextBox.Text;
                ClientManager.BindPort = int.Parse(PORTtextBox.Text);

                ClientManager.ServerIP = ServerIPtextBox.Text;
                ClientManager.ServerPort = int.Parse(ServerPORTtextBox.Text);

                ClientManager.ClientStart();

                IsClientStop = false;
            }

            else
            {
                IsClientStop = true;
               // ClientManager.ClientStop();
            }
        }

        // 보내기
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ClientManager.Message = SendMessageTextBox.Text;
            ClientManager.SendDataStream();
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

    public class TCPclientManager
    {
        public string BindIP { get; set; }
        public int BindPort { get; set; }
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }

        // 서버측으로 보낼 메시지
        public string Message { get; set; }

        public TcpClient client = null; // System.Net.Socket
        public IPEndPoint clientAddress, serverAddress; // System.Net

        public TCPclientManager()
        {

        }

        public void ClientStart()
        {
            try
            {
                // EndPoint :: 어떠한 소프트웨어나 제품에 최종목적지인 사용자, 
                // 그 예로는 PC나 노트북, 핸드폰등 유저가 사용하는 devices등
                //if (clientAddress == null || serverAddress == null)
                //{
                //    clientAddress = new IPEndPoint(IPAddress.Parse(BindIP), BindPort);

                //    serverAddress = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);

                //}
                //client = new TcpClient(clientAddress);

                //client.Connect(serverAddress);
                if (BindIP == "" || ServerIP == "") return;

                clientAddress = new IPEndPoint(IPAddress.Parse(BindIP), BindPort);

                serverAddress = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);

                // client = new TcpClient(clientAddress);
                // client.Client.Bind(clientAddress);
                // client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                // client.Connect(serverAddress);
            }

            catch (SocketException ex)
            {

            }
        }


        public void SendDataStream()
        {
            try
            {
                client = new TcpClient(clientAddress);
                // client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.Connect(serverAddress);

                // TextBox로부터 들어온 보낼 메세지를 TCP 통신을 위한 byte data로 변환
                byte[] data = System.Text.Encoding.Default.GetBytes(Message);

                // 데이터 송신을 위한 stream 객체를 생성
                NetworkStream stream =  client.GetStream();

                // stream에 data들을 싣음 (data내용, offset, data길이)
                stream.Write(data, 0, data.Length);

                data = new byte[256];

                string responseData = "";

                int bytes = stream.Read(data, 0, data.Length);

                responseData = Encoding.Default.GetString(data, 0, bytes);

                // 수신 :: responseData , Textbox에 뿌리던지 말든지
                stream.Close();
               // client.Close();
            }
            catch (SocketException e)
            {

            }
        }

        internal void ClientStop()
        {
        }
    }

}
