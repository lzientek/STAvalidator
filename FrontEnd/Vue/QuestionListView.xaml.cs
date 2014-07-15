using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MP22NET.DATA.ClassesData;
using MP22NET.Vue;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd.Vue
{
    /// <summary>
    /// Logique d'interaction pour QuestionListView.xaml
    /// </summary>
    public partial class QuestionListView : UserControl, IRechercheUserControl
    {
        public QuestionListView()
        {
            InitializeComponent();
            if (ListBoxCourses.Items.Count > 0)
                ListBoxCourses.SelectedIndex = 0;
            if (ListBoxQuestion.Items.Count > 0)
                ListBoxQuestion.SelectedIndex = 0;
        }

        #region click bouton

        private void ButtonAjouterMatiere_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataControleur.Data.IsInRecheche)//on peut pas ajouter a la liste rechercher du coup on clear proprement
                (Application.Current.MainWindow as MainWindow).CloseRecherche();
            var c = new Courses
            {
                Name = "nom",
                Id = "1AAA"
            };
            DataControleur.Data.Add(c);
            ListBoxCourses.SelectedItem = c;
            TextBoxId.IsEnabled = true;
            TextBoxId.Focus();
            TextBoxId.SelectAll();

        }
        private void ButtonSupprimerMatiere_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment supprimer la matiere et ses questions?", "Annuler",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                var c = ListBoxCourses.SelectedItem as Courses;
                //suppression de toute les dépendances.
                Connection.ConnectionBdd.Repartition.RemoveRange(c.Repartition);
                Connection.ConnectionBdd.Questions.RemoveRange(c.Questions);
                DataControleur.Data.Remove(c);

            }
        }
        private void ButtonAjouterQuestion_OnClick(object sender, RoutedEventArgs e)
        {
            var c = ListBoxCourses.SelectedItem as Courses;
            var q = new Questions { Texte = "Question?", Type = 0 };
            c.Questions.Add(q);
            ListBoxCourses.SelectedIndex = -1;
            ListBoxCourses.SelectedItem = c;
            ListBoxQuestion.SelectedItem = q;
            TextBoxQuestion.Focus();
            TextBoxQuestion.SelectAll();


        }
        private void ButtonSupprimerQuestion_OnClick(object sender, RoutedEventArgs e)
        {
            var c = ListBoxCourses.SelectedItem as Courses;
            var q = ListBoxQuestion.SelectedItem as Questions;
            c.Questions.Remove(q);
            Connection.ConnectionBdd.Questions.Remove(q);
            ListBoxCourses.SelectedIndex = -1;
            ListBoxCourses.SelectedItem = c;
            

        }

        #endregion

        private void TextBoxId_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(TextBoxId.Text, "[1-5][A-Z]{3}"))
                TextBoxId.IsEnabled = false;
            else
            {
                MessageBox.Show("Entré un Id de matiere valide (exemple 1ADS ou 2NET)");
                TextBoxId.Focus();
                TextBoxId.SelectAll();
            }
        }

        public void Rechercher()
        {

            //revert binding selectionne le cours de la question selectionner (ne doit fonctionner uniquement pendant la recherche)
            Binding bnd = new Binding("SelectedItem.Courses") { ElementName = "ListBoxQuestion" };
            BindingOperations.SetBinding(ListBoxCourses, ListBox.SelectedItemProperty, bnd);


            var d = DataControleur.Data;
            var questions = new List<Questions>(Connection.ConnectionBdd.Questions);
            var motsRecherche = d.Recherche.Split(' ');

            if (motsRecherche.Count() == 1)//economie de ressources (pas de list provisoire)
                ListBoxQuestion.ItemsSource = questions.Where(
                        q => q.Texte.Contains(motsRecherche[0]));

            else
            {
                var qs = new List<Questions>();
                foreach (var m in motsRecherche.Where(m => !string.IsNullOrWhiteSpace(m)))
                    qs.AddRange(questions.Where(q => q.Texte.Contains(m)));

                ListBoxQuestion.ItemsSource = qs.Where(q => qs.Count(q1 => q == q1)
                                                >= motsRecherche.Count(m => !string.IsNullOrWhiteSpace(m)))
                                                .Distinct();
            }


            if (ListBoxQuestion.HasItems)
                ListBoxQuestion.SelectedIndex = 0;
        }

        public void CloseRecherche()
        {

            //binding de retour on le supprime (sinon génère des bugs d'affichage)
            BindingOperations.ClearBinding(ListBoxCourses, ListBox.SelectedItemProperty);

            //on recréer le binding des cours selectionner
            Binding bnd = new Binding("SelectedItem.Questions") { ElementName = "ListBoxCourses" };
            BindingOperations.SetBinding(ListBoxQuestion, ListBox.ItemsSourceProperty, bnd);
        }
    }
}
