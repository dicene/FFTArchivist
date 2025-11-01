namespace FFTArchivist.Models.Base
{
    public interface IDestinationMapping<T>
    {
        public Task WriteToDestination<T>(T value);
    }
}
