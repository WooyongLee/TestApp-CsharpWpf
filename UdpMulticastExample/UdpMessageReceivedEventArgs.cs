using System;
using System.Net;
using System.Net.Sockets;

namespace UdpMulticastExample
{
    /// <summary>
    /// Multicast UdpClient wrapper with send and receive capabilities.
    /// Usage: pass local and remote multicast IPs and port to constructor.
    /// Use Send method to send data,
    /// subscribe to Received event to get notified about received data.
    /// </summary>
    public class MulticastUdpClient
    {
        // UdpClient 및 port
        UdpClient _udpclient;
        int _port;
        
        // 멀티캐스트, 로컬 IP
        IPAddress _multicastIPaddress, _localIPaddress;
        
        // 멀티캐스트 EndPoint
        IPEndPoint _localEndPoint, _remoteEndPoint;

        public MulticastUdpClient(IPAddress multicastIPaddress, int port, IPAddress localIPaddress = null)
        {
            // 매개변수 대입
            _multicastIPaddress = multicastIPaddress;
            _port = port;
            _localIPaddress = localIPaddress;
            if (localIPaddress == null)
                _localIPaddress = IPAddress.Any;

            // Ip 주소를 통해서 Endpoint 생성
            _remoteEndPoint = new IPEndPoint(_multicastIPaddress, port);
            _localEndPoint = new IPEndPoint(_localIPaddress, port);

            // UdpClinent 객체 생성 및
            // 다수의 Client를 같은 PC 안에서 운영할 수 있도록 설정
            _udpclient = new UdpClient();
            _udpclient.ExclusiveAddressUse = false;
            _udpclient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpclient.ExclusiveAddressUse = false;

            // Bind, Join
            _udpclient.Client.Bind(_localEndPoint);
            _udpclient.JoinMulticastGroup(_multicastIPaddress, _localIPaddress);

            // 들어오는 데이터를 받는 이벤트 연결
            _udpclient.BeginReceive(new AsyncCallback(ReceivedCallback), null);
        }

        /// <summary>
        /// UDP로 멀티캐스트 주소를 통해 데이터(buffer) 송신
        /// </summary>
        /// <param name="bufferToSend"></param>
        public void SendMulticast(byte[] bufferToSend)
        {
            _udpclient.Send(bufferToSend, bufferToSend.Length, _remoteEndPoint);
        }

        /// <summary>
        /// Udp로 들어오는 Packet이 들어올 때 마다 불리는 콜백
        /// </summary>
        /// <param name="ar"></param>
        private void ReceivedCallback(IAsyncResult ar)
        {
            // Get received data
            IPEndPoint sender = new IPEndPoint(0, 0);
            Byte[] receivedBytes = _udpclient.EndReceive(ar, ref sender);

            // fire event if defined
            if (UdpMessageReceived != null)
                UdpMessageReceived(this, new UdpMessageReceivedEventArgs() { Buffer = receivedBytes });

            // Restart listening for udp data packages
            _udpclient.BeginReceive(new AsyncCallback(ReceivedCallback), null);
        }

        /// <summary>
        /// UDP 메시지가 들어올 때 호출 될 Event Handler
        /// </summary>
        public event EventHandler<UdpMessageReceivedEventArgs> UdpMessageReceived;

        /// <summary>
        /// UdpMessageReceived 이벤트 핸들러
        /// </summary>
        public class UdpMessageReceivedEventArgs : EventArgs
        {
            public byte[] Buffer { get; set; }
        }

    }
}