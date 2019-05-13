using System;
using System.Collections.Generic;
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

namespace IDataErrorInfoTest
{
    // 참고 링크 : https://www.codeproject.com/Tips/858492/WPF-Validation-Using-IDataErrorInfo

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public Position PosVM = new Position();
        public MainWindow()
        {
            InitializeComponent();

            this.PositionEditPanel.DataContext = PosVM;
            this.PositionEditButtonPanel.DataContext = PosVM;
        }
    }
}
