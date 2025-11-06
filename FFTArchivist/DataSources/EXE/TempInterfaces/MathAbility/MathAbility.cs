using FFTArchivist.DataSources.EXE.TempInterfaces;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility
{
    public class MathAbility : DiffableModelBase<MathAbility>, IDiffableModel<MathAbility>, IIdentifiableModel
    {
        public int Id { get; set; }
        public byte? Key { get; set; }
        public byte? Value { get; set; }

        public static Dictionary<string, DiffablePropertyItem<MathAbility>> PropertyMap { get; } = new Dictionary<string, DiffablePropertyItem<MathAbility>>
        {
            ["Key"] = new DiffablePropertyItem<MathAbility, byte?>("Key", (i) => i.Key, delegate (MathAbility i, byte? v)
            {
                i.Key = v;
            }),

            ["Value"] = new DiffablePropertyItem<MathAbility, byte?>("Value", (i) => i.Value, delegate (MathAbility i, byte? v)
            {
                i.Value = v;
            }),
        };


        public static MathAbility FromStructure(int id, ref MATH_ABILITY_DATA @struct)
        {
            MathAbility MathAbility = new()
            {
                Id = id,
                Key = (byte?)(@struct.Flags & 0xF0),
                Value = (byte?)(@struct.Flags & 0x0F),
            };

            return MathAbility;
        }

        public MathAbility Clone()
        {
            return new MathAbility
            {
                Id = Id,
                Key = Key,
                Value = Value,
            };
        }
    }
}
