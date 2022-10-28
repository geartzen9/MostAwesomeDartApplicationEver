using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.Views;

namespace MostAwesomeDartApplicationEver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DartDbContext _context;

        /// <summary>
        /// Awful hack to avoid requiring DI and similar mess.
        /// </summary>
        internal static DartDbContext? Context { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWin = new MainWindow();

            Context = _context = new DartDbContext();
            _context.Database.EnsureCreated();

            ViewModels.DarterViewModel darterViewModel = new(_context);
            mainWin.DataContext = darterViewModel;
            mainWin.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _context.Dispose();
            Context = null;
        }
    }
}
