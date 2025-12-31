using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility
{
    public class ItemAbilityTable : TableBase<ItemAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
