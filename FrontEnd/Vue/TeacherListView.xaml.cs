using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MP22NET.DATA.ClassesData;
using MP22NET.DATA.ClassesMetier;
using MP22NET.Vue;
using MP22NET.WpfFrontEnd.Controleurs;
using MP22NET.WpfFrontEnd.Prompt;

namespace MP22NET.WpfFrontEnd.Vue
{
    /// <summary>
    /// Logique d'interaction pour TeacherListVue.xaml
    /// </summary>
    public partial class TeacherListView : UserControl, IRechercheUserControl
    {
        public TeacherListView()
        {
            this._oldGridHeightFlipView = 200;
            InitializeComponent();
            if (TeachersListBox.Items.Count > 0)
                TeachersListBox.SelectedIndex = 0;
        }


        #region gest popup


        private void InputOnValidCertif(object sender, EventArgs cancelEventArgs)
        {
            var fenetre = sender as Prompt2Champ;
            var t = ListBoxCertif.DataContext as Teacher;
            t.Certification.Add(new Certification
            {
                Name = fenetre.Champ.TextUn,
                Note = fenetre.Champ.Selected,
                Teacher = t,
            });
            //partie moche pour actualiser les certifs
            RefreshFormulaire();
            DataControleur.Data.Save();

        }

        private void InputOnValidCours(object sender, EventArgs eventArgs)
        {
            var fenetre = sender as Prompt2Champ;
            var t = ListBoxCours.DataContext as Teacher;
            var cours = Connection.ConnectionBdd.Courses.Where(c => fenetre.Champ.Selected.StartsWith(c.Id)).First();
            t.Repartition.Add(new Repartition
            {
                Courses = cours,
                Campus = fenetre.Champ.TextUn,
                Teacher = t,
            });
            RefreshFormulaire();
            DataControleur.Data.Save();

        }
        #endregion

        #region bouton click

        private void ButtonAjoutCertif_OnClick(object sender, RoutedEventArgs e)
        {
            var champ = new Champ
            {
                Title = "Ajouter une certifiction",
                ChampUn = "Nom :",
                ChampDeux = "Note :",
                TextDeux = new List<string> { "A", "B" }
            };
            var input = new Prompt2Champ(champ) { Title = "Ajouter" };
            input.Show();
            input.Valided += InputOnValidCertif;
        }

        private void ButtonAjoutCours_OnClick(object sender, RoutedEventArgs e)
        {
            DataControleur.Data.Save();
            if (Connection.ConnectionBdd.Courses.Any())
            {
                var champ = new Champ
                {
                    Title = "Cours donné",
                    ChampUn = "Campus :",
                    ChampDeux = "Cours :",
                    TextDeux = new List<string>(from cours in Connection.ConnectionBdd.Courses
                                                select cours.Id + " " + cours.Name)
                };
                var input = new Prompt2Champ(champ);
                input.Title = "Ajouter";
                input.Show();
                input.Valided += InputOnValidCours;
            }
            else
            {
                MessageBox.Show("Il faut ajouter au moins un cours dans l'onglet question.");
            }
        }

        private void ButtonSupprimerCertif_OnClick(object sender, RoutedEventArgs e)
        {
            var t = ListBoxCertif.DataContext as Teacher;
            t.Certification.Remove((Certification)ListBoxCertif.SelectedItem);
            RefreshFormulaire();
        }

        private void ButtonSupprimerCours_OnClick(object sender, RoutedEventArgs e)
        {
            var t = ListBoxCertif.DataContext as Teacher;
            t.Repartition.Remove((Repartition)ListBoxCours.SelectedItem);
            RefreshFormulaire();
        }

        private void ButtonAjouterTeacher_OnClick(object sender, RoutedEventArgs e)
        {
            var data = DataControleur.Data;
            if (data.IsInRecheche)//on peut pas ajouter a la liste rechercher du coup on clear proprement
                (Application.Current.MainWindow as MainWindow).CloseRecherche();

            var t = new Teacher
            {
                Id = 1,
                Name = "nom",
                Firstname = "prenom",
                PromoCurrent = 1,
                PromoDuring = 1,
                Email = "@supinfo.com"
            };
            data.Add(t);
            TeachersListBox.SelectedItem = t;
            TextBoxId.IsEnabled = true;
            TextBoxId.Focus();
            TextBoxId.SelectAll();

        }

