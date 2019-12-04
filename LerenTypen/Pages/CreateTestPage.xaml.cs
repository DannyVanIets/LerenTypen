using LerenTypen.Controllers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CreateTestPage : Page
    {
        private List<TextBox> textBoxes;
        private List<string> textBoxValues;
        static int i = 0;
        private MainWindow m;

        public CreateTestPage(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            textBoxes = new List<TextBox>();
            textBoxValues = new List<string>();
            CreateInputLine();
            CreateInputLine();
            CreateInputLine();
            textInputTestName.MaxLength = 50;
        }

        private void AddLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateInputLine();
        }

        private void RemoveLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveInputLine(sender);
        }

        /// <summary>
        /// Used to remove textinput lines, when created, hyperlinks(sender) get a tag which corresponds to the name of the panel its on.
        /// textBoxes is a list which tracks the active textboxes, these need to be removed aswell.
        /// The addLinelink gets removed at the beginning and added after the method to make sure the foreach loops dont include this link.
        /// </summary>
        /// <param name="sender"></param>
        private void RemoveInputLine(object sender)
        {
            Hyperlink link = (Hyperlink)sender;
            testLinesPane.Children.Remove(addLineLink);

            foreach (StackPanel p in testLinesPane.Children)
            {
                if (p.Name.Equals("Panel" + link.Tag.ToString()))
                {
                    testLinesPane.Children.Remove(p);
                    break;
                }
            }
            foreach (TextBox t in textBoxes)
            {
                if (t.Name.Equals("textBox" + link.Tag.ToString()))
                {
                    textBoxes.Remove(t);
                    break;
                }
            }

            addLineLink.IsEnabled = true;
            Run run = new Run("Voeg een nieuwe regel toe");
            addLine.Inlines.Clear();
            addLine.Inlines.Add(run);

            testLinesPane.Children.Add(addLineLink);
        }

        /// <summary>
        /// Method for adding textinputlines, the lines include a hyperlink to remove themselves.
        /// Tags and Names are added for the RemoveInputLine method.
        /// </summary>
        private void CreateInputLine()
        {
            Thickness margin = new Thickness();
            StackPanel panel = new StackPanel();
            TextBlock tbl = new TextBlock();
            TextBox tb = new TextBox();
            Hyperlink removeLink = new Hyperlink();

            panel.Name = "Panel" + i.ToString();
            removeLink.Tag = i;
            removeLink.Inlines.Add("X");
            removeLink.Click += RemoveLine_Click;
            tbl.Inlines.Add(removeLink);
            tb.Height = 25;
            tb.MaxLength = 115;
            tb.TabIndex = i;
            panel.Orientation = Orientation.Horizontal;
            tbl.VerticalAlignment = VerticalAlignment.Center;

            margin.Left = 50;
            margin.Right = 20;
            margin.Top = 0;
            margin.Bottom = 10;
            tb.MinWidth = 900;
            tb.Margin = margin;
            tb.Name = "textBox" + i;
            textBoxes.Add(tb);

            panel.Children.Add(tb);
            panel.Children.Add(tbl);

            testLinesPane.Children.Remove(addLineLink);
            testLinesPane.Children.Add(panel);

            if (textBoxes.Count > 50)
            {
                addLineLink.IsEnabled = false;
                Run run = new Run("Max aantal regels bereikt (50)");
                addLine.Inlines.Clear();
                addLine.Inlines.Add(run);
            }

            testLinesPane.Children.Add(addLineLink);
            i++;
            scrollViewer.ScrollToEnd();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextFieldCheck())
            {
                if (SaveToDatabase())
                {
                    MessageBox.Show("Uw toets is succesvol opgeslagen", "Succesvol opgeslagen");
                    m.frame.Navigate(new CreateTestPage(m));
                }
            }
        }

        /// <summary>
        /// Method for sending the test to the database.
        /// </summary>
        private bool SaveToDatabase()
        {
            string title = textInputTestName.Text;
            int difficulty = comboBoxDifficulty.SelectedIndex;
            int type = comboBoxType.SelectedIndex;
            int privateTest = 0;

            if (privateRadio.IsChecked == true)
            {
                privateTest = 1;
            }

            // Text.Split splits the text into words using spaces
            // Empty words are not added to the counter amountOfWords
            // Decision was made to count words from db so function is not used
            foreach (TextBox t in textBoxes)
            {
                if (t.Text.Contains(" ") && comboBoxType.SelectedIndex.Equals(1))
                {
                    MessageBox.Show("Er mogen geen spaties gebruikt worden voor toetstype woorden", "Error");
                    return false;
                }
                textBoxValues.Add(t.Text);
            }

            int accountID = m.Ingelogd;
            TestController.AddTest(title, type, difficulty, privateTest, textBoxValues, accountID);
            return true;
        }

        /// <summary>
        /// Checks if all textboxes are filled and textboxes are included
        /// </summary>
        /// <returns>returns a boolean</returns>
        private bool TextFieldCheck()
        {
            bool textEmpty = false;

            foreach (TextBox t in textBoxes)
            {
                if (t.Text.Trim().Equals(""))
                {
                    textEmpty = true;
                    break;
                }
            }

            if (!textInputTestName.Text.Equals("") && !textEmpty && !textBoxes.Count.Equals(0))
            {
                return true;
            }
            else if (textBoxes.Count.Equals(0))
            {
                MessageBox.Show("De toets bevat geen regels", "Error");
            }
            else if (textEmpty)
            {
                MessageBox.Show("Vul alle toetsregels", "Error");
            }
            else
            {
                MessageBox.Show("De toets heeft geen titel", "Error");
            }
            return false;
        }
    }
}
