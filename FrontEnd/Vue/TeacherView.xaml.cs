using System.Windows;
using System.Windows.Controls;

namespace MP22NET.WpfFrontEnd.Vue
{
    /// <summary>
    /// Logique d'interaction pour TeacherVue.xaml
    /// </summary>
    public partial class TeacherView : UserControl
    {
        public TeacherView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AffichageLevelProperty =
                 DependencyProperty.Register("AffichageLevel", typeof(int),
                 typeof(TeacherView), new FrameworkPropertyMetadata(default(int)));

        public int AffichageLevel
        {
            get { return (int)GetValue(AffichageLevelProperty); }
            set { SetValue(AffichageLevelProperty, value); }
        }
    }
}
