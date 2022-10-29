using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace MostAwesomeDartApplicationEver.Views.Converters
{
    /// <summary>
    /// Interaction logic for DarterOverview.xaml
    /// </summary>
    public partial class DarterOverview : Page
    {
        private DarterViewModel ViewModel => (DarterViewModel)this.DataContext;

        public DarterOverview()
        {
            InitializeComponent();

            List<Darter> darters = new List<Darter>();
            darters.Add(new Darter("Gerrit", "de Heij"));
            DarterOverviewDataGrid.ItemsSource = darters;

            //DarterOverviewDataGrid.ItemsSource = ViewModel.Data;
        }

        private void DarterRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DarterOverviewDataGrid == null || DarterOverviewDataGrid.SelectedItem == null)
            {
                return;
            }

            Darter SelectedDarter = DarterOverviewDataGrid.SelectedItem as Darter ?? throw new ArgumentException("SelectedDarter should not be null...");
            NavigationService!.Navigate(new DarterDetails(SelectedDarter, this));
        }
    }
}
