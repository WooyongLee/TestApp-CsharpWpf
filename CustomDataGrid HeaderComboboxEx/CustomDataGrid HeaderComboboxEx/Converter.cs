
using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomDataGrid_HeaderComboboxEx
{
    public class MaxHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double pctHeight = (double)parameter;

            if ( (pctHeight <= 0.0) || (pctHeight > 100.0))
            {
                throw new Exception("MaxHeightConverter의 파라미터 범위 (0,100]을 벗어남");
            }
            return ((double)value * pctHeight);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}