using System.Collections.Generic;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private List<Test> trendingTests = new List<Test>()
        {
            new Test() { Name = "Een heleboel woorden", Author = "Slimpie" },
            new Test() { Name = "Nog meer woorden", Author = "Slimpie2" },
            new Test() { Name = "Zinnen", Author = "Slimpie3" },
        };

        public HomePage()
        {
            InitializeComponent();
            trendingTestsListView.ItemsSource = trendingTests;
        }
    }

    class Test
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public override string ToString()
        {
            return $"{Name} door {Author}";
        }
    }
}
