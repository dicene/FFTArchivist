using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ActionAbility
{
    public class ActionAbilityTable : TableBase<ActionAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
