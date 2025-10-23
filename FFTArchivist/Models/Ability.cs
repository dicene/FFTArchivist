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
    public class Ability(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXLinkage("0004.en", "Ability", 2)]
        public new DataItem<string> Name { get; set; }

        [NEXLinkage("0004.en", "Ability", "Description")]
        //[NEXLinkage("0004.en", "Ability", 3)]
        public DataItem<string> Description { get; set; }

        [NEXLinkage("0004.en", "Ability", 19)]
        public DataItem<byte> JpCost1 { get; set; }

        [NEXLinkage("0004.en", "Ability", 20)]
        public DataItem<byte> JpCost2 { get; set; }
    }
}
