using MVVMSample.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVMSample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // ViewModel 객체 정의 및 생성
        public MainViewModel MyViewModel = new MainViewModel();

        //System.Collections.ObjectModel 
        public ObservableCollection<MainViewModel> MyObservableListViewModel
            = new ObservableCollection<MainViewModel>();

        public MainWindow()
        {
            InitializeComponent();
            
            MyViewModel.Name = "피자빵";
            MyViewModel.Price = 1800;

            // 일반적인 DataContext 바인딩
            this.MyBindingGrid.DataContext = MyViewModel;
            
            // 리스트 형태의 Control에 대한 ItemsSource 바인딩
            this.MyBindingDataGrid.ItemsSource = MyObservableListViewModel;

        }

        // 버튼 클릭 이벤트
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.TotalPrice = GetTotalPrice(MyViewModel.Count, MyViewModel.Price);
        }

        /// <summary>
        /// 총 금액을 구함
        /// </summary>
        /// <param name="count">가격</param>
        /// <param name="price">총 갯수</param>
        /// <returns></returns>
        public int GetTotalPrice(int count, int price)
        {
            return count * price;
        }

        #region DataGrid를 컨트롤 하기 위한 함수들
        private void 추가_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel AddViewModel = new MainViewModel();
            AddViewModel.Name = NameTextBox.Text;
            AddViewModel.Price = int.Parse(PriceTextBox.Text);
            AddViewModel.DTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            MyObservableListViewModel.Add(AddViewModel);
        }

        // 선택한 행의 데이터 삭제
        private void 삭제_Click(object sender, RoutedEventArgs e)
        {
            if (MyBindingDataGrid.SelectedItem != null)
            {
                // DataGrid의 선택된 행에 대한 접근법
                var selectedItem = MyBindingDataGrid.SelectedItem as MainViewModel;

                var selectedIndex = MyBindingDataGrid.SelectedIndex;

                MyObservableListViewModel.RemoveAt(selectedIndex);
            }
        }

        #endregion
        
        private void 상태임시발생_Click(object sender, RoutedEventArgs e)
        {
            MyViewModel.StateHungry = !MyViewModel.StateHungry;
            MyViewModel.StateTired = !MyViewModel.StateHungry;
        }
    }
}
