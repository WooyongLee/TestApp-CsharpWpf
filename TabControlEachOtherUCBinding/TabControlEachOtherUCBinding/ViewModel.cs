using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TabControlEachOtherUCBinding
{
    public class TabViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ViewModelA : TabViewModel, INotifyPropertyChanged
    {
        private string header;
        private string value;
        private string datetime;

        // Message #1, Message #2 하는 메세지 인덱스
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                NotifyPropertyChanged("Header");
            }
        }

        // Message Name : Value 쌍을 정의하는 Value
        public string Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                NotifyPropertyChanged("Value");
            }
        }

        public string DateTime
        {
            get { return datetime; }
            set
            {
                this.datetime = value;
                NotifyPropertyChanged("DateTime");
            }
        }

        public ObservableCollection<int> ArrayValue { get; set; }

        public ViewModelA()
        {
            ArrayValue = new ObservableCollection<int>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
    public class ViewModelB : TabViewModel, INotifyPropertyChanged
    {
        private string header;
        private string value;
        private string datetime;
        private string[] arrayValue = new string[5];

        // Message #1, Message #2 하는 메세지 인덱스
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                NotifyPropertyChanged("Header");
            }
        }

        // Message Name : Value 쌍을 정의하는 Value
        public string Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                NotifyPropertyChanged("Value");
            }
        }

        public string DateTime
        {
            get { return datetime; }
            set
            {
                this.datetime = value;
                NotifyPropertyChanged("DateTime");
            }
        }

        public string[] ArrayValue
        {
            get { return arrayValue; }
            set
            {
                arrayValue = value;
                NotifyPropertyChanged("ArrayValue");
            }
        }
        public ViewModelB()
        {
            ArrayValue = new string[5];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class ViewModelC : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
