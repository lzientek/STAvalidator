using System;
using System.Globalization;
using System.Windows.Data;

namespace MP22NET.WpfFrontEnd.Converters
{
    public class IsValidToTextConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool) value;
            if(parameter==null)
            {
                if (v)
                    return "Validé avec succés";
                return "Non validé";
            }
            if (v)
                return "Enregistrer et envoyer le mail";
            return "Fermer";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
