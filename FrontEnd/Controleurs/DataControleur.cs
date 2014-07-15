using MP22NET.DATA.ClassesData;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MP22NET.WpfFrontEnd.Controleurs
{
    public class DataControleur : INotifyPropertyChanged
    {
        #region static

        public const string DefaultRechercheText = "Rechercher...";

        public static DataControleur Data { get { return (DataControleur)Application.Current.FindResource("Data"); } }

        #endregion

        #region attribut
        private string _recherche;
        private bool _isInRecheche;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region propiete
        public ObservableCollection<Teacher> Teachers { get; private set; }
        public ObservableCollection<Courses> Courses { get; private set; }

        public bool IsInRecheche
        {
            get { return _isInRecheche; }
            set { _isInRecheche = value; OnPropertyChanged(); }
        }

        public string Recherche
        {
            get { return _recherche; }
            set { _recherche = value; OnPropertyChanged(); }
        }

        #endregion

        public DataControleur()
        {
            Teachers = new ObservableCollection<Teacher>(Connection.ConnectionBdd.Teacher);
            Courses = new ObservableCollection<Courses>(Connection.ConnectionBdd.Courses);
            Recherche = DefaultRechercheText;
            IsInRecheche = false;
        }

        public void Save()
        {
            try
            {
                Connection.ConnectionBdd.SaveChanges();
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        #region add / remove
        public void Add(Teacher t)
        {
            Teachers.Add(t);
            Connection.ConnectionBdd.Teacher.Add(t);
        }

        public void Add(Courses c)
        {
            Courses.Add(c);
            Connection.ConnectionBdd.Courses.Add(c);
        }

        public void Remove(Teacher t)
        {
            Teachers.Remove(t);
            Connection.ConnectionBdd.Teacher.Remove(t);
            Save();

        }

        public void Remove(Courses c)
        {
            Courses.Remove(c);
            Connection.ConnectionBdd.Courses.Remove(c);
            Save();

        }

        public void Modif(Teacher t)
        {
            Teachers.Add(t);
            Connection.ConnectionBdd.Teacher.Add(t);
        }

        #endregion


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
