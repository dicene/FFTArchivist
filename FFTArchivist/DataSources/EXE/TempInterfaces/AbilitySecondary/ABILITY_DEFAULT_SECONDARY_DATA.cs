using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.AbilitySecondary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ABILITY_DEFAULT_SECONDARY_DATA
    {
        public byte Range { get; set; }
        public byte EffectArea { get; set; }
        public byte Vertical { get; set; }
        public byte Flags1 { get; set; }
        public byte Flags2 { get; set; }
        public byte Flags3 { get; set; }
        public byte Flags4 { get; set; }
        public byte Element { get; set; }
        public byte Formula { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte InflictStatus { get; set; }
        public byte CT { get; set; }
        public byte MPCost { get; set; }
        public byte unk_e { get; set; }
        public byte unk_f { get; set; }
        public byte unk_10 { get; set; }
        public byte unk_11 { get; set; }
        public byte unk_12 { get; set; }
        public byte unk_13 { get; set; }
    }
}
