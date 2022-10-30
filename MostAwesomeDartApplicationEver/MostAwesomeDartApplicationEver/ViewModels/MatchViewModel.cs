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
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class MatchViewModel
    {
        private int dartNumber = 1;
        private bool p1;

        Throw currentThrow = new Throw();
        Round currentRound = new Round();
        Leg currentLeg = new Leg();
        Set currentSet = new Set();
        Enum hitArea = new HitArea();

        private int player1RoundScore;
        private int player1LegScore;
        private int player1SetScore;
    
        private int player2RoundScore;
        private int player2LegScore;
        private int player2SetScore;



        private HitArea _scoreType = HitArea.None;

        [ObservableProperty]
        private string _searchText = "";
        [ObservableProperty]
        private string _scoreTypeString = "";
        [ObservableProperty]
        private string _player1Text = "";
        [ObservableProperty]
        private string _player2Text = "";
        [ObservableProperty]
        private string _playingText = "";
        [ObservableProperty]
        private int _sets;
        [ObservableProperty]
        private DateTime _scheduledDateTime;

        public ObservableCollection<Throw> Player1Throws { get; set; } = new();
        public ObservableCollection<Throw> Player2Throws { get; set; } = new();


        private void PrepareMatch()
        {
            
        }
      
        [RelayCommand]
        private void NavigateToResults(Window win)
        {
            win.Content = Results.NewForFinishedMatch();
        }

        [RelayCommand]
        private void KeyDown(ValueTuple<bool, HitArea> input)
        {
            if (!input.Item1) return;

            _scoreType = input.Item2;
            ScoreTypeString = input.Item2.ToString();

            if (_scoreType == HitArea.Bullseye) SearchText = "50";
        }

        [RelayCommand]
        private void Submit()
        {
            if (_searchText != "")
            {
                currentThrow = new Throw();
                currentThrow.Hit = (_scoreType, Int32.Parse(_searchText));

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
                ScoreTypeString = "";

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
                            player1RoundScore += _throw.Hit.Item2;
                            break;
                        case HitArea.Double:
                            player1RoundScore += _throw.Hit.Item2 * 2;
                            break;
                        case HitArea.Triple:
                            player1RoundScore += _throw.Hit.Item2 * 3;
                            break;
                        case HitArea.Bullseye:
                            player1RoundScore += 50;
                            break;
                        case HitArea.None:
                            player1RoundScore += 0;
                            break;

                    }
                }
                player1LegScore += player1RoundScore;
                player1RoundScore = 0;

            }
            if (player == 2)
            {
                foreach (Throw _throw in Player2Throws)
                {
                    switch (_throw.Hit.Item1)
                    {
                        case HitArea.Single:
                            player2RoundScore += _throw.Hit.Item2;
                            break;
                        case HitArea.Double:
                            player2RoundScore += _throw.Hit.Item2 * 2;
                            break;
                        case HitArea.Triple:
                            player2RoundScore += _throw.Hit.Item2 * 3;
                            break;
                        case HitArea.Bullseye:
                            player2RoundScore += 50;
                            break;
                        case HitArea.None:
                            player2RoundScore += 0;
                            break;

                    }
                }
                player2LegScore += player2RoundScore;
                player2RoundScore = 0;
            }
        }
    }
}

