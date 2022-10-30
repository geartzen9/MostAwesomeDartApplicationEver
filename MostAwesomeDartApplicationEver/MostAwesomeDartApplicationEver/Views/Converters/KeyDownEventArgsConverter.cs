using MostAwesomeDartApplicationEver.Models;
using MostAwesomeDartApplicationEver.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MostAwesomeDartApplicationEver.Views.Converters
{
    internal class KeyDownEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var keyDownEventArgs = value as KeyEventArgs;

            if (keyDownEventArgs is null)
            {
                throw new ArgumentException("Expected value to be of type KeyEventArgs", nameof(value));
            }

            switch(keyDownEventArgs.Key)
            {
                case Key.T:
                    return (true, HitArea.Triple);
                case Key.D:
                    return (true, HitArea.Double);
                case Key.B:
                    return (true, HitArea.Bullseye);
                case Key.N:
                    return (true, HitArea.None);
                case Key.S:
                    return (true, HitArea.Single);
            }

            return (false, HitArea.None);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
