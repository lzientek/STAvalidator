using System;
using System.Windows.Controls;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd.Vue.Pages
{
    /// <summary>
    /// Logique d'interaction pour ValidationPageQuestions.xaml
    /// </summary>
    public partial class ValidationPageQuestions : Page
    {
        public ValidationPageQuestions()
        {
            InitializeComponent();
            DataContext = ValidationControleur.ValidationSta;
        }

        private void ButtonValidation_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ValidationControleur.ValidationSta.CalculScoreEcts();
                ValidationControleur.ValidationSta.CheckFinalScore(); 
                NavigationService.Navigate(new Uri(@"\Vue\Pages\ValidationPageFinal.xaml", UriKind.Relative));

            }
            catch (Exception exception)
            {
            }
        }

        private void ButtonRetour_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //on vide les question pour pas les avoir en double
            ValidationControleur.ValidationSta.Questions.Clear();

            if (NavigationService != null)
                NavigationService.GoBack();
        }
    }
}
