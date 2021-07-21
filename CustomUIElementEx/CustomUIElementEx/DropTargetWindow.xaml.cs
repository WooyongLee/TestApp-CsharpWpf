using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace CustomUIElementEx
{
    /// <summary>
    /// DropTargetWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DropTargetWindow : Window
    {
        public DropTargetWindow()
        {
            InitializeComponent();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string xaml = (string)e.Data.GetData(DataFormats.Xaml);
            this.Content = XamlReader.Load(new XmlTextReader(new StringReader(xaml)));
        }
    }
}
