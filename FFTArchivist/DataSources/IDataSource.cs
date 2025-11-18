using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources
{
    public interface IDataSource
    {
        //public Task<T> ReadData<T>(int id, int column);
        public Task<T> ReadData<T>(int id, string columnName);
        //public Task WriteData<T>(int id, int column, T value);
        public Task WriteData<T>(int id, string columnName, T value);
    }
}
