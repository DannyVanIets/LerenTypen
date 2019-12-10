using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TipPage : Page
    {

        private MainWindow MainWindow;
        public TipPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;

        }

        private void LoginRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new LoginPage(MainWindow));
        }
    }
}
