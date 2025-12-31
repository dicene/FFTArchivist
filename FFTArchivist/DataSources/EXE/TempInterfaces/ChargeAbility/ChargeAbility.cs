using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ChargeAbility
{
    public class ChargeAbility : DiffableModelBase<ChargeAbility>, IDiffableModel<ChargeAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? CT { get; set; }
        public byte? Power { get; set; }

        public static Dictionary<string, DiffablePropertyItem<ChargeAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<ChargeAbility>>
        {
            ["CT"] = new DiffablePropertyItem<ChargeAbility, byte?>("CT", (i) => i.CT, delegate (ChargeAbility i, byte? v)
            {
                i.CT = v;
            }),

            ["Power"] = new DiffablePropertyItem<ChargeAbility, byte?>("Power", (i) => i.Power, delegate (ChargeAbility i, byte? v)
            {
                i.Power = v;
            }),
        };


        public static ChargeAbility FromStructure(int id, ref CHARGE_ABILITY_DATA @struct)
        {
            ChargeAbility chargeAbility = new()
            {
                Id = id,
                CT = @struct.CT,
                Power = @struct.Power,
            };

            return chargeAbility;
        }

        public ChargeAbility Clone()
        {
            return new ChargeAbility
            {
                Id = Id,
                CT = CT,
                Power = Power,
            };
        }
    }
}
