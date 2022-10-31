using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class StartViewModel
    {
        private readonly DartDbContext _dbContext;

        internal StartViewModel()
        {
            _dbContext = DartDbContext.Context;
        }

        [RelayCommand]
        private void NavigateToMatchSearcher(Window win)
        {
            win.Content = new MatchSearcher();
        }

        [RelayCommand]
        private void NavigateToMatchLauncer(Window win)
        {
            win.Content = new MatchLauncher();
        }

        [RelayCommand]
        private void Export() => JsonExporter.Instance.SerializeAndSave(_dbContext.Matches);
    }
}
