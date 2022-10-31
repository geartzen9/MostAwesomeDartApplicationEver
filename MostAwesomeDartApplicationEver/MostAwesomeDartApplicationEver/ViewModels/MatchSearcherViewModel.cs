using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class MatchSearcherViewModel
    {
        [ObservableProperty]
        private string _playerNameInput = "";

        public ObservableCollection<Views.Match> SearchResults { get; set; } = new();

        [ObservableProperty]
        private DateTime? _matchDate = null;

        private readonly DartDbContext _dbContext;

        internal MatchSearcherViewModel()
        {
            _dbContext = DartDbContext.Context;
        }

        [RelayCommand]
        private void NavigateToResults(Window win)
        {
            win.Content = Results.NewForSearchedMatch();
        }

        [RelayCommand]
        private void Search()
        {
            SearchResults = _dbContext.Matches.AsQueryable().Where(
                match =>
                {
                    bool isMatch = true;

                    if (string.IsNullOrWhiteSpace(PlayerNameInput))
                    {
                        isMatch = isMatch && match._darter.Contains(PlayerNameInput);
                    }

                    if (MatchDate.HasValue)
                    {
                        isMatch = isMatch && (match._scheduledDateTime.Date == MatchDate.Value.Date);
                    }

                    return isMatch;
                }
            );
        }
    }
}
