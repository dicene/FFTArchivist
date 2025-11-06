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
        public ChargeAbilityEXESource() : base("ChargeAbility.xml", "03 01 04 02 05 03 06 04 07 05 08 07 0A 0A 0D 14", 8)
        {
        }
    }
}
