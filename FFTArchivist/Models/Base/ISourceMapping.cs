using FFTArchivist.DataSources;

namespace FFTArchivist.Models.Base
{
    public interface ISourceMapping
    {
        public Type SourceType { get; }
        public Task<T> ReadFromSource<T>();
        public Task<T> ReadFromSource<T>(IDataSource dataSource);
    }
}
