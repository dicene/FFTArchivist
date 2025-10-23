using FFTArchivist.Models;
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
        public Task<bool> LoadData();
        public List<T> GetDataList<T>() where T : BaseModel;
    }
}
