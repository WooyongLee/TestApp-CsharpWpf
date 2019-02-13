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

namespace ComboboxInDataGridTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<PeopleInfo> InfoList;
        public MainWindow()
        {
            InitializeComponent();
            InfoList = new ObservableCollection<PeopleInfo>();

            PeopleInfoDataGrid.ItemsSource = InfoList;
        }

        private void GeneratePeopleInfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoList.Add(new PeopleInfo { Name = "이", Age = 25, Address = "대전" });
            InfoList.Add(new PeopleInfo { Name = "김", Age = 26, Address = "광주" });
            InfoList.Add(new PeopleInfo { Name = "우", Age = 27, Address = "부산" });
        }
    }
}
