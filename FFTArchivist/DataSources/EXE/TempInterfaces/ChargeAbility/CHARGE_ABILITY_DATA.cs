using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ChargeAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARGE_ABILITY_DATA
    {
        public byte CT { get; set; }
        public byte Power { get; set; }
    }
}
