using MostAwesomeDartApplicationEver.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MostAwesomeDartApplicationEver.Models;

namespace MostAwesomeDartApplicationEver.Views
{
    public partial class Match : Page
    {
        Enum hitArea = new HitArea();
        private MatchViewModel ViewModel => (MatchViewModel)this.DataContext;
        public Match(string player1Name, string player2Name, int sets, DateTime scheduledDateTime)
        {
            InitializeComponent();
            ViewModel.Player1Text = player1Name;
            ViewModel.Player2Text = player2Name;
            ViewModel.PlayingText = "Currently Playing: " + player1Name;
            ViewModel.Sets = sets;
            ViewModel.ScheduledDateTime = scheduledDateTime;
        }

        private void InputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
