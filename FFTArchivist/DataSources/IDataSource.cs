namespace FFTArchivist.DataSources
{
    public interface IDataSource
    {
        public Task<T> ReadData<T>(int id, string columnName);
        public Task WriteData<T>(int id, string columnName, T value);
    }
}
