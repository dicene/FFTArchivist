namespace FFTArchivist.DataSources.EXE
{
    internal interface IEXEDataSource : IDataSource
    {
        public string Path { get; }
        public long BaseOffset { get; }
        public int RowCount { get; }
        public Type RowType { get; }
        public string Pattern { get; }
        public string Filename { get; }
        public Task LoadModSource(string filename);
        public Task WriteToFile(string tablesPath);
    }
}
