using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LerenTypen.Models
{
   public class Tips
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
    }
}
