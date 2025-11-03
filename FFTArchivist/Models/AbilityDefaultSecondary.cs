using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.EXE.TempInterfaces.AbilitySecondary;
using FFTArchivist.DataSources.NEX.Ability;
using FFTArchivist.DataSources.NEX.Item;
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
    public class AbilityDefaultSecondary(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(AbilityDefaultSecondaryEXESource), "Range")]
        public DataItem<byte> Range { get; set; }
    }
}
