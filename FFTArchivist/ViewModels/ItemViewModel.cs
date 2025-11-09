using FFTArchivist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Entries
{
    internal class ItemViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private Item item;

        public event PropertyChangedEventHandler? PropertyChanged;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
            }
        }

        public int Id { get => item.Id; set => item.Id = value; }

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
            get => item.Description?.Value.Replace("<br>", "\n") ?? "";
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

        public ItemViewModel()
        {
            this.item = new Item();
        }

        public ItemViewModel(Item item)
        {
            this.item = item;
        }

        public void ChangeIndex(int index)
        {
            Item = App.DataManager.GetDataList<Item>()[index];
        }
    }
}
