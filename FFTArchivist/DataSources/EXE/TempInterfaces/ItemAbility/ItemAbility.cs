using FFTArchivist.DataSources.EXE.TempInterfaces;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility
{
    public class ItemAbility : DiffableModelBase<ItemAbility>, IDiffableModel<ItemAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? ItemId { get; set; }

        public static Dictionary<string, DiffablePropertyItem<ItemAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<ItemAbility>>
        {
            ["ItemId"] = new DiffablePropertyItem<ItemAbility, byte?>("Range", (i) => i.ItemId, delegate (ItemAbility i, byte? v)
            {
                i.ItemId = v;
            }),
        };

        public static ItemAbility FromStructure(int id, ref ITEM_ABILITY_DATA @struct)
        {
            ItemAbility abilityItemSecondary = new()
            {
                Id = id,
                ItemId = @struct.ItemId,
            };

            return abilityItemSecondary;
        }

        public ItemAbility Clone()
        {
            return new ItemAbility
            {
                Id = Id,
                ItemId = ItemId,
            };
        }
    }
}
