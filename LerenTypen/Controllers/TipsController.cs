using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LerenTypen.Models
{
   public class TipsController
    {

        public static string ShowKey(KeyEventArgs e, List<string> names)
        {
            string result = "no outcome";

            //e.Key == System.Windows.Input.Key.D;
            foreach (string name in names)
            {
                var checkKey = e.Key;

                name.ToUpper();
                if (e.Key.ToString().Equals(name))
                {
                    result = name;
                }
            }
            return result;
        }

        public static List<string> KeyDistributor(UIElementCollection names)
        {
            List<string> allNamesInStackPanel = new List<string>();
            foreach (var item in names)
            {
                    List<System.Windows.Shapes.Rectangle> allVars = new List<System.Windows.Shapes.Rectangle>();
                    allVars.Add((System.Windows.Shapes.Rectangle)item);
                    foreach (var i in allVars)
                    {
                        allNamesInStackPanel.Add(i.Name.Substring(19));
                    }
            }
            return allNamesInStackPanel;
        }

        public static void FindPressedKey(UIElementCollection allTotalnames, string fullName)
        {
            foreach (var item in allTotalnames)
            {
                    List<System.Windows.Shapes.Rectangle> allVars = new List<System.Windows.Shapes.Rectangle>();
                    allVars.Add((System.Windows.Shapes.Rectangle)item);
                    foreach (var i in allVars)
                    {
                        //MessageBox.Show("i is" + i);
                        if (i.Name.Equals(fullName))
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
        }
    }
}
