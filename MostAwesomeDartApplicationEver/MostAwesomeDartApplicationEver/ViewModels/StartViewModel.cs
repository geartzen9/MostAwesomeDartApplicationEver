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
    internal partial class StartViewModel
    {
        [RelayCommand]
        private void NavigateToMatchSearcher(Window win)
        {
            win.Content = new MatchSearcher();
        }

        [RelayCommand]
        private void NavigateToMatchLauncer(Window win)
        {
            win.Content = new MatchLauncher();
        }

        [RelayCommand]
        private void Export()
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            SaveFileDialog saveFileDialog = new()
            {
                DefaultExt = "json",
                Filter = "Dart wedstrijden (*.json)|*.json",
                FileName = System.IO.Path.Combine(myDocuments, "dartsdata.json"),
                InitialDirectory = myDocuments,
                OverwritePrompt = true,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }

            // TODO: Export data here

            File.WriteAllText(saveFileDialog.FileName, @"{""Dummy"":true}");
        }
    }
}
