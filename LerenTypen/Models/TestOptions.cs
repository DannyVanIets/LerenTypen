using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Models
{
    //This class is used to remember the testOptions accross multiple tests.
    //In here you can see all the possible options. Automatically they will be put on false.
    public class TestOptions
    {
        public bool Sound { get; set; }

        public TestOptions()
        {
            Sound = false;
        }
    }
}
