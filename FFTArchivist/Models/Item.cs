using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.NEX.Item;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class Item : BaseModel, INotifyPropertyChanged
    {
        public Item() : base() { }
        public Item(int id) : base(id) { }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXMapping(typeof(ItemNEXSource), nameof(ItemNEXModel.Name))]
        public DataItem<string> Name { get; set; }

        [NEXMapping(typeof(ItemNEXSource), nameof(ItemNEXModel.Description))]
        public DataItem<string> Description { get; set; }

        [EXESourceMapping(typeof(ItemEXESource), nameof(Item.Price))]
        public DataItem<ushort> Price { get; set; }
    }
}
