using DevExpress.Xpf.Editors;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TextEditValueMinMaxCheck
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ResultTextBlock.Text = CustomTextEdit.TextValue;
            // ResultTextBlock.Text = CustomTextEdit.EditTextValue.ToString();
            ResultTextBlock.Text = CustomTextEdit.DoubleTextValue.ToString();
        }
    }
}
