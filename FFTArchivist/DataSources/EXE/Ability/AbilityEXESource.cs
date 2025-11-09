using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    internal class AbilityEXESource : AEXEDataSource<Ability, ABILITY_COMMON_DATA, AbilityTable>
    {
        public AbilityEXESource() : base("AbilityData.xml", "00 00 00 00 82 02 01 81 32 00 5A 41 81 75 00 80", 512)
        {
        }
    }
}
