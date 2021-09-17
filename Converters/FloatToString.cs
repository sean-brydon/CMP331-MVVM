using System;
using System.Globalization;
using System.Windows.Data;

namespace CMP332.Converters
{
    public class FloatToString: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((float)value).ToString() + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return float.Parse(value as string);
        }
    }
}