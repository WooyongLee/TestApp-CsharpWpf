using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;

namespace CommandConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RelayCommand cmd = new RelayCommand();

            CmdTestObject testObj = new CmdTestObject();
            testObj.Command = cmd;
            testObj.Run();
        }
    }

    // 서로 다른 객체 간의 명령 전달 및 처리를 구현하기 위한 ICommand 인터페이스 구현
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public RelayCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            // 명령어 처리 불가하도록 하는 조건을 구현
            // 숫자가 들어간 텍스트는 False 반환
            if (parameter is string)
            {
                Regex rgx = new Regex("[^0-9]");
                if ( rgx.IsMatch((string)parameter))
                {
                    // Event 호출
                    CanExecuteChanged(parameter, EventArgs.Empty);
                    return true;
                }

                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            string txt = DateTime.Now + " : RelayCommand.Excute() - " + parameter;
            Console.WriteLine(txt);

            
        }
    }

    public class CmdTestObject
    {
        // Command 객체 생성
        public ICommand Command { get; set; }

        public void Run()
        {
            Command.CanExecuteChanged += Command_CanExecuteChanged;

            while (true)
            {
                string txt = Console.ReadLine();
                if ( string.IsNullOrEmpty(txt))
                {
                    break;   
                }

                if ( Command.CanExecute(txt))
                {
                    Command.Execute(txt);
                }

                // Command.Execute(txt);
            }
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Command_CanExecuteChanged() Call!!" + (string)sender);
        }
    }
}
