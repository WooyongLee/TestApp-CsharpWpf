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

namespace DelegateExample
{
    // 이벤트 핸들러
    public delegate void EventHandler(string msg);

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate int MyDelegate(int a, int b);

        public int Plus(int a, int b)
        {
            return a + b;
        }

        public int Minus2(int a, int b)
        {
            return a - b;
        }

        public double Minus(int a, int b)
        {
            return a - b;
        }

        public int Divide(double a, double b)
        {
            if (b == 0) return 0;
            return (int)(a / b);
        }

        delegate int Compare(int a, int b);

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

        // 제네릭 버전
        delegate int Compare<T>(T a, T b);

        // System.Int32, Double을 비롯한 모든 수치형식과 String은
        // IComparable을 상속하여 CompareTo() 메소드를 구현
        static int AscendCompare<T>(T a, T b) where T : IComparable<T>
        {
            // a < b :: -1 / a == b :: 0 / a > b :; 1 
            return a.CompareTo(b);
        }

        static void BubbleSort<T>(T[] DataSet, Compare<T> Comparer)
        {
            // 연산수행
        }

        // 익명 대리자 연습
        delegate int Calculate(int a, int b);

        public MainWindow()
        {
            InitializeComponent();

            DelegateTest();
        }

        public void DelegateTest()
        {
            // Delegate Ex 1
            MyDelegate Callback;

            // 델리게이트 메소드 객체 생성 및 메소드 참조
            Callback = new MyDelegate(Plus);
            int resultCallBack = Callback(3, 4);

            // Callback = new MyDelegate(Minus); // 반환형이 달라서 오류
            // Callback = new MyDelegate(Divide); // 매개변수 형태가 달라서 오류 

            // Delegate Ex 2
            int[] array = { 3, 6, 7, 8, 9 };
            Compare comparer;

            // 콜백메소드 객체 생성, 참조
            comparer = new Compare(AscendCompare);

            // 콜백메소드 comparer를 통해서 BubbleSort 연산하기 
            BubbleSort(array, comparer);

            // delegate 무명 인스턴스 생성 및 호출
            // BubbleSort(array, new Compare(AscendCompare);

            // Delegate Ex 3-1 (일반화)
            double[] arrayDouble = { 3.3, 2.2, 5.5, 6.6 };
            BubbleSort<double>(arrayDouble, new Compare<double>(AscendCompare));

            // Delegate Ex 3-2 (체인)
            // 최초 델리게이트 객체는 new로 생성을 해야함
            MyDelegate myChainDelegate = new MyDelegate(Plus);
            myChainDelegate += new MyDelegate(Minus2); // 체인 생성
            myChainDelegate += new MyDelegate(Plus);
            myChainDelegate -= new MyDelegate(Plus); // 체인 끊기
            int resultChainDelegate = myChainDelegate(3, 4);// 마지막에 등록된 메소드에 참조됨

            // Delegate Ex 3-3 (무명)
            Calculate calc = delegate (int a, int b)
            {
                return a + b;
            };
            // Calculate calc = new Calculate(Plus);
            int noNameDelegateResult = calc(3, 4);

        }
    }
}