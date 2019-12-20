using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LerenTypen.Models
{
    public class TipsController
    {
        /// <summary>
        /// Couples the right rectangle to the right pressed key
        /// </summary>
        /// <param name="e"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string ShowKey(KeyEventArgs e, List<string> names)
        {
            string result = "";

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

        /// <summary>
        /// We take the names of all the rectangles and find the difference bewteen them by substringing them till the variable names are different
        /// Then we add all the variable names to one list and return this list
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static List<string> SubstringRectangleVariables(UIElementCollection names)
        {
            List<string> allNamesInStackPanel = new List<string>();
            foreach (var item in names)
            {
                List<Rectangle> allVars = new List<Rectangle>();
                allVars.Add((Rectangle)item);
                foreach (var i in allVars)
                {
                    allNamesInStackPanel.Add(i.Name.Substring(19));
                }
            }
            return allNamesInStackPanel;
        }

        /// <summary>
        /// This function makes a rectangle hidden if its key is pressed. It also returns the tag of the Rectangle so we can use it to
        /// match it to the right finger
        /// </summary>
        /// <param name="allTotalnames"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static Rectangle FindPressedKey(UIElementCollection allTotalnames, string fullName)
        {
            Rectangle result = null;
            List<Rectangle> allVars = new List<Rectangle>();

            foreach (var item in allTotalnames)
            {
                allVars.Add((Rectangle)item);
                foreach (Rectangle i in allVars)
                {
                    if (i.Name.Equals(fullName))
                    {
                        i.Visibility = Visibility.Hidden;
                        result = i;
                    }
                    else
                    {
                        i.Visibility = Visibility.Visible;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// To display the right finger, we match the tag in the variable names of the rectangles to the fingers.
        /// </summary>
        /// <param name="color"></param>
        public static void FindCorrespondingFinger(string color, List<Ellipse> ellipses)
        {
            foreach (Ellipse item in ellipses)
            {
                if (item.Tag.ToString().Equals(color))
                {
                    item.Visibility = Visibility.Hidden;
                }
                else
                {
                    item.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Finds the circle around the finger
        /// </summary>
        /// <param name="circleColor"></param>
        /// <param name="ellipsesForCircles"></param>
        public static void FindCorrespondingCircle(string circleColor, List<Ellipse> ellipsesForCircles)
        {
            foreach (Ellipse item in ellipsesForCircles)
            {
                if (item.Tag.ToString().Equals(circleColor))
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Hidden;
                }
            }
        }
        /// <summary>
        /// Finds the corresponding finger and cirkel from a rectangle
        /// </summary>
        /// <param name="r"></param>
        /// <param name="ellipses"></param>
        /// <param name="ellipsesForCircles"></param>
        public static void SelectKey(Rectangle r, List<Ellipse> ellipses, List<Ellipse> ellipsesForCircles)
        {
            r.Visibility = Visibility.Hidden;
            string fingerTag = r.Tag.ToString();
            string circleTag = fingerTag + "Circle";
            TipsController.FindCorrespondingFinger(fingerTag, ellipses);
            TipsController.FindCorrespondingCircle(circleTag, ellipsesForCircles);
        }
        /// <summary>
        /// Sets the border of a label
        /// </summary>
        /// <param name="l"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void BorderSetter(Label l, Brush color, int thickness)
        {
            l.BorderBrush = color;
            l.BorderThickness = new Thickness(thickness);
        }
    }
}



