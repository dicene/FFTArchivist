using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.EXE.TempInterfaces.ActionAbility;
using FFTArchivist.DataSources.NEX.Ability;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class ActionAbility : BaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Range))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Range))]
        public DataItem<byte> Range { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.EffectArea))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.EffectArea))]
        public DataItem<byte> EffectArea { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Vertical))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Vertical))]
        public DataItem<byte> Vertical { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Flags1))]
        //[NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Flags12))]
        public DataItem<byte> Flags1 { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Flags2))]
        //[NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Flags12))]
        public DataItem<byte> Flags2 { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Flags3))]
        //[NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Flags34))]
        public DataItem<byte> Flags3 { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Flags4))]
        //[NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Flags34))]
        public DataItem<byte> Flags4 { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Element))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Element))]
        public DataItem<byte> Element { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Formula))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Formula))]
        public DataItem<byte> Formula { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.X))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.X))]
        public DataItem<byte> X { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.Y))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.Y))]
        public DataItem<byte> Y { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.InflictStatus))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.InflictStatus))]
        public DataItem<byte> InflictStatus { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.CT))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.CT))]
        public DataItem<byte> CT { get; set; }

        [EXESourceMapping(typeof(ActionAbilityEXESource), nameof(ActionAbility.MPCost))]
        [NEXOverrideMapping(typeof(OverrideAbilityActionDataNEXSource), nameof(OverrideAbilityActionDataNEXModel.MPCost))]
        public DataItem<byte> MPCost { get; set; }

        public ActionAbility() : base() { }
        public ActionAbility(int id) : base(id) { }
    }
}
