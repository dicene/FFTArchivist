using FFTArchivist.DataSources;
using FFTArchivist.Managers;
using System.Data.Common;
using System.Diagnostics;
using System.Windows;

namespace FFTArchivist.Models.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NEXMappingAttribute : Attribute
    {
        public Type DataSourceType { get; private set; }
        public string ColumnName { get; private set; }

        public NEXMappingAttribute(Type dataSourceType, string columnName)
        {
            DataSourceType = dataSourceType;
            ColumnName = columnName;
        }
    }

    public class NEXMapping<T> : ISourceMapping<T>, IDestinationMapping<T>
    {
        public ANEXDataSource DataSource { get; private set; }
        public int Id { get; private set; }
        public string ColumnName { get; private set; }
        public Type DataType { get; private set; }

        public NEXMapping()
        {
            //Debug.WriteLine($"New NEXMapping with 0 args.");
        }

        public NEXMapping(Type type)
        {
            //Debug.WriteLine($"New NEXMapping with type args.");
        }

        public NEXMapping(T type, ANEXDataSource dataSource, int id, string columnName)
        {
            DataSource = dataSource;
            Id = id;
            ColumnName = columnName;
            DataType = typeof(T);
        }
        //public NEXMapping<T> (Type dataSourceType, int id, string columnName)
        //{
        //}
        public async Task<T> ReadFromSource()
        {
            if (DataSource == null)
            {
                return default;
            }

            return await DataSource.ReadData<T>(Id, ColumnName);
        }

        public async Task WriteToDestination<T>(T value)
        {
            await DataSource.WriteData<T>(Id, ColumnName, value);
        }
    }

    //public class NEXLinkage<T> : ILinkage<T>
    //{
    //    public string TableName { get; private set; }
    //    public string ColumnName { get; private set; }
    //    public IDataSource DataSource { get; private set; }

    //    public NEXLinkage(string tableName, string columnName)
    //    {
    //        TableName = tableName;
    //        ColumnName = columnName;
    //    }

    //    public async Task<T> ReadFromSource(int id)
    //    {
    //        //var dataSource = await DataManager.Instance.GetDataSource(this);

    //        //if (dataSource == null)
    //        //{
    //        //    return default;
    //        //}

    //        return await dataSource.ReadData<T>(id, ColumnName);
    //    }

    //    public async Task WriteToMod<T>(int id, T value)
    //    {
    //        var dataSource = await DataManager.Instance.GetDataSource(this);

    //        dataSource.WriteData(id, ColumnName, value);
    //        // TODO: Write with column names
    //        //Debug.WriteLine($"Wrote {} bytes to new nex file {filePath}");
    //    }
    //}
}
