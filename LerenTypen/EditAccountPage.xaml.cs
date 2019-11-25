using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class EditAccountPage : Page
    {
        private MainWindow MainWindow;

        public EditAccountPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void DeleteAccountButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.Black;
        }

        private void DeleteAccountButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.White;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string firstname = firstNameTextBox.Text;
            string lastname = lastNameTextbox.Text;
            string username = usernameTextBox.Text;
            string oldPassword = oldPasswordTextBox.Password;
            string newPassword = newPasswordTextBox.Password;
            string newPasswordRepeat = passwordRepeatTextBox.Password;
            string securityQuestion = securityQuestionComboBox.Text;
            string securityAnswer = securityAnswerTextBox.Text;

            if (string.IsNullOrEmpty(oldPassword) && string.IsNullOrEmpty(newPassword) && string.IsNullOrEmpty(newPasswordRepeat))
            {
                //Here we will update everything, except the password.
                MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
            }
            else if (string.IsNullOrEmpty(oldPassword) && (!string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(newPasswordRepeat)))
            {
                //Here we are gonna give errors for the password
                MessageBox.Show("U moet het oude wachtwoord invoeren voordat u een nieuw wachtwoord kunt invoeren.", "Error");
            }
            else if (!string.IsNullOrEmpty(oldPassword) && (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newPasswordRepeat)))
            {
                //Here we are gonna give errors for the password
                MessageBox.Show("U moet een nieuw wachtwoord en herhaling van het nieuwe wachtwoord invoeren!", "Error");
            }
            else
            {
                //Hier wordt alles geüpdate!
                MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
            }
        }
    }
}
