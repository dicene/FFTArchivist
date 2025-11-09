using FFTArchivist.DataSources.EXE.TempInterfaces.ThrowAbility;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    internal class ThrowAbilityEXESource : AEXEDataSource<ThrowAbility, THROW_ABILITY_DATA, ThrowAbilityTable>
    {
        public ThrowAbilityEXESource() : base("ThrowAbility.xml", "20 01 03 09 05 02 06 0F 10 04 0E 21 00 00 00 00", 12)
        {
        }
    }
}
