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
        private bool p1;
        private int submittedScore;
        private string submittedScoreType;

        Throw currentThrow = new Throw();
        Round currentRound = new Round();
        Leg currentLeg = new Leg();
        Set currentSet = new Set();
        Enum hitArea = new HitArea();

        RoundScore player1RoundScore = new RoundScore();
        LegScore  player1LegScore= new LegScore();
        SetScore  player1SetScore = new SetScore();

        RoundScore player2RoundScore = new RoundScore();
        LegScore player2LegScore = new LegScore();
        SetScore player2SetScore = new SetScore();

        [ObservableProperty]
        private string _searchText = "";
        [ObservableProperty]
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

        public ObservableCollection<MatchScore> Player1MatchScores { get; set; } = new();
        public ObservableCollection<MatchScore> Player2MatchScores { get; set; } = new();
        public ObservableCollection<SetScore> Player1SetScores { get; set; } = new();
        public ObservableCollection<SetScore> Player2SetScores { get; set; } = new();
        public ObservableCollection<LegScore> Player1LegScores { get; set; } = new();
        public ObservableCollection<LegScore> Player2LegScores { get; set; } = new();
        public ObservableCollection<RoundScore> Player1RoundScores { get; set; } = new();
        public ObservableCollection<RoundScore> Player2RoundScores { get; set; } = new();
        public ObservableCollection<Throw> Player1Throws { get; set; } = new();
        public ObservableCollection<Throw> Player2Throws { get; set; } = new();

      
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
                    p1 = !p1;
                     currentRound = new Round();

                    if (p1) 
                    {
                        CalculateRoundScore(1);
                        Player1Throws.Clear();
                    }

                    else
                    {
                        CalculateRoundScore(2);
                        Player2Throws.Clear();
                    }
                       
                }
                //player 1 turn
                if (p1)
                {
                    PlayingText = "Currently Playing: " + Player1Text;
                    Player1Throws.Add(currentThrow);   
                }
                //player 2 turn
                else
                {
                    PlayingText = "Currently Playing: " + Player2Text;
                    Player2Throws.Add(currentThrow);   
                }

                SearchText = string.Empty;

                dartNumber++;
            } 
        }
        private void CalculateRoundScore(int player)
        {
            if (player == 1)
            {
                foreach (Throw _throw in Player1Throws)
                {
                    switch (_throw.Hit.Item1)
                    {
                        case HitArea.Single:
                            player1RoundScore.Score += _throw.Hit.Item2;
                            break;
                        case HitArea.Double:
                            player1RoundScore.Score += _throw.Hit.Item2 * 2;
                            break;
                        case HitArea.Triple:
                            player1RoundScore.Score += _throw.Hit.Item2 * 3;
                            break;
                        case HitArea.Bullseye:
                            player1RoundScore.Score += 50;
                            break;
                        case HitArea.None:
                            player1RoundScore.Score += 0;
                            break;

                    }
                }
                player1LegScore.Score += player1RoundScore.Score;
                player1RoundScore.Score = 0;

            }
            if (player == 2)
            {
                foreach (Throw _throw in Player2Throws)
                {
                    switch (_throw.Hit.Item1)
                    {
                        case HitArea.Single:
                            player2RoundScore.Score += _throw.Hit.Item2;
                            break;
                        case HitArea.Double:
                            player2RoundScore.Score += _throw.Hit.Item2 * 2;
                            break;
                        case HitArea.Triple:
                            player2RoundScore.Score += _throw.Hit.Item2 * 3;
                            break;
                        case HitArea.Bullseye:
                            player2RoundScore.Score += 50;
                            break;
                        case HitArea.None:
                            player2RoundScore.Score += 0;
                            break;
                    }
                }
                player2LegScore.Score += player2RoundScore.Score;
                player2RoundScore.Score = 0;
            }
        }
    }
}

