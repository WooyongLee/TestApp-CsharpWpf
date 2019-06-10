using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

namespace AnimatedProgressBar
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // 이중 실행 방지를 위한 DLL을 Import 해온다.
        [DllImportAttribute("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string clsName, string windowName);

        public MainWindow()
        {
            InitializeComponent();
        }

        ProgBar progbar;
        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            progbar = new ProgBar();

            string progBarTitle = progbar.Title;

            progbar.VM.Text = "임무계획 생성 중..";
            progbar.VM.IsProgressBarEnable = true;

            if (FindWindow(null, progBarTitle) > 1)
            {
                progbar.Close();
            }
            else
            {
                progbar.Show();
            }
        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            if (progbar != null)
            {
                progbar.Progressing();
            }
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            if (progbar != null)
            {
                progbar.Complete();
            }
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string text;
        private bool isProgressBarEnable;
        public string Text
        {
            get { return text; }
            set
            {
                if (this.Text != value)
                {
                    text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
        public bool IsProgressBarEnable
        {
            get { return isProgressBarEnable; }
            set
            {
                isProgressBarEnable = value;
                NotifyPropertyChanged("IsProgressBarEnable");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
