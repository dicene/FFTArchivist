using FFTArchivist.Models;
using FFTArchivist.Models.Base;
using FFTArchivist.ViewModels.DataItems;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages
{
    internal class ItemPageViewModel : BaseDataPageViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemPageViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameDataItemViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceDataItemViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
            }
        }

        public int Id { get => item.Id; set => item.Id = value; }

        private TextBoxStringDataItemViewModel nameDataItemViewModel = new();
        public TextBoxStringDataItemViewModel NameDataItemViewModel
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

        private TextBoxIntDataItemViewModel priceDataItemViewModel = new();
        public TextBoxIntDataItemViewModel PriceDataItemViewModel
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

        public string Name
        {
            get => item.Name?.Value ?? "";
            set
            {
                if (item.Name == null)
                {
                    return;
                }

                item.Name.Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Description
        {
            get => item.Description?.Value?.Replace("<br>", "\n") ?? "";
            set
            {
                if (item.Description.Value == value.Replace("\n", "<br>"))
                {
                    return;
                }

                item.Description.Value = value.Replace("\n", "<br>");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public ushort Price
        {
            get => item.Price?.Value ?? 0;
            set
            {
                if (item.Price.Value == value)
                {
                    return;
                }

                item.Price.Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
            }
        }

        public ItemPageViewModel()
        {
            item = new Item();
            NameDataItemViewModel.Label = "Name";
            PriceDataItemViewModel.Label = "Price";
        }

        public ItemPageViewModel(Item item)
        {
            this.item = item;
        }

        public void ChangeIndex(int index)
        {
            Item = App.DataManager.GetDataList<Item>()[index];
        }
    }
}
