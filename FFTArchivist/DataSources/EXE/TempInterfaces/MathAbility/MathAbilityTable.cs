using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility
{
    public class MathAbilityTable : TableBase<MathAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
