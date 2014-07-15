using System;
using System.Windows;
using System.Windows.Controls;
using MP22NET.DATA.ClassesData;
using MP22NET.DATA.ClassesMetier;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd.Vue.Pages
{
    /// <summary>
    /// Logique d'interaction pour ValidationPageChoixProf.xaml
    /// </summary>
    public partial class ValidationPageChoixProf : Page
    {
        public ValidationPageChoixProf()
        {
            InitializeComponent();
        }

        private void ButtonValider_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxTeachers.SelectedIndex != -1)
            {
                try
                {
                    ValidationControleur.ValidationSta = new ValidationSTA(ListBoxTeachers.SelectedItem as Teacher);
                    NavigationService.Navigate(new Uri(@"\Vue\Pages\ValidationPageChoixCoursQuestion.xaml", UriKind.Relative));

                }
                catch (Exception)
                {
                    
                    
                }
            }
            else
            {
                MessageBox.Show("Choisissez un STA.");
            }
        }
    }
}
