namespace FFTArchivist.Models.Base
{
    public interface IDestinationMapping<T>
    {
        public Task WriteToMod<T>(T value);
    }
}
