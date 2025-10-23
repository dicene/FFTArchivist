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
    public class Poach(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXLinkage("0004.en", "PoachItem", 2)]
        public new DataItem<string> Name { get; set; }

        [NEXLinkage("0004.en", "PoachItem", 6)]
        public DataItem<string> Description { get; set; }

        [NEXLinkage("0004.en", "PoachItem", 14)]
        public DataItem<int> ItemID { get; set; }
    }
}
