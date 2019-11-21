using System.Collections.Generic;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private MainWindow mainWindow;

        public HomePage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;           
        }

        private void LoginRegisterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ChangePage(new LoginPage(mainWindow));
        }

        private void MoreTrendingTestsLink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ChangePage(new TrendingTestsPage());
        }
    }

    class Test
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public override string ToString()
        {
            return $"{Name} door {Author}";
        }
    }
}
