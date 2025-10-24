using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Managers
{
    internal interface IModManager
    {
        public static IModManager Instance { get; }
        public Task ExportMod(string modName, string modId, string modVersion, string modAuthor, string modDescription, string modPath);
    }
}
