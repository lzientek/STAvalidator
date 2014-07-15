using System;
using System.Windows;
using System.Windows.Controls;
using MP22NET.DATA.ClassesData;
using MP22NET.DATA.ClassesMetier;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd.Vue.Pages
{
    /// <summary>
    /// Logique d'interaction pour ValidationPageChoixCoursQuestion.xaml
    /// </summary>
    public partial class ValidationPageChoixCoursQuestion : Page
    {
        public ValidationPageChoixCoursQuestion()
        {
            InitializeComponent();
            DataContext = ValidationControleur.ValidationSta;
        }

        private void ButtonValidation_OnClick(object sender, RoutedEventArgs e)
        {
            //si on a bien un campus ajouter
            if (ValidationControleur.ValidationSta.CampusList.Count <= 0)
                MessageBox.Show("Vous devez ajouter au moins un campus.");
            //si on a bien un cours de choisi et une question
            else if (ComboBoxCours.SelectedIndex != -1 && ListBoxQuestion.SelectedItems.Count > 0)
                try
                {
                    foreach (var item in ListBoxQuestion.SelectedItems)
                        ValidationControleur.ValidationSta.Questions.Add(new ScoredQuestion((Questions)item));


                    ValidationControleur.ValidationSta.Courses = ComboBoxCours.SelectedItem as Courses;
                    NavigationService.Navigate(new Uri(@"\Vue\Pages\ValidationPageQuestions.xaml", UriKind.Relative));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }

            else
                MessageBox.Show("Vous devez choisir une matiere et au moins une question.");


        }

        private void ButtonRetour_OnClick(object sender, RoutedEventArgs e)
        {
            ValidationControleur.ValidationSta.CampusList.Clear();
            if (NavigationService != null)
                NavigationService.GoBack();
        }

        private void ButtonAjouterCampus_OnClick(object sender, RoutedEventArgs e)
        {
            ValidationControleur.ValidationSta.CampusList.Add(TextBoxCampus.Text);
            TextBoxCampus.Text = string.Empty;
            TextBoxCampus.Focus();
        }
    }
}
