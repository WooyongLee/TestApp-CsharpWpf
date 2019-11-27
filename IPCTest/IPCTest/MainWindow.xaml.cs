using System.Runtime.Remoting.Channels.Ipc;
using System.Windows;
using RemoteControl;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Collections.ObjectModel;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Text;

namespace IPCTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public object LockObject = new object();

        public IpcServerChannel ServerChannel;
        public RemoteObject IPCObject;

        public ObservableCollection<MessageItem> MessageObsCollection;

        public MainWindow()
        {
            InitializeComponent();

            this.InitServer();

            MessageObsCollection = new ObservableCollection<MessageItem>();
            this.MsgListBox.ItemsSource = this.MessageObsCollection;

            // 메모리 공유 - 파일 매핑한 공유메모리 읽기
            MemoryMappedFile mapFile = MemoryMappedFile.OpenExisting(@"D:\TestApp\IPCTest\IPCTest\bin\Debug\MemoryMapTest.txt", MemoryMappedFileRights.ReadWrite);
            Thread thread = new Thread(() =>
            {
                Thread.Sleep(1500);
                while(true)
                { 
                    using (Stream view = mapFile.CreateViewStream())
                    {
                        // stream을 String으로 변환 
                        view.Position = 0;
                        using (StreamReader reader = new StreamReader(view, Encoding.UTF8))
                        {
                            lock (LockObject)
                            {
                                MessageObsCollection.Add(new MessageItem() { Msg = reader.ReadToEnd() });
                            }
                        }
                    }
                }
            });
        }

        public void InitServer()
        {
            IPCObject = new RemoteObject();

            ServerChannel = new IpcServerChannel("remote");
            ChannelServices.RegisterChannel(ServerChannel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject), "IPC", WellKnownObjectMode.Singleton);

            IPCObject = new RemoteObject();
        }

        public string ReceiveFromClient()
        {
            string msg = string.Empty;
            try
            {
                msg = IPCObject.GetServerMessage();
                if (MessageObsCollection != null)
                {
                    MessageObsCollection.Add(new MessageItem() { Msg = msg });
                }
                return msg;
            }
            catch
            {
                msg = "DisConnected";
                return msg;
            }
        }

        // 
        private void NewWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow CWindow = new ClientWindow();
            CWindow.Show();
        }
    }
    
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MessageItem
    {
        public string Msg { get; set; }
    }
}
