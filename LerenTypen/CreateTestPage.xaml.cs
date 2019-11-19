using System;
using System.Collections;
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
        // De eerste vier variablen kan je in de functie zelf aanmaken, hoeven niet voor de hele klasse beschikbaar te zijn
        private TextBox tb;
        private StackPanel panel; 
        private TextBlock tbl;
        private Thickness margin;
        private List<TextBox> textBoxes;
        private List<string> textBoxValues;
        static int i = 0;


        public CreateTestPage()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            textBoxValues = new List<string>();
            createInputLine();
            createInputLine();
            createInputLine();
            

            // Overbodige witruimte



        }
        
        // Methodes horen te beginnen met een hoofdletter!
        private void addLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            createInputLine();
        }
        private void removeLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            removeInputLine(sender);

           
        }

        private void removeInputLine(object sender)
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


            testLinesPane.Children.Add(addLineLink);
        }



        
        private void createInputLine()
        {
            panel = new StackPanel(); // Hier dus gewoon Panel panel = new StackPanel();
            panel.Name = "Panel" + i.ToString();
            tbl = new TextBlock();
            tb = new TextBox();


            Hyperlink removeLink = new Hyperlink();
            removeLink.Tag = i;
            removeLink.Inlines.Add("X");
            removeLink.Click += removeLine_Click;
            tbl.Inlines.Add(removeLink);

            tb.Height = 25;
            panel.Orientation = Orientation.Horizontal;

            tbl.VerticalAlignment = VerticalAlignment.Center;

            margin.Left = 50;
            margin.Right = 20;
            margin.Top = 0;
            margin.Bottom = 10;
            tb.Width = 800;
            tb.Margin = margin;
            tb.Name = "textBox" + i;
            textBoxes.Add(tb);
            
            panel.Children.Add(tb);
            panel.Children.Add(tbl);

            testLinesPane.Children.Remove(addLineLink);
            testLinesPane.Children.Add(panel);
            testLinesPane.Children.Add(addLineLink);
            i++;
            scrollViewer.ScrollToEnd();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (textFieldCheck())
            {
                saveToDatabase();
            }



        }
        private void saveToDatabase()
        {
            string title = textInputTestName.Text;
            int difficulty = comboBoxDifficulty.SelectedIndex;
            int type = comboBoxType.SelectedIndex;
           

            int privateTest = 0;
            if (privateRadio.IsChecked == true)
            {
                privateTest = 1;
            }
            
            int amountOfWords = 0;
            foreach(TextBox t in textBoxes)
            {

                string[] words = t.Text.Split();
                foreach(string word in words)
                {
                    if (!word.Equals(""))
                    {
                        amountOfWords++;
                    }
                }
                
                textBoxValues.Add(t.Text);
            }
            // Error?
            Database.AddTest(title, type, difficulty, privateTest, amountOfWords, textBoxValues, 0);

            

            

        }




        private bool textFieldCheck()
        {
            bool textEmpty = false;

            foreach (TextBox t in textBoxes)
            {
                // Beter .Trim() == "" gebruiken, omdat dit ook werkt voor meerdere spaties/tabs
                if (t.Text.Equals(""))
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
                MessageBox.Show("De toets bevat geen regels", "Voeg een regel toe");
            }
            else if (textEmpty)
            {
                MessageBox.Show("Vul alle toetsregels", "Er is iets fout gegaan");
                
            }
            else
            {
                MessageBox.Show("De toets heeft geen titel", "Er is iets fout gegaan");
                
            }
            return false;
        }
    }
}
