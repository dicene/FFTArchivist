using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.NEX.Ability;
using FFTArchivist.DataSources.NEX.Item;
using FFTArchivist.Models.Base;
using fftivc.utility.modloader.Interfaces.Tables.Models;
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
    public class Ability(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.Name))]
        public DataItem<string> Name { get; set; }

        [NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.Description))]
        public DataItem<string> Description { get; set; }

        [NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.JpCost1))]
        public DataItem<byte> JpCost1 { get; set; }

        [NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.JpCost2))]
        public DataItem<byte> JpCost2 { get; set; }

        [EXESourceMapping(typeof(AbilityEXESource), nameof(fftivc.utility.modloader.Interfaces.Tables.Models.Ability.ChanceToLearn))]
        public DataItem<byte> ChanceToLearn { get; set; }
    }
}
