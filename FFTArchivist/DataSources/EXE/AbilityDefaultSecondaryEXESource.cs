using FFTArchivist.DataSources.EXE.TempInterfaces.AbilitySecondary;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    internal class AbilityDefaultSecondaryEXESource : AEXEDataSource<AbilityDefaultSecondary, ABILITY_DEFAULT_SECONDARY_DATA, AbilityDefaultSecondaryTable>
    {
        public AbilityDefaultSecondaryEXESource() : base("AbilityDefaultSecondary.xml", "00 00 00 20 00 08 92 00 0D 00 00 00 00 00 00 00 FF FF FF FF 04 01 01 00 00 E2 00 00 0C 00 0E 00 04 06 00 00 FF FF FF FF 04 01 01 00 00 E2 00 00 0C 00 14 00 05 0A 00 00 FF FF FF FF 04 01 02 00", 368)
        {
        }
    }
}
