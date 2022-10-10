using DartScore.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace DartScore.Views.Converters
{
    internal class CellEditEndingEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cellEditEndingEventArgs = value as DataGridCellEditEndingEventArgs;

            if (cellEditEndingEventArgs is null)
            {
                throw new ArgumentException("Expected value to be of type DataGridCellEditEndingEventArgs", nameof(value));
            }
            
            Darter darter = (Darter)cellEditEndingEventArgs.Row.Item;

            if (cellEditEndingEventArgs.Column.Header.ToString() == "First Name")
            {
                darter.FirstName = ((TextBox)cellEditEndingEventArgs.EditingElement).Text;
            }
            else
            {
                darter.LastName = ((TextBox)cellEditEndingEventArgs.EditingElement).Text;
            }

            return darter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
