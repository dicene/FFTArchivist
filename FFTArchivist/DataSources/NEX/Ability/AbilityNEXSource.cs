using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.NEX.Ability
{
    internal class AbilityNEXSource : NEXDataSource
    {
        public string Name { get; set; }
        public AbilityNEXSource() : base("Ability.<locale>")
        {

        }
    }
}
