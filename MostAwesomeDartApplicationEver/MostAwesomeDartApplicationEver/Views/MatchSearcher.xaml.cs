using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class MatchSearcher : Page
    {
        public MatchSearcher()
        {
            InitializeComponent();
        }

        private void Test_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(Results.NewForSearchedMatch());
    }
}
