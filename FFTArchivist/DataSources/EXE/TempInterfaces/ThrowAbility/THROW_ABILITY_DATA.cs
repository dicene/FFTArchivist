using System.Runtime.InteropServices;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ThrowAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct THROW_ABILITY_DATA
    {
        public byte ItemId { get; set; }
    }
}
