using MostAwesomeDartApplicationEver.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Match : Page
    {
        private MatchViewModel ViewModel => (MatchViewModel)this.DataContext;
        public Match(string player1Name, string player2Name)
        {
            InitializeComponent();
            ViewModel.Player1Text = player1Name;
            ViewModel.Player2Text = player2Name;
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
