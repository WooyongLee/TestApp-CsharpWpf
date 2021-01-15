using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TextEditValueMinMaxCheck
{
    /// <summary>
    /// CustomTextEditControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomTextEditControl : UserControl //, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        ///// <summary>
        ///// OnPropertyChanged function.
        ///// </summary>
        ///// <param name="name">The field Name.</param>
        //protected void OnPropertyChanged(string name)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        //#region OnPropertyChanged 영역
        // TextEdit의 값과 바인딩 되어있는 Text 값, Get/Set 할 때 사용할 수 있음 
        //// TextEdit과 바인딩 되어있는 Text 값, Get/Set 할 때 사용할 수 있음 
        //private string editText;
        //public string EditText
        //{
        //    get { return this.editText; }
        //    set { this.editText = value; this.OnPropertyChanged("EditText"); }
        //}
        //#endregion

        #region 의존 프로퍼티 영역
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register
        (
             "MinValue",
             typeof(double),
             typeof(CustomTextEditControl),
             new PropertyMetadata(double.MinValue)
        );

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register
        (
             "MaxValue",
             typeof(double),
             typeof(CustomTextEditControl),
             new PropertyMetadata(double.MaxValue)
        );

        public static readonly DependencyProperty DefaultTextProperty = DependencyProperty.Register
        (
             "DefaultText",
             typeof(string),
             typeof(CustomTextEditControl),
             new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register
        (
             "TextValue",
             typeof(string),
             typeof(CustomTextEditControl),
             new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty DoubleValueProperty = DependencyProperty.Register
        (
             "DoubleTextValue",
             typeof(double),
             typeof(CustomTextEditControl),
             new PropertyMetadata(0.0)
        );

        public static readonly DependencyProperty IntegerFlagProperty = DependencyProperty.Register
        (
             "IntegerFlag",
             typeof(bool),
             typeof(CustomTextEditControl),
             new PropertyMetadata(true)
        );

        public static readonly DependencyProperty PositiveNumFlagProperty = DependencyProperty.Register
        (
             "PositiveNumFlag",
             typeof(bool),
             typeof(CustomTextEditControl),
             new PropertyMetadata(true)
        );

        // 최소 값
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // 최대 값
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // 입력 안했을 때 디폴트 값
        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        // TextEdit의 값
        public string TextValue
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set { SetValue(ValueProperty, value); }
        }

        // True = 소수점 입력 가능
        // False = (소수점 없는)정수 값만 입력 가능
        public bool IntegerFlag
        {
            get { return (bool)GetValue(IntegerFlagProperty); }
            set { SetValue(IntegerFlagProperty, value); }
        }

        // True = 양, 음수 값만 입력 가능
        // False = 양수만 입력 가능(마이너스 입력 불가)
        public bool PositiveNumFlag
        {
            get { return (bool)GetValue(PositiveNumFlagProperty); }
            set { SetValue(PositiveNumFlagProperty, value); }
        }

        // TextEdit의 Double 값
        public double DoubleTextValue
        {
            get
            {
                return (double)GetValue(DoubleValueProperty);
            }
            set { SetValue(DoubleValueProperty, value); }
        }
        #endregion

        // 이전 입력 값
        private string PrevStrValue = string.Empty;

        // 최대 TextEdit의 허용 길이
        private const int MaximumTextLength = 20;

        public CustomTextEditControl()
        {
            PositiveNumFlag = false;
            IntegerFlag = false;

            InitializeComponent();

          //  this.DataContext = this;
        }

        private void InputTextEdit_Loaded(object sender, RoutedEventArgs e)
        {
            this.InputTextEdit.MaxLength = MaximumTextLength;
            this.InputTextEdit.NullText = DefaultText;
            this.InputTextEdit.Text = TextValue.ToString();

            // MaxValue를 MinValue보다 크게 설정한 경우에 대한 예외처리(MinValue를 ManValue보다 작게 설정한 경우 마찬가지)
            if ( this.MaxValue < this.MinValue)
            {
                this.MaxValue = this.MinValue + 1;
            }
        }

        #region TextEdit 이벤트 함수

        // Input을 하면서 Text의 입력문을 Check 하는 이벤트 함수
        private void InputTextEdit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            // 마이너스, 소수점, 숫자만 입력 가능하도록 
            sb.Append("[^0-9");

            if (PositiveNumFlag) // 양수 조건
            {
                sb.Append("-");
            }

            if (IntegerFlag) // 정수 조건(소수점 없는)
            {
                sb.Append(".");
            }
            sb.Append("]+");
            // "[^0-9-.]+"
            Regex regex = new Regex(sb.ToString());
            e.Handled = regex.IsMatch(e.Text);
        }

        private int GetTextEditMaxLength()
        {
            int retMaxLength = 0;
            if (IntegerFlag)
            {
                retMaxLength = MinValue.ToString().Length > MaxValue.ToString().Length ? MinValue.ToString().Length + 1 : MaxValue.ToString().Length + 1;
            }

            else
            {
                retMaxLength = MaxValue.ToString().Length; // MinValue는 0이기 때문
            }

            return MinValue.ToString().Length > MaxValue.ToString().Length ? MinValue.ToString().Length + 1 : MaxValue.ToString().Length + 1;
        }

        // Key를 하나 입력 후에 발생하는 이벤트 함수
        private void InputTextEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputTextEdit.Text != string.Empty)
            {
                // 스페이스 입력받았을 경우 그냥 싹 지우기
                if (e.Key == Key.Space)
                {
                    InputTextEdit.Text = "";
                    return;
                }

                // MaxValue와 MinValue를 이용한 최대 입력길이 제한값을 설정
                //  this.InputTextEdit.MaxLength = MinValue.Length > MaxValue.Length ? MinValue.Length + 1 : MaxValue.Length + 1;
                this.InputTextEdit.MaxLength = this.GetTextEditMaxLength();

                string strInputText = InputTextEdit.Text;

                // 소수점 포함에 대한 예외처리
                if (IntegerFlag)
                {
                    if (!this.IsCheckDot(strInputText))
                    {
                        return;
                    }
                }

                // 음수에 대한 예외처리
                if (PositiveNumFlag)
                {
                    if (!this.IsCheckMinus(strInputText))
                    {
                        return;
                    }
                }

                // ("00") 앞쪽에 0 두개 입력 시 이전값으로 원복
                if (strInputText.IndexOf("00") == 0)
                {
                    this.InputTextEdit.Text = PrevStrValue;
                }

                // 마이너스 하나만 들어간 경우
                if (strInputText == "-")
                {
                    TextValue = "0";
                    DoubleTextValue = 0;
                    return;
                }

                // 지정한 최대, 최소 상한값에 대한 TextEdit의 Text Value 재설정
                this.SetFilteredText(InputTextEdit.Text);

                PrevStrValue = InputTextEdit.Text;
            } // end if (InputTextEdit.Text != string.Empty)
        }

        #endregion

        // 마이너스 포함된 문자열을 처리하기 위한 함수
        private bool IsCheckMinus(string strInputText)
        {
            bool ret = true;
            if (strInputText.Contains("-"))
            {
                //// (-) 최초 입력 시 숫자가 없고 마이너스만 들어가는 경우에 대한 예외처리
                //if (strInputText.Length <= 1)
                //{
                //    this.InputTextEdit.Text = string.Empty;
                //    ret = false;
                //}

                // ("--") 전체 텍스트에서 마이너스 두개 입력 시 값 되돌리기
                int countOfMinus = strInputText.Count(f => f == '-');
                if (countOfMinus >= 2)
                {
                    this.InputTextEdit.Text = PrevStrValue;
                    ret = false;
                }

                // ("23-")숫자 중간에 또는 끝에 마이너스 들어가는 경우에 대한 예외처리
                int minusInputIndex = strInputText.IndexOf("-");
                if (minusInputIndex >= 1)
                {
                    // 이전 입력한 값으로 되돌리기
                    this.InputTextEdit.Text = PrevStrValue;
                    ret = false;
                }

                // ("-.") 마이너스 다음에 소수점 들어가는 경우에 대해서
                if (strInputText.IndexOf(".") == strInputText.IndexOf("-") + 1)
                {
                    // 이전 입력한 값으로 되돌리기
                    this.InputTextEdit.Text = string.Empty;
                    ret = false;
                }
            }
            return ret;
        }

        // 소수점 포함된 문자열을 처리하기 위한 함수
        private bool IsCheckDot(string strInputText)
        {
            bool ret = true;

            // ("..") 전체 텍스트에서 소수점 두개 입력 시 값 되돌리기
            int countOfDot = strInputText.Count(f => f == '.');
            if (countOfDot >= 2)
            {
                this.InputTextEdit.Text = PrevStrValue;
                ret = false;
            }

            // (".")소수점 입력 시 MaxLength를 반 무제한으로 설정
            if (strInputText.Contains("."))
            {
                // (".3912")소수점 처음에 입력할 시 앞에 0 붙여주기
                if (strInputText.IndexOf(".") == 0)
                {
                    this.InputTextEdit.Text = "0.";
                }

                // 최대 길이 제한값 조정
                this.InputTextEdit.MaxLength = MaximumTextLength;
            }

            return ret;
        }

        // 최대, 최소값에 필터링 된 텍스트를 설정함
        private void SetFilteredText(string strInputText)
        {
            // Space가 포함되지 않는 경우에만
            if (!strInputText.Contains(" "))
            {
                double numInput = double.Parse(strInputText);

                double decimalMaxValue = MaxValue;
                double decimalMinValue = MinValue;

                if (numInput > decimalMaxValue)
                {
                    InputTextEdit.Text = MaxValue.ToString();
                    InputTextEdit.SelectAll();
                }
                else if (numInput < decimalMinValue)
                {
                    InputTextEdit.Text = MinValue.ToString();
                    InputTextEdit.SelectAll();
                }

                // TextValue = InputTextEdit.Text;
                TextValue = numInput.ToString();
                DoubleTextValue = double.Parse(InputTextEdit.Text);
            }
        }
    }
}
