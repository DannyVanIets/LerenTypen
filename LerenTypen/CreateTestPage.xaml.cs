using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CreateTestPage : Page
    {
        private TextBox tb;
        private Thickness margin;
        static int i = 4;
        public CreateTestPage()
        {
            InitializeComponent();




        }
        
        
        private void addLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            tb = new TextBox();
            tb.Height = 25;
           
            margin.Left = 50;
            margin.Right = 50;
            margin.Top = 0;
            margin.Bottom = 10;
            tb.Margin = margin;

            tb.Name = "textInputLine" + i.ToString();
            
            testLinesPane.Children.Remove(addLineLink);
            testLinesPane.Children.Add(tb);
            testLinesPane.Children.Add(addLineLink);
            i++;
            scrollViewer.ScrollToEnd();
        }

    }
}
