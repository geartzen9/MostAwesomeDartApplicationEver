using MostAwesomeDartApplicationEver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MostAwesomeDartApplicationEver.ViewModels;
using MostAwesomeDartApplicationEver.Views.Converters;

namespace MostAwesomeDartApplicationEver.Views
{
    /// <summary>
    /// Interaction logic for DarterDetails.xaml
    /// </summary>
    public partial class DarterDetails : Page
    {
        private readonly Darter _darter;
        DarterOverview _backButtonTarget;
        public DarterDetails(Darter darter, DarterOverview backButtonTarget)
        {
            _darter = darter;
            _backButtonTarget = backButtonTarget;
            InitializeComponent();
            SetDarterData();
        }

        private void SetDarterData()
        {
            DarterNameLabel.Content = _darter.FirstName + " " + _darter.LastName;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e) => NavigationService!.Navigate(_backButtonTarget);
    }
}
