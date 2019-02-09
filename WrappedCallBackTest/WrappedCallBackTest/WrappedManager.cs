using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrappedCallBackTest
{
    public class WrappedManager
    {
        // 아래 Data들은 각 클래스들이라고 생각함
        private string sData1; // string
        private string sData2; // string
        private string sData3; // string
        private int iData4; // int
        private int index = 0;

        public string Data1
        {
            get { return sData1; }
            set { sData1 = value; }
        }

        public string Data2
        {
            get { return sData2; }
            set { sData2 = value; }
        }

        public string Data3
        {
            get { return sData3; }
            set { sData3 = value; }
        }

        public int Data4
        {
            get { return iData4; }
            set { iData4 = value; }
        }
        
        public void ReceiveData1()
        {
            Data1 = index.ToString() + "111";
            Data4 = index++;
        }

        public void ReceiveData2()
        {
            Data2 = index.ToString() + "222";
            Data4 = index++;
        }

        public void ReceiveData3()
        {
            Data3 = index.ToString() + "333";
            Data4 = index++;
        }

    }
}
