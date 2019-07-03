using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MuiltiBindingTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    // Command
    public class RelayCommand : ICommand
    {
        Action<object> _ExecuteMethod;
        Func<object, bool> _CanExecuteMethod;

        public RelayCommand(Action<object> executemMethod, Func<object, bool> canExecuteMethod)
        {
            _ExecuteMethod = executemMethod;
            _CanExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_ExecuteMethod != null)
            {
                return _CanExecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _ExecuteMethod(parameter);
        }
    }

    // ViewModel
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand MyCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private int _number1;
        public int Number1
        {
            get { return _number1; }
            set { _number1 = value; OnPropertyChanged("Number1"); }
        }


        private int _number2;
        public int Number2
        {
            get { return _number2; }
            set { _number2 = value; OnPropertyChanged("Number2"); }
        }


        private int nubersum;

        public int NumberSum
        {
            get { return nubersum; }
            set { nubersum = value; OnPropertyChanged("NumberSum"); }
        }


        public ViewModel()
        {
            MyCommand = new RelayCommand(execute, canexecute);
        }

        private bool canexecute(object parameter)
        {
            if (Number1 != null || Number2 != null)
            {
                return true;
            }
            else { return false; }
        }

        private void execute(object parameter)
        {
            var values = (object[])parameter;  
            int num1 = Convert.ToInt32((string)values[0]);  
            int num2 = Convert.ToInt32((string)values[1]);  
            NumberSum = num1+num2;
        }
    }

    // Converter
    public class MyValueConverter:IMultiValueConverter  
    {  
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)  
        {  
            return values.Clone();  
        }  
  
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)  
        {  
            return null;  
        }  
    } 


}
