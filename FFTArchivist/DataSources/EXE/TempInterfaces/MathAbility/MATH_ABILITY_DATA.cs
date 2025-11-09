using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MATH_ABILITY_DATA
    {
        public MathFlags MathFlags { get; set; }
    }

    [Flags]
    public enum MathFlags : byte
    {
        Flag3 = 1 << 0,
        Flag4 = 1 << 1,
        Flag5 = 1 << 2,
        FlagPrime = 1 << 3,
        FlagHeight = 1 << 4,
        FlagExp = 1 << 5,
        FlagLevel = 1 << 6,
        FlagCT = 1 << 7,
    }
}
