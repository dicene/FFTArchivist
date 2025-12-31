using FFTArchivist.DataSources.EXE.TempInterfaces.JumpAbility;

namespace FFTArchivist.DataSources.EXE
{
    internal class JumpAbilityEXESource : AEXEDataSource<JumpAbility, JUMP_ABILITY_DATA, JumpAbilityTable>
    {
        public JumpAbilityEXESource() : base("JumpAbility.xml", "02 00 03 00 04 00 05 00 08 00 00 02 00 03 00 04 00 05 00 06 00 07 00 08", 12)
        {
        }
    }
}
