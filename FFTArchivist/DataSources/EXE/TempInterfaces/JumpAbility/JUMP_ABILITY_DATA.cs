using System.Runtime.InteropServices;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct JUMP_ABILITY_DATA
    {
        public byte Range { get; set; }
        public byte Vertical { get; set; }
    }
}
