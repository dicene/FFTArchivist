using FFTArchivist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Entries
{
    internal class AbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private Ability ability;

        public event PropertyChangedEventHandler? PropertyChanged;

        //public Item Item
        //{
        //    get
        //    {
        //        return item;
        //    }

        //    set
        //    {
        //        if (item == value)
        //        {
        //            return;
        //        }

        //        item = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
        //    }
        //}

        //public int Id { get => item.Id; set => item.Id = value; }
        //public string Name { get => item.Name.Value; set => item.Name.Value = value; }
        //public string Description { get => item.Description.Value; set => item.Description.Value = value; }
        //public short Price { get => item.Price.Value; set => item.Price.Value = value; }

        //public ItemViewModel()
        //{
        //    this.item = new Item(0);
        //}

        //public ItemViewModel(Item item)
        //{
        //    this.item = item;
        //}
    }
}
