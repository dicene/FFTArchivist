using System.Runtime.InteropServices;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_ABILITY_DATA
    {
        public byte ItemId { get; set; }
    }
}
