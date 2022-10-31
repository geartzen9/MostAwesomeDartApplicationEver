using System;
using System.Windows;
using System.Windows.Controls;
using MostAwesomeDartApplicationEver.ViewModels;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Results : Page
    {
        private ResultsViewModel ViewModel => (ResultsViewModel)this.DataContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backButtonText">Display text for the back button</param>
        /// <param name="backButtonTarget">Uri to the target page to display</param>
        public Results(string backButtonText, string backButtonTarget)
        {            
            InitializeComponent();
            
            ViewModel.BackButtonText = backButtonText;
            ViewModel.BackButtonTarget = backButtonTarget;
        }

        public static Results NewForFinishedMatch() => new("Terug naar het beginscherm", "Start");

        public static Results NewForSearchedMatch() => new("Terug naar het vorige scherm", "MatchSearcher");
    }
}
