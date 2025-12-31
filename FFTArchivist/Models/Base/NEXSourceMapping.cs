using FFTArchivist.DataSources;

namespace FFTArchivist.Models.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NEXMappingAttribute : Attribute
    {
        public Type SourceType { get; private set; }
        public string ColumnName { get; private set; }

        public NEXMappingAttribute(Type dataSourceType, string columnName)
        {
            SourceType = dataSourceType;
            ColumnName = columnName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NEXOverrideMappingAttribute : Attribute
    {
        public Type SourceType { get; private set; }
        public string ColumnName { get; private set; }

        public NEXOverrideMappingAttribute(Type dataSourceType, string columnName)
        {
            SourceType = dataSourceType;
            ColumnName = columnName;
        }
    }

    public class NEXMapping : ISourceMapping, IDestinationMapping
    {
        public ANEXDataSource DataSource { get; private set; }
        public int Id { get; private set; }
        public string ColumnName { get; private set; }
        public Type SourceType { get; set; }

        //public Type DataType { get; private set; }

        public NEXMapping()
        {
            //Debug.WriteLine($"New NEXMapping with 0 args.");
        }

        public NEXMapping(Type type)
        {
            //Debug.WriteLine($"New NEXMapping with type args.");
        }

        public NEXMapping(ANEXDataSource dataSource, int id, string columnName)
        {
            DataSource = dataSource;
            Id = id;
            ColumnName = columnName;
            SourceType = dataSource.GetType();
            //DataType = typeof(T);
        }

        public NEXMapping(Type dataSourceType, int id, string columnName)
        {
            //DataSource = dataSource;
            Id = id;
            ColumnName = columnName;
            SourceType = dataSourceType;
            //DataType = typeof(T);
        }

        //public NEXMapping(T type, ANEXDataSource dataSource, int id, string columnName)
        //{
        //    DataSource = dataSource;
        //    Id = id;
        //    ColumnName = columnName;
        //    DataType = typeof(T);
        //}
        //public NEXMapping<T> (Type dataSourceType, int id, string columnName)
        //{
        //}
        public async Task<T> ReadFromSource<T>()
        {
            if (DataSource == null)
            {
                return default;
            }

            return await DataSource.ReadData<T>(Id, ColumnName);
        }

        public async Task<T> ReadFromSource<T>(IDataSource dataSource)
        {
            return await dataSource.ReadData<T>(Id, ColumnName);
        }

        public async Task WriteToSource<T>(IDataSource dataSource, T value)
        {
            await DataSource.WriteData<T>(Id, ColumnName, value);
        }

        public async Task WriteToDestination<T>(T value)
        {
            await DataSource.WriteData<T>(Id, ColumnName, value);
        }

        public async Task WriteToDestination<T>(IDataSource dataSource, T value)
        {
            await dataSource.WriteData<T>(Id, ColumnName, value);
        }

        public void SetSource(ANEXDataSource dataSource)
        {
            DataSource = dataSource;
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
