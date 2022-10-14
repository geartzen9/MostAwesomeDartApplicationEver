using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MostAwesomeDartApplicationEver.Models;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    internal class ViewModel<T>
    {
        public ObservableCollection<T> Data { get; protected set; }
        protected readonly DartDBContext _context;

        public ViewModel(DartDBContext context)
        {
            Data = new ObservableCollection<T>();
            _context = context;
        }
    }
}
