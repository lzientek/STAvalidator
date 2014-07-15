using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MP22NET.WpfFrontEnd.Converters
{
    public class QuestionTypeToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = (int) value;
            if (v == 0)
                return new SolidColorBrush(Colors.YellowGreen);
            return new SolidColorBrush(Colors.OrangeRed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
