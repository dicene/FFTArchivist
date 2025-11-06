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
    public class MathAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(MathAbilityEXESource), "Key")]
        public DataItem<byte> Key { get; set; }

        [EXESourceMapping(typeof(MathAbilityEXESource), "Value")]
        public DataItem<byte> Value { get; set; }
    }
}
