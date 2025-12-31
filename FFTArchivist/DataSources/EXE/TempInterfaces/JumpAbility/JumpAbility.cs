using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility
{
    public class JumpAbility : DiffableModelBase<JumpAbility>, IDiffableModel<JumpAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? Range { get; set; }
        public byte? Vertical { get; set; }

        public static Dictionary<string, DiffablePropertyItem<JumpAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<JumpAbility>>
        {
            ["Range"] = new DiffablePropertyItem<JumpAbility, byte?>("Range", (i) => i.Range, delegate (JumpAbility i, byte? v)
            {
                i.Range = v;
            }),

            ["Vertical"] = new DiffablePropertyItem<JumpAbility, byte?>("Vertical", (i) => i.Vertical, delegate (JumpAbility i, byte? v)
            {
                i.Vertical = v;
            }),
        };


        public static JumpAbility FromStructure(int id, ref JUMP_ABILITY_DATA @struct)
        {
            JumpAbility jumpAbility = new()
            {
                Id = id,
                Range = @struct.Range,
                Vertical = @struct.Vertical,
            };

            return jumpAbility;
        }

        public JumpAbility Clone()
        {
            return new JumpAbility
            {
                Id = Id,
                Range = Range,
                Vertical = Vertical,
            };
        }
    }
}
