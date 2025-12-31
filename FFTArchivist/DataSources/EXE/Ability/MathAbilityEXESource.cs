using FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility;

namespace FFTArchivist.DataSources.EXE
{
    internal class MathAbilityEXESource : AEXEDataSource<MathAbility, MATH_ABILITY_DATA, MathAbilityTable>
    {
        public MathAbilityEXESource() : base("MathAbility.xml", "80 40 20 10 08 04 02 01", 8)
        {
        }
    }
}
