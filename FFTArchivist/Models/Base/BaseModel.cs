using FFTArchivist.DataSources;
using System.Reflection;
using System.Windows.Markup;

namespace FFTArchivist.Models.Base
{
    public abstract class BaseModel
    {
        public int Id { get; internal set; }

        public virtual Dictionary<string, ADataItem> DataItems { get; internal set; } = new Dictionary<string, ADataItem>();

        public BaseModel()
        {
            
        }

        //public async Task ReadDataFromOriginalSource(IDataSource dataSource)
        //{
        //    foreach (var (itemName, dataItem) in DataItems)
        //    {
        //        await dataItem.ReadFromOriginalSource();
        //    }
        //}

        public void SetOriginalSources(Dictionary<Type, IDataSource> DataSources)
        {
            foreach (var (itemName, dataItem) in DataItems)
            {
                var sourceType = dataItem.SourceMapping.SourceType;
                if (sourceType != null && DataSources.TryGetValue(sourceType, out var source))
                {
                    dataItem.SetOriginalSource(source);
                }
                //var mapping = dataItem.Mapp;
                //if (DataSources.TryGetValue(mapping.DataSourceType, out var source))
                //{
                //    mapping.DataSource = source;
                //}
            }
        }

        public async Task ReadData()
        {
            foreach (var (name, dataItem) in DataItems)
            {
                dataItem.ReadFromOriginalSource();
            }
            //foreach (var prop in GetType().GetProperties())
            //{
            //    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>))
            //    {
            //        var nexLinkageAttribute = prop.GetCustomAttribute<NEXMappingAttribute>();

            //        if (nexLinkageAttribute != null)
            //        {
            //            //Debug.WriteLine($"{GetType().Name}: {prop.Name}: {nexLinkageAttribute.DataSourceType.Name} - {nexLinkageAttribute.ColumnName}");
            //            if (DataManager.Instance.DataSources.TryGetValue(nexLinkageAttribute.DataSourceType, out var source))
            //            {
            //                //source.ReadData<>
            //            }

            //            //source.ReadData<string>(prop., nexLinkageAttribute.ColumnName);
            //            //Debug.WriteLine($"");
            //        }

            //        var exeSourceMappingAttribute = prop.GetCustomAttribute<EXESourceMappingAttribute>();

            //        if (exeSourceMappingAttribute != null)
            //        {
            //            //Debug.WriteLine($"{GetType().Name}: {prop.Name}: {nexLinkageAttribute.DataSourceType.Name} - {nexLinkageAttribute.ColumnName}");
            //            if (DataManager.Instance.DataSources.TryGetValue(exeSourceMappingAttribute.DataSourceType, out var source))
            //            {
            //                //source.ReadData<>
            //            }

            //            //source.ReadData<string>(prop., nexLinkageAttribute.ColumnName);
            //            //Debug.WriteLine($"");
            //        }
            //    }
            //}
        }

