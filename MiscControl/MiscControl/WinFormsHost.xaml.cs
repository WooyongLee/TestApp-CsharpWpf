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
using System.Windows.Shapes;

namespace MiscControl
{
    /// <summary>
    /// WinFormsHost.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WinFormsHost : Window
    {
        public WinFormsHost()
        {
            InitializeComponent();
            // wfhSample이라는 WindorsFormHost를 WebBroser Form으로 이용, 
            // 지정된 URL을 로드함
            try
            {
                (wfhSample.Child as System.Windows.Forms.WebBrowser).Navigate("http://www.pixoneer.co.kr/");
            }
            catch (Exception e)
            {
                
            }
        }

        private void wbWinForms_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Title = (sender as System.Windows.Forms.WebBrowser).DocumentTitle;
        }

    }
}
