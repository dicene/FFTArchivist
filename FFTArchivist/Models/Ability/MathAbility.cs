using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility;
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

        [EXESourceMapping(typeof(MathAbilityEXESource), "MathFlags")]
        public DataItem<MathFlags> MathFlags { get; set; }
    }
}
