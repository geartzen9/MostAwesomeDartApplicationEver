using System.ComponentModel;
using System.Runtime.CompilerServices;
using MostAwesomeDartApplicationEver.Models;

namespace MostAwesomeDartApplicationEver.ViewModels
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        private string _backButtonText = "This is a bug. 💣 (Unless viewed in the WPF preview)";
        
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies that the <paramref name="propertyName"/> property was changed.
        /// </summary>
        /// <param name="propertyName">Name of the property. Omit to have the compiler provide this, pass <code>null</code> to refer to all.</param>
        private void NotifyPropertyChanged([CallerMemberName]string? propertyName = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? ""));
        
        public Darter Player1 { get; set; } = new();
        public Darter Player2 { get; set; } = new();
        
        public string Player1Results { get; set; } = "Dummy data.";
        public string Player2Results { get; set; } = "Dummy data.";

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
