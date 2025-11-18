using FFTArchivist.DataSources;
using FFTArchivist.DataSources.EXE;
using FFTArchivist.Managers;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using Vortice.Direct3D12;

namespace FFTArchivist.Models.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EXESourceMappingAttribute : Attribute
    {
        public Type DataSourceType { get; private set; }
        public string PropertyName { get; private set; }
        public EXESourceMappingAttribute(Type dataSourceType, string propertyName)
        {
            DataSourceType = dataSourceType;
            PropertyName = propertyName;
        }
    }
    public class EXESourceMapping : ISourceMapping, IDestinationMapping
    {
        public int Id { get; set; }
        public long BaseOffset { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public int ColumnOffset { get; set; }
        public IDataSource DataSource { get; set; }
        public Type DataType { get; set; }
        public string PropertyName { get; set; }
        public Type SourceType { get; set; }

        public EXESourceMapping() { }

        public EXESourceMapping(IDataSource dataSource, int id, string propertyName)
        {
            DataSource = dataSource;
            Id = id;
            PropertyName = propertyName;
            SourceType = dataSource.GetType();
            //DataType = typeof(T);
        }

        public EXESourceMapping(Type t, IDataSource dataSource, int id, string propertyName)
        {
            DataSource = dataSource;
            Id = id;
            PropertyName = propertyName;
            SourceType = dataSource.GetType();
            //DataType = typeof(T);
            //BaseOffset = baseOffset;
            //Count = count;
            //Size = size;
            //ColumnOffset = columnOffset;
        }

        public EXESourceMapping(Type t, int id, string propertyName)
        {
            //DataSource = dataSource;
            Id = id;
            PropertyName = propertyName;
            SourceType = t;
            //DataType = typeof(T);
            //BaseOffset = baseOffset;
            //Count = count;
            //Size = size;
            //ColumnOffset = columnOffset;
        }

        public EXESourceMapping(long baseOffset, int count, int size, int columnOffset)
        {
            BaseOffset = baseOffset;
            Count = count;
            Size = size;
            ColumnOffset = columnOffset;
        }

        public async Task<T> ReadFromSource<T>()
        {
            //var dataSource = await DataManager.Instance.GetDataSource(this);

            //if (dataSource == null)
            //{
            //    return default(T);
            //}

            //var data = await dataSource.ReadData<T>(id, ColumnOffset);
            //return data;
            if (DataSource == null)
            {
                return default;
            }

            return await DataSource.ReadData<T>(Id, PropertyName);
        }

        public async Task<T> ReadFromSource<T>(IDataSource dataSource)
        {
            return await dataSource.ReadData<T>(Id, PropertyName);
        }

        public async Task<T> ReadFromModSource<T>(IDataSource dataSource)
        {
            return await dataSource.ReadData<T>(Id, PropertyName);
        }

        public async Task WriteToMod<T>(T value)
        {
            return;
        }

        public async Task WriteToDestination<T>(IDataSource dataSource, T value)
        {
            //Debug.WriteLine($"Attempting to write via EXESourceMapping...");

            if (dataSource == null)
            {
                return;
            }

            var dataSourceType = dataSource.GetType().BaseType;
            //if (DataSource.GetType().GetGenericTypeDefinition() == typeof(AEXEDataSource<>))

            if (dataSource is IDataSource iDataSource)
            {
                dataSource.WriteData<T>(Id, PropertyName, value);
            }

            if (dataSourceType.IsGenericType && dataSourceType.GetGenericTypeDefinition() == typeof(AEXEDataSource<,,>))
            {
                var propertyClassArg = dataSourceType.GetGenericArguments()[0];
                var propertyStructArg = dataSourceType.GetGenericArguments()[1];
                var propertyTableArg = dataSourceType.GetGenericArguments()[2];
                Type genericType = typeof(AEXEDataSource<,,>);
                Type specificType = genericType.MakeGenericType(propertyClassArg, propertyStructArg, propertyTableArg);

                var writeMethod = specificType.GetMethods().FirstOrDefault(m => m.Name == "WriteData");

                var genericWriteMethod = writeMethod.MakeGenericMethod(typeof(T));

                var args = new object[] { Id, PropertyName, value };
                //var args = new object[] { specificType, null, Id, nexLinkageAttribute.ColumnName };
                genericWriteMethod.Invoke(dataSource, args);
                //object newMapping = Activator.CreateInstance(specificType, args);
            }

            return;
        }
    }
}
