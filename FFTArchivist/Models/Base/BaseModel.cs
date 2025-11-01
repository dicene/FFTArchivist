using FFTArchivist.Managers;
using System.CodeDom;
using System.Diagnostics;
using System.Reflection;

namespace FFTArchivist.Models.Base
{
    public abstract class BaseModel
    {
        public int Id { get; internal set; }

        public BaseModel()
        {

        }

        public async Task ReadData()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>))
                {
                    var nexLinkageAttribute = prop.GetCustomAttribute<NEXMappingAttribute>();

                    if (nexLinkageAttribute != null)
                    {
                        //Debug.WriteLine($"{GetType().Name}: {prop.Name}: {nexLinkageAttribute.DataSourceType.Name} - {nexLinkageAttribute.ColumnName}");
                        if (DataManager.Instance.DataSources.TryGetValue(nexLinkageAttribute.DataSourceType, out var source))
                        {
                            //source.ReadData<>
                        }

                        //source.ReadData<string>(prop., nexLinkageAttribute.ColumnName);
                        //Debug.WriteLine($"");
                    }
                }
            }
        }

        public BaseModel(int id)
        {
            Id = id;

            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>))
                {
                    var nexMappingAttribute = prop.GetCustomAttribute<NEXMappingAttribute>();

                    if (nexMappingAttribute != null)
                    {
                        //Debug.WriteLine($"{GetType().Name}: {prop.Name}: {nexMappingAttribute.DataSourceType.Name} - {nexMappingAttribute.ColumnName}");

                        var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
                        Type genericType = typeof(NEXMapping<>);
                        Type specificType = genericType.MakeGenericType(propertyTypeArg);
                        var args = new object[] { };
                        //var args = new object[] { specificType, null, Id, nexLinkageAttribute.ColumnName };
                        object newMapping = Activator.CreateInstance(specificType, args);

                        if (newMapping == null)
                        {
                            continue;
                        }

                        var dataSource = DataManager.Instance.GetDataSource(nexMappingAttribute.DataSourceType);
                        var dataSourceProp = specificType.GetProperty("DataSource");
                        var columnNameProp = specificType.GetProperty("ColumnName");
                        var idProp = specificType.GetProperty("Id");
                        var dataTypeProp = specificType.GetProperty("DataType");

                        //var dataSourceProp = genericType.GetProperty("DataSource");
                        //var columnNameProp = genericType.GetProperty("ColumnName");
                        //var idProp = genericType.GetProperty("Id");

                        //var data
                        dataSourceProp.SetValue(newMapping, dataSource);
                        columnNameProp.SetValue(newMapping, nexMappingAttribute.ColumnName);
                        idProp.SetValue(newMapping, Id);
                        dataTypeProp.SetValue(newMapping, propertyTypeArg);

                        var newDataItem = Activator.CreateInstance(prop.PropertyType, newMapping, id);




                        //newDataItem.ReadFromSource();
                        prop.SetValue(this, newDataItem);

                        //if (DataManager.Instance.DataSources.TryGetValue(nexLinkageAttribute.DataSourceType, out var source))
                        //{
                        //    //source.ReadData<>
                        //}

                        //source.ReadData<string>(prop., nexLinkageAttribute.ColumnName);
                        //Debug.WriteLine($"");
                    }
                }
            }
            //foreach (var prop in GetType().GetProperties())
            //{
            //    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>))
            //    {
            //        var nexLinkageAttribute = prop.GetCustomAttribute<NEXLinkageAttribute>();
            //        if (nexLinkageAttribute != null)
            //        {
            //            var linkageAttr = nexLinkageAttribute as NEXLinkageAttribute;
            //            var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
            //            Type genericType = typeof(NEXLinkage<>);
            //            Type specificType = genericType.MakeGenericType(propertyTypeArg);
            //            var args = new object[] { linkageAttr.PackName, linkageAttr.TableName, linkageAttr.Column.HasValue ? linkageAttr.Column : linkageAttr.ColumnName };
            //            object newLinkage = Activator.CreateInstance(specificType, args);

            //            if (newLinkage == null)
            //            {
            //                continue;
            //            }

            //            var newDataItem = Activator.CreateInstance(prop.PropertyType, newLinkage, id);

            //            //newDataItem.ReadFromSource();
            //            prop.SetValue(this, newDataItem);
            //        }
            //        //var nexLinkageAttribute = prop.GetCustomAttributes(typeof(NEXLinkageAttribute), true).FirstOrDefault();

            //        var exeLinkageAttribute = prop.GetCustomAttribute<EXELinkageAttribute>();
            //        if (exeLinkageAttribute != null)
            //        {
            //            var linkageAttr = exeLinkageAttribute as EXELinkageAttribute;
            //            var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
            //            Type genericType = typeof(EXELinkage<>);
            //            Type specificType = genericType.MakeGenericType(propertyTypeArg);
            //            var args = new object[] { linkageAttr.BaseOffset, linkageAttr.Count, linkageAttr.Size, linkageAttr.ColumnOffset };
            //            object newLinkage = Activator.CreateInstance(specificType, args);

            //            if (newLinkage == null)
            //            {
            //                continue;
            //            }

            //            var newDataItem = Activator.CreateInstance(prop.PropertyType, newLinkage, id);

            //            prop.SetValue(this, newDataItem);
            //        }
            //    }
            //}
        }
    }
}