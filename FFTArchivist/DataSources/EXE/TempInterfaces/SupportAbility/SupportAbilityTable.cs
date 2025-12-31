using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.SupportAbility
{
    public class SupportAbilityTable : TableBase<SupportAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
