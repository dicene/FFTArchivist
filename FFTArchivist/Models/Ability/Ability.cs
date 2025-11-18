using FFTArchivist.DataSources;
using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.NEX.Ability;
using FFTArchivist.DataSources.NEX.Item;
using FFTArchivist.Managers;
using FFTArchivist.Models.Base;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Models
{
    public class Ability : BaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //[NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.Name))]
        public DataItem<string> Name { get; set; }

        //[NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.Description))]
        public DataItem<string> Description { get; set; }

        //[NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.JpCost1))]
        public DataItem<byte> JpCost1 { get; set; }

        //[NEXMapping(typeof(AbilityNEXSource), nameof(AbilityNEXModel.JpCost2))]
        public DataItem<byte> JpCost2 { get; set; }

        //[EXESourceMapping(typeof(AbilityEXESource), nameof(fftivc.utility.modloader.Interfaces.Tables.Models.Ability.ChanceToLearn))]
        public DataItem<byte> ChanceToLearn { get; set; }

        public Ability(int id)
        {
            Name = new DataItem<string>(new NEXMapping(typeof(AbilityNEXSource), id, nameof(Name)), id);
            Description = new DataItem<string>(new NEXMapping(typeof(AbilityNEXSource), id, nameof(Description)), id);
            JpCost1 = new DataItem<byte>(new NEXMapping(typeof(AbilityNEXSource), id, nameof(JpCost1)), id);
            JpCost2 = new DataItem<byte>(new NEXMapping(typeof(AbilityNEXSource), id, nameof(JpCost2)), id);
            ChanceToLearn = new DataItem<byte>(new EXESourceMapping(typeof(AbilityEXESource), id, nameof(ChanceToLearn)), id);

            DataItems.Add(nameof(Name), Name);
            DataItems.Add(nameof(Description), Description);
            DataItems.Add(nameof(JpCost1), JpCost1);
            DataItems.Add(nameof(JpCost2), JpCost2);
            DataItems.Add(nameof(ChanceToLearn), ChanceToLearn);
        }
    }
}
