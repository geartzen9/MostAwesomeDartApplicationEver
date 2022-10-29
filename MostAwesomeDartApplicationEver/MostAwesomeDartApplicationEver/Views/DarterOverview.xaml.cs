using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MostAwesomeDartApplicationEver.Views.Converters
{
    /// <summary>
    /// Interaction logic for DarterOverview.xaml
    /// </summary>
    public partial class DarterOverview : Page
    {
        private DarterOverviewViewModel ViewModel => (DarterOverviewViewModel)this.DataContext;

        private readonly Uri _backButtonTarget;

        public DarterOverview(string backButtonText, Uri backButtonTarget)
        {
            _backButtonTarget = backButtonTarget;

            InitializeComponent();

            ViewModel.BackButtonText = backButtonText;
            DarterOverviewDataGrid.ItemsSource = ViewModel.Data;
        }

        private void Back_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(_backButtonTarget);

        private void DarterRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DarterOverviewDataGrid == null || DarterOverviewDataGrid.SelectedItem == null)
            {
                return;
            }

            Darter SelectedDarter = DarterOverviewDataGrid.SelectedItem as Darter ?? throw new ArgumentException("SelectedDarter should not be null...");
            NavigationService!.Navigate(new DarterDetails(SelectedDarter));
        }
    }
}
