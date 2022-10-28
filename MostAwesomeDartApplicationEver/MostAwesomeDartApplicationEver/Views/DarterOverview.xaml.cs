using MostAwesomeDartApplicationEver.Models;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.Views.Converters
{
    /// <summary>
    /// Interaction logic for DarterOverview.xaml
    /// </summary>
    public partial class DarterOverview : Page
    {
        public Darter SelectedDarter { get; private set; }

        public DarterOverview()
        {
            InitializeComponent();
        }
    }
}
