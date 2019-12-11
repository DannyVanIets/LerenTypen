using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TipPage : Page
    {

        private MainWindow MainWindow;
        public TipPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;

        }

        private void LoginRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new LoginPage(MainWindow));
        }

        private void KeyUp_Clicker(object sender, System.Windows.Input.KeyEventArgs e)
        {
            List<string> allNames = new List<string>();
            foreach (var item in TipPage_StackPanel_Keyboard_Row2.Children)
            {
                List<System.Windows.Shapes.Rectangle> allVars = new List<System.Windows.Shapes.Rectangle>();
                allVars.Add((System.Windows.Shapes.Rectangle)item);
                foreach (var i in allVars)
                {
                    allNames.Add(i.Name.Substring(19));

                }
            }
            string check = Models.Tips.ShowKey(e, allNames);
           // MessageBox.Show(check);
            var k = "TipPage_Canvas_Key_" + check;
            //MessageBox.Show(k);
            foreach (var item in TipPage_StackPanel_Keyboard_Row2.Children)
            {
                List<System.Windows.Shapes.Rectangle> allVars = new List<System.Windows.Shapes.Rectangle>();
                allVars.Add((System.Windows.Shapes.Rectangle)item);
                foreach (var i in allVars)
                {
                    //MessageBox.Show("i is" + i);
                    if (i.Name.Equals(k))
                    {
                        //MessageBox.Show("same");
                        i.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        i.Visibility = Visibility.Visible;
                    }

                }
            }
            //MessageBox.Show(test);
            //MessageBox.Show(TipPage_Canvas_Key_44.Name.Substring(19));
        }
    }
}
