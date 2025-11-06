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
        public ThrowAbilityEXESource() : base("ThrowAbility.xml", "F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD", 14)
        {
        }
    }
}
