using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct JUMP_ABILITY_DATA
    {
        public byte Range { get; set; }
        public byte Vertical { get; set; }
    }
}
