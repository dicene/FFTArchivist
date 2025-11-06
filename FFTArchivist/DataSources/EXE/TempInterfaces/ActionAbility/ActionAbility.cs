using FFTArchivist.DataSources.EXE.TempInterfaces;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ActionAbility
{
    public class ActionAbility : DiffableModelBase<ActionAbility>, IDiffableModel<ActionAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? Range { get; set; }
        public byte? EffectArea { get; set; }
        public byte? Vertical { get; set; }
        public byte? Flags1 { get; set; }
        public byte? Flags2 { get; set; }
        public byte? Flags3 { get; set; }
        public byte? Flags4 { get; set; }
        public byte? Element { get; set; }
        public byte? Formula { get; set; }
        public byte? X { get; set; }
        public byte? Y { get; set; }
        public byte? InflictStatus { get; set; }
        public byte? CT { get; set; }
        public byte? MPCost { get; set; }

        public static Dictionary<string, DiffablePropertyItem<ActionAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<ActionAbility>>
        {
            ["Range"] = new DiffablePropertyItem<ActionAbility, byte?>("Range", (i) => i.Range, delegate (ActionAbility i, byte? v)
            {
                i.Range = v;
            }),
            ["EffectArea"] = new DiffablePropertyItem<ActionAbility, byte?>("EffectArea", (i) => i.EffectArea, delegate (ActionAbility i, byte? v)
            {
                i.EffectArea = v;
            }),
            ["Vertical"] = new DiffablePropertyItem<ActionAbility, byte?>("Vertical", (i) => i.Vertical, delegate (ActionAbility i, byte? v)
            {
                i.Vertical = v;
            }),
            ["Flags1"] = new DiffablePropertyItem<ActionAbility, byte?>("Flags1", (i) => i.Flags1, delegate (ActionAbility i, byte? v)
            {
                i.Flags1 = v;
            }),
            ["Flags2"] = new DiffablePropertyItem<ActionAbility, byte?>("Flags2", (i) => i.Flags2, delegate (ActionAbility i, byte? v)
            {
                i.Flags2 = v;
            }),
            ["Flags3"] = new DiffablePropertyItem<ActionAbility, byte?>("Flags3", (i) => i.Flags3, delegate (ActionAbility i, byte? v)
            {
                i.Flags3 = v;
            }),
            ["Flags4"] = new DiffablePropertyItem<ActionAbility, byte?>("Flags4", (i) => i.Flags4, delegate (ActionAbility i, byte? v)
            {
                i.Flags4 = v;
            }),
            ["Element"] = new DiffablePropertyItem<ActionAbility, byte?>("Element", (i) => i.Element, delegate (ActionAbility i, byte? v)
            {
                i.Element = v;
            }),
            ["Formula"] = new DiffablePropertyItem<ActionAbility, byte?>("Formula", (i) => i.Formula, delegate (ActionAbility i, byte? v)
            {
                i.Formula = v;
            }),
            ["X"] = new DiffablePropertyItem<ActionAbility, byte?>("X", (i) => i.X, delegate (ActionAbility i, byte? v)
            {
                i.X = v;
            }),
            ["Y"] = new DiffablePropertyItem<ActionAbility, byte?>("Y", (i) => i.Y, delegate (ActionAbility i, byte? v)
            {
                i.Y = v;
            }),
            ["InflictStatus"] = new DiffablePropertyItem<ActionAbility, byte?>("InflictStatus", (i) => i.InflictStatus, delegate (ActionAbility i, byte? v)
            {
                i.InflictStatus = v;
            }),
            ["CT"] = new DiffablePropertyItem<ActionAbility, byte?>("CT", (i) => i.CT, delegate (ActionAbility i, byte? v)
            {
                i.CT = v;
            }),
            ["MPCost"] = new DiffablePropertyItem<ActionAbility, byte?>("MPCost", (i) => i.MPCost, delegate (ActionAbility i, byte? v)
            {
                i.MPCost = v;
            }),
        };


        public static ActionAbility FromStructure(int id, ref ACTION_ABILITY_DATA @struct)
        {
            ActionAbility abilityDefaultSecondary = new()
            {
                Id = id,
                Range = @struct.Range,
                EffectArea = @struct.EffectArea,
                Vertical = @struct.Vertical,
                Flags1 = @struct.Flags1,
                Flags2 = @struct.Flags2,
                Flags3 = @struct.Flags3,
                Flags4 = @struct.Flags4,
                Element = @struct.Element,
                Formula = @struct.Formula,
                X = @struct.X,
                Y = @struct.Y,
                InflictStatus = @struct.InflictStatus,
                CT = @struct.CT,
                MPCost = @struct.MPCost
            };

            return abilityDefaultSecondary;
        }

        public ActionAbility Clone()
        {
            return new ActionAbility
            {
                Id = Id,
                Range = Range,
                EffectArea = EffectArea,
                Vertical = Vertical,
                Flags1 = Flags1,
                Flags2 = Flags2,
                Flags3 = Flags3,
                Flags4 = Flags4,
                Element = Element,
                Formula = Formula,
                X = X,
                Y = Y,
                InflictStatus = InflictStatus,
                CT = CT,
                MPCost = MPCost
            };
        }
    }
}
