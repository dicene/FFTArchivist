using FFTArchivist.DataSources.NEX.Item;
using FFTArchivist.DataSources.NEX.PoachItem;
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

        [NEXMapping(typeof(PoachItemNEXSource), nameof(PoachItemNEXModel.Unknown8))]
        public DataItem<string> Name { get; set; }

        [NEXMapping(typeof(PoachItemNEXSource), nameof(PoachItemNEXModel.Unknown18))]
        public DataItem<string> Description { get; set; }

        [NEXMapping(typeof(PoachItemNEXSource), nameof(PoachItemNEXModel.Unknown2C))]
        public DataItem<int> RewardID { get; set; }
    }
}
