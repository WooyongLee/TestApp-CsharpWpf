using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TabControlEachOtherUCBinding
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TabViewModel> TabViewModels { get; set; }
        public TabViewModel SelectedTabViewModel { get; set; }


        public int index {get; set;}
        public ViewModelA VMA = new ViewModelA();
        public ViewModelB VMB = new ViewModelB();

        public MainWindow()
        {
            InitializeComponent();

            VMA.Value = "11111";
            VMA.DateTime = "11111111@@@@";

            VMB.Value = "222222222";
            VMB.DateTime = "2222222222#######";

            //TabViewModels = new ObservableCollection<TabViewModel>();
            //TabViewModels.Add(new ViewModelA { Header = "Tab A" });
            //TabViewModels.Add(new ViewModelB { Header = "Tab B" });

            //SelectedTabViewModel = TabViewModels[0];

            Tab1Uc.DataContext = VMA; 
            Tab2Uc.DataContext = VMB; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for ( int i = 0 ; i < 5 ; i++)
            {
                VMA.ArrayValue[i] = index * i;
                VMB.ArrayValue[i] = (index * i).ToString();
            }
            index++;
        }
    }
}
