using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArrayBindingTest
{
    public class ArrayClassTest : INotifyPropertyChanged
    {
        private int[] myArray;
        private string[] myStringArray;

        public int[] MyArray
        {
            get { return myArray; }
            set
            {
                myArray = value;
                NotifyPropertyChanged("MyArray");

            }
        }

        public string[] MyStringArray
        {
            get { return myStringArray; }
            set
            {
                myStringArray = value;
                NotifyPropertyChanged("MyStringArray");

            }
        }

        public ArrayClassTest()
        {
            myArray = new int[5];
            MyStringArray = new string[5];
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
