using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboboxInDataGridTest
{
    public class PeopleInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }

    public class AddressList : List<string>
    {
        public AddressList()
        {
            this.Add("서울");
            this.Add("광주");
            this.Add("대전");
            this.Add("부산");
        }
    }


}
