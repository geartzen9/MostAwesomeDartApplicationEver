using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DartScore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DartScore.ViewModels
{   
    [INotifyPropertyChanged]
    internal partial class DarterViewModel : ViewModel<Darter>
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteDarterCommand))]
        private Darter? selectedItem;

        public DarterViewModel(DartDBContext context) : base(context)
        {
            _context.Darters.Load();
            Data = _context.Darters.Local.ToObservableCollection();
        }
        
        [RelayCommand]
        private async Task AddUpdateDarterAsync(Darter darter)
        {
            if (darter.Id is null)
            {
                _context.Darters.Add(darter);
            }
            else
            {
                _context.Darters.Update(darter);
            }
            
            await _context.SaveChangesAsync();
        }

        [RelayCommand(CanExecute = nameof(CanDeleteDarter))]
        private async Task DeleteDarterAsync(Darter darter)
        {
            _context.Darters.Remove(darter);
            await _context.SaveChangesAsync();
        }

        private bool CanDeleteDarter(Darter? darter)
        {
            return darter is not null;
        }
    }
}
