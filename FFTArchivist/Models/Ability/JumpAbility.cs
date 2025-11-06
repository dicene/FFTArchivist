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
    public class JumpAbility(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(JumpAbilityEXESource), "Range")]
        public DataItem<byte> Range { get; set; }

        [EXESourceMapping(typeof(JumpAbilityEXESource), "Vertical")]
        public DataItem<byte> Vertical { get; set; }
    }
}
