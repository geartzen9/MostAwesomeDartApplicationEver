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
        private string _playerNameInput = "";

        public string PlayerNameInput
        { 
            get => _playerNameInput;
            set
            {
                _playerNameInput = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Views.Match> _searchResults = new();

        public ObservableCollection<Views.Match> SearchResults 
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime? _matchDate = null;

        public DateTime? MatchDate
        {
            get => _matchDate;
            set
            {
                _matchDate = value;
                NotifyPropertyChanged();
            }
        }

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

                    if (string.IsNullOrWhiteSpace(_playerNameInput))
                    {
                        isMatch = isMatch && match._darter.Contains(_playerNameInput);
                    }

                    if (_matchDate.HasValue)
                    {
                        isMatch = isMatch && (match._scheduledDateTime.Date == _matchDate.Value.Date);
                    }

                    return isMatch;
                }
            );
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
