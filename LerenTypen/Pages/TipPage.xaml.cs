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

        /// <summary>
        /// Event when a key gets pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewKeyDown_Clicker(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string fingerTag = "";
            string circleTag = "";
            List<string> allNames = new List<string>();
            List<string> allTotalNames = new List<string>();
            List<UIElementCollection> allPanels = new List<UIElementCollection>();

            // Adding all the rectangles of each stackpanel to one list
            allPanels.Add(TipPage_StackPanel_Keyboard_Row0.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row1.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row2.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row3.Children);
            allPanels.Add(TipPage_StackPanel_Keyboard_Row4.Children);

            // Now we add the rectangles' variable names to one list
            foreach (UIElementCollection uIElementCollection in allPanels)
            {
                allNames = Models.TipsController.KeyDistributor(uIElementCollection);
                foreach (var item in allNames)
                {
                    allTotalNames.Add(item);
                }
            }
            // After getting all the variable names, we find the right key 
            string FoundKey = Models.TipsController.ShowKey(e, allTotalNames);

            // Convert the found key back to the full variable name
            string fullName = "TipPage_Canvas_Key_" + FoundKey;


            List<System.Windows.Shapes.Ellipse> ellipses = new List<System.Windows.Shapes.Ellipse>();
            List<System.Windows.Shapes.Ellipse> ellipsesForCircles = new List<System.Windows.Shapes.Ellipse>();

            // Adds all the ellipses from the fingernails to a list.
            foreach (System.Windows.Shapes.Ellipse item in TipPage_Canvas_AllFingers.Children)
            {
                if (item.Tag.ToString().Contains("Circle"))
                {
                    ellipsesForCircles.Add(item);
                }
                else
                {
                    ellipses.Add(item);
                }
            }

            // After getting the right variable, coupled with the pressed key, we can now make the right rectangle hidden
            foreach (UIElementCollection uIElementCollection in allPanels)
            {
                System.Windows.Shapes.Rectangle foundRectangle = Models.TipsController.FindPressedKey(uIElementCollection, fullName);
                if (foundRectangle != null)
                {
                    // This stores the colortag of the pressed key, so we can use it to find the corresponding finger
                    fingerTag = foundRectangle.Tag.ToString();
                }
            }

            circleTag = fingerTag + "Circle";
            // Here we find the corresponding finger to the pressed key
            Models.TipsController.FindCorrespondingFinger(fingerTag, ellipses);
            Models.TipsController.FindCorrespondingCircle(circleTag, ellipsesForCircles);
        }
    }
}
