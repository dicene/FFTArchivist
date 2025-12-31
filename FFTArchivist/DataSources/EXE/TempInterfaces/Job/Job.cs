using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.Job
{
    public class Job : DiffableModelBase<Job>, IDiffableModel<Job>, IIdentifiableModel
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

        public static Dictionary<string, DiffablePropertyItem<Job>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<Job>>
        {
            [nameof(Range)]         = new DiffablePropertyItem<Job, byte?>(nameof(Range), (i) => i.Range, (Job i, byte? v) => i.Range = v),
            [nameof(EffectArea)]    = new DiffablePropertyItem<Job, byte?>(nameof(EffectArea), (i) => i.EffectArea, (Job i, byte? v) => i.EffectArea = v),
            [nameof(Vertical)]      = new DiffablePropertyItem<Job, byte?>(nameof(Vertical), (i) => i.Vertical, (Job i, byte? v) => i.Vertical = v),
            [nameof(Flags1)]        = new DiffablePropertyItem<Job, byte?>(nameof(Flags1), (i) => i.Flags1, (Job i, byte? v) => i.Flags1 = v),
            [nameof(Flags2)]        = new DiffablePropertyItem<Job, byte?>(nameof(Flags2), (i) => i.Flags2, (Job i, byte? v) => i.Flags2 = v),
            [nameof(Flags3)]        = new DiffablePropertyItem<Job, byte?>(nameof(Flags3), (i) => i.Flags3, (Job i, byte? v) => i.Flags3 = v),
            [nameof(Flags4)]        = new DiffablePropertyItem<Job, byte?>(nameof(Flags4), (i) => i.Flags4, (Job i, byte? v) => i.Flags4 = v),
            [nameof(Element)]       = new DiffablePropertyItem<Job, byte?>(nameof(Element), (i) => i.Element, (Job i, byte? v) => i.Element = v),
            [nameof(Formula)]       = new DiffablePropertyItem<Job, byte?>(nameof(Formula), (i) => i.Formula, (Job i, byte? v) => i.Formula = v),
            [nameof(X)]             = new DiffablePropertyItem<Job, byte?>(nameof(X), (i) => i.X, (Job i, byte? v) => i.X = v),
            [nameof(Y)]             = new DiffablePropertyItem<Job, byte?>(nameof(Y), (i) => i.Y, (Job i, byte? v) => i.Y = v),
            [nameof(InflictStatus)] = new DiffablePropertyItem<Job, byte?>(nameof(InflictStatus), (i) => i.InflictStatus, (Job i, byte? v) => i.InflictStatus = v),
            [nameof(CT)]            = new DiffablePropertyItem<Job, byte?>(nameof(CT), (i) => i.CT, (Job i, byte? v) => i.CT = v),
            [nameof(MPCost)]        = new DiffablePropertyItem<Job, byte?>(nameof(MPCost), (i) => i.MPCost, (Job i, byte? v) => i.MPCost = v),
        };


        public static Job FromStructure(int id, ref JOB_DATA @struct)
        {
            Job job = new()
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

            return job;
        }

        public Job Clone()
        {
            return new Job
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
