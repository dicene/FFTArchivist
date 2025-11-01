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
    public class EXESourceMapping<T> : ISourceMapping<T>, IDestinationMapping<T>
    {
        public int Id { get; private set; }
        public long BaseOffset { get; private set; }
        public int Count { get; private set; }
        public int Size { get; private set; }
        public int ColumnOffset { get; private set; }
        public IDataSource DataSource { get; private set; }
        public Type DataType { get; private set; }
        public string PropertyName { get; private set; }

        public EXESourceMapping() { }
        public EXESourceMapping(Type t, IDataSource dataSource, int id, string propertyName)
        {
            DataSource = dataSource;
            Id = id;
            PropertyName = propertyName;
            DataType = typeof(T);
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

        public async Task<T> ReadFromSource()
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

        public async Task WriteToMod<T>(T value)
        {
            return;
        }

        public async Task WriteToDestination<T>(T value)
        {
            Debug.WriteLine($"Attempting to write via EXESourceMapping...");

            if (DataSource == null)
            {
                return;
            }

            var dataSourceType = DataSource.GetType().BaseType;
            //if (DataSource.GetType().GetGenericTypeDefinition() == typeof(AEXEDataSource<>))
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
                genericWriteMethod.Invoke(DataSource, args);
                //object newMapping = Activator.CreateInstance(specificType, args);
            }

            return;
        }
    }
}
