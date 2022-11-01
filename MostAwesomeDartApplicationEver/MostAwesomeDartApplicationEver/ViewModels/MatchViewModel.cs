using Accessibility;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
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
        private String _winner = "";

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

        [ObservableProperty]
        private string _player1RoundScore = "";
        [ObservableProperty]
        private string _player1LegScore = "";
        [ObservableProperty]
        private string _player1SetScore = "";
        [ObservableProperty]
        private string _player2RoundScore = "";
        [ObservableProperty]
        private string _player2LegScore = "";
        [ObservableProperty]
        private string _player2SetScore = "";


        public ObservableCollection<Throw> Player1Throws { get; set; } = new();
        public ObservableCollection<Throw> Player2Throws { get; set; } = new();

        private ObservableCollection<Throw> DbThrows;
        private ObservableCollection<Round> DbRounds;
        private ObservableCollection<Leg> DbLegs;
        private ObservableCollection<Set> DbSets;
        private ObservableCollection<Models.Match> DbMatches;

        public MatchViewModel() : base()
        {
            Context.Throws.Load();
            Context.Rounds.Load();
            Context.Legs.Load();
            Context.Sets.Load();
            Context.Matches.Load();

            DbThrows = Context.Throws.Local.ToObservableCollection();
            DbRounds = Context.Rounds.Local.ToObservableCollection();
            DbLegs = Context.Legs.Local.ToObservableCollection();
            DbSets = Context.Sets.Local.ToObservableCollection();
            DbMatches = Context.Matches.Local.ToObservableCollection();

            PrepareMatch();
        }


        private void PrepareMatch()
        {
            var match = new Models.Match()
            {
                ScheduledDateTime = _scheduledDateTime,
                Darters = new[]
                {
                    new Darter(),
                    new Darter()
                }
            };

            //DbMatches.Add(match);
            //Context.SaveChanges();

            for (int i = 0; i < 2; i++)
            {
                Set set = new Set()
                {
                    
                    Match = match,
                    Name = "Set 1",
                    Darter = match.Darters.ToArray()[i]
                };

                _sets.Add(set);
                _currentSets[i] = set;

                //DbSets.Add(set);
                //Context.SaveChanges();
            }

            for (int i = 0; i < 2; i++)
            {
                Leg leg = new Leg()
                {
                    
                    Name = "Leg 1",
                    Set = _sets[i]
                };
                _legs.Add(leg);
                _currentLegs[i] = leg;

                //DbLegs.Add(leg);
                //Context.SaveChanges();
            }

            for (int i = 0; i < 2; i++)
            {
                Round round = new Round()
                {
                   
                    Name = "Round 1",
                    Leg = _legs[i]
                };

                _rounds.Add(round);
                _currentRounds[i] = round;

                //DbRounds.Add(round);
                //Context.SaveChanges();
            }
        }

        private void AdvanceMatch()
        {
            bool newLegs = false;
            bool newSets = false;

            //draw
            if (_currentLegs[0].Score == 501 && Player1Throws.Last().Hit.HitArea == HitArea.Double &&
            _currentLegs[1].Score == 501 && Player2Throws.Last().Hit.HitArea == HitArea.Double)
            {
                var emptyDarter = new Darter();
                _currentLegs[0].Winner = emptyDarter;
                _currentLegs[1].Winner = emptyDarter;
            }
            //Player 1 wins leg
            else if (_currentLegs[0].Score == 501 && _currentLegs[1].Score != 501)
            {
                _currentLegs[0].Winner = _currentRounds[0].Darter;
                _currentLegs[1].Winner = _currentRounds[0].Darter;
                _currentSets[0].Score += 1;

                Player1SetScore = _currentSets[0].Score.ToString();
                newLegs = true;
            }
            //Player 2 wins leg
            else if (_currentLegs[1].Score == 501 && _currentLegs[0].Score != 501)
            {
                _currentLegs[0].Winner = _currentRounds[1].Darter;
                _currentLegs[1].Winner = _currentRounds[1].Darter;
                _currentSets[1].Score += 1;

                Player2SetScore = _currentSets[1].Score.ToString();
                newLegs = true;
            }

            //Legs draw, therefore make more legs
            if (_currentSets[0].Score == 3 && _currentSets[1].Score == 3)
            {
                newLegs = true;
            }
            //Player 1 wins set
            else if (_currentSets[0].Score == 3)
            {
                _currentSets[0].Winner = _currentRounds[0].Darter;
                _currentSets[1].Winner = _currentRounds[0].Darter;
                newSets = true;
            }
            //Player 2 wins set
            else if (_currentSets[1].Score == 3)
            {
                _currentSets[0].Winner = _currentRounds[1].Darter;
                _currentSets[1].Winner = _currentRounds[1].Darter;
                newSets = true;
            }

            //All the sets are played, player with the most sets won is the winner of the match
            if (NumberOfSets == _sets.ToList().Count / 2 && _legs.Where((Leg l) => (l.Set == _currentSets[0] || l.Set == _currentSets[1]) && l.Set.Winner is not null).Count() / 2 == 3)
            {
                if (_sets.Where((Set s) => s.Winner.FirstName == _currentSets[0].Darter.FirstName).Count() > _sets.Where((Set s) => s.Winner.FirstName == _currentSets[1].Darter.FirstName).Count())
                {
                    Winner = _currentRounds[0].Darter.FirstName;
                }
                else
                {
                    Winner = _currentRounds[1].Darter.FirstName;
                }
            }

            if (newSets) MakeMatchComponents(typeof(Set), false);
            if (newLegs) MakeMatchComponents(typeof(Leg), newSets);
            MakeMatchComponents(typeof(Round), newLegs);
        }
        private void MakeMatchComponents(Type t, bool reset)
        {
            if (t == typeof(Round))
            {
                int roundCount = _rounds.Where((Round r) => r.Leg == _currentLegs[0] || r.Leg == _currentLegs[1]).Count() / 2;
                for (int i = 0; i < 2; i++)
                {
                    var nextRound = new Round()
                    {
                        Id = _currentRounds[i].Id + 2,
                        Leg = _currentLegs[i],
                        Name = reset ? "Round 1" : "Round " + (roundCount + 1)
                    };
                    _rounds.Add(nextRound);
                    _currentRounds[i] = nextRound;
                }
            }
            else if (t == typeof(Leg))
            {
                int legCount = _legs.Where((Leg l) => l.Set == _currentSets[0] || l.Set == _currentSets[1]).Count() / 2;
                for (int i = 0; i < 2; i++)
                {
                    var nextLeg = new Leg()
                    {
                        Id = _currentLegs[i].Id + 2,
                        Set = _currentSets[i],
                        Name = reset ? "Leg 1" : "Leg " + (legCount + 1)
                    };
                    _legs.Add(nextLeg);
                    _currentLegs[i] = nextLeg;
                }
            }
            else if (t == typeof(Set))
            {
                for (int i = 0; i < 2; i++)
                {
                    int setCount = _sets.Count / 2;
                    var nextSet = new Set()
                    {
                        Id = _currentSets[i].Id + 2,
                        Match = _sets[0].Match,
                        Name = "Set " + (setCount + 1),
                        Darter = _currentRounds[i].Darter
                    };
                    _sets.Add(nextSet);
                    _currentSets[i] = nextSet;
                }
            }
        }

        partial void OnPlayer1TextChanged(string value)
        {
            _currentSets[0].Darter.FirstName = value;       
        }

        partial void OnPlayer2TextChanged(string value)
        {
            _currentSets[1].Darter.FirstName = value;
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
                currentThrow.Hit = new Hit()
                {
                    HitArea = _scoreType,
                    Score = Int32.Parse(_searchText)
                };

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
                    currentThrow.Score = CalculateThrowScore(currentThrow);

                    if (_currentLegs[0].Score + currentThrow.Score > 501 || (_currentLegs[0].Score + currentThrow.Score == 501 && currentThrow.Hit.HitArea != HitArea.Double))
                    {
                        dartNumber--;
                    }
                    else
                    {
                        Player1Throws.Add(currentThrow);
                        _currentRounds[0].Score += currentThrow.Score;
                        _currentLegs[0].Score += currentThrow.Score;

                        Player1RoundScore = _currentRounds[0].Score.ToString();
                        Player1LegScore = _currentLegs[0].Score.ToString();
                    }

                    if (_currentLegs[0].Score == 501 && currentThrow.Hit.HitArea == HitArea.Double)
                    {
                        dartNumber = 2;
                    }
                }
                //player 2 turn
                else
                {
                    PlayingText = "Currently Playing: " + Player2Text;
                    currentThrow.Round = _currentRounds[1];
                    currentThrow.Score = CalculateThrowScore(currentThrow);

                    if (_currentLegs[1].Score + currentThrow.Score > 501 || (_currentLegs[1].Score + currentThrow.Score == 501 && currentThrow.Hit.HitArea != HitArea.Double))
                    {
                        dartNumber--;
                    }
                    else
                    {
                        Player2Throws.Add(currentThrow);
                        _currentRounds[1].Score += currentThrow.Score;
                        _currentLegs[1].Score += currentThrow.Score;

                        Player2RoundScore = _currentRounds[1].Score.ToString();
                        Player2LegScore = _currentLegs[1].Score.ToString();
                    }

                    if (_currentLegs[1].Score == 501 && currentThrow.Hit.HitArea == HitArea.Double)
                    {
                        dartNumber = 2;
                    }
                }

                SearchText = string.Empty;
                ScoreTypeString = "";
                dartNumber++;
            }
        }

        private int CalculateThrowScore(Throw t)
        {
            if (t.Hit.HitArea == HitArea.Single) return t.Hit.Score;
            if (t.Hit.HitArea == HitArea.Double) return t.Hit.Score * 2;
            if (t.Hit.HitArea == HitArea.Triple) return t.Hit.Score * 3;
            if (t.Hit.HitArea == HitArea.Bullseye) return 50;
            else return 0;
        }
    }
}

