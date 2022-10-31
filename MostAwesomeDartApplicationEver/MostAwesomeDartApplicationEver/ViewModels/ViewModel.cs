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
    internal class ViewModel
    {
        protected readonly DartDbContext Context;

        public ViewModel()
        {
            Context = DartDbContext.Context;
        }
    }
}
