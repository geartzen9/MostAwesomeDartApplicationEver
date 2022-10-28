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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DartDbContext.Context.Dispose();
            base.OnExit(e);
        }
    }
}
