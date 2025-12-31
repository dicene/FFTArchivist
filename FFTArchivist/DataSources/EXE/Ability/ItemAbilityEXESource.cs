using FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility;

namespace FFTArchivist.DataSources.EXE
{
    internal class ItemAbilityEXESource : AEXEDataSource<ItemAbility, ITEM_ABILITY_DATA, ItemAbilityTable>
    {
        public ItemAbilityEXESource() : base("ItemAbility.xml", "F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD", 14)
        {
        }
    }
}
