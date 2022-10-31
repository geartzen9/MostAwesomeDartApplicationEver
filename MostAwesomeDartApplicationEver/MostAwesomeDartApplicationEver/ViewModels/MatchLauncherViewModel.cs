using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
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
    internal partial class MatchLauncherViewModel
    {
        [RelayCommand]
        private void NavigateToMatch(Window win)
        {
            win.Content = new Match(_player1Name, _player2Name, _sets, _scheduledDateTime);
        }

        [ObservableProperty]
        private string _player1Name = "";
        [ObservableProperty]
        private string _player2Name = "";
        [ObservableProperty]
        private int _sets = 0;
        [ObservableProperty]
        private DateTime _scheduledDateTime = DateTime.UtcNow;
    }
}
