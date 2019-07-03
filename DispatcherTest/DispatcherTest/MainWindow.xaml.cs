using System.Security.Permissions;
using System.Windows;
using System.Windows.Threading;
namespace DispatcherTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DispatcherFrameDoEvent();
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void DispatcherFrameDoEvent()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);

        }

        private object ExitFrame(object arg)
        {
            ((DispatcherFrame)arg).Continue = false;
            return null;
        }
    }
}
