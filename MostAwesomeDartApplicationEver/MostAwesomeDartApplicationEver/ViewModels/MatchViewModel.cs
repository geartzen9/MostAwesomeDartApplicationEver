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
    internal partial class MatchViewModel : ViewModel
    {
        private int dartNumber = 0;
        private bool p1 = true;

        private List<Set> _sets = new List<Set>();
        private List<Leg> _legs = new List<Leg>();
        private List<Round> _rounds = new List<Round>();

        private HitArea _scoreType = HitArea.None;

        private Round[] _currentRounds = new Round[2];
        private Leg[] _currentLegs = new Leg[2];
        private Set[] _currentSets = new Set[2];

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
        private int _numberOfSets;
        [ObservableProperty]
        private DateTime _scheduledDateTime;

        public ObservableCollection<Throw> Player1Throws { get; set; } = new();
        public ObservableCollection<Throw> Player2Throws { get; set; } = new();

        public MatchViewModel() : base()
        {
            PrepareMatch();
        }

        private void PrepareMatch()
        {
            var match = new Models.Match()
            {
                ScheduledDateTime = _scheduledDateTime,
                Darters = new[]
                {
                    new Darter()
                    {
                        FirstName = _player1Text
                    },
                    new Darter()
                    {
                        FirstName = _player2Text
                    }
                }
            };

            for (int i = 0; i < 2; i++)
            {
                Set set = new Set()
                {
                    Id = i,
                    Match = match,
                    Darter = match.Darters.ToArray()[i]
                };

                _sets.Add(set);
                _currentSets[i] = set;
            }

            for (int i = 0; i < 2; i++)
            {
                Leg leg = new Leg()
                {
                    Id = i,
                    Set = _sets[i]
                };
                _legs.Add(leg);
                _currentLegs[i] = leg;
            }

            for (int i = 0; i < 2; i++)
            {
                Round round = new Round()
                {
                    Id = i,
                    Leg = _legs[i]
                };

                _rounds.Add(round);
                _currentRounds[i] = round;
            }
        }

        private void AdvanceMatch()
        {
            for (int i = 1; i < 3; i++)
            {
                CalculateRoundScore(i);
            }

            if (_currentLegs[0].Score == 501 && Player1Throws.Last().Hit.Item1 == HitArea.Double ||
                _currentLegs[1].Score == 501 && Player2Throws.Last().Hit.Item1 == HitArea.Double)
            {

            }
            else if (_currentLegs[0].Score == 501 && Player1Throws.Last().Hit.Item1 == HitArea.Double &&
                _currentLegs[1].Score == 501 && Player2Throws.Last().Hit.Item1 == HitArea.Double)
            {

            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    var nextRound = new Round()
                    {
                        Id = _currentRounds[i].Id + 2,
                        Leg = _currentLegs[i]
                    };
                    _rounds.Add(nextRound);
                    _currentRounds[i] = nextRound;
                }
            }

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
                var currentThrow = new Throw();
                currentThrow.Hit = (_scoreType, Int32.Parse(_searchText));

                if (dartNumber == 3 && !p1)
                {
                    AdvanceMatch();
                }
                //round switch after 3 throws
                if (dartNumber == 3)
                {
                    p1 = !p1;
                    dartNumber = 0;
                }
                //player 1 turn
                if (p1)
                {
                    PlayingText = "Currently Playing: " + Player1Text;
                    currentThrow.Round = _currentRounds[0];
                    Player1Throws.Add(currentThrow);   
                }
                //player 2 turn
                else
                {
                    PlayingText = "Currently Playing: " + Player2Text;
                    currentThrow.Round = _currentRounds[1];
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
                var p1Round = _currentRounds[0];
                var p1Leg = _currentLegs[0];

                foreach (Throw _throw in Player1Throws.Where((Throw t) => t.Round == p1Round))
                {
                    switch (_throw.Hit.Item1)
                    {
                        case HitArea.Single:
                            p1Round.Score += _throw.Hit.Item2;
                            break;
                        case HitArea.Double:
                            p1Round.Score += _throw.Hit.Item2 * 2;
                            break;
                        case HitArea.Triple:
                            p1Round.Score += _throw.Hit.Item2 * 3;
                            break;
                        case HitArea.Bullseye:
                            p1Round.Score += 50;
                            break;
                        case HitArea.None:
                            p1Round.Score += 0;
                            break;

                    }
                }
                p1Leg.Score += p1Round.Score;

            }
            if (player == 2)
            {
                var p2Round = _currentRounds[1];
                var p2Leg = _currentLegs[1];

                foreach (Throw _throw in Player2Throws.Where((Throw t) => t.Round == p2Round))
                {
                    switch (_throw.Hit.Item1)
                    {
                        case HitArea.Single:
                            p2Round.Score += _throw.Hit.Item2;
                            break;
                        case HitArea.Double:
                            p2Round.Score += _throw.Hit.Item2 * 2;
                            break;
                        case HitArea.Triple:
                            p2Round.Score += _throw.Hit.Item2 * 3;
                            break;
                        case HitArea.Bullseye:
                            p2Round.Score += 50;
                            break;
                        case HitArea.None:
                            p2Round.Score += 0;
                            break;

                    }
                }
                p2Leg.Score += p2Round.Score;
            }
        }
    }
}

