using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCallBackTest
{
    public class WrappedTest
    {

        public string FirstTestString { get; set; }
        public string SecondTestString { get; set; }
        public string ThirdTestString { get; set; }

        public WrappedTest()
        {

        }

        public void FirstFunction(string msg)
        {
            int a = 0;
        }

        public void SecondFunction(string msg)
        {
            int a = 0;
        }
        public void ThirdFunction(string msg)
        {
            int a = 0;
        }
    }
}
