using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility
{
    public class JumpAbilityTable : TableBase<JumpAbility>, IVersionableModel
    {
        public uint Version { get; set; } = 1u;
    }
}
