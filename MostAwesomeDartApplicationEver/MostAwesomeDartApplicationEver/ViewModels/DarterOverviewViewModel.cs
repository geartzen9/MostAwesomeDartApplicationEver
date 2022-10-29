using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MostAwesomeDartApplicationEver.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MostAwesomeDartApplicationEver.ViewModels
{   
    [INotifyPropertyChanged]
    internal partial class DarterOverviewViewModel : ViewModel<Darter>
    {
        private string _backButtonText = "This is a bug. 💣 (Unless viewed in the WPF preview)";

        /// <summary>
        /// Notifies that the <paramref name="propertyName"/> property was changed.
        /// </summary>
        /// <param name="propertyName">Name of the property. Omit to have the compiler provide this, pass <code>null</code> to refer to all.</param>
        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? ""));

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteDarterCommand))]
        private Darter? _selectedItem;

        public DarterOverviewViewModel()
        {
            Context.Darters.Load();
            Data = Context.Darters.Local.ToObservableCollection();
        }
        
        [RelayCommand]
        private async Task AddUpdateDarterAsync(Darter darter)
        {
            if (darter.Id is null)
            {
                Context.Darters.Add(darter);
            }
            else
            {
                Context.Darters.Update(darter);
            }
            
            await Context.SaveChangesAsync();
        }

        [RelayCommand(CanExecute = nameof(CanDeleteDarter))]
        private async Task DeleteDarterAsync(Darter darter)
        {
            Context.Darters.Remove(darter);
            await Context.SaveChangesAsync();
        }

        private bool CanDeleteDarter(Darter? darter)
        {
            return darter is not null;
        }

        public string BackButtonText
        {
            get => _backButtonText;
            set
            {
                _backButtonText = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
