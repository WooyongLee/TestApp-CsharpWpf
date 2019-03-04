using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Shapes;

namespace MiscControl
{
    /// <summary>
    /// ProgressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressBarWindow : Window
    {
        public ProgressBarWindow()
        {
            InitializeComponent();
        }

        // 윈도우가 로드 되기 전에 항목이나 변경 사항을 추가하려면 Loaded 이벤트를 사용,
        // 스크린 샷을 찍는 등 Window의 컨텐츠와 관련된 작업을 수행하려면 ContentRendered 이벤트를 사용해야한다.
        // https://stackoverflow.com/questions/18452756/whats-the-difference-between-the-window-loaded-and-window-contentrendered-event

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            // BackgroundWorker의 진행률을 모니터링하기 위한 속성 => True로 설정
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        // Background 작업을 수행하는 함수, 0.1초에 한번 sleep
        // 수행률을 계속 모니터링하여 수행률 값을 ProgressChanged에 보고하도록 함
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                BackgroundWorker bgw = (BackgroundWorker)sender;
                bgw.ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        // Worker의 진행률에 따라 ProgressBar가 움직이도록 함
        // 진행 Percentage가 Bar의 상태값에 쭉 따라가도록 하였음 
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ProgressBarStatus.Value = e.ProgressPercentage;
        }
    }
}
