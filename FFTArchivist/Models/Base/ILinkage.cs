namespace FFTArchivist.Models.Base
{
    public interface ILinkage<T>
    {
        public Task<T> ReadFromSource(int id);
        public Task WriteToMod<T>(int id, T value);
    }
}
