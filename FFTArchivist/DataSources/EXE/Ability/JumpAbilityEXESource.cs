using FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    internal class JumpAbilityEXESource : AEXEDataSource<JumpAbility, JUMP_ABILITY_DATA, JumpAbilityTable>
    {
        public JumpAbilityEXESource() : base("JumpAbility.xml", "02 00 03 00 04 00 05 00 08 00 00 02 00 03 00 04 00 05 00 06 00 07 00 08", 12)
        {
        }
    }
}
