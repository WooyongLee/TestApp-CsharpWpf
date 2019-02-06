using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DelegateCallBackTest
{
    public partial class MainWindow : Window
    {
        public WrappedTest TestClass;
        int temp = 0;

        public void TestClassInit()
        {
            if (TestClass == null)
            {
                TestClass = new WrappedTest();
            }
        }

        public void FirstEventHandler(string msg)
        {
            if (D_Target.targetButtonClick == 1)
            {
                Wrapped.FirstFunction("111");
                D_Target.targetString = (++temp).ToString();
            }

            else if (D_Target.targetButtonClick == 2)
            {
                Wrapped.SecondFunction("222");
                D_Target.targetString = (++temp).ToString();
            }

            else
            {
                Wrapped.ThirdFunction("333");
                D_Target.targetString = (++temp).ToString();
            }
        }

        public void SecondEventHandler(string msg)
        {
            TestClass.SecondTestString = msg;
        }

        public void ThirdEventHandler(string msg)
        {
            TestClass.ThirdTestString = msg;
        }




        // 추후 TestClass 생성

    }
}
