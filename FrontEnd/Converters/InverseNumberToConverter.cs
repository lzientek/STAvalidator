using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MP22NET.WpfFrontEnd.Converters
{
    /// <summary>
    /// si la valeur binder est inferieur au parametre alors il devient visible
    /// </summary>
    public class InverseNumberToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int v = System.Convert.ToInt32(value);

            if (v <= int.Parse(parameter.ToString()))
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