        private void ButtonSupprimerTeacher_OnClick(object sender, RoutedEventArgs e)
        {
            var data = DataControleur.Data;
            var t = TeachersListBox.SelectedItem as Teacher;
            data.Remove(t);
            if (TeachersListBox.Items.Count > 0)
                TeachersListBox.SelectedIndex = 0;
        }
        #endregion

        private void TextboxID_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(TextBoxId.Text);
                TextBoxId.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Entré une valeur valide dans le champ Id!");
                TextBoxId.Focus();
                TextBoxId.SelectAll();
            }
        }

        private void RefreshFormulaire()
        {
            int index = TeachersListBox.SelectedIndex;
            TeachersListBox.SelectedIndex = -1;
            TeachersListBox.SelectedIndex = index;
        }

        public void Rechercher()
        {

            var d = DataControleur.Data;
            var motsRecherche = d.Recherche.Split(' ');
            if (motsRecherche.Count() == 1)
            {
                TeachersListBox.ItemsSource = d.Teachers.Where(
                        t => t.Firstname.Contains(motsRecherche[0])
                             || t.Name.Contains(motsRecherche[0])
                             || t.Email.Contains(motsRecherche[0]));
            }
            else
            {
                var ts = new List<Teacher>();
                foreach (var m in motsRecherche.Where(m => !string.IsNullOrWhiteSpace(m)))
                    ts.AddRange(d.Teachers.Where(
                        t => t.Firstname.Contains(m)
                             || t.Name.Contains(m)
                             || t.Email.Contains(m)));

                TeachersListBox.ItemsSource = ts.Where(t => ts.Count(t1 => t == t1)
                                                >= motsRecherche.Count(m => !string.IsNullOrWhiteSpace(m)))
                                                .Distinct();
            }
            if (TeachersListBox.HasItems)
                TeachersListBox.SelectedIndex = 0;

        }

        public void CloseRecherche()
        {
            TeachersListBox.ItemsSource = DataControleur.Data.Teachers;
        }

        private void MenuItemPassageCertif_OnClick(object sender, RoutedEventArgs e)
        {
            if (TeachersListBox.SelectedIndex >= 0)
            {
                ValidationControleur.ValidationSta = new ValidationSTA(TeachersListBox.SelectedItem as Teacher);
                var main = Application.Current.MainWindow as MainWindow;
                main.TabControlMenu.SelectedIndex = 2;
                var frame = main.TabControlMenu.SelectedContent as Frame;
                frame.Navigate(new Uri(@"\Vue\Pages\ValidationPageChoixCoursQuestion.xaml", UriKind.Relative));
            }
        }


        private double _oldGridHeightFlipView;
        private void FlipViewDisp_OnVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (FlipView.IsVisible)
            {
                _oldGridHeightFlipView = ListboxGrid.RowDefinitions[0].ActualHeight;
                ListboxGrid.RowDefinitions[0].Height = new GridLength(0);
            }
            else
                ListboxGrid.RowDefinitions[0].Height = new GridLength(_oldGridHeightFlipView);


        }

        private void FlipView_OnSuivantClick(object sender, EventArgs e)
        {
            if (TeachersListBox.Items.Count > 0)
                if (TeachersListBox.SelectedIndex != (TeachersListBox.Items.Count - 1))
                    TeachersListBox.SelectedIndex++;
                else
                    TeachersListBox.SelectedIndex = 0;
        }

        private void FlipView_OnPrecedentClick(object sender, EventArgs e)
        {
            if (TeachersListBox.Items.Count > 0)
                if (TeachersListBox.SelectedIndex != 0)
                    TeachersListBox.SelectedIndex--;
                else
                    TeachersListBox.SelectedIndex = TeachersListBox.Items.Count - 1;
        }
    }
}
