using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility
{
    public class MathAbility : DiffableModelBase<MathAbility>, IDiffableModel<MathAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public MathFlags? MathFlags { get; set; }

        public static Dictionary<string, DiffablePropertyItem<MathAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<MathAbility>>
        {
            [nameof(MathFlags)] = new DiffablePropertyItem<MathAbility, MathFlags?>(nameof(MathFlags), i => i.MathFlags, (i, v) => i.MathFlags = v),
        };


        public static MathAbility FromStructure(int id, ref MATH_ABILITY_DATA @struct)
        {
            MathAbility MathAbility = new()
            {
                Id = id,
                MathFlags = @struct.MathFlags,
            };

            return MathAbility;
        }

        public MathAbility Clone()
        {
            return new MathAbility
            {
                Id = Id,
                MathFlags = MathFlags,
            };
        }
    }
}
