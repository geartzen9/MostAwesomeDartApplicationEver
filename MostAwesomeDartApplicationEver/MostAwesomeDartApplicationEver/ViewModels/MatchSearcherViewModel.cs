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
    internal partial class MatchSearcherViewModel : ViewModel
    {
        [ObservableProperty]
        private string _playerFirstNameInput = "";

        [ObservableProperty]
        private string _playerLastNameInput = "";

        public ObservableCollection<Models.Match> SearchResults { get; set; } = new();

        [ObservableProperty]
        private DateTime? _matchDate = null;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(NavigateToResultsCommand))]
        private Models.Match? _selectedMatch;

        [RelayCommand(CanExecute = nameof(CanNavigateToResults))]
        private void NavigateToResults(Window win)
        {
            win.Content = Results.NewForSearchedMatch();
        }

        private bool CanNavigateToResults(Window _)
        {
            return _selectedMatch != null;
        }

        [RelayCommand]
        private void Search()
        {
            IQueryable<Models.Match> queryable = Context.Matches.AsQueryable();

            if (string.IsNullOrWhiteSpace(PlayerFirstNameInput))
            {
                queryable = queryable.Where(m => m.Darters.Any(d => d.FirstName == PlayerFirstNameInput));
            }

            if (string.IsNullOrWhiteSpace(PlayerLastNameInput))
            {
                queryable = queryable.Where(m => m.Darters.Any(d => d.FirstName == PlayerLastNameInput));
            }

            if (MatchDate.HasValue)
            {
                queryable = queryable.Where(m => m.ScheduledDateTime.Date == MatchDate.Value.Date);
            }

            SearchResults = new ObservableCollection<Models.Match>(queryable);
        }
    }
}
