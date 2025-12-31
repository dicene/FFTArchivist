using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.ENTD
{
    public class ENTDTable : TableBase<ENTD>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