        public BaseModel(int id)
        {
            Id = id;

            //foreach (var dataItem in DataItems)
            //{
            //    dataItem.Value.ReadFromSource();
            //}

            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>))
                {
                    var nexMappingAttribute = prop.GetCustomAttribute<NEXMappingAttribute>();

                    if (nexMappingAttribute != null)
                    {
                        //Debug.WriteLine($"{GetType().Name}: {prop.Name}: {nexMappingAttribute.DataSourceType.Name} - {nexMappingAttribute.ColumnName}");

                        var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
                        Type genericType = typeof(NEXMapping);
                        //Type genericType = typeof(NEXMapping<>);
                        //Type specificType = genericType.MakeGenericType(propertyTypeArg);
                        var args = new object[] { };
                        //var args = new object[] { specificType, null, Id, nexLinkageAttribute.ColumnName };
                        object newMapping = Activator.CreateInstance(genericType, args);
                        //object newMapping = Activator.CreateInstance(specificType, args);

                        if (newMapping == null)
                        {
                            continue;
                        }

                        //////var dataSource = DataManager.Instance.GetDataSource(nexMappingAttribute.DataSourceType);
                        //////var dataSourceProp = specificType.GetProperty("DataSource");
                        //var columnNameProp = specificType.GetProperty("ColumnName");
                        //var idProp = specificType.GetProperty("Id");
                        //var dataTypeProp = specificType.GetProperty("DataType");

                        var columnNameProp = genericType.GetProperty("ColumnName");
                        var idProp = genericType.GetProperty("Id");
                        var sourceType = genericType.GetProperty("SourceType");
                        //var dataTypeProp = genericType.GetProperty("DataType");

                        //var dataSourceProp = genericType.GetProperty("DataSource");
                        //var columnNameProp = genericType.GetProperty("ColumnName");
                        //var idProp = genericType.GetProperty("Id");

                        //var data
                        //////dataSourceProp.SetValue(newMapping, dataSource);
                        columnNameProp.SetValue(newMapping, nexMappingAttribute.ColumnName);
                        idProp.SetValue(newMapping, Id);
                        sourceType.SetValue(newMapping, nexMappingAttribute.SourceType);
                        //dataTypeProp.SetValue(newMapping, propertyTypeArg);

                        var newDataItem = Activator.CreateInstance(prop.PropertyType, newMapping, id);

                        //newDataItem.ReadFromSource();
                        prop.SetValue(this, newDataItem);

                        DataItems.Add(prop.Name, (ADataItem)newDataItem);

                        //if (DataManager.Instance.DataSources.TryGetValue(nexLinkageAttribute.DataSourceType, out var source))
                        //{
                        //    //source.ReadData<>
                        //}

                        //source.ReadData<string>(prop., nexLinkageAttribute.ColumnName);
                        //Debug.WriteLine($"");
                    }

                    var exeSourceMappingAttribute = prop.GetCustomAttribute<EXESourceMappingAttribute>();

                    if (exeSourceMappingAttribute != null)
                    {
                        //Debug.WriteLine($"{GetType().Name}: {prop.Name}: {nexMappingAttribute.DataSourceType.Name} - {nexMappingAttribute.ColumnName}");

                        var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
                        Type specificType = typeof(EXESourceMapping);
                        //Type genericType = typeof(EXESourceMapping<>);
                        //Type specificType = genericType.MakeGenericType(propertyTypeArg);
                        var args = new object[] { };
                        //var args = new object[] { specificType, Id, exeSourceMappingAttribute.PropertyName };
                        //EXESourceMapping(Type t, AEXEDataSource dataSource, int id, string propertyName)
                        object newMapping = Activator.CreateInstance(specificType, args);

                        if (newMapping == null || newMapping is not EXESourceMapping newEXESourceMapping)
                        {
                            continue;
                        }

                        //////var dataSource = DataManager.Instance.GetDataSource(exeSourceMappingAttribute.DataSourceType);

                        //var dataSourceProp = specificType.GetProperty("DataSource");
                        //var propertyNameProp = specificType.GetProperty("PropertyName");
                        //var idProp = specificType.GetProperty("Id");

                        //////var dataTypeProp = specificType.GetProperty("DataType");

                        //var dataSourceProp = genericType.GetProperty("DataSource");
                        //var columnNameProp = genericType.GetProperty("ColumnName");
                        //var idProp = genericType.GetProperty("Id");

                        //var data
                        //////dataSourceProp.SetValue(newMapping, dataSource);

                        //propertyNameProp.SetValue(newMapping, exeSourceMappingAttribute.PropertyName);
                        //idProp.SetValue(newMapping, Id);
                        newEXESourceMapping.PropertyName = exeSourceMappingAttribute.PropertyName;
                        newEXESourceMapping.Id = Id;
                        newEXESourceMapping.SourceType = exeSourceMappingAttribute.DataSourceType;

                        //////dataTypeProp.SetValue(newMapping, propertyTypeArg);

                        var newDataItem = Activator.CreateInstance(prop.PropertyType, newMapping, id);

                        //newDataItem.ReadFromSource();
                        prop.SetValue(this, newDataItem);

                        DataItems.Add(prop.Name, (ADataItem)newDataItem);

                        //if (DataManager.Instance.DataSources.TryGetValue(nexLinkageAttribute.DataSourceType, out var source))
                        //{
                        //    //source.ReadData<>
                        //}

                        //source.ReadData<string>(prop., nexLinkageAttribute.ColumnName);
                        //Debug.WriteLine($"");
                    }
                }
            }
        }
    }
}