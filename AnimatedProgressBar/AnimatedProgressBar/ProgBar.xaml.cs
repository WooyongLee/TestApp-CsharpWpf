using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnimatedProgressBar
{
    /// <summary>
    /// ProgBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgBar : Window
    {
        // 이중 실행 방지를 위한 DLL을 Import 해온다.
        [DllImportAttribute("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string clsName, string windowName);

        public ViewModel VM = new ViewModel();

        public ProgBar()
        {
         //   Duplicate_execution("ProgBar");

            InitializeComponent();

            this.DataPanel.DataContext = VM;
            // this.ProgressBarStatus.IsIndeterminate = false;
        }

        Mutex mutex = null;
        // 중복 실행을 방지하기 위한 Mutex 활용
        private void Duplicate_execution(string mutexName)
        {
            try
            {
                mutex = new Mutex(false, mutexName);
            }
            catch (Exception ex)
            {
                Application.Current.Shutdown();
            }

            if (mutex.WaitOne(0, false))
            {
                InitializeComponent();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        // 작업 진행 중
        public void Progressing()
        {
            VM.Text = "임무계획 생성 중..";
            VM.IsProgressBarEnable = true;
        }

        // 작업 완료
        public void Complete()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                    VM.Text = "임무계획 생성 완료";
                    VM.IsProgressBarEnable = false;
            }));

            // 잠시 대기 후 창 닫기
            // Thread.Sleep(3000);
            // this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int ClosingValue = 1;
        }
    }
}
