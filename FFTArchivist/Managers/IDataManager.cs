using FFTArchivist.DataSources;
using FFTArchivist.Models.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Managers
{
    public interface IDataManager
    {
        public static IDataManager Instance { get; }
        public Dictionary<Type, IEnumerable<BaseModel>> DataLists { get; }
        string FFTBasePath { get; }
        string FFTExecutablePath { get; }
        string DataFolderPath { get; }

        public Task OpenPack(string packDirectory);
        public Task ClosePack();
        public Task<bool> LoadData();
        public List<T> GetDataList<T>() where T : BaseModel;
        public Task LoadDataSources();
        public IDataSource GetDataSource(Type type);
    }
}
