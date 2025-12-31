using FFTArchivist.DataSources.EXE;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class JumpAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(JumpAbilityEXESource), "Range")]
        public DataItem<byte> Range { get; set; }

        [EXESourceMapping(typeof(JumpAbilityEXESource), "Vertical")]
        public DataItem<byte> Vertical { get; set; }
    }
}
