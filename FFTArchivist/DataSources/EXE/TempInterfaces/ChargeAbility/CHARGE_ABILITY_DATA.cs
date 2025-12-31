using System.Runtime.InteropServices;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ChargeAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARGE_ABILITY_DATA
    {
        public byte CT { get; set; }
        public byte Power { get; set; }
    }
}
