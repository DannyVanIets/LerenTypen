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
            var kijk = e.Key.ToString();
            List<string> allNames = new List<string>();
            List<string> allTotalNames = new List<string>();
            List<UIElementCollection> allPanels = new List<UIElementCollection>();
            allPanels.Add(TipPage_StackPanel_Keyboard_Row0.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row1.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row2.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row3.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row4.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row5.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row6.Children);
            foreach (UIElementCollection uIElementCollection in allPanels)
            {
                    allNames = Models.TipsController.KeyDistributor(uIElementCollection);
                    foreach (var item in allNames)
                    {
                        allTotalNames.Add(item);
                    }
            }
            //allNames = Models.TipsController.KeyDistributor(TipPage_StackPanel_Keyboard_Row2.Children);
            string check = Models.TipsController.ShowKey(e, allTotalNames);
           // MessageBox.Show(check);
            string fullName = "TipPage_Canvas_Key_" + check;
            //MessageBox.Show(k);
            foreach (UIElementCollection uIElementCollection in allPanels)
            {
                Models.TipsController.FindPressedKey(uIElementCollection, fullName);
            }

            //MessageBox.Show(test);
            //MessageBox.Show(TipPage_Canvas_Key_44.Name.Substring(19));
        }


    }
}
