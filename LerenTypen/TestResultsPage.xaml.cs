using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestResultsPage : Page
    {
        private int testID;
        public TestResultsPage(int testID)
        {
            InitializeComponent();
            this.testID = testID;
        }
    }
}
