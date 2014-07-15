using System;
using System.Globalization;
using System.Windows.Data;
using MP22NET.Vue;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd.Converters
{
    class CanSearchToIsEnableMultiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedcontent = value as IRechercheUserControl;
            //si la recherche est activer sur le usercontrol du menu 
            return selectedcontent != null;

        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
