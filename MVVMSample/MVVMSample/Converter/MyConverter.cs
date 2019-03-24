using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MVVMSample.Converter
{
    public class MyDataConverter : IValueConverter
    {
        // Source -> Target
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TargetType -> String으로 들어옴 (TextBox 안에 .Text는 String이므로)
            // Int로 캐스팅 해서 분기
            switch ((int)value)
            {
                case (0): return "피자빵";
                case (1): return "내가더양파";
                case (2): return "단팥빵";
                case (3): return "에그타르트";
                default: return "";
            }

        }

        // Target -> Source
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }





    public class BooleanToBackGroundChangeConverter : IValueConverter
    {
        /// <summary>
        /// Binding Source(.cs) -> Binding Target(.xaml)
        /// </summary>
        /// <param name="value">바인딩 소스에서 생성되는 값</param>
        /// <param name="targetType">바인딩 타겟의 형식</param>
        /// <param name="parameter">사용할 변환기 매개변수</param>
        /// <param name="culture">변환기에서 사용할 문화권</param>
        /// <returns>바인딩 타겟에 들어가는 값</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush;
            if ((bool)value)
            {
                brush = new SolidColorBrush(Colors.Green);
            }

            else
            {
                brush = new SolidColorBrush(Colors.Red);
            }
            return brush;
        }

        /// <summary>
        /// Binding Target(.xaml) -> Binding Source(.cs)
        /// </summary>
        /// <param name="value">바인딩 타겟에서 생성되는 값</param>
        /// <param name="targetType">바인딩 소스의 형식</param>
        /// <param name="parameter">사용할 변환기 매개변수</param>
        /// <param name="culture">변환기에서 사용할 문화권</param>
        /// <returns>바인딩 소스에 들어가는 값</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //public class BooleanToBackGroundChangeConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        SolidColorBrush brush;

    //        if ( values == null || values.Length != 3)
    //        {
    //            return null;
    //        }

    //        for ( int i = 0; i < values.Length; i++)
    //        {
    //            bool flag = (values[i] is bool) ? (bool)values[i] : false;
    //            if (flag)
    //            {
    //                Color color = Color.FromRgb(0, 255, 0);
    //                brush = new SolidColorBrush(color);
    //            }

    //            else
    //            {
    //                Color color = Color.FromRgb(255, 0, 0);
    //                brush = new SolidColorBrush(color);
    //            }
    //        }
    //        return brush;
    //    }

    //    public object[] ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
