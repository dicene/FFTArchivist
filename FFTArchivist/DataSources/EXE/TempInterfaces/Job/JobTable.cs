using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.Job
{
    public class JobTable : TableBase<Job>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
