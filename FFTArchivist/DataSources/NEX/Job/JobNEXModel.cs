using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.NEX.Job
{
    internal class JobNEXModel
    {
        public string Name { get; set; }
        public int jobtype_Id { get; set; }
        public int jobcommand_Id { get; set; }
        public int TexturePartsIndex { get; set; }
        public int uijobabilityhelp_Id { get; set; }
        public int egg_Id { get; set; }
        public int HideJobTree { get; set; }
    }
}
