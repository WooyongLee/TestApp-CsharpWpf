using System.Runtime.Remoting.Channels.Ipc;
using System.Windows;
using RemoteControl;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Security;
using System;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace IPCTest
{
    /// <summary>
    /// ClientWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ClientWindow : Window
    {
        public object LockObject = new object();

        IpcClientChannel ClientChannel;
        RemoteObject IPCObject;

        public ClientWindow()
        {
            InitializeComponent();

            this.InitClient();
        }

        public void InitClient()
        {
            try
            {
                ClientChannel = new IpcClientChannel();
                ChannelServices.RegisterChannel(ClientChannel, false);

                RemotingConfiguration.RegisterWellKnownClientType(typeof(RemoteObject), "ipc://remote/IPC");

                IPCObject = new RemoteObject();
            }

            catch ( Exception e)
            {
                string strError = e.ToString();
            }
        }

        public bool ClientSend(string msg)
        {
            try
            {
                IPCObject.ClientToServer(msg);
                return true;
            }

            catch (Exception e)
            {
                string strError = e.ToString();
                return false;
            }
        }

        private void ClientMessageSendButton_Click(object sender, RoutedEventArgs e)
        {
            string msg = ClientMessageTextBox.Text;
            this.ClientSend(msg);

            MemoryMappedFile mapFile = MemoryMappedFile.OpenExisting(@"D:\TestApp\IPCTest\IPCTest\bin\Debug\MemoryMapTest.txt", MemoryMappedFileRights.ReadWrite);
            MemoryMappedViewAccessor accessor = mapFile.CreateViewAccessor();

            // 공유 Memory에 쓰기
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(msg + "\0");

            lock (LockObject)
            {
                accessor.WriteArray(0, Buffer, 0, Buffer.Length);
            }

            accessor.Dispose();
            mapFile.Dispose();
        }
    }
}
