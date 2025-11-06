using FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    internal class MathAbilityEXESource : AEXEDataSource<MathAbility, MATH_ABILITY_DATA, MathAbilityTable>
    {
        public MathAbilityEXESource() : base("MathAbility.xml", "80 40 20 10 08 04 02 01", 8)
        {
        }
    }
}
