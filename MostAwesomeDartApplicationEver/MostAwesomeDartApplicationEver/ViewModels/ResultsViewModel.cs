using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.Views;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    [INotifyPropertyChanged]
    public partial class ResultsViewModel
    {
        public Darter Player1 { get; set; } = new();
        public Darter Player2 { get; set; } = new();
        
        public string Player1Results { get; set; } = "Dummy data.";
        public string Player2Results { get; set; } = "Dummy data.";

        public string? BackButtonTarget;

        [ObservableProperty]
        private string _backButtonText = "This is a bug. 💣 (Unless viewed in the WPF preview)";

        [RelayCommand]
        private void BackButtonNavigate(Window win)
        {
            if (BackButtonTarget == "Start") win.Content = new Start();
            if (BackButtonTarget == "MatchSearcher") win.Content = new MatchSearcher();
        }

        [RelayCommand]
        private void Export() => JsonExporter.Instance.SerializeAndSave(BackedMatch);

        /// <summary>
        /// TODO: Wire other properties to update this.
        /// </summary>
        public Models.Match BackedMatch => new();
    }
}
