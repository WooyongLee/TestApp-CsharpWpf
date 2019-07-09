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
using System.Windows.Input;
using System.Windows.Threading;

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

        public List<int> BackUpIDList { get; set; }

        // Key : GrantID, Value : (DL Term) ID - RBChecker Dic 쌍의 Dictionary 정의
        public Dictionary<uint, Dictionary<uint, RBChecker>> GrantTermIDRBDic = new Dictionary<uint, Dictionary<uint, RBChecker>>();

        // 네자리 숫자, 각 자리수 0 또는 1

        private bool IsInit = false;
        private uint TermID = 0;

        private uint prevGrantID = 0;

        // 체크가 수정 되었는 지 확인하는 변수 
        private bool IsModified = false;

        public MainWindow()
        {
            InitializeComponent();

            CheckBoxVM = new RBChecker();

            TermIDList = new List<int>() { 1, 2, 3, 4, 5 };
            BackUpIDList = new List<int>() { 1, 2, 3 };

            this.Loaded += async (s, e) =>
                {
                    await Task.Delay(3000);
                    this.CheckBoxVM.ClearCurrentState();
                };

            for (uint i = 1; i <= 5; i++)
            {
            }

            this.DataContext = CheckBoxVM;
            this.IDCombobox.ItemsSource = TermIDList;
            this.BackUpCombobox.ItemsSource = BackUpIDList;
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

            // BackUp TermID 설정
            if ( BackUpCombobox.SelectedItem == null)
            {
                return;
            }

            TermID = uint.Parse(IDCombobox.SelectedItem.ToString());

            uint selectedGrantTermID = uint.Parse(BackUpCombobox.SelectedItem.ToString());
            Dictionary<uint, RBChecker> tmpIdRBCheckerDic = new Dictionary<uint, RBChecker>();
            if (GrantTermIDRBDic.ContainsKey(selectedGrantTermID))
            {
                if (GrantTermIDRBDic[selectedGrantTermID].ContainsKey(TermID))
                {
                    CheckBoxVM = (RBChecker)GrantTermIDRBDic[selectedGrantTermID][TermID].Clone();
                }
            }

            CheckBoxVM.ArrayNotifyChanger();
            this.DataContext = CheckBoxVM;
        }

        // 추가 버튼 클릭 시
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            IsModified = false;

            if (!CheckBoxVM.CheckRectangle())
            {
                return;
            }

            // BackUp TermID (GrantTermID) 설정
            if (BackUpCombobox.SelectedItem == null)
            {
                return;
            }

            var strTermID = IDCombobox.SelectedItem.ToString();

            TermID = uint.Parse(strTermID);

            // 강제로 Checkbox UI를 ViewModel에 할당 
            CheckBoxVM.IsCheck = this.CheckedSetter();
            CheckBoxVM.IsEnable = this.EnabledSetter();

            uint selectedGrantTermID = uint.Parse(BackUpCombobox.SelectedItem.ToString());
            Dictionary<uint, RBChecker> tmpIdRBCheckerDic = new Dictionary<uint, RBChecker>();
            if (GrantTermIDRBDic.ContainsKey(selectedGrantTermID))
            {
                if (GrantTermIDRBDic[selectedGrantTermID].ContainsKey(TermID))
                {
                    GrantTermIDRBDic[selectedGrantTermID][TermID] = (RBChecker)CheckBoxVM.Clone();
                }
                else
                {
                    GrantTermIDRBDic[selectedGrantTermID].Add(TermID, (RBChecker)CheckBoxVM.Clone());
                }
            }

            else
            {
                tmpIdRBCheckerDic.Add(TermID, (RBChecker)CheckBoxVM.Clone());
                GrantTermIDRBDic.Add(selectedGrantTermID, tmpIdRBCheckerDic);
            }

            Dispatcher.BeginInvoke(new Action(() => 
            {
                BitmapTextBox.Text = CheckBoxVM.RBBinaryString;
            }));

            this.DataContext = CheckBoxVM;
        }

        // UI Thread 활용 테스트함수 (19. 6. 27)
        public void DispatcherTestMothod()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() => { }));
            DispatcherFrame frame = new DispatcherFrame();
            this.Dispatcher.BeginInvoke(new Action(() => { frame.Continue = false; }));
            Dispatcher.PushFrame(frame);
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

        public void RBReset()
        {
            this.CheckBoxVM.ClearAll();
            this.AllEnableAndUncheck();

            uint selectedGrantID = uint.Parse(BackUpCombobox.SelectedItem.ToString());
            if (GrantTermIDRBDic.ContainsKey(selectedGrantID))
            {
                GrantTermIDRBDic[selectedGrantID].Clear();
            }
        }

        // 초기화 버튼 클릭 이벤트
        private void ResteButton_Click(object sender, RoutedEventArgs e)
        {
            this.RBReset();
        }

        // UI 상에 직접 접근하여 모든 Checkbox 초기화
        private void AllEnableAndUncheck()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                RBCheckBox1.IsChecked = false;
                RBCheckBox2.IsChecked = false;
                RBCheckBox3.IsChecked = false;
                RBCheckBox4.IsChecked = false;
                RBCheckBox5.IsChecked = false;
                RBCheckBox6.IsChecked = false;
                RBCheckBox7.IsChecked = false;
                RBCheckBox8.IsChecked = false;
                RBCheckBox9.IsChecked = false;
                RBCheckBox10.IsChecked = false;
                RBCheckBox11.IsChecked = false;
                RBCheckBox12.IsChecked = false;
                RBCheckBox13.IsChecked = false;
                RBCheckBox14.IsChecked = false;
                RBCheckBox15.IsChecked = false;
                RBCheckBox16.IsChecked = false;

                RBCheckBox1.IsEnabled = true;
                RBCheckBox2.IsEnabled = true;
                RBCheckBox3.IsEnabled = true;
                RBCheckBox4.IsEnabled = true;
                RBCheckBox5.IsEnabled = true;
                RBCheckBox6.IsEnabled = true;
                RBCheckBox7.IsEnabled = true;
                RBCheckBox8.IsEnabled = true;
                RBCheckBox9.IsEnabled = true;
                RBCheckBox10.IsEnabled = true;
                RBCheckBox11.IsEnabled = true;
                RBCheckBox12.IsEnabled = true;
                RBCheckBox13.IsEnabled = true;
                RBCheckBox14.IsEnabled = true;
                RBCheckBox15.IsEnabled = true;
                RBCheckBox16.IsEnabled = true;
            }));
        }

        // Backup된 리스트 저장
        private void SaveBackUpIDButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveBackUp();
        }

        public void SaveBackUp()
        {
            if (BackUpCombobox.SelectedItem != null )
            {
                uint curBackupID = uint.Parse(BackUpCombobox.SelectedItem.ToString()); // DLTermID

                Dictionary<uint, RBChecker> tmpTermIDRBCheckDic = new Dictionary<uint, RBChecker>();
                // 가장 많이 체크 된 TermID에 대한 Dictionary를 curID가 키인 곳에 저장
                for (int i = 0; i < TermIDList.Count; i++)
                {
                    uint curRBID = (uint)TermIDList[i];

                    if (GrantTermIDRBDic.ContainsKey(curBackupID))
                    {
                    }
                }

                // 선택한 BackUp ID 키에 따른 Value로 Dictionary에 저장 (필요할 경우 깊은복사)
                if (GrantTermIDRBDic.ContainsKey(curBackupID))
                {
                    GrantTermIDRBDic[curBackupID] = tmpTermIDRBCheckDic;
                }

                else
                {
                    GrantTermIDRBDic.Add(curBackupID, tmpTermIDRBCheckDic);
                }
            }
        }

        // BackUp ID Combobox 선택 변경 시  
        private void BackUpCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BackUpCombobox.SelectedItem != null)
            {
                uint curGrantID = uint.Parse(BackUpCombobox.SelectedItem.ToString());

                CheckBoxVM.ClearAll();

                if (this.GrantTermIDRBDic.ContainsKey(curGrantID))
                {
                    if (IDCombobox.SelectedItem == null)
                    {
                        return;
                    }

                   // uint rbID = uint.Parse(IDCombobox.SelectedItem.ToString());
                    uint rbID = 1;

                    if (GrantTermIDRBDic[curGrantID].ContainsKey(rbID))
                    {
                        CheckBoxVM = (RBChecker)GrantTermIDRBDic[curGrantID][rbID].Clone();
                        this.prevGrantID = curGrantID;
                    }

                    // 이전 선택으로 되돌리기
                    else
                    {
                        GrantTermIDRBDic[curGrantID].Add(rbID, new RBChecker());
                        CheckBoxVM.ClearAll();
                        CheckBoxVM.RbStateMsg = "해당 RB ID를 먼저 적용바람";
                    }



                    this.DataContext = CheckBoxVM;
                    CheckBoxVM.ArrayNotifyChanger();
                }

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    IDCombobox.SelectedIndex = 0;
                }));

                this.prevGrantID = curGrantID;
            }
        }
    }

    // CheckBox들에 할당 된 View Model
    public class RBChecker : INotifyPropertyChanged, ICloneable
    {
        const int CountOfCheckbox = 16;

        public ICommand RBCheckCommand { get; set; }
        public ICommand ApplyButtonCommand { get; set; }

        public int CommandParam { get; set; }

        // 2진 RB를 10진으로 변환한 값
        public int RBAssign { get; set; }
        
        // 체크 타당성을 알려주는 메세지
        private string rbStateMsg;
        public string RbStateMsg
        {
            get { return rbStateMsg; }
            set { rbStateMsg = value; NotifyPropertyChanged(); }
        }

        // 8자리 2진 값으로 표현되는 RB 데이터
        private string rbBinaryString;
        public string RBBinaryString
        {
            get { return rbBinaryString; }
            set { rbBinaryString = value; NotifyPropertyChanged(); }
        }

        // Checkbox의 IsChecked 속성에 바인딩 된 Check 배열 (ViewModel)
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

        // CheckBox의 IsEnabled 속성에 바인딩 된 Check 배열 (ViewModel)
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

        // 체크박스 체크 명령을 주기 직전의 체크 여부(현재 체크 명령이 Checked 인 지 Unchecked 인 지 판별하기 위함)
        public bool[] PrevCheck { get; set; }

        // RB 할당후 확인을 누른 직후에 저장한 데이터(이미 다른 키에서 할당이 된 데이터들)
        private bool[] alCheck;
        public bool[] AlCheck
        {
            get { return alCheck; }
            set { alCheck = value;  }
        }

        private bool[] alEnable;
        public bool[] AlEnable
        {
            get { return alEnable; }
            set { alEnable = value; }
        }

        public Dictionary<uint, bool[]> TermIDRBCheckDic = new Dictionary<uint, bool[]>();

        // 강제로 NotifyChange 호출
        public void ArrayNotifyChanger()
        {
            NotifyPropertyChanged("IsCheck");
            NotifyPropertyChanged("IsEnable");
        }

        public RBChecker()
        {
            // Command 생성
            this.RBCheckCommand = new RelayCommand(ExecuteMethod, CanExecuteMethod);
            this.ApplyButtonCommand = new RelayCommand(ExecuteApplyButton, CanExecuteMethod); 

            this.IsCheck = new bool[CountOfCheckbox];
            this.IsEnable = new bool[CountOfCheckbox];
            this.PrevCheck = new bool[CountOfCheckbox];
            this.AlCheck = new bool[CountOfCheckbox];
            this.AlEnable = new bool[CountOfCheckbox];

            // 배열 내에 모든 인자 true로 초기화
            this.IsEnable = Enumerable.Repeat(true, CountOfCheckbox).ToArray();
            this.AlEnable = Enumerable.Repeat(true, CountOfCheckbox).ToArray();
        }

        // ICloneable Function 복사생성자 함수
        public object Clone()
        {
            RBChecker newCopyRB = new RBChecker();
            newCopyRB.RBAssign = this.RBAssign;
            newCopyRB.RBBinaryString = this.RBBinaryString;

            for (int i = 0; i < CountOfCheckbox; i++)
            {
                newCopyRB.IsCheck[i] = this.IsCheck[i];
                newCopyRB.IsEnable[i] = this.IsEnable[i];

                newCopyRB.PrevCheck[i] = this.PrevCheck[i];

                newCopyRB.AlEnable[i] = this.AlEnable[i];
                newCopyRB.AlCheck[i] = this.AlCheck[i];
            }
            return newCopyRB;
        }

        // 객체를 복사하되, AlEnable 및 AlCheck는 이전 데이터 손실이 우려되기 때문에 안전한 복사를 위해 파라미터를 두었음
        public object Clone(bool[] alEnable, bool[] alCheck)
        {
            RBChecker newCopyRB = new RBChecker();
            newCopyRB.RBAssign = this.RBAssign;
            newCopyRB.RBBinaryString = this.RBBinaryString;

            for (int i = 0; i < CountOfCheckbox; i++)
            {
                newCopyRB.IsCheck[i] = this.IsCheck[i];
                newCopyRB.IsEnable[i] = this.IsEnable[i];

                newCopyRB.PrevCheck[i] = this.PrevCheck[i];

                // 파라미터로 부터 들어온 데이터 복사
                newCopyRB.AlEnable[i] = alEnable[i];
                newCopyRB.AlCheck[i] = alCheck[i];
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
                // Check는 false로, Enable는 true로 모두 초기화
                this.IsCheck[i] = false;
                this.IsEnable[i] = true;
                this.PrevCheck[i] = false;
                this.AlCheck[i] = false;
                this.AlEnable[i] = false;
            }

            this.RBAssign = 0;
            this.RBBinaryString = "00000000";
            this.RbStateMsg = "초기화 완료";

            this.ArrayNotifyChanger();
        }

        public void ClearCurrentState()
        {
            for (int i = 0; i < CountOfCheckbox; i++)
            {
                // Check는 false로, Enable는 true로 모두 초기화
                this.IsCheck[i] = false;
                this.IsEnable[i] = true;
                this.PrevCheck[i] = false;
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

        // RB할당 체크박스를 하나씩 체크할 때 마다 발동되는 커맨드 함수
        private void ExecuteMethod(object parameter)
        {
           // 방금 체크된 것 확인 및 모든 체크박스 체크 해제 되어있는 지 확인
            int index = 0;
            bool check = false;
            int totalUnCheck = 0;

            for (int i = 0; i < CountOfCheckbox; i++)
            {
                // 체크가 바뀐 데이터 확인
                if (this.PrevCheck[i] != this.IsCheck[i])
                {
                    index = i;
                    check = this.IsCheck[i];
                }

                // 현재 체크 갯수 확인
                if (this.IsCheck[i] == false)
                {
                    totalUnCheck++;
                }
            }

            // 이전에 체크 된 데이터 최신화 (prevCheck에 데이터 재설정)
            for (int i = 0; i < CountOfCheckbox; i++) { if (this.PrevCheck[i] != this.IsCheck[i]) this.PrevCheck[i] = this.IsCheck[i]; }

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

            // 체크한 위치로 부터 상, 하, 좌, 우 Index
            int topIndex = index - 4; // 체크 한 위치의 상측 위치
            int leftIndex = index - 1;  // 체크 한 위치의 좌측 위치
            int rightIndex = index + 1; // 체크 한 위치의 우측 위치
            int bottomIndex = index + 4; // 체크 한 위치의 하측 위치

            // 현재 명령 : Checked
            if (check)
            {
                // 하나 체크될 경우 최초 체크되었다고 가정하여 모든 체크박스 비활성화 해놓고 시작하기
                if (totalUnCheck == CountOfCheckbox - 1)
                {
                    this.IsEnable = Enumerable.Repeat(false, CountOfCheckbox).ToArray();
                }

                this.IsEnable[index] = true;
                
                if (topIndex >= 0) // 상
                {
                    // 이전에 이미 체크 되어있을 경우에 계속 여전히 Disable 상태로 남겨두기
                    if (this.AlCheck[topIndex])
                    {
                        this.IsEnable[topIndex] = false;
                    }
                    else
                    {
                        this.IsEnable[topIndex] = true;
                    }
                }

                if (leftIndex >= 0) // 좌
                {
                    // 왼쪽 최 외각일 경우 이전상태 유지
                    if (CheckLeftEmpty(index)) { }

                    else
                    {
                        // 이전에 이미 체크 되어있을 경우에 계속 여전히 Disable 상태로 남겨두기
                        if (this.AlCheck[leftIndex])
                        {
                            this.IsEnable[leftIndex] = false;
                        }
                        else
                        {
                            this.IsEnable[leftIndex] = true;
                        }
                    }
                }

                if (rightIndex < CountOfCheckbox) // 우
                {
                    // 오른쪽 최 외각일 경우 이전상태 유지
                    if (CheckRightEmpty(index)) { }  
                    
                    else
                    {
                        // 이전에 이미 체크 되어있을 경우에 계속 여전히 Disable 상태로 남겨두기
                        if (this.AlCheck[rightIndex])
                        {
                            this.IsEnable[rightIndex] = false;
                        }
                        else
                        {
                            this.IsEnable[rightIndex] = true;
                        }
                    }
                }

                if (bottomIndex < CountOfCheckbox) // 하
                {
                    // 이전에 이미 체크 되어있을 경우에 계속 여전히 Disable 상태로 남겨두기
                    if (this.AlCheck[bottomIndex])
                    {
                        this.IsEnable[bottomIndex] = false;
                    }
                    else
                    {
                        this.IsEnable[bottomIndex] = true;
                    }
                }
                this.RbStateMsg = index.ToString() + "번 노드 체크";
            } // end if (check)

            // 현재 명령 : UnChecked
            else
            {
                // 체크한 곳이 상-하 및 좌-우 사이 (중간 항목)일 경우 체크명령 원복
                if ((topIndex >= 0) && (bottomIndex < CountOfCheckbox))
                {
                    if (this.IsCheck[topIndex] && this.IsCheck[bottomIndex])
                    {
                        if (index == 5 || index == 6 || index == 9 || index == 10)
                        {
                            this.IsCheck[index] = true;
                            this.PrevCheck[index] = true;
                            RbStateMsg = "최 외곽 항목을 먼저 해제해 주세요";
                            this.ArrayNotifyChanger();
                            return;
                        }
                    }
                }

                if ((leftIndex >= 0) && (rightIndex < CountOfCheckbox))
                {
                    if (this.IsCheck[leftIndex] && this.IsCheck[rightIndex])
                    {
                        if (index == 5 || index == 6 || index == 9 || index == 10)
                        {
                            this.IsCheck[index] = true;
                            this.PrevCheck[index] = true;
                            RbStateMsg = "최 외곽 항목을 먼저 해제해 주세요";
                            this.ArrayNotifyChanger();
                            return;
                        }
                    }
                }

                // 체크 해제 하는 칸에 인접한 곳(상, 하, 좌, 우) IsEnable 여부 판별하기
                // 인접한 칸에서 또 인접한 부분에 체킹이 되어 있는 지 여부(Enable True로 설정하기 위해서)
                if (topIndex >= 0) // 상
                {
                    this.IsEnable[topIndex] = false;
                    // 이미 Enable한 지역만 
                    if (!this.AlCheck[topIndex]) 
                    { 
                        // Disable 하려는 체크박스가 이미 체크 되어 있는 경우 Enable
                        if (this.IsCheck[topIndex])
                        {
                            this.IsEnable[topIndex] = true;
                        }

                        // 인접 Checkbox의 Checked 여부를 확인하기(상, 좌, 우 Checked 확인하기)
                        // 확인해서 Checked 가능한 곳이라 판단되면 Enable
                        if (topIndex - 4 >= 0) // 상-상
                        {
                            if (this.IsCheck[topIndex - 4])
                            {
                                this.IsEnable[topIndex] = true; 
                            }
                        }

                        if (topIndex - 1 >= 0) // 상-좌
                        {
                            if (CheckLeftEmpty(topIndex)) { }

                            else
                            {
                                if (this.IsCheck[topIndex - 1])
                                {
                                    this.IsEnable[topIndex] = true;
                                }
                            }
                        }

                        if (topIndex + 1 < CountOfCheckbox) // 상-우
                        {
                            if (CheckRightEmpty(topIndex)) { }

                            else
                            {
                                if (this.IsCheck[topIndex + 1])
                                {
                                    this.IsEnable[topIndex] = true;
                                }
                            }
                        }
                    } // End if (this.AlEnable[topIndex]) 

                    // 계속 Enable하지만, 체크된 상태로 동떨어져서 Disable 되지 않는 체크박스 Check 해제시키기
                    if (!this.AlCheck[topIndex] && this.IsCheck[topIndex] && !this.IsEnable[topIndex])
                    {
                        this.IsCheck[topIndex] = false;
                        this.PrevCheck[topIndex] = false;
                    }
                } // End if (topIndex >= 0)

                if (leftIndex >= 0) // 좌
                {
                    // 최 왼쪽 체크박스의 경우는 예외처리 (좌측이 없기 때문에)
                    if (CheckLeftEmpty(index)) { } // [4], [8], [12]

                    // 최 왼쪽 체크박스가 아닌 경우에만 판단
                    else
                    {
                        this.IsEnable[leftIndex] = false;
                        if (!this.AlCheck[leftIndex])
                        {
                            // Disable 하려는 체크박스가 이미 체크 되어 있는 경우 Enable
                            if (this.IsCheck[leftIndex])
                            {
                                this.IsEnable[leftIndex] = true;
                            }

                            // 인접 Checkbox의 Checked 여부를 확인하기(상, 좌, 하 Checked 확인하기)
                            // 확인해서 Checked 가능한 곳이라 판단되면 Enable
                            if (leftIndex - 4 >= 0) // 좌-상
                            {
                                if (this.IsCheck[leftIndex - 4])
                                {
                                    this.IsEnable[leftIndex] = true;
                                }
                            }

                            if (leftIndex - 1 >= 0) // 좌-좌
                            {
                                if (CheckLeftEmpty(leftIndex)) { } // [4], [8], [12]

                                else
                                {
                                    if (this.IsCheck[leftIndex - 1])
                                    {
                                        this.IsEnable[leftIndex] = true;
                                    }
                                }
                            }

                            if (leftIndex + 4 < CountOfCheckbox) // 좌-하
                            {
                                if (this.IsCheck[leftIndex + 4])
                                {
                                    this.IsEnable[leftIndex] = true;
                                }
                            }
                        } // End if (this.AlEnable[leftIndex])
                    } // End else - if (CheckLeftEmpty(index))

                    // 계속 Enable하지만, 체크된 상태로 동떨어져서 Disable 되지 않는 체크박스 Check 해제시키기
                    if (!this.AlCheck[leftIndex] && this.IsCheck[leftIndex] && !this.IsEnable[leftIndex])
                    {
                        this.IsCheck[leftIndex] = false;
                        this.PrevCheck[leftIndex] = false;
                    }
                } // End if (leftIndex >= 0)

                if (rightIndex < CountOfCheckbox) // 우
                {
                    // 최 오른쪽 체크박스의 경우는 예외처리 (우측이 없기 때문에)
                    if (CheckRightEmpty(index)) { } // [3], [7], [11]

                    else
                    {
                        this.IsEnable[rightIndex] = false;
                        if (!this.AlCheck[rightIndex])
                        {
                            // Disable 하려는 체크박스가 이미 체크 되어 있는 경우 Enable
                            if (this.IsCheck[rightIndex])
                            {
                                this.IsEnable[rightIndex] = true;
                            }

                            // 인접 Checkbox의 Checked 여부를 확인하기(상, 우, 하 Checked 확인하기)
                            // 확인해서 Checked 가능한 곳이라 판단되면 Enable
                            if (rightIndex - 4 >= 0) // 우-상
                            {
                                if (this.IsCheck[rightIndex - 4])
                                {
                                    this.IsEnable[rightIndex] = true;
                                }
                            }

                            if (rightIndex + 1 < CountOfCheckbox) // 우-우
                            {
                                if (CheckRightEmpty(rightIndex)) { } // [3], [7], [11]

                                else
                                {
                                    if (this.IsCheck[rightIndex + 1])
                                    {
                                        this.IsEnable[rightIndex] = true;
                                    }
                                }
                            }

                            if (rightIndex + 4 < CountOfCheckbox) // 우-하
                            {
                                if (this.IsCheck[rightIndex + 4])
                                {
                                    this.IsEnable[rightIndex] = true;
                                }
                            }
                        } // End if (this.AlEnable[rightIndex])
                    } // End else - if (CheckRightEmpty(index))

                    // 계속 Enable하지만, 체크된 상태로 동떨어져서 Disable 되지 않는 체크박스 Check 해제시키기
                    if (!this.AlCheck[rightIndex] && this.IsCheck[rightIndex] && !this.IsEnable[rightIndex])
                    {
                        this.IsCheck[rightIndex] = false;
                        this.PrevCheck[rightIndex] = false;
                    }

                } // End if (rightIndex < CountOfCheckbox) 

                if (bottomIndex < CountOfCheckbox) // 하
                {
                    this.IsEnable[bottomIndex] = false;
                    if (!this.AlCheck[bottomIndex])
                    {
                        // Disable 하려는 체크박스가 이미 체크 되어 있는 경우 Enable
                        if (this.IsCheck[bottomIndex])
                        {
                            this.IsEnable[bottomIndex] = true;
                        }

                        // 인접 Checkbox의 Checked 여부를 확인하기(좌, 우, 하 Checked 확인하기)
                        // 확인해서 Checked 가능한 곳이라 판단되면 Enable
                        if (bottomIndex - 1 >= 0) // 하-좌
                        {
                            if (CheckLeftEmpty(bottomIndex)) { }

                            else
                            {
                                if (this.IsCheck[bottomIndex - 1])
                                {
                                    this.IsEnable[bottomIndex] = true;
                                }
                            }
                        }

                        if (bottomIndex + 1 < CountOfCheckbox) // 하-우
                        {
                            if (CheckRightEmpty(bottomIndex)) { }

                            else
                            {
                                if (this.IsCheck[bottomIndex + 1])
                                {
                                    this.IsEnable[bottomIndex] = true;
                                }
                            }
                        }

                        if (bottomIndex + 4 < CountOfCheckbox) // 하-하
                        {
                            if (this.IsCheck[bottomIndex + 4])
                            {
                                this.IsEnable[bottomIndex] = true;
                            }
                        }
                    } // End (this.AlEnable[bottomIndex])

                    // 계속 Enable하지만, 체크된 상태로 동떨어져서 Disable 되지 않는 체크박스 Check 해제시키기
                    if (!this.AlCheck[bottomIndex] && this.IsCheck[bottomIndex] && !this.IsEnable[bottomIndex])
                    {
                        this.IsCheck[bottomIndex] = false;
                        this.PrevCheck[bottomIndex] = false;
                    }
                } // End if (bottomIndex >= 0)
                this.RbStateMsg = index.ToString() + "번 노드 체크 해제";
            } // end else (check)

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

        // 버튼 클릭 시 AICheck 및 AlEnable 변수에 데이터 할당하는 커맨드 함수
        private void ExecuteApplyButton(object parameter)
        {
            if (this.CheckRectangle() && (parameter != null))
            {
                // Already Check 및 Enable에 백업
                for (int i = 0; i < this.IsCheck.Length; i++)
                {
                    this.AlCheck[i] = this.IsCheck[i];
                    this.AlEnable[i] = this.IsEnable[i];

                    // Enable이 True인 곳에 현재 Check된 곳이 True가 되면 false 로 바꿔쳐주기
                    if (this.AlEnable[i])
                    {
                        this.AlEnable[i] = !this.IsCheck[i];
                    }

                    if (this.IsCheck[i])
                    {
                        this.IsEnable[i] = false;
                    }
                }
                ArrayNotifyChanger();
                this.RbStateMsg = "적용 완료";
            } // end if (parameter != null)
        }

        // 체크박스 더미의 오른쪽 최 외곽이 비어있음을 체크함 (우측 Check를 확인함)
        private bool CheckRightEmpty(int index)
        {
            if ((index + 1) % 4 == 0)  // [3], [7], [11]
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        // 체크박스 더미의 왼쪽 최 외곽이 비어있음을 체크함 (좌측 Check를 확인함)
        private bool CheckLeftEmpty(int index)
        {
            if (index % 4 == 0) // [4], [8], [12]
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        // 현재 체크 된 영역이 사각형인지 판별함
        public bool CheckRectangle()
        {
            // 체크박스로 이루어진 한 라인의 길이
            const int LineLength = 4;

            // width , height 길이 각각 4씩, 모든 요소 0으로 초기화
            int[] rowCheck = new int[CountOfCheckbox / LineLength]; // <->
            int[] colCheck = new int[CountOfCheckbox / LineLength];  // 

            rowCheck = Enumerable.Repeat(0, CountOfCheckbox / LineLength).ToArray();
            colCheck = Enumerable.Repeat(0, CountOfCheckbox / LineLength).ToArray();

            for (int i = 0; i < CountOfCheckbox; i++)
            {
                int indexOfRow = i / (CountOfCheckbox / LineLength); // 0/4, 1/4, ... 16/4 - 분모고정

                // row check
                if (this.IsCheck[i])
                {
                    // 한 줄에 체크된 갯수를 카운팅
                    rowCheck[indexOfRow]++;
                }
            }

            for (int indexOfCol = 0; indexOfCol < CountOfCheckbox / LineLength; indexOfCol++)
            {
                for (int i = 0; i < CountOfCheckbox / LineLength; i++)
                {
                    // col check
                    if (this.IsCheck[4 * i + indexOfCol])
                    {
                        // 한 열에 체크 된 갯수를 카운팅
                        colCheck[indexOfCol]++;
                    }
                }
            }

            for (int i = 0; i < CountOfCheckbox / LineLength; i++)
            {
                for ( int j = i + 1 ; j < CountOfCheckbox / LineLength ; j++)
                {
                    // 0이 아닌 행에 대해서
                    if (rowCheck[i] != 0 && rowCheck[j] != 0)
                    {
                        if (rowCheck[j] != rowCheck[i])
                        {
                            RbStateMsg = "사각형이 아님, 행 체크 확인바람";
                            return false;
                        }
                    }

                    // 0이 아닌 열에 대해서
                    if (colCheck[i] != 0 && colCheck[j] != 0)
                    {
                        if (colCheck[j] != colCheck[j])
                        {
                            RbStateMsg = "사각형이 아님, 열 체크 확인바람";
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        // 최대 체크 갯수 확인하기
        public void CheckMax()
        {

        }

        // 자원할당 설정 완료 시 이전의 TermID 에서 설정한 Check 모두 설정한 채로 Disable
        public void RBAllocSet(uint termID)
        {
            // 16개 false로 초기화
            // bool[] isCheckedArray =Enumerable.Repeat(false, 16).ToArray();
            bool[] isCheckedArray = new bool[16];
            for (int i = 0; i < isCheckedArray.Length; i++ )
            {
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
