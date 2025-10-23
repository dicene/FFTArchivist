using FFTArchivist.Models.Base;
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
    public class Item(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXLinkage("0004.en", "Item", 1)]
        public new DataItem<string> Name { get; set; }

        [NEXLinkage("0004.en", "Item", 4)]
        public DataItem<string> Description { get; set; }

        [EXELinkage(0x807B30, 256, 0xC, 0x8)]
        public DataItem<short> Price { get; set; }
    }
}
