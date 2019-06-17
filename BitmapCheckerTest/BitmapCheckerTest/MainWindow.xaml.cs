using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BitmapCheckerTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public RB CheckBoxVM = new RB();

        // 네자리 숫자, 각 자리수 0 또는 1
        public int RowValue = 0;
        public int ColValue = 0;
        
        public MainWindow()
        {
            InitializeComponent();

            #region Binding Test
            //CheckBoxVM.IsCheck[0] = true;
            //CheckBoxVM.IsCheck[5] = true;
            //CheckBoxVM.IsCheck[10] = true;
            //CheckBoxVM.IsCheck[15] = true;
            #endregion

            this.DataContext = CheckBoxVM;

        }

        private void RBCheckBox1_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void RBCheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }

    // CheckBox들에 할당 된 View Model
    public class RB : INotifyPropertyChanged
    {
        const int CountOfCheckbox = 16;

        public ICommand RBCheckCommand { get; set; }
        
        // Check 한 인덱스
        public int Index { get; set; }

        public bool[] PrevCheck { get; set; }

        // Checkbox의 IsChecked 속성에 바인딩 된 Check 배열 초기화
        private bool[] isCheck = new bool[CountOfCheckbox];
        public bool[] IsCheck
        {
            get { return isCheck; }
            set 
            { 
                isCheck = value;
                NotifyPropertyChanged();
            }
        }

        // CheckBox의 IsEnabled 속성에 바인딩 된 Check 배열 초기화
        private bool[] isEnable;
        public bool[] IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                NotifyPropertyChanged();
            }
        }

        // 강제로 NotifyChange 호출
        public void ArrayNotifyChanger()
        {
            NotifyPropertyChanged("IsCheck");
            NotifyPropertyChanged("IsEnable");
        }

        public RB()
        {
            RBCheckCommand = new RelayCommand(ExecuteMethod, CanExecuteMethod);
            this.IsCheck = new bool[CountOfCheckbox];
            this.IsEnable = new bool[CountOfCheckbox];
            this.PrevCheck = new bool[CountOfCheckbox];

            // 배열 내에 모든 인자 true로 초기화
            this.IsEnable = Enumerable.Repeat(true, CountOfCheckbox).ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

        private void ExecuteMethod(object parameter)
        {
            // 방금 체크된 것 확인 및 모든 체크박스 체크 해제 되어있는 지 확인
            int index = 0; 
            bool check = false;
            int totalUnCheck = 0;

            for (int i = 0; i < CountOfCheckbox; i++)
            {
                // 체크가 바뀐 데이터 확인
                if (this.PrevCheck[i] != this.isCheck[i])
                {
                    index = i;
                    check = this.isCheck[i];
                }

                // 현재 체크 갯수 확인
                if (this.isCheck[i] == false)
                {
                    totalUnCheck++;
                }
            }

            // 이전에 체크 된 데이터 최신화 (prevCheck에 데이터 재설정)
            for (int i = 0; i < CountOfCheckbox; i++) { if (this.PrevCheck[i] != this.isCheck[i]) this.PrevCheck[i] = this.isCheck[i]; }

            // 모두 체크 해제되어 있을 경우 모든 체크박스 활성화
            if (totalUnCheck == CountOfCheckbox)
            {
                this.IsEnable = Enumerable.Repeat(true, CountOfCheckbox).ToArray(); // 모든 인덱스 True로 채우기
                return;
            }

            // 현재 유저가 체크/체크해제를 하였음을 판별
            this.IsCheck[index] = check;

            if (check)
            {
                // 하나 체크될 경우 최초 체크되었다고 가정하여 모든 체크박스 비활성화 해놓고 시작하기
                if (totalUnCheck == CountOfCheckbox - 1)
                {
                    this.IsEnable = Enumerable.Repeat(false, CountOfCheckbox).ToArray();
                }

                this.IsEnable[index] = true;

                if (index - 4 >= 0) // 상
                {
                    this.IsEnable[index - 4] = true;
                }

                if (index - 1 >= 0) // 좌
                {
                    // [4] - > [3] , [8] -> [7] , [12] -> [11] : x
                    if (index != 4 && index != 8 && index != 12)
                    {
                        this.IsEnable[index - 1] = true;
                    }
                }

                if (index + 1 < CountOfCheckbox) // 우
                {
                    // [3] - > [4] , [7] -> [8] , [11] -> [12] : x
                    if (index != 3 && index != 7 && index != 11)
                    {
                        this.IsEnable[index + 1] = true;
                    }
                }

                if (index + 4 < CountOfCheckbox) // 하
                {
                    this.IsEnable[index + 4] = true;
                }
            }

            else
            {
                if (index - 4 >= 0) // 상
                {
                    if (!this.isCheck[index - 4]) // 비활성화 시킬 자리에 체크가 되어 있으면
                    {
                        this.IsEnable[index - 4] = false;
                    }
                }

                if (index - 1 >= 0) // 좌
                {
                    if (!this.isCheck[index - 1])
                    {
                        this.IsEnable[index - 1] = false;
                    }
                }

                if (index + 1 < CountOfCheckbox) // 우
                {
                    if (!this.IsCheck[index + 1])
                    {
                        this.IsEnable[index + 1] = false;
                    }
                }

                if (index + 4 < CountOfCheckbox)  // 하
                {
                    if (!this.IsCheck[index + 4])
                    {
                        this.IsEnable[index + 4] = false;
                    }
                }

                this.isEnable[index] = true;
            }

            this.ArrayNotifyChanger();

            string bitmap = RABitmapManager.CheckArrrayToBitmap(IsCheck);
            int RBAssign = 0;

            // binary string -> int
            Regex binary = new Regex("^[01]{1,32}$", RegexOptions.Compiled);
            if (binary.IsMatch(bitmap))
            {
                RBAssign = Convert.ToInt32(bitmap, 2);
            }
        }
    }

    public static class RABitmapManager
    {
        // CheckBox 배열을 여덟자리 Binary 값으로 변환시키기
        // 총 8자리 : 앞 4자리는 row, 뒤 4자리는 column을 명시
        // 해당 행,열 내에 데이터가 하나라도 있으면 1, 아무것도 없으면 0 
        public static string CheckArrrayToBitmap(bool[] checkArray)
        {
            StringBuilder SbRow = new StringBuilder();
            StringBuilder SbCol= new StringBuilder();

            if ( checkArray == null || checkArray.Length != 16)
            {
                return "00000000";
            }

            int arrSize = 4;

            // 열 데이터 먼저 채워넣기
            for (int i = 0; i < arrSize; i++) // 행 반복
            {
                for (int j = 0; j < arrSize; j++) // 열 반복
                {
                    // check된 값 중 true 가 있으면 바로 1 할당
                    if (checkArray[i + j * arrSize])
                    {
                        SbCol.Append("1");
                        break;
                    }

                    // arrSize만큼 반복했는데 true값이 없을 경우에
                    if (j == arrSize - 1)
                    {
                        SbCol.Append("0");
                        break;
                    }
                }
            }

            // 행 데이터 채워넣기
            for (int i = 0; i < arrSize; i++) // 행 반복
            {
                for (int j = 0; j < arrSize; j++) // 열 반복
                {
                    // check된 값 중 true 가 있으면 바로 1 할당
                    if (checkArray[i * arrSize + j])
                    {
                        SbRow.Append("1");
                        break;
                    }

                    // arrSize만큼 반복했는데 true값이 없을 경우에
                    if (j == arrSize - 1)
                    {
                        SbRow.Append("0");
                        break;
                    }
                }
            }

            return SbRow.ToString() + SbCol.ToString();
        }
    }

    // Command
    public class RelayCommand : ICommand
    {
        Action<object> _ExecuteMethod;
        Func<object, bool> _CanExecuteMethod;
    
        public RelayCommand(Action<object> executemMethod, Func<object, bool> canExecuteMethod)
        {
            _ExecuteMethod = executemMethod;
            _CanExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if ( _ExecuteMethod != null )
            {
                return _CanExecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add {CommandManager.RequerySuggested += value;}
            remove {CommandManager.RequerySuggested -= value;}
        }

        public void Execute(object parameter){
            _ExecuteMethod(parameter);
        }
    
    }
}
