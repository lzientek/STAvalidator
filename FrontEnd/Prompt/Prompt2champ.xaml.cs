using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MP22NET.DATA.ClassesData;

namespace MP22NET.WpfFrontEnd.Prompt
{
    /// <summary>
    /// Logique d'interaction pour Prompt2champ.xaml
    /// </summary>
    public partial class Prompt2Champ 
    {
        public Champ Champ { get; set; }

        public Prompt2Champ(Champ c)
        {
            

            Champ = c;
            InitializeComponent();
            DataContext = Champ;
            TextBox1.Focus();
            Application.Current.MainWindow.IsEnabled = false;
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Champ.TextUn) && Champ.Selected != null)
            {
                OnValided();
                Close();
            }
            else
                MessageBox.Show(this, "Remplir tous les champs.");
        }

        public event EventHandler<EventArgs> Valided;

        protected virtual void OnValided()
        {
            EventHandler<EventArgs> handler = Valided;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void Prompt2Champ_OnClosed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.IsEnabled = true;
        }
    }

    public class Champ : INotifyPropertyChanged
    {
        private string _title;
        private string _champUn;
        private string _champDeux;
        private string _textUn;
        private List<string> _textDeux;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string ChampUn
        {
            get { return _champUn; }
            set { _champUn = value; OnPropertyChanged(); }
        }

        public string ChampDeux
        {
            get { return _champDeux; }
            set { _champDeux = value; OnPropertyChanged(); }
        }

        public string TextUn
        {
            get { return _textUn; }
            set { _textUn = value; OnPropertyChanged(); }
        }

        public List<string> TextDeux
        {
            get { return _textDeux; }
            set { _textDeux = value; OnPropertyChanged(); }
        }

        public string Selected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
