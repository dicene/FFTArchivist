using FFTArchivist.DataSources.EXE;
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
    public class ChargeAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(ChargeAbilityEXESource), "CT")]
        public DataItem<byte> CT { get; set; }

        [EXESourceMapping(typeof(ChargeAbilityEXESource), "Power")]
        public DataItem<byte> Power { get; set; }
    }
}
