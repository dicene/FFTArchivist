using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.NEX.PoachItem
{
    internal class PoachItemNEXModel
    {
        public string Unknown8 { get; set; } // Name
        public string LowerCaseSingularName { get; set; }
        public string LowerCasePluralName { get; set; }
        public string DuplicateName { get; set; }
        public string Unknown18 { get; set; } // Description
        public int Unknown2C { get; set; } // RewardID
    }
}
