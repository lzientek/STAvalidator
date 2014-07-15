using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MP22NET.WpfFrontEnd.Converters
{
    public class IdToBitmapImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int) value;
            try
            {

                return new BitmapImage(new Uri(string.Format("http://www.campus-booster.net/actorpictures/{0}.jpg",id)));
            }
            catch (Exception)
            {
               return new BitmapImage(new Uri("pack://application:,,,/FrontEnd;component/User-Profile.png"));

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
