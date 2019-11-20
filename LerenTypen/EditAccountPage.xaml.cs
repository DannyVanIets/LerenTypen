using System.Windows.Controls;
using System.Windows.Media;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class EditAccountPage : Page
    {
        public EditAccountPage()
        {
            InitializeComponent();
        }

        private void DeleteAccountButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.Black;
        }

        private void DeleteAccountButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.White;
        }  
    }
}
