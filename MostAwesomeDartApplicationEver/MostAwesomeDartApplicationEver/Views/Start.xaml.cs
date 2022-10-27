using MostAwesomeDartApplicationEver.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Start : Page
    {
        private readonly DartDbContext _dbContext;

        internal Start()
        {
            InitializeComponent();
            _dbContext = DartDbContext.Context;
        }

        private void Export_OnClick(object sender, RoutedEventArgs e) => JsonExporter.Instance.SerializeAndSave(_dbContext.Matches);
    }
}
