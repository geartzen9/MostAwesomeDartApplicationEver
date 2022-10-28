using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.ViewModels;
using System;
using System.Collections.Generic;
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

        public Darter SelectedDarter { get; private set; }

        public DarterOverview()
        {
            InitializeComponent();

            // TODO: Delete in production
            List<Darter> darters = new List<Darter>();
            darters.Add(new Darter("Gerrit", "de Heij"));
            DarterOverviewDataGrid.ItemsSource = darters;

            //DarterOverviewDataGrid.ItemsSource = ViewModel.Data;
        }

        private void DarterRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DarterOverviewDataGrid.SelectedItem == null)
            {
                return;
            }

            SelectedDarter = DarterOverviewDataGrid.SelectedItem as Darter;
            
            // TODO: Navigate to the details screen.
        }
    }
}
