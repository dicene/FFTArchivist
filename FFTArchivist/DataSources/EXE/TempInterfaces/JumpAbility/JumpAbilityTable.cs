using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility
{
    public class JumpAbilityTable : TableBase<JumpAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
