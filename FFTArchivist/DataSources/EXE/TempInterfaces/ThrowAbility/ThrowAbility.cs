using FFTArchivist.DataSources.EXE.TempInterfaces;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ThrowAbility
{
    public class ThrowAbility : DiffableModelBase<ThrowAbility>, IDiffableModel<ThrowAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? ItemId { get; set; }

        public static Dictionary<string, DiffablePropertyItem<ThrowAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<ThrowAbility>>
        {
            ["ItemId"] = new DiffablePropertyItem<ThrowAbility, byte?>("ItemId", (i) => i.ItemId, delegate (ThrowAbility i, byte? v)
            {
                i.ItemId = v;
            }),
        };


        public static ThrowAbility FromStructure(int id, ref THROW_ABILITY_DATA @struct)
        {
            ThrowAbility abilityThrowSecondary = new()
            {
                Id = id,
                ItemId = @struct.ItemId,
            };

            return abilityThrowSecondary;
        }

        public ThrowAbility Clone()
        {
            return new ThrowAbility
            {
                Id = Id,
                ItemId = ItemId,
            };
        }
    }
}
