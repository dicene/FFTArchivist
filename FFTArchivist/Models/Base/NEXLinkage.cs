using FFTArchivist.Managers;
using System.Data.Common;
using System.Windows;

namespace FFTArchivist.Models.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NEXLinkageAttribute : Attribute
    {
        public string PackName { get; private set; }
        public string TableName { get; private set; }
        public int? Column { get; private set; }
        public string ColumnName { get; private set; }
        public NEXLinkageAttribute(string packName, string tableName, int column)
        {
            PackName = packName;
            TableName = tableName;
            Column = column;
        }
        public NEXLinkageAttribute(string packName, string tableName, string columnName)
        {
            PackName = packName;
            TableName = tableName;
            ColumnName = columnName;
        }
    }

    public class NEXLinkage<T> : ILinkage<T>
    {
        public string PackName { get; private set; }
        public string TableName { get; private set; }
        public int? Column { get; private set; }
        public string ColumnName { get; private set; }

        public NEXLinkage(string packName, string tableName, int column)
        {
            PackName = packName;
            TableName = tableName;
            Column = column;
        }
        public NEXLinkage(string packName, string tableName, string columnName)
        {
            PackName = packName;
            TableName = tableName;
            ColumnName = columnName;
        }

        public async Task<T> ReadFromSource(int id)
        {
            var dataSource = await DataManager.Instance.GetDataSource(this);
            if (Column.HasValue)
            {
                return await dataSource.ReadData<T>(id, Column.Value);
            }
            else if (ColumnName != null)
            {
                return await dataSource.ReadData<T>(id, ColumnName);
            }

            return default;
        }

        public async Task WriteToMod<T>(int id, T value)
        {
            var dataSource = await DataManager.Instance.GetDataSource(this);

            if (Column.HasValue)
            {
                dataSource.WriteData(id, Column.Value, value);
            }
            else
            {
                dataSource.WriteData(id, ColumnName, value);
                // TODO: Write with column names
            }
            //Debug.WriteLine($"Wrote {} bytes to new nex file {filePath}");
        }
    }
}
