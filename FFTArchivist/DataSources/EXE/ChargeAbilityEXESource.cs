using FFTArchivist.DataSources.EXE.TempInterfaces.ChargeAbility;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    internal class ChargeAbilityEXESource : AEXEDataSource<ChargeAbility, CHARGE_ABILITY_DATA, ChargeAbilityTable>
    {
        public ChargeAbilityEXESource() : base("ChargeAbility.xml", "F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD", 14)
        {
        }
    }
}
