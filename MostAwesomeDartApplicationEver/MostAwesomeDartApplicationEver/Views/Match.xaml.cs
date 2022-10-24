using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Match : Page
    {
        public Match()
        {
            InitializeComponent();
        }

        private void Test_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(Results.NewForFinishedMatch());
    }
}
