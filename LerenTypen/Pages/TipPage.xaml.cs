using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LerenTypen.Models;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TipPage : Page
    {

        private MainWindow MainWindow;
        private int ButtonOption;
        private List<Ellipse> Ellipses = new List<Ellipse>();
        private List<Ellipse> EllipsesForCircles = new List<Ellipse>();
        private int CurrentLetter;
        private List<UIElementCollection> allPanels = new List<UIElementCollection>();


        public TipPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;
            this.ButtonOption = 0;
            this.CurrentLetter = 1;
            
            

            // Adds all the ellipses from the fingernails to a list.
            foreach (Ellipse item in TipPage_Canvas_AllFingers.Children)
            {
                if (item.Tag.ToString().Contains("Circle"))
                {
                    EllipsesForCircles.Add(item);
                }
                else
                {
                    Ellipses.Add(item);
                }
            }

            // Adding all the rectangles of each stackpanel to one list
            allPanels.Add(TipPage_StackPanel_Keyboard_Row0.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row1.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row2.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row3.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row4.Children);

        }

        /// <summary>
        /// Event to process the buttonclick to go to the loginPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new LoginPage(MainWindow));
        }

        /// <summary>
        /// Event when a key gets pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewKeyDown_Clicker(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Happens when something is typed in the textbox while the "voorbeeldwoorden" button is active.
            if (ButtonOption == 1)
            {
                TipPage_TestBox.Text = "";
                // First we check if the right K is typed
                if (e.Key == System.Windows.Input.Key.K && CurrentLetter == 1)
                {
                    // We use currentLetter to show which letter it is
                    CurrentLetter++;
                    // Turns the border green if the right key is pressed
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Green, 2);
                    // Makes the clicked key visible
                    TipPage_Canvas_Key_K.Visibility = Visibility.Visible;
                    // Selects the next key to show on the keyboard
                    TipsController.SelectKey(TipPage_Canvas_Key_A, Ellipses, EllipsesForCircles);
                    // Highlights the next key
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.A && CurrentLetter == 2)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Green, 2);
                    TipPage_Canvas_Key_A.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_T, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.T && CurrentLetter == 3)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Green, 2);
                    TipPage_Canvas_Key_T.Visibility = Visibility.Visible;

                    // Sets the new word
                    TipPage_Label_Letter1.Content = "H";
                    TipPage_Label_Letter2.Content = "O";
                    TipPage_Label_Letter3.Content = "N";
                    TipPage_Label_Letter4.Content = "D";
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Red, 2);
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Black, 1);

                    TipsController.SelectKey(TipPage_Canvas_Key_H, Ellipses, EllipsesForCircles);
                }
                else if (e.Key == System.Windows.Input.Key.H && CurrentLetter == 4)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Green, 2);
                    TipPage_Canvas_Key_H.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_O, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.O && CurrentLetter == 5)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Green, 2);
                    TipPage_Canvas_Key_O.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_N, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.N && CurrentLetter == 6)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Green, 2);
                    TipPage_Canvas_Key_N.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_D, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.D && CurrentLetter == 7)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Green, 2);
                    TipPage_Canvas_Key_D.Visibility = Visibility.Visible;

                    TipPage_Label_Letter1.Content = "M";
                    TipPage_Label_Letter2.Content = "U";
                    TipPage_Label_Letter3.Content = "I";
                    TipPage_Label_Letter4.Content = "S";
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Red, 2);
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Black, 1);

                    TipsController.SelectKey(TipPage_Canvas_Key_M, Ellipses, EllipsesForCircles);
                }
                else if (e.Key == System.Windows.Input.Key.M && CurrentLetter == 8)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Green, 2);
                    TipPage_Canvas_Key_M.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_U, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.U && CurrentLetter == 9)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Green, 2);
                    TipPage_Canvas_Key_U.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_I, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.I && CurrentLetter == 10)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Green, 2);
                    TipPage_Canvas_Key_I.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_S, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.S && CurrentLetter == 11)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Green, 2);
                    TipPage_Canvas_Key_S.Visibility = Visibility.Visible;

                    TipPage_Label_Letter1.Content = "S";
                    TipPage_Label_Letter2.Content = "T";
                    TipPage_Label_Letter3.Content = "I";
                    TipPage_Label_Letter4.Content = "E";
                    TipPage_Label_Letter5.Content = "R";
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Red, 2);
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Black, 1);
                    TipsController.BorderSetter(TipPage_Label_Letter5, Brushes.Black, 1);

                    TipsController.SelectKey(TipPage_Canvas_Key_S, Ellipses, EllipsesForCircles);
                }
                else if (e.Key == System.Windows.Input.Key.S && CurrentLetter == 12)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Green, 2);
                    TipPage_Canvas_Key_S.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_T, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.T && CurrentLetter == 13)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Green, 2);
                    TipPage_Canvas_Key_T.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_I, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.I && CurrentLetter == 14)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Green, 2);
                    TipPage_Canvas_Key_I.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_E, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.E && CurrentLetter == 15)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Green, 2);
                    TipPage_Canvas_Key_E.Visibility = Visibility.Visible;
                    TipsController.SelectKey(TipPage_Canvas_Key_R, Ellipses, EllipsesForCircles);
                    TipsController.BorderSetter(TipPage_Label_Letter5, Brushes.Red, 2);
                }
                else if (e.Key == System.Windows.Input.Key.R && CurrentLetter == 16)
                {
                    CurrentLetter++;
                    TipsController.BorderSetter(TipPage_Label_Letter5, Brushes.Green, 2);
                    TipPage_Canvas_Key_R.Visibility = Visibility.Visible;
                    foreach (Ellipse ellipse in Ellipses)
                    {
                        ellipse.Visibility = Visibility.Visible;
                    }
                    foreach (Ellipse ellipse in EllipsesForCircles)
                    {
                        ellipse.Visibility = Visibility.Hidden;
                    }

                }
            }
            else if (ButtonOption == 2)
            {
                string fingerTag = "";
                string circleTag = "";
                List<string> allNames = new List<string>();
                List<string> allTotalNames = new List<string>();

               
                // Now we add the rectangles' variable names to one list
                foreach (UIElementCollection uIElementCollection in allPanels)
                {
                    allNames = TipsController.SubstringRectangleVariables(uIElementCollection);
                    foreach (var item in allNames)
                    {
                        allTotalNames.Add(item);
                    }
                }
                // After getting all the variable names, we find the right key 
                string FoundKey = TipsController.ShowKey(e, allTotalNames);

                // Convert the found key back to the full variable name
                string fullName = "TipPage_Canvas_Key_" + FoundKey;

                // After getting the right variable, coupled with the pressed key, we can now make the right rectangle hidden
                foreach (UIElementCollection uIElementCollection in allPanels)
                {
                    Rectangle foundRectangle = TipsController.FindPressedKey(uIElementCollection, fullName);
                    if (foundRectangle != null)
                    {
                        // This stores the colortag of the pressed key, so we can use it to find the corresponding finger
                        fingerTag = foundRectangle.Tag.ToString();
                    }
                }

                circleTag = fingerTag + "Circle";
                // Here we find the corresponding finger to the pressed key
                TipsController.FindCorrespondingFinger(fingerTag, Ellipses);
                // Here we find the corresponding circle around the finger
                TipsController.FindCorrespondingCircle(circleTag, EllipsesForCircles);
            }
        }
        /// <summary>
        /// Button that handles the event when its clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipPage_Button_MaakZelfWoorden_Click(object sender, RoutedEventArgs e)
        {
            ButtonOption = 2;
            TipPage_Button_MaakZelfWoorden.IsEnabled = false;
            TipPage_Button_Voorbeeldwoorden.IsEnabled = true;
            TipPage_TestBox.IsEnabled = true;
            TipPage_TestBox.Visibility = Visibility.Visible;
            TipPage_TestBox.Text = "";
            TipPage_Label_Uitleg2.Content = "Maak zelf woorden";
            TipPage_Label_Uitleg2.FontSize = 30;

            // First, we make every rectangle visible, incase they arent
            foreach (UIElementCollection uIElementCollection in allPanels)
            {
                Rectangle foundRectangle = TipsController.FindPressedKey(uIElementCollection, "");
            }

            // This makes every label with individual letters hidden.
            foreach (Label label in TipPage_StackPanel_AllLetters.Children)
            {
                label.Visibility = Visibility.Hidden;
            }

            TipPage_Label_Intro.Content = "Typ hier zelf letters en zie op het plaatje hiernaast hoe je het moet typen";
            TipPage_Label_Intro.Visibility = Visibility.Visible;

            TipPage_TestBox.Width = 200;
            TipPage_TestBox.Height = 30;

            // Sets all the keyboardvalues to default
            foreach (Ellipse ellipse in Ellipses)
            {
                ellipse.Visibility = Visibility.Visible;
            }
            foreach (Ellipse ellipse in EllipsesForCircles)
            {
                ellipse.Visibility = Visibility.Hidden;
            }


        }
        /// <summary>
        /// Handles the event when the user clicks on this button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipPage_Button_Voorbeeldwoorden_Click(object sender, RoutedEventArgs e)
        {
            ButtonOption = 1;
            TipPage_Button_Voorbeeldwoorden.IsEnabled = false;
            TipPage_Button_MaakZelfWoorden.IsEnabled = true;
            TipPage_TestBox.IsEnabled = true;
            TipPage_TestBox.Visibility = Visibility.Visible;
            TipPage_TestBox.Text = "";
            CurrentLetter = 1;

            TipPage_Label_Uitleg2.Content = "Voorbeeldwoorden";
            TipPage_Label_Uitleg2.FontSize = 30;
            TipPage_Label_Intro.Content = "Hieronder staat een voorbeeldwoord.\nKijk in het plaatje hiernaast hoe je dit moet typen";
            TipPage_Label_Intro.Visibility = Visibility.Visible;

            TipPage_TestBox.BorderBrush = Brushes.Black;
            TipPage_TestBox.BorderThickness = new Thickness(1);


            TipPage_Label_Letter1.Content = "K";
            TipPage_Label_Letter2.Content = "A";
            TipPage_Label_Letter3.Content = "T";
            TipPage_Label_Letter4.Content = "";
            TipPage_Label_Letter5.Content = "";

            // Sets all the keyboardvalues to default
            foreach (Label label in TipPage_StackPanel_AllLetters.Children)
            {
                label.Visibility = Visibility.Visible;
            }
            foreach (UIElementCollection uIElementCollection in allPanels)
            {
                Rectangle foundRectangle = TipsController.FindPressedKey(uIElementCollection, "");
            }
            // Selects the first key
            TipsController.SelectKey(TipPage_Canvas_Key_K, Ellipses, EllipsesForCircles);

            // Sets the defaultvalues for the borders
            TipsController.BorderSetter(TipPage_Label_Letter1, Brushes.Red, 2);
            TipsController.BorderSetter(TipPage_Label_Letter2, Brushes.Transparent, 0);
            TipsController.BorderSetter(TipPage_Label_Letter3, Brushes.Transparent, 0);
            TipsController.BorderSetter(TipPage_Label_Letter4, Brushes.Transparent, 0);
            TipsController.BorderSetter(TipPage_Label_Letter5, Brushes.Transparent, 0);

        }
    }
}
