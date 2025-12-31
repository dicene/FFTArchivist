namespace FFTArchivist.DataSources.NEX.Ability
{
    internal class AbilityNEXSource : ANEXDataSource
    {
        public string Name { get; set; }
        public AbilityNEXSource() : base("Ability.<locale>")
        {

        }
    }
}
