using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.NEX;
using FFTArchivist.DataSources.NEX.Ability;
using FFTArchivist.DataSources.NEX.Item;
using FFTArchivist.DataSources.NEX.PoachItem;
using FFTArchivist.Models.Base;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
