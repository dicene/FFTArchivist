using FFTArchivist.DataSources.EXE.TempInterfaces;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.SupportAbility
{
    public class SupportAbility : DiffableModelBase<SupportAbility>, IDiffableModel<SupportAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? AbilityId { get; set; }

        public static Dictionary<string, DiffablePropertyItem<SupportAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<SupportAbility>>
        {
            ["AbilityId"] = new DiffablePropertyItem<SupportAbility, byte?>("AbilityId", (i) => i.AbilityId, delegate (SupportAbility i, byte? v)
            {
                i.AbilityId = v;
            }),
        };


        public static SupportAbility FromStructure(int id, ref SUPPORT_ABILITY_DATA @struct)
        {
            SupportAbility supportAbility = new()
            {
                Id = id,
                AbilityId = @struct.AbilityId,
            };

            return supportAbility;
        }

        public SupportAbility Clone()
        {
            return new SupportAbility
            {
                Id = Id,
                AbilityId = AbilityId,
            };
        }
    }
}
