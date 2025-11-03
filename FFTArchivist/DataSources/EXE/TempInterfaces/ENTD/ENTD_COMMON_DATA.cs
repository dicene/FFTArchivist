using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ENTD
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ENTD_COMMON_DATA
    {
        //public ENTD_COMMON_UNIT_DATA[] EventUnits = new ENTD_COMMON_UNIT_DATA[0x10];
        public ENTD_COMMON_UNIT_DATA EventUnit1;
        public ENTD_COMMON_UNIT_DATA EventUnit2;
        public ENTD_COMMON_UNIT_DATA EventUnit3;
        public ENTD_COMMON_UNIT_DATA EventUnit4;
        public ENTD_COMMON_UNIT_DATA EventUnit5;
        public ENTD_COMMON_UNIT_DATA EventUnit6;
        public ENTD_COMMON_UNIT_DATA EventUnit7;
        public ENTD_COMMON_UNIT_DATA EventUnit8;
        public ENTD_COMMON_UNIT_DATA EventUnit9;
        public ENTD_COMMON_UNIT_DATA EventUnit10;
        public ENTD_COMMON_UNIT_DATA EventUnit11;
        public ENTD_COMMON_UNIT_DATA EventUnit12;
        public ENTD_COMMON_UNIT_DATA EventUnit13;
        public ENTD_COMMON_UNIT_DATA EventUnit14;
        public ENTD_COMMON_UNIT_DATA EventUnit15;
        public ENTD_COMMON_UNIT_DATA EventUnit16;

        public ENTD_COMMON_DATA()
        {

        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ENTD_COMMON_UNIT_DATA
    {
        public byte SpriteSet;
        public byte Flags;
        public byte SpecialName;
        public byte Level;
        public byte Month;
        public byte Day;
        public byte Bravery;
        public byte Faith;
        public byte PrerequisiteJob;
        public byte PrerequisiteJobLevel;
        public byte Job;
        public byte SecondaryAction;
        public short Reaction;
        public short Support;
        public short Movement;
        public byte Head;
        public byte Body;
        public byte Accessory;
        public byte RightHand;
        public byte LeftHand;
        public byte Palette;
        public byte Flags2;
        public byte X;
        public byte Y;
        public byte FacingDirAndUpperLevel;
        public byte Experience;
        public byte SkillSet;
        public byte WarTrophy;
        public byte BonusMoney;
        public byte UnitID;
        public byte TargetX;
        public byte TargetY;
        public byte Flags3;
        public byte Target;
        public byte Unk25;
        public byte Flags4;
        public byte Unk27;
    }
}
