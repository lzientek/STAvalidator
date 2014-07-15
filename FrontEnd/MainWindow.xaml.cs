using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MP22NET.DATA.ClassesData;
using MP22NET.Vue;
using MP22NET.WpfFrontEnd.Controleurs;

namespace MP22NET.WpfFrontEnd
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        #region evenements
        private void RechercheTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (RechercheTextBox.Text == DataControleur.DefaultRechercheText)
                RechercheTextBox.Text = "";
        }

        private void RechercheTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RechercheTextBox.Text))
                RechercheTextBox.Text = DataControleur.DefaultRechercheText;
        }

        /// <summary>
        /// a la fermeture on enregistre tout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
  
                DataControleur.Data.Save();

        }


        private void ButtonStopRecherche_OnClick(object sender, RoutedEventArgs e)
        {
            CloseRecherche();
        }

        private void TabControlMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataControleur.Data.IsInRecheche && e.Source is TabControl)
                CloseRecherche();
        }

        private void RechercheTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (RechercheTextBox.Text != DataControleur.DefaultRechercheText &&
                !string.IsNullOrWhiteSpace(RechercheTextBox.Text))
                Rechercher();
            
            else if(DataControleur.Data.IsInRecheche && !RechercheTextBox.IsFocused)
                    CloseRecherche();
            
        }


        #endregion

        public void CloseRecherche()
        {
            DataControleur.Data.IsInRecheche = false;
            var uc = (IRechercheUserControl)TabControlMenu.SelectedContent;
            uc.CloseRecherche();
            RechercheTextBox.Text = DataControleur.DefaultRechercheText;
        }
        private void Rechercher()
        {
            var uc = TabControlMenu.SelectedContent as IRechercheUserControl;

            uc.Rechercher();
            DataControleur.Data.IsInRecheche = true;
        }

        private void Button_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (button.IsVisible)
            {
                var story = Resources["AffichageBoutonRecherche"] as Storyboard;
                story.Begin();
            }
            
        }

        private void ButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonMaximize_OnClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Buttonminimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Deplacement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // deplace la fenetre
            DragMove();
        }
    }
}
