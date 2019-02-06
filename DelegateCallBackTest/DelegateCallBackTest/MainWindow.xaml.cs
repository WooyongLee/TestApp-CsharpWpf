using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace DelegateCallBackTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DelegateTarget D_Target;
        delegate int MyDelegate(int a, int b);
        delegate int Compare(int a, int b);
        
        // 제네릭 버전
        delegate int Compare<T>(T a, T b);

        // 익명 대리자 연습
        delegate int Calculate(int a, int b);

        public int Plus(int a, int b)
        {
            return a + b;
        }

        static int AscendCompare(int a, int b)
        {
            if (a > b) return 1;
            else if (a == b) return 0;
            else return -1;
        }
        
        static void BubbleSort(int[] DataSet, Compare cmpr)
        {
            // 연산수행
        }
       
        // System.Int32, Double을 비롯한 모든 수치형식과 String은 IComparable을 상속하여 CompareTo() 메소드를 구현
        static int AscendCompare<T>(T a, T b) where T:IComparable<T>
        {
            // 위의 AscendCompare(int a, int b) 함수와 똑같은 기능을 수행함
            return a.CompareTo(b);
        }
        
        static void BubbleSort<T>( T[] DataSet, Compare<T> Comparer)
        {
            // 연산수행
        }

        

        public MainWindow()
        {
            InitializeComponent();

            DelegateTest();
        }

        public void DelegateTest()
        {
            // Delegate Ex 1
            MyDelegate Callback;

            Callback = new MyDelegate(Plus);
            int resultCallBack = Callback(3, 4);

            // Delegate Ex 2
            int[] array = { 3, 6, 7, 8, 9 };
            Compare comparer;

            // 함수를 그대로 대리 호출
            comparer = new Compare(AscendCompare);
            BubbleSort(array, comparer);

            // delegate 무명 인스턴스 생성 및 호출
            // BubbleSort(array, new Compare(AscendCompare);

            // Delegate Ex 3
            double[] arrayDouble = { 3.3, 2.2, 5.5, 6.6 };
            BubbleSort<double>(arrayDouble, new Compare<double>(AscendCompare));

            // Delegate Ex 4
            Calculate calc = delegate (int a, int b)
            {
                return a + b;
            };
            int noNameDelegateResult = calc(3, 4);

        }

        private void openDelegateTargetButton_Click(object sender, RoutedEventArgs e)
        {
            if (D_Target == null)
            {
                D_Target = new DelegateTarget();
            }
            D_Target.Show();
        }
    }
}
