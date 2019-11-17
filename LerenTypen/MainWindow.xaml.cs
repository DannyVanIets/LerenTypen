using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Page1(), page1Button);
        }

        private void Page2Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Page2(), page2Button);
        }

        private void Page3Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Page3(), page3Button);
        }

        private void Page4Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Page4(), page4Button);
        }

        private void Page5Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Page5(), page5Button);
        }

        /// <summary>
        /// Changes the page to the specified page if this page is not 
        /// already open and updates the menu buttons accordingly
        /// </summary>
        /// <param name="pageToChangeTo"></param>
        /// <param name="pageToggleButton"></param>
        private void ChangePage(Page pageToChangeTo, ToggleButton pageToggleButton)
        {
            if (frame.Content != pageToChangeTo)
            {
                frame.Navigate(pageToChangeTo);
                SwitchMenuButtons(pageToggleButton);
            }           
        }

        /// <summary>
        /// Checks the specified button and unchecks all other buttons of the menu
        /// </summary>
        /// <param name="buttonToSwitchTo">The ToggleButton to check</param>
        private void SwitchMenuButtons(ToggleButton buttonToSwitchTo)
        {
            page1Button.IsChecked = false;
            page2Button.IsChecked = false;
            page3Button.IsChecked = false;
            page4Button.IsChecked = false;
            page5Button.IsChecked = false;

            buttonToSwitchTo.IsChecked = true;
        }
    }
}
