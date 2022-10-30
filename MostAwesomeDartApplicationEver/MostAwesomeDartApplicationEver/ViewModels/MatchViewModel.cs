using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MostAwesomeDartApplicationEver.Models;
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
        private int submittedScore;
        private string submittedScoreType;

        Throw currentThrow = new Throw();
        Round currentRound = new Round();
        Leg currentLeg = new Leg();
        Set currentSet = new Set();
        Models.Match currentMatch = new Models.Match();
        Enum hitArea = new HitArea();

        //[ObservableProperty]
        private string _searchText = "";
        private string _scoreType = "none";

        [ObservableProperty]
        private string _player1Text = "";
        [ObservableProperty]
        private string _player2Text = "";
        [ObservableProperty]
        private string _playingText = "Currently Playing: no one";
        [ObservableProperty]
        private int _sets;
        [ObservableProperty]
        private DateTime _scheduledDateTime;
        
        

        public ObservableCollection<SetScore> Player1MatchScore { get; set; } = new();
        public ObservableCollection<SetScore> Player2MatchScore { get; set; } = new();
        public ObservableCollection<LegScore> Player1SetScore { get; set; } = new();
        public ObservableCollection<LegScore> Player2SetScore { get; set; } = new();
        public ObservableCollection<RoundScore> Player1LegScore { get; set; } = new();
        public ObservableCollection<RoundScore> Player2LegScore { get; set; } = new();
        public ObservableCollection<Throw> Player1RoundScore { get; set; } = new();
        public ObservableCollection<Throw> Player2RoundScore { get; set; } = new();

        public string SearchText
        {
            get { return _searchText; }
            set 
            {
                _searchText = value; 
              
                OnPropertyChanged(nameof(SearchText)); 
            }
        }
        public string ScoreType
        {
            get { return _scoreType; }
            set
            {
                _scoreType = value;

                OnPropertyChanged(nameof(ScoreType));
            }
        }

        [RelayCommand]
        private void NavigateToResults(Window win)
        {
            win.Content = Results.NewForFinishedMatch();
        }

        [RelayCommand]
        private void EnterKey()
        {
            if (_searchText != "")
            {
                currentThrow = new Throw();
                submittedScore = Int32.Parse(_searchText);
                submittedScoreType = _scoreType;

                //round switch after 3 throws
                if (dartNumber % 3 == 0)
                {
                    p1p2 = !p1p2;
                     currentRound = new Round();
                }
                //player 1 turn
                if (p1p2)
                {
                    PlayingText = "Currently Playing: " + Player1Text;
                    //currentThrow.Hit = (submittedScoreType, submittedScore);
                    Player1RoundScore.Add(currentThrow);   
                }
                //player 2 turn
                else
                {
                    PlayingText = "Currently Playing: " + Player2Text;
                    //currentThrow.Hit = (submittedScoreType, submittedScore);
                    Player2RoundScore.Add(currentThrow);   
                }

                SearchText = string.Empty;

                dartNumber++;
            } 

        }
        private void CalculateWinner()
        {
            foreach(Throw _throw in Player1RoundScore)
            {
                
            }
        }


    }
}

