using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ENTD
{
    public class ENTD_EntryUnit
    {
        public byte SpriteSet { get; set; }
        public ENTD_EntryUnit() { }
    }

    public class ENTD : DiffableModelBase<ENTD>, IDiffableModel<ENTD>, IIdentifiableModel
    {
        public int Id { get; set; }
        public List<ENTD_EntryUnit> Units { get; set; } = new List<ENTD_EntryUnit>();

        public static Dictionary<string, DiffablePropertyItem<ENTD>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<ENTD>>
        {
            //["Unit"] = new DiffablePropertyItem<ENTD, byte?>("JPCost", (i) => i.JPCost, delegate (ENTD i, byte? v)
            //{
            //    i.JPCost = v;
            //}),
            //["ChanceToLearn"] = new DiffablePropertyItem<ENTD, byte?>("ChanceToLearn", (i) => i.ChanceToLearn, delegate (ENTD i, byte? v)
            //{
            //    i.ChanceToLearn = v;
            //}),
            //["Flags"] = new DiffablePropertyItem<ENTD, AbilityFlags?>("Flags", (i) => i.Flags, delegate (ENTD i, AbilityFlags? v)
            //{
            //    i.Flags = v;
            //}),
            //["AbilityType"] = new DiffablePropertyItem<ENTD, AbilityType?>("AbilityType", (i) => i.AbilityType, delegate (ENTD i, AbilityType? v)
            //{
            //    i.AbilityType = v;
            //}),
            //["AIBehaviorFlags"] = new DiffablePropertyItem<ENTD, AIBehaviorFlags?>("AIBehaviorFlags", (i) => i.AIBehaviorFlags, delegate (ENTD i, AIBehaviorFlags? v)
            //{
            //    i.AIBehaviorFlags = v;
            //})
        };


        public static ENTD FromStructure(int id, ref ENTD_COMMON_DATA @struct)
        {
            List<ENTD_EntryUnit> units = new();

            var unit1 = new ENTD_EntryUnit();
            unit1.SpriteSet = @struct.EventUnit1.SpriteSet;
            units.Add(unit1);

            ENTD entry = new();
            entry.Units = units;

            return entry;
        }

        public ENTD Clone()
        {
            return new ENTD
            {
                Id = Id,
            };
        }
    }
}
