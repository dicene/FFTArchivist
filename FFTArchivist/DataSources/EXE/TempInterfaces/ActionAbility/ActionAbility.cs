using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

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
            [nameof(Range)]         = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Range), (i) => i.Range, (ActionAbility i, byte? v) => i.Range = v),
            [nameof(EffectArea)]    = new DiffablePropertyItem<ActionAbility, byte?>(nameof(EffectArea), (i) => i.EffectArea, (ActionAbility i, byte? v) => i.EffectArea = v),
            [nameof(Vertical)]      = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Vertical), (i) => i.Vertical, (ActionAbility i, byte? v) => i.Vertical = v),
            [nameof(Flags1)]        = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Flags1), (i) => i.Flags1, (ActionAbility i, byte? v) => i.Flags1 = v),
            [nameof(Flags2)]        = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Flags2), (i) => i.Flags2, (ActionAbility i, byte? v) => i.Flags2 = v),
            [nameof(Flags3)]        = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Flags3), (i) => i.Flags3, (ActionAbility i, byte? v) => i.Flags3 = v),
            [nameof(Flags4)]        = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Flags4), (i) => i.Flags4, (ActionAbility i, byte? v) => i.Flags4 = v),
            [nameof(Element)]       = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Element), (i) => i.Element, (ActionAbility i, byte? v) => i.Element = v),
            [nameof(Formula)]       = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Formula), (i) => i.Formula, (ActionAbility i, byte? v) => i.Formula = v),
            [nameof(X)]             = new DiffablePropertyItem<ActionAbility, byte?>(nameof(X), (i) => i.X, (ActionAbility i, byte? v) => i.X = v),
            [nameof(Y)]             = new DiffablePropertyItem<ActionAbility, byte?>(nameof(Y), (i) => i.Y, (ActionAbility i, byte? v) => i.Y = v),
            [nameof(InflictStatus)] = new DiffablePropertyItem<ActionAbility, byte?>(nameof(InflictStatus), (i) => i.InflictStatus, (ActionAbility i, byte? v) => i.InflictStatus = v),
            [nameof(CT)]            = new DiffablePropertyItem<ActionAbility, byte?>(nameof(CT), (i) => i.CT, (ActionAbility i, byte? v) => i.CT = v),
            [nameof(MPCost)]        = new DiffablePropertyItem<ActionAbility, byte?>(nameof(MPCost), (i) => i.MPCost, (ActionAbility i, byte? v) => i.MPCost = v),
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
