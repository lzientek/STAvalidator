using System;
using System.Globalization;
using System.Windows.Data;

namespace MP22NET.WpfFrontEnd.Converters
{
    public class SelectedIndexToBoolConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = (int) value;
            if (v >= 0)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
