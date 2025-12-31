using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class MathAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(MathAbilityEXESource), "MathFlags")]
        public DataItem<MathFlags> MathFlags { get; set; }
    }
}
