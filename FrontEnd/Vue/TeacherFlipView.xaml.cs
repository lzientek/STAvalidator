using System;
using System.Windows;
using System.Windows.Controls;

namespace MP22NET.WpfFrontEnd.Vue
{
    /// <summary>
    /// Logique d'interaction pour TeacherFlipView.xaml
    /// </summary>
    public partial class TeacherFlipView : UserControl
    {

        #region events

        public event EventHandler PrecedentClick;
        public event EventHandler SuivantClick;

        #endregion

        public TeacherFlipView()
        {
            InitializeComponent();
        }

        private void ButtonPrecedent_OnClick(object sender, RoutedEventArgs e)
        {
            if (PrecedentClick != null)
                PrecedentClick(this, e);
        }

        private void ButtonSuivant_OnClick(object sender, RoutedEventArgs e)
        {
            if (SuivantClick != null)
                SuivantClick(this, e);
        }
    }
}
