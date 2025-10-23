using FFTArchivist.Models.Base;
using System.CodeDom;
using System.Diagnostics;
using System.Reflection;

namespace FFTArchivist.Models
{
    public abstract class BaseModel
    {
        public int Id { get; internal set; }
        public virtual DataItem<string> Name { get; set; }
        public BaseModel(int id)
        {
            Id = id;
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>))
                {
                    var nexLinkageAttribute = prop.GetCustomAttribute<NEXLinkageAttribute>();
                    if (nexLinkageAttribute != null)
                    {
                        var linkageAttr = nexLinkageAttribute as NEXLinkageAttribute;
                        var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
                        Type genericType = typeof(NEXLinkage<>);
                        Type specificType = genericType.MakeGenericType(propertyTypeArg);
                        var args = new object[] { linkageAttr.PackName, linkageAttr.TableName, linkageAttr.Column.HasValue ? linkageAttr.Column : linkageAttr.ColumnName };
                        object newLinkage = Activator.CreateInstance(specificType, args);

                        if (newLinkage == null)
                        {
                            continue;
                        }

                        var newDataItem = Activator.CreateInstance(prop.PropertyType, newLinkage, id);

                        //newDataItem.ReadFromSource();
                        prop.SetValue(this, newDataItem);
                    }
                    //var nexLinkageAttribute = prop.GetCustomAttributes(typeof(NEXLinkageAttribute), true).FirstOrDefault();

                    var exeLinkageAttribute = prop.GetCustomAttribute<EXELinkageAttribute>();
                    if (exeLinkageAttribute != null)
                    {
                        var linkageAttr = exeLinkageAttribute as EXELinkageAttribute;
                        var propertyTypeArg = prop.PropertyType.GetGenericArguments()[0];
                        Type genericType = typeof(EXELinkage<>);
                        Type specificType = genericType.MakeGenericType(propertyTypeArg);
                        var args = new object[] { linkageAttr.BaseOffset, linkageAttr.Count, linkageAttr.Size, linkageAttr.ColumnOffset };
                        object newLinkage = Activator.CreateInstance(specificType, args);

                        if (newLinkage == null)
                        {
                            continue;
                        }

                        var newDataItem = Activator.CreateInstance(prop.PropertyType, newLinkage, id);

                        prop.SetValue(this, newDataItem);
                    }
                }
            }
        }
    }
}