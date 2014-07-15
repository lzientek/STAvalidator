using System;
using System.Globalization;
using System.Windows.Data;

namespace MP22NET.WpfFrontEnd.Converters
{
    public class BoolAndMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;
            foreach (bool b in values)//marche avec une infinité de valeur
                result = result && b;

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
