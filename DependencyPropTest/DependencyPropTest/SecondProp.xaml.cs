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

namespace DependencyPropTest
{
    /// <summary>
    /// SecondProp.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SecondProp : Window
    {
        public class MyBtn : Button
        {
            public static readonly DependencyProperty HiddenTextProperty 
                = DependencyProperty.Register("HiddenText", typeof(String), typeof(FrameworkElement), new FrameworkPropertyMetadata("default", FrameworkPropertyMetadataOptions.AffectsMeasure));

            public MyBtn() : base()
            {
            }
            public String HiddenText
            {
                get { return (String)GetValue(HiddenTextProperty); }
                set { SetValue(HiddenTextProperty, value); }
            }

            protected override void OnClick()
            {
                base.Content = this.HiddenText;
                base.OnClick();
            }
        }
        public SecondProp()
        {
            InitializeComponent();
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            MyBtn mb = new MyBtn();
            mb.HiddenText = "hidden test";
            mb.Width = 200;
            mb.Height = 100;

            MyGrid.Children.Add((UIElement)mb);

            // MyGrid.Children.Remove((UIElement)mb);
        }
    }
}
