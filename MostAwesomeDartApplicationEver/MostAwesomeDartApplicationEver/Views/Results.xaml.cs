using System;
using System.Windows;
using System.Windows.Controls;
using MostAwesomeDartApplicationEver.ViewModels;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Results : Page
    {
        private ResultViewModel ViewModel => (ResultViewModel)this.DataContext;
        
        private readonly Uri _backButtonTarget;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backButtonText">Display text for the back button</param>
        /// <param name="backButtonTarget">Uri to the target page to display</param>
        public Results(string backButtonText, Uri backButtonTarget)
        {
            _backButtonTarget = backButtonTarget;
            
            InitializeComponent();
            
            ViewModel.BackButtonText = backButtonText;
        }

        private void Back_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(_backButtonTarget);

        public static Results NewForFinishedMatch() => new("Terug naar het beginscherm", new Uri("Views/Start.xaml", UriKind.Relative));

        public static Results NewForSearchedMatch() => new("Terug naar het vorige scherm", new Uri("Views/MatchSearcher.xaml", UriKind.Relative));
    }
}
