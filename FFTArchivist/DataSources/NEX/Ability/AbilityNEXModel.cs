namespace FFTArchivist.DataSources.NEX.Ability
{
    internal class AbilityNEXModel
    {
        public int IconId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int JpCost1 { get; set; }
        public int JpCost2 { get; set; }
        public int IsRandomDamage { get; set; }
        public int IsRandomStatus { get; set; }
    }
}
