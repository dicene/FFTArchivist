using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace FFTArchivist.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Page? currentPage;
        public Page? CurrentPage
        {
            get => currentPage; set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
                }
            }
        }

        private ObservableCollection<string> itemList = new();
        public ObservableCollection<string> ItemList
        {
            get => itemList;
            set
            {
                if (itemList != value)
                {
                    itemList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                }
            }
        }

        public MainWindowViewModel()
        {
            Debug.WriteLine($"Creating MainWindowViewModel");
        }
    }
}
