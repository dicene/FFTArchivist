using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;

namespace FFTArchivist.DataSources.EXE
{
    internal class ItemEXESource : AEXEDataSource<Item, ITEM_COMMON_DATA, ItemTable>
    {
        public ItemEXESource() : base("ItemData.xml", "00 00 00 80 00 00 00 00 00 00 00 00 00 01 01 80 01 01 00 00 64 00 01 00 00 02 03 80 02 01 00 00", 256)
        {
        }
    }
}
