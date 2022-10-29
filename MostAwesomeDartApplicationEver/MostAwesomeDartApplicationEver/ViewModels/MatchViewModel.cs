using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MostAwesomeDartApplicationEver.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class MatchViewModel
    {
        private int dartNumber;
        private bool p1p2;
        [ObservableProperty]
        private string _searchText = "";
        [ObservableProperty]
        private string _player1Text = "";
        [ObservableProperty]
        private string _player2Text = "";

        public ObservableCollection<string> Speler1Items { get; set; } = new();
        public ObservableCollection<string> Speler2Items { get; set; } = new();

        [RelayCommand]
        private void NavigateToResults(Window win)
        {
            win.Content = Results.NewForFinishedMatch();
        }

        [RelayCommand]
        private void EnterKey()
        {
            if (dartNumber % 3 == 0) p1p2 = !p1p2;
            if (p1p2) Speler1Items.Add(_searchText);
            else Speler2Items.Add(_searchText);
            dartNumber++;

        }


    }
}

