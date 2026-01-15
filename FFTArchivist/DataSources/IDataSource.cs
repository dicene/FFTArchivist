using System.Runtime.CompilerServices;

namespace FFTArchivist.DataSources
{
    public interface IDataSource
    {
        public int RowCount { get; }
        public void ForEachRow(Action<int> action);
        public Task<T> ReadData<T>(int id, string columnName);
        public Task WriteData<T>(int id, string columnName, T value);
    }
}
