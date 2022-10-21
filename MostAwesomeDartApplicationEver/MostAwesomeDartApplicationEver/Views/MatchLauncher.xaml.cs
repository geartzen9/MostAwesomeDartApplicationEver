using System;
using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class MatchLauncher : Page
    {
        public MatchLauncher()
        {
            InitializeComponent();
        }

        private void Launch_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(new Uri("Views/Match.xaml", UriKind.Relative));
    }
}
