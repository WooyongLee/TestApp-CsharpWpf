using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace CustomDataGrid_HeaderComboboxEx
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MainViewModel> mvObColl;
        ObservableCollection<MainViewModel> mvObColl2;

        public MainWindow()
        {
            InitializeComponent();

            this.ComboboxDataGrid.DataContext = new MainViewModel();
            mvObColl = new ObservableCollection<MainViewModel>();
            mvObColl2 = new ObservableCollection<MainViewModel>();
        }
    }

    #region View Model
    public class GridSample
    {
        public string Name { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private string comboValue;
        public string ComboValue
        {
            get { return comboValue; }
            set
            {
                if (comboValue != value)
                {
                    comboValue = value;
                    NotifyPropertyChanged("ComboValue");
                }
            }
        }

        //private string dog;
        //public string Dog
        //{
        //    get { return dog; }
        //    set
        //    {
        //        if (dog != value)
        //        {
        //            dog = value;
        //            NotifyPropertyChanged("Dog");
        //        }
        //    }
        //}


        //private string moredog;
        //public string MoreDog
        //{
        //    get { return moredog; }
        //    set
        //    {
        //        if (moredog != value)
        //        {
        //            moredog = value;
        //            NotifyPropertyChanged("MoreDog");
        //        }
        //    }
        //}


        public MainViewModel()
        {
            this.GridItems = new ObservableCollection<GridSample>();
            AddComboboxItem();
            MakeSampleData();
        }

        public void AddComboboxItem()
        {
            ComboItems = new ObservableCollection<string>();
            ComboItems.Add("Img_X");
            ComboItems.Add("Img_Y");
            ComboItems.Add("Lat");
            ComboItems.Add("Lon");
            ComboItems.Add("Height");

            ComboValue = "Img_X";
        }


        public void MakeSampleData()
        {
            GridItems.Add(new GridSample() { Name = "11" });
            GridItems.Add(new GridSample() { Name = "22" });
            // new GridSample() { Name = "11"} ,new GridSample() { Name = "22"} };

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string str)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }
        }

        public ObservableCollection<GridSample> GridItems { get; set; }

        public ObservableCollection<string> ComboItems { get; set; }
    }
    #endregion
}
