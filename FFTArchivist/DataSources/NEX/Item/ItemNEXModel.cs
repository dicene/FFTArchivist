using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.NEX.Item
{
    internal class ItemNEXModel
    {
        public string Name { get; set; }
        public string NameSingular { get; set; }
        public string NamePlural { get; set; }
        public string Description { get; set; }
        public string Name2 { get; set; }
        public int UiStatusEffectId { get; set; }
        public int SortOrder { get; set; }
        public int IsRandomDamager { get; set; }
    }
}
