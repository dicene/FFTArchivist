using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ThrowAbility
{
    public class ThrowAbilityTable : TableBase<ThrowAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
