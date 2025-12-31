using FFTArchivist.DataSources.EXE;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class ThrowAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(ThrowAbilityEXESource), "ItemId")]
        public DataItem<byte> ItemId { get; set; }
    }
}
