using FFTArchivist.DataSources.EXE.TempInterfaces;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.AbilitySecondary
{
    public class AbilityDefaultSecondary : DiffableModelBase<AbilityDefaultSecondary>, IDiffableModel<AbilityDefaultSecondary>, IIdentifiableModel
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

        public static Dictionary<string, DiffablePropertyItem<AbilityDefaultSecondary>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<AbilityDefaultSecondary>>
        {
            ["Range"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Range", (i) => i.Range, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Range = v;
            }),
            ["EffectArea"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("EffectArea", (i) => i.EffectArea, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.EffectArea = v;
            }),
            ["Vertical"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Vertical", (i) => i.Vertical, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Vertical = v;
            }),
            ["Flags1"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Flags1", (i) => i.Flags1, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Flags1 = v;
            }),
            ["Flags2"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Flags2", (i) => i.Flags2, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Flags2 = v;
            }),
            ["Flags3"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Flags3", (i) => i.Flags3, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Flags3 = v;
            }),
            ["Flags4"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Flags4", (i) => i.Flags4, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Flags4 = v;
            }),
            ["Element"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Element", (i) => i.Element, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Element = v;
            }),
            ["Formula"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Formula", (i) => i.Formula, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Formula = v;
            }),
            ["X"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("X", (i) => i.X, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.X = v;
            }),
            ["Y"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("Y", (i) => i.Y, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.Y = v;
            }),
            ["InflictStatus"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("InflictStatus", (i) => i.InflictStatus, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.InflictStatus = v;
            }),
            ["CT"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("CT", (i) => i.CT, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.CT = v;
            }),
            ["MPCost"] = new DiffablePropertyItem<AbilityDefaultSecondary, byte?>("MPCost", (i) => i.MPCost, delegate (AbilityDefaultSecondary i, byte? v)
            {
                i.MPCost = v;
            }),
        };


        public static AbilityDefaultSecondary FromStructure(int id, ref ABILITY_DEFAULT_SECONDARY_DATA @struct)
        {
            AbilityDefaultSecondary abilityDefaultSecondary = new()
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

        public AbilityDefaultSecondary Clone()
        {
            return new AbilityDefaultSecondary
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
