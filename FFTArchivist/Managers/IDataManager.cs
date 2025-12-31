using FFTArchivist.DataSources;
using FFTArchivist.Models.Base;

namespace FFTArchivist.Managers
{
    public interface IDataManager
    {
        public static IDataManager Instance { get; }
        public Dictionary<Type, IEnumerable<BaseModel>> DataLists { get; }
        string FFTBasePath { get; }
        string FFTExecutablePath { get; }
        string DataFolderPath { get; }

        public Task<bool> OpenPack(string packDirectory);
        public Task<bool> ClosePack();
        public Task<bool> LoadData();
        public List<T> GetDataList<T>() where T : BaseModel;
        public Task<bool> LoadDataSources();
        public IDataSource GetDataSource(Type type);
        public void ClearDataSources();
    }
}
