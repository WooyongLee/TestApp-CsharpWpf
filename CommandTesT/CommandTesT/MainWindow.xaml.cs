
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommandTesT
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        ICommand clickCommand = new RelayCommand();
        public ICommand ClickCommand
        {
            get { return clickCommand; }
        }

        // RoutedUICommand Test
        public static readonly RoutedUICommand CopyCommand = new RoutedUICommand("MyCopy", "CopyFunc", typeof(MainWindow), null);

        public MainWindow()
        {
            InitializeComponent();

            TestTextBox.TextChanged += TestTextBox_TextChanged;


            RelayCommand cmd = new RelayCommand(this.TestTextBox);
        }

        private void TestTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // 텍스트 상자 안에 사용자가 텍스트를 입력한 경우 Button을 활성화
            (clickCommand as RelayCommand).FireChanged(TestTextBox.Text.Length != 0);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("RoutedUICommand.Execute() Call!");
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }

    public class RelayCommand : ICommand
    {
        TextBox textBox;

        public RelayCommand()
        {

        }

        public RelayCommand(TextBox _textBox)
        {
            this.textBox = _textBox;
        }

        public event EventHandler CanExecuteChanged;
        //{
        //    // 의존 속성들에 대한 상태변경을 스스로 알 수 있다는 점을 통해
        //    // 그런 경우 발생하는 알림 이벤트를 별도로 정의 : CommandManager.RequerySuggested
        //    add
        //    {
        //        // Button객체가 다음을 호출, 여기서 전달한 이벤트 핸들러 메서드를 가리키는 value는 다시 Command.RequerySuggested 이벤트로 전달되어
        //        // WPF 내부에서 RequerySuggested 이벤트를 발생시킬 때 마다 CanExecuteChagned 알림을 받게 되고, CanExecute 호출해 객체의 상태 조회함
        //        CommandManager.RequerySuggested += value;
        //    }

        //    remove
        //    {
        //        CommandManager.RequerySuggested -= value;
        //    }
        //}

        // 최초 한번만 실행시켜 상태를 알아본 후 그다음부터  호출하지 않느 ㄴ함수
        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(textBox.Text);
        }

        public void Execute(object parameter)
        {
            string txt = DateTime.Now + " : RelayCommand.Excute() - " + parameter;
            MessageBox.Show(txt);
        }

        // CommandManager.RequerySuggedsted 덕분에 호출하지 않아도 되는 함수
        public void FireChanged(bool flag)
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }


}
