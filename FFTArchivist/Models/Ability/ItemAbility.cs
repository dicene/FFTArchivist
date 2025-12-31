using FFTArchivist.DataSources.EXE;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class ItemAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(ItemAbilityEXESource), "ItemId")]
        public DataItem<byte> ItemId { get; set; }
    }
}
