using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MostAwesomeDartApplicationEver.Views.Converters;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();
        }

        private void LaunchMatch_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(new Uri("Views/MatchLauncher.xaml", UriKind.Relative));

        private void SearchMatch_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(new Uri("Views/MatchSearcher.xaml", UriKind.Relative));

        private void DarterOverview_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(new DarterOverview("Terug", new Uri("Views/Start.xaml", UriKind.Relative)));

        private void Export_OnClick(object sender, RoutedEventArgs e)
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            SaveFileDialog saveFileDialog = new()
            {
                DefaultExt = "json",
                Filter = "Dart wedstrijden (*.json)|*.json",
                FileName = System.IO.Path.Combine(myDocuments, "dartsdata.json"),
                InitialDirectory = myDocuments,
                OverwritePrompt = true,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            
            // TODO: Export data here
            
            File.WriteAllText(saveFileDialog.FileName,@"{""Dummy"":true}");
        }
    }
}
