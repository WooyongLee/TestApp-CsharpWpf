using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PriorityBindingTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataObject();
        }
    }

    public class DataObject
    {
        public int NumericValue
        {
            get { return 42; }
        }

        public string StringValue
        {
            get { return "Forty-two"; }
        }

        public string Slow
        {
            get
            {
                Thread.Sleep(10000);
                return "All done";
            }
        }

        public string Medium
        {
            get
            {
                Thread.Sleep(7000);
                return "Nearly there";
            }
        }

        public string Fast
        {
            get
            {
                Thread.Sleep(4000);
                return "Getting ready";
            }
        }

    }
}
