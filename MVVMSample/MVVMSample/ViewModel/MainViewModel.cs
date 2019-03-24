using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSample.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string name;
        private int count;
        private int totalprice;
        private int code;
        private bool statehungry;
        private bool statetired;

        // 빵 가격
        public int Price { get; set; }

        // 시각
        public string DTime { get; set; }

        // 빵의 이름
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }

        // 빵의 갯수
        public int Count
        {
            get { return this.count; }
            set
            {
                this.count = value;
                OnPropertyChanged("Count");
            }
        }

        // 전체 빵의 가격
        public int TotalPrice
        {
            get { return this.totalprice; }
            set
            {
                this.totalprice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        // 전체 빵의 코드
        public int Code
        {
            get { return this.code; }
            set
            {
                this.code = value;
                OnPropertyChanged("Code");
            }
        }
        
        

        public bool StateHungry
        {
            get { return this.statehungry; }
            set
            {
                this.statehungry = value;
                OnPropertyChanged("StateHungry");
            }
        }

        public bool StateTired
        {
            get { return this.statetired; }
            set
            {
                this.statetired = value;
                OnPropertyChanged("StateTired");
            }
        }


        public MainViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
