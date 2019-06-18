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
        private RBChecker checkBoxVM;
        public RBChecker CheckBoxVM
        {
            get { return checkBoxVM; }
            set { checkBoxVM = value; }
        }
        // Combobox와 바인딩 된 리스트
        public List<int> TermIDList { get; set; }

        public Dictionary<uint, RBChecker> TermIDRBCheckDic = new Dictionary<uint, RBChecker>();

        // 네자리 숫자, 각 자리수 0 또는 1
        public int RowValue = 0;
        public int ColValue = 0;

        private bool IsInit = false;
        private uint TermID = 0;

        // 체크가 수정 되었는 지 확인하는 변수 
        private bool IsModified = false;

        public MainWindow()
        {
            InitializeComponent();

            CheckBoxVM = new RBChecker();

            TermIDList = new List<int>() { 1, 2, 3, 4, 5 };

            this.Loaded += async (s, e) =>
                {
                    await Task.Delay(3000);
                    this.CheckBoxVM.ClearAll();
                };
            
            
            this.DataContext = CheckBoxVM;
            this.IDCombobox.ItemsSource = TermIDList;
        }

        private void RBCheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            IsModified = true;
        }

        private void RBCheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            IsModified = true;
        }

        // Combobox Selection Change
        private void IDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IDCombobox.SelectedItem == null) return;

            IsModified = false;

            // 최초 Combobox Selection Changing 시에는 Dictionary에 추가하는 연산하지 않음
            if (!IsInit)
            {
                IsInit = true;
                return;
            }

            // Selection 전 ID에 Value 저장
            // 키가 있을 경우
            if (TermIDRBCheckDic.ContainsKey(TermID))
            {
                TermIDRBCheckDic[TermID] = (RBChecker)CheckBoxVM.Clone();
            }

            // 키가 없을 경우 - 키를 생성하여 새 값을 넣어주기
            else
            {
                TermIDRBCheckDic.Add(TermID, (RBChecker)CheckBoxVM.Clone());
            }

            var strTermID = IDCombobox.SelectedItem.ToString();

            TermID = uint.Parse(strTermID);
        
            // Selection 후 ID에 Value 저장
            if (TermIDRBCheckDic.ContainsKey(TermID))
            {
                // 기존에 갖고 있던 데이터 적용
                CheckBoxVM = (RBChecker)TermIDRBCheckDic[TermID].Clone();
            }
            else
            {
                CheckBoxVM.ClearAll();
                TermIDRBCheckDic.Add(TermID, CheckBoxVM);
            }

            this.DataContext = TermIDRBCheckDic[TermID];
        }

        // 추가 버튼 클릭 시
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            IsModified = false;

            var strTermID = IDCombobox.SelectedItem.ToString();

            TermID = uint.Parse(strTermID);

            CheckBoxVM.IsCheck = this.CheckedSetter();
            CheckBoxVM.IsEnable = this.EnabledSetter();

            // Selection 전 ID에 Value 저장
            // 키가 있을 경우
            if (TermIDRBCheckDic.ContainsKey(TermID))
            {
                TermIDRBCheckDic[TermID] = (RBChecker)CheckBoxVM.Clone();
            }

            // 키가 없을 경우 - 키를 생성하여 새 값을 넣어주기
            else
            {
                TermIDRBCheckDic.Add(TermID, (RBChecker)CheckBoxVM.Clone());
            }

            this.DataContext = TermIDRBCheckDic[TermID];
        }

        // UI에서 데이터 변경 최종 설정 시 Data쪽에 수정하기
        public RBChecker UIToDataSetter(RBChecker rbChecker)
        {
            #region IsChecked
            if ((bool)RBCheckBox1.IsChecked) { rbChecker.IsCheck[0] = true; }
            else { rbChecker.IsCheck[0] = false; }

            if ((bool)RBCheckBox2.IsChecked) { rbChecker.IsCheck[1] = true; }
            else { rbChecker.IsCheck[1] = false; }

            if ((bool)RBCheckBox3.IsChecked) { rbChecker.IsCheck[2] = true;}
            else { rbChecker.IsCheck[2] = false; }

            if ((bool)RBCheckBox4.IsChecked) { rbChecker.IsCheck[3] = true; }
            else { rbChecker.IsCheck[3] = false; }

            if ((bool)RBCheckBox5.IsChecked)
            {
                rbChecker.IsCheck[4] = true;
            }
            else
            {
                rbChecker.IsCheck[4] = false;
            }

            if ((bool)RBCheckBox6.IsChecked)
            {
                rbChecker.IsCheck[5] = true;
            }
            else
            {
                rbChecker.IsCheck[5] = false;
            }

            if ((bool)RBCheckBox7.IsChecked)
            {
                rbChecker.IsCheck[6] = true;
            }
            else
            {
                rbChecker.IsCheck[6] = false;
            }

            if ((bool)RBCheckBox8.IsChecked)
            {
                rbChecker.IsCheck[7] = true;
            }
            else
            {
                rbChecker.IsCheck[7] = false;
            }

            if ((bool)RBCheckBox9.IsChecked)
            {
                rbChecker.IsCheck[8] = true;
            }
            else
            {
                rbChecker.IsCheck[8] = false;
            }

            if ((bool)RBCheckBox10.IsChecked)
            {
                rbChecker.IsCheck[9] = true;
            }
            else
            {
                rbChecker.IsCheck[9] = false;
            }

            if ((bool)RBCheckBox11.IsChecked)
            {
                rbChecker.IsCheck[10] = true;
            }
            else
            {
                rbChecker.IsCheck[10] = false;
            }

            if ((bool)RBCheckBox12.IsChecked)
            {
                rbChecker.IsCheck[11] = true;
            }
            else
            {
                rbChecker.IsCheck[11] = false;
            }

            if ((bool)RBCheckBox13.IsChecked)
            {
                rbChecker.IsCheck[12] = true;
            }
            else
            {
                rbChecker.IsCheck[12] = false;
            }

            if ((bool)RBCheckBox14.IsChecked)
            {
                rbChecker.IsCheck[13] = true;
            }
            else
            {
                rbChecker.IsCheck[13] = false;
            }

            if ((bool)RBCheckBox15.IsChecked)
            {
                rbChecker.IsCheck[14] = true;
            }
            else
            {
                rbChecker.IsCheck[14] = false;
            }

            if ((bool)RBCheckBox16.IsChecked)
            {
                rbChecker.IsCheck[15] = true;
            }
            else
            {
                rbChecker.IsCheck[15] = false;
            }
            #endregion

            #region IsEnabled
            if ((bool)RBCheckBox1.IsEnabled) { rbChecker.IsEnable[0] = true; }
            else { rbChecker.IsEnable[0] = false; }

            if ((bool)RBCheckBox2.IsEnabled) { rbChecker.IsEnable[1] = true; }
            else { rbChecker.IsEnable[1] = false; }

            if ((bool)RBCheckBox3.IsEnabled) { rbChecker.IsEnable[2] = true; }
            else { rbChecker.IsEnable[2] = false; }

            if ((bool)RBCheckBox4.IsEnabled) { rbChecker.IsEnable[3] = true; }
            else { rbChecker.IsEnable[3] = false; }

            if ((bool)RBCheckBox5.IsEnabled) { rbChecker.IsEnable[4] = true; }
            else { rbChecker.IsEnable[4] = false; }

            if ((bool)RBCheckBox6.IsEnabled) { rbChecker.IsEnable[5] = true; }
            else { rbChecker.IsEnable[5] = false; }

            if ((bool)RBCheckBox7.IsEnabled) { rbChecker.IsEnable[6] = true; }
            else { rbChecker.IsEnable[6] = false; }

            if ((bool)RBCheckBox8.IsEnabled) { rbChecker.IsEnable[7] = true; }
            else { rbChecker.IsEnable[7] = false; }

            if ((bool)RBCheckBox9.IsEnabled) { rbChecker.IsEnable[8] = true; }
            else { rbChecker.IsEnable[8] = false; }

            if ((bool)RBCheckBox10.IsEnabled) { rbChecker.IsEnable[9] = true; }
            else { rbChecker.IsEnable[9] = false; }

            if ((bool)RBCheckBox11.IsEnabled) { rbChecker.IsEnable[10] = true; }
            else { rbChecker.IsEnable[10] = false; }

            if ((bool)RBCheckBox12.IsEnabled) { rbChecker.IsEnable[11] = true; }
            else { rbChecker.IsEnable[11] = false; }

            if ((bool)RBCheckBox13.IsEnabled) { rbChecker.IsEnable[12] = true; }
            else { rbChecker.IsEnable[12] = false; }

            if ((bool)RBCheckBox14.IsEnabled) { rbChecker.IsEnable[13] = true; }
            else { rbChecker.IsEnable[13] = false; }

            if ((bool)RBCheckBox15.IsEnabled) { rbChecker.IsEnable[14] = true; }
            else { rbChecker.IsEnable[14] = false; }

            if ((bool)RBCheckBox16.IsEnabled) { rbChecker.IsEnable[15] = true; }
            else { rbChecker.IsEnable[15] = false; }
            #endregion 
            
            // 매개변수 받은거 그대로 반환
            return rbChecker;
        }

        public bool[] CheckedSetter()
        {
            bool[] IsCheck = new bool[16];

            if ((bool)RBCheckBox1.IsChecked) { IsCheck[0] = true; }
            else { IsCheck[0] = false; }

            if ((bool)RBCheckBox2.IsChecked) { IsCheck[1] = true; }
            else { IsCheck[1] = false; }

            if ((bool)RBCheckBox3.IsChecked) { IsCheck[2] = true; }
            else { IsCheck[2] = false; }

            if ((bool)RBCheckBox4.IsChecked) { IsCheck[3] = true; }
            else { IsCheck[3] = false; }

            if ((bool)RBCheckBox5.IsChecked) { IsCheck[4] = true; }
            else { IsCheck[4] = false; }

            if ((bool)RBCheckBox6.IsChecked) { IsCheck[5] = true; }
            else { IsCheck[5] = false; }

            if ((bool)RBCheckBox7.IsChecked) { IsCheck[6] = true; }
            else { IsCheck[6] = false; }

            if ((bool)RBCheckBox8.IsChecked) { IsCheck[7] = true; }
            else { IsCheck[7] = false; }

            if ((bool)RBCheckBox9.IsChecked) { IsCheck[8] = true; }
            else { IsCheck[8] = false; }

            if ((bool)RBCheckBox10.IsChecked) { IsCheck[9] = true; }
            else { IsCheck[9] = false; }

            if ((bool)RBCheckBox11.IsChecked) { IsCheck[10] = true; }
            else { IsCheck[10] = false; }

            if ((bool)RBCheckBox12.IsChecked) { IsCheck[11] = true; }
            else { IsCheck[11] = false; }

            if ((bool)RBCheckBox13.IsChecked) { IsCheck[12] = true; }
            else { IsCheck[12] = false; }

            if ((bool)RBCheckBox14.IsChecked) { IsCheck[13] = true; }
            else { IsCheck[13] = false; }

            if ((bool)RBCheckBox15.IsChecked) { IsCheck[14] = true; }
            else { IsCheck[14] = false; }

            if ((bool)RBCheckBox16.IsChecked) { IsCheck[15] = true; }
            else { IsCheck[15] = false; }
            return IsCheck;
        }

        public bool[] EnabledSetter()
        {
            bool[] IsEnable = new bool[16];
            if ((bool)RBCheckBox1.IsEnabled) { IsEnable[0] = true; }
            else { IsEnable[0] = false; }

            if ((bool)RBCheckBox2.IsEnabled) { IsEnable[1] = true; }
            else { IsEnable[1] = false; }

            if ((bool)RBCheckBox3.IsEnabled) { IsEnable[2] = true; }
            else { IsEnable[2] = false; }

            if ((bool)RBCheckBox4.IsEnabled) { IsEnable[3] = true; }
            else { IsEnable[3] = false; }

            if ((bool)RBCheckBox5.IsEnabled) { IsEnable[4] = true; }
            else { IsEnable[4] = false; }

            if ((bool)RBCheckBox6.IsEnabled) { IsEnable[5] = true; }
            else { IsEnable[5] = false; }

            if ((bool)RBCheckBox7.IsEnabled) { IsEnable[6] = true; }
            else { IsEnable[6] = false; }

            if ((bool)RBCheckBox8.IsEnabled) { IsEnable[7] = true; }
            else { IsEnable[7] = false; }

            if ((bool)RBCheckBox9.IsEnabled) { IsEnable[8] = true; }
            else { IsEnable[8] = false; }

            if ((bool)RBCheckBox10.IsEnabled) { IsEnable[9] = true; }
            else { IsEnable[9] = false; }

            if ((bool)RBCheckBox11.IsEnabled) { IsEnable[10] = true; }
            else { IsEnable[10] = false; }

            if ((bool)RBCheckBox12.IsEnabled) { IsEnable[11] = true; }
            else { IsEnable[11] = false; }

            if ((bool)RBCheckBox13.IsEnabled) { IsEnable[12] = true; }
            else { IsEnable[12] = false; }

            if ((bool)RBCheckBox14.IsEnabled) { IsEnable[13] = true; }
            else { IsEnable[13] = false; }

            if ((bool)RBCheckBox15.IsEnabled) { IsEnable[14] = true; }
            else { IsEnable[14] = false; }

            if ((bool)RBCheckBox16.IsEnabled) { IsEnable[15] = true; }
            else { IsEnable[15] = false; }
            return IsEnable;
        }
    }

    // CheckBox들에 할당 된 View Model
    public class RBChecker : INotifyPropertyChanged, ICloneable
    {
        const int CountOfCheckbox = 16;

        public ICommand RBCheckCommand { get; set; }

        // 2진 RB를 10진으로 변환한 값
        public int RBAssign { get; set; }

        // 8자리 2진 값으로 표현되는 RB 데이터
        private string rbBinaryString;

        public string RBBinaryString
        {
            get { return rbBinaryString; }
            set { rbBinaryString = value; NotifyPropertyChanged(); }
        }

        public bool[] PrevCheck { get; set; }

        public bool[] CurCheck { get; set; }

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

        public bool[] ConstEnable { get; set; }

        public Dictionary<uint, bool[]> TermIDRBCheckDic= new Dictionary<uint, bool[]>();



        // 강제로 NotifyChange 호출
        public void ArrayNotifyChanger()
        {
            NotifyPropertyChanged("IsCheck");
            NotifyPropertyChanged("IsEnable");
        }

        public RBChecker()
        {
            RBCheckCommand = new RelayCommand(ExecuteMethod, CanExecuteMethod);
            this.IsCheck = new bool[CountOfCheckbox];
            this.IsEnable = new bool[CountOfCheckbox];
            this.PrevCheck = new bool[CountOfCheckbox];
            this.CurCheck = new bool[CountOfCheckbox];
            this.ConstEnable = new bool[CountOfCheckbox];

            // 배열 내에 모든 인자 true로 초기화
            this.IsEnable = Enumerable.Repeat(true, CountOfCheckbox).ToArray();
            this.ConstEnable = Enumerable.Repeat(true, CountOfCheckbox).ToArray();

            this.ArrayNotifyChanger();
        }

        // ICloneable Function 복사생성자
        public object Clone()
        {
            RBChecker newCopyRB = new RBChecker();
            newCopyRB.RBAssign = this.RBAssign;
            newCopyRB.RBBinaryString = this.RBBinaryString;

            for (int i = 0; i < CountOfCheckbox; i++)
            {
                newCopyRB.IsCheck[i] = this.IsCheck[i];
                newCopyRB.PrevCheck[i] = this.PrevCheck[i];
                newCopyRB.IsEnable[i] = this.IsEnable[i];
                newCopyRB.CurCheck[i] = this.CurCheck[i];
                newCopyRB.ConstEnable[i] = this.ConstEnable[i];
            }

            return newCopyRB;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ClearAll()
        {
            for (int i = 0; i < CountOfCheckbox; i++)
            {
                this.IsCheck[i] = false;
                this.IsEnable[i] = true;
                this.PrevCheck[i] = false;
                this.CurCheck[i] = false;
                this.ConstEnable[i] = true;
            }

            this.TermIDRBCheckDic.Clear();
            this.RBAssign = 0;
            this.RBBinaryString = "00000000";

            this.ArrayNotifyChanger();
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
                this.RBBinaryString = "00000000";
                this.RBAssign = 0;
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
            } // end if (check)

            else
            {
                // 체크 해제 하는 칸에 인접한 곳 모두 Enable False로 만들기
                // 인접한 칸에서 또 인접한 부분에 체킹이 되어 있는 지 여부(Enable True로 설정하기 위해서)
                if (index - 4 >= 0) // 상
                {
                    if (!this.isCheck[index - 4]) // 비활성화 시킬 자리에 체크가 되어 있으면
                    {
                        this.IsEnable[index - 4] = false;
                    }

                    if (!this.IsCheck[index - 4])
                    { 
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


            
            } // end else (check)

            // 양 끝 모서리만 활성화 되고 주변부분 비활성화 되지 않도록 만들기
            if (IsEnable[0] && !IsEnable[1] && !IsEnable[4]) // 좌상단
            {
                IsEnable[0] = false;
            }

            if (IsEnable[3] && !IsEnable[2] && !IsEnable[7]) // 우상단
            {
                IsEnable[3] = false;
            }

            if (IsEnable[12] && !IsEnable[8] && !IsEnable[13]) // 좌하단
            {
                IsEnable[12] = false;
            }

            if (IsEnable[15] && !IsEnable[14] && !IsEnable[11]) // 우하단
            {
                IsEnable[15] = false;
            }


            // 최종적으로 PrevEnable이 false 인 곳들도 모두 Enable false로 설정
            for (int i = 0; i < CountOfCheckbox; i++ )
            {
                if (!this.ConstEnable[i])
                {
                    this.IsEnable[i] = false;
                }
            }

            this.ArrayNotifyChanger();

            this.RBBinaryString = RABitmapManager.CheckArrrayToBitmap(IsCheck);

            // binary string -> int
            Regex binary = new Regex("^[01]{1,32}$", RegexOptions.Compiled);
            if (binary.IsMatch(RBBinaryString))
            {
                this.RBAssign = Convert.ToInt32(RBBinaryString, 2);

            }
            else
            {
                this.RBAssign = 0;
            }
        }

        // 자원할당 설정 완료 시 이전의 TermID 에서 설정한 Check 모두 설정한 채로 Disable
        public void RBAllocSet(uint termID)
        {
            // 16개 false로 초기화
            // bool[] isCheckedArray =Enumerable.Repeat(false, 16).ToArray();
            bool[] isCheckedArray = new bool[16];
            for (int i = 0; i < isCheckedArray.Length; i++ )
            {
                if ( this.IsCheck[i])
                {
                    this.ConstEnable[i] = false;
                }

                isCheckedArray[i] = this.IsCheck[i];
            }

            if ( !this.TermIDRBCheckDic.ContainsKey(termID))
            {
                this.TermIDRBCheckDic.Add(termID, isCheckedArray);
            }
            else
            {
                this.TermIDRBCheckDic[termID] = isCheckedArray;
            }


            for (int i = 0; i < CountOfCheckbox; i++)
            {
                if (this.IsCheck[i] && ! this.IsEnable[i])
                {
                    this.CurCheck[i] = true;
                }

                if (this.IsCheck[i])
                {
                    // Enable False로 설정
                    this.IsEnable[i] = false;
                }
            }

            this.ArrayNotifyChanger();
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
