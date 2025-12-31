using FFTArchivist.DataSources.EXE;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class SupportAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(SupportAbilityEXESource), "AbilityId")]
        public DataItem<byte> AbilityId { get; set; }
    }
}
