using System.Runtime.InteropServices;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.SupportAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SUPPORT_ABILITY_DATA
    {
        public byte AbilityId { get; set; }
    }
}
