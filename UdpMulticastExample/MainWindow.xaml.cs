﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace UdpMulticastExample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MulticastUdpClient udpClientWrapper;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // address 객체 생성
            int port = Int32.Parse(txtPort.Text);
            IPAddress multicastIPaddress = IPAddress.Parse(txtRemoteIP.Text);
            IPAddress localIPaddress = IPAddress.Any;
                
            // MulticastUdpClient 객체 생성 및 이벤트 할당
            udpClientWrapper = new MulticastUdpClient(multicastIPaddress, port, localIPaddress);
            udpClientWrapper.UdpMessageReceived += OnUdpMessageReceived;

            AddToLog("UDP Client started");
        }

        int i = 1;
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // 메시지 생성
            string msgString = String.Format("Message from {0} pid {1} #{2}",
                GetLocalIPAddress(),
                System.Diagnostics.Process.GetCurrentProcess().Id,
                i.ToString());
            i++;
            byte[] buffer = Encoding.Unicode.GetBytes(msgString);

            // 멀티캐스트로 송신
            udpClientWrapper.SendMulticast(buffer);
            AddToLog("Sent message: " + msgString);
        }

        /// <summary>
        /// UDP 메시지 수신 이벤트
        /// </summary>
        void OnUdpMessageReceived(object sender, MulticastUdpClient.UdpMessageReceivedEventArgs e)
        {
            string receivedText = ASCIIEncoding.Unicode.GetString(e.Buffer);
            AddToLog("Received message: " + receivedText);
        }

        /// <summary>
        /// log에 송-수신 정보를 출력
        /// </summary>
        void AddToLog(string s)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                txtLog.Text += Environment.NewLine;
                txtLog.Text += s;
            }), null);
        }

        // http://stackoverflow.com/questions/6803073/get-local-ip-address
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
