using FFTArchivist.DataSources;

namespace FFTArchivist.Models.Base
{
    public interface IDestinationMapping
    {
        public Task WriteToDestination<T>(IDataSource dataSource, T value);
    }
}
