using FFTArchivist.DataSources.NEX.PoachItem;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class PoachItem(int id) : BaseModel(id), INotifyPropertyChanged
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
