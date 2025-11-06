using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_ABILITY_DATA
    {
        public byte ItemId { get; set; }
    }
}
