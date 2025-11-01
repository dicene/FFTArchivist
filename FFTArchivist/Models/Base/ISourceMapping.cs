namespace FFTArchivist.Models.Base
{
    public interface ISourceMapping<T>
    {
        public Task<T> ReadFromSource();
    }
}
