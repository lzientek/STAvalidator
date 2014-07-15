using System;
using System.Windows;
using System.Windows.Controls;
using MP22NET.DATA.ClassesData;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd.Vue.Pages
{
    /// <summary>
    /// Logique d'interaction pour ValidationPageFinal.xaml
    /// </summary>
    public partial class ValidationPageFinal : Page
    {
        public ValidationPageFinal()
        {
            InitializeComponent();
            DataContext = ValidationControleur.ValidationSta;
        }

        private void ButtonValidation_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var vSta = ValidationControleur.ValidationSta;
            if (vSta.IsValid)
            {
                foreach (var campus in vSta.CampusList)
                    vSta.Teacher.Repartition.Add(new Repartition
                    {
                        Campus = campus,
                        Courses = vSta.Courses,
                        Teacher = vSta.Teacher
                    });

                if (MessageBox.Show("Voulez vous vraiment envoyer le mail?", "envoie", MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                    vSta.Mail.SendMail();
            }
            DataControleur.Data.Save();
            ValidationControleur.ValidationSta = null;
            
            NavigationService.Navigate(new Uri(@"\Vue\Pages\ValidationPageChoixProf.xaml", UriKind.Relative));
        }
    }
}
