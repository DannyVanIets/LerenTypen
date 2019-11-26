using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private MainWindow mainWindow;

        public HomePage()
        {
            InitializeComponent();
        }

        public HomePage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

        }
    }
}