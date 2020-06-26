using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyCustomExpander
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<VM> VmCollec { get; set; } = new ObservableCollection<VM>();

        public MainWindow()
        {
            InitializeComponent();

            // DataGrid1.ItemsSource = VmCollec;

            VmCollec.Add(new VM() { IsCheck = false, Content = "1" });
            VmCollec.Add(new VM() { IsCheck = false, Content = "2" });
            VmCollec.Add(new VM() { IsCheck = true, Content = "3" });
            VmCollec.Add(new VM() { IsCheck = true, Content = "4" });

            CIBListBox.ItemsSource = VmCollec;
        }
    }

    public class VM
    {
        public bool IsCheck { get; set; }
        public string Content { get; set; }
    }

    public class MultiplyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 1.0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is double)
                    result *= (double)values[i];
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }
}
