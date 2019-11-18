using System;
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
        private StackPanel panel;
        private TextBox tb;
        private TextBlock tbl;
        private Thickness margin;
        static int i = 0;
        public CreateTestPage()
        {
            InitializeComponent();
            createInputLine();
            createInputLine();
            createInputLine();




        }
        
        
        private void addLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            createInputLine();
        }
        private void removeLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;

            testLinesPane.Children.Remove(addLineLink);

            foreach (StackPanel p in testLinesPane.Children)
            {
                
                if (p.Name.Equals("Panel"+link.Tag.ToString()))
                {
                    testLinesPane.Children.Remove(p);
                    break;
                }
            }
            testLinesPane.Children.Add(addLineLink);




            
           
        }




        private void createInputLine()
        {
            panel = new StackPanel();
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

            
            panel.Children.Add(tb);
            panel.Children.Add(tbl);

            testLinesPane.Children.Remove(addLineLink);
            testLinesPane.Children.Add(panel);
            testLinesPane.Children.Add(addLineLink);
            i++;
            scrollViewer.ScrollToEnd();
        }

    }
}
