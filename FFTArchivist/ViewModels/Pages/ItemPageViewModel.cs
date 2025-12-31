using FFTArchivist.Models;
using FFTArchivist.ViewModels.DataItems;
using System.ComponentModel;
using System.Diagnostics;

namespace FFTArchivist.ViewModels.Pages
{
    internal class ItemPageViewModel : BaseDataPageViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get => item.Id; }

        private Item item;
        public Item Item
        {
            get
            {
                return item;
            }

            set
            {
                if (item == value)
                {
                    return;
                }

                item = value;
                nameDataItemViewModel.DataItem = value.Name;
                priceDataItemViewModel.DataItem = value.Price;
                descriptionDataItemViewModel.DataItem = value.Description;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemPageViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameDataItemViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceDataItemViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionDataItemViewModel)));
            }
        }

        private StringDataItemViewModel nameDataItemViewModel = new();
        public StringDataItemViewModel NameDataItemViewModel
        {
            get => nameDataItemViewModel;
            set
            {
                if (nameDataItemViewModel == value)
                {
                    return;
                }

                nameDataItemViewModel = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameDataItemViewModel)));
            }
        }

        //public ushortDataItemViewModel PriceDataItemViewModel => Price;
        private ushortDataItemViewModel priceDataItemViewModel = new();
        public ushortDataItemViewModel PriceDataItemViewModel
        {
            get => priceDataItemViewModel;
            set
            {
                if (priceDataItemViewModel == value)
                {
                    return;
                }

                priceDataItemViewModel = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceDataItemViewModel)));
            }
        }

        private StringDataItemViewModel descriptionDataItemViewModel = new();
        public StringDataItemViewModel DescriptionDataItemViewModel
        {
            get => descriptionDataItemViewModel;
            set
            {
                if (descriptionDataItemViewModel == value)
                {
                    return;
                }

                descriptionDataItemViewModel = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionDataItemViewModel)));
            }
        }

        public ItemPageViewModel()
        {
            Debug.WriteLine($"Constructing new {nameof(ItemPageViewModel)}");
            item = new Item();
            //priceDataItemViewModel.CreatedBy = "ItemPageViewModel";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameDataItemViewModel)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceDataItemViewModel)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionDataItemViewModel)));
        }

        public ItemPageViewModel(Item item)
        {
            Debug.WriteLine($"Constructing new ItemPageViewModel");
            this.item = item;
        }

        public void ChangeIndex(int index)
        {
            if (index < 0 || index >= App.DataManager.GetDataList<Item>().Count)
            {
                Item = new Item();
                return;
            }

            Item = App.DataManager.GetDataList<Item>()[index];
        }
    }
}
