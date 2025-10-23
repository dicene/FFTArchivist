using FFTArchivist.Managers;

namespace FFTArchivist.Models.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NEXLinkageAttribute : Attribute
    {
        public string PackName { get; private set; }
        public string TableName { get; private set; }
        public int Column { get; private set; }
        public NEXLinkageAttribute(string packName, string tableName, int column)
        {
            PackName = packName;
            TableName = tableName;
            Column = column;
        }
    }

    public class NEXLinkage<T> : ILinkage<T>
    {
        public string PackName { get; private set; }
        public string TableName { get; private set; }
        public int Column { get; private set; }

        public NEXLinkage(string packName, string tableName, int column)
        {
            PackName = packName;
            TableName = tableName;
            Column = column;
        }

        public async Task<T> ReadFromSource(int id)
        {
            var dataSource = await DataManager.Instance.GetDataSource(this);
            var data = await dataSource.ReadData<T>(id, Column);
            return data;
        }

        public async Task WriteToMod()
        {
            return;
        }
    }
}
