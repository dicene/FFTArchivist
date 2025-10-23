using FFTArchivist.Managers;
using System.Data.Common;

namespace FFTArchivist.Models.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EXELinkageAttribute : Attribute
    {
        public long BaseOffset { get; private set; }
        public int Count { get; private set; }
        public int Size { get; private set; }
        public int ColumnOffset { get; private set; }
        public EXELinkageAttribute(long baseOffset, int count, int size, int columnOffset)
        {
            BaseOffset = baseOffset;
            Count = count;
            Size = size;
            ColumnOffset = columnOffset;
        }
    }
    public class EXELinkage<T> : ILinkage<T>
    {
        public long BaseOffset { get; private set; }
        public int Count { get; private set; }
        public int Size { get; private set; }
        public int ColumnOffset { get; private set; }

        public EXELinkage(long baseOffset, int count, int size, int columnOffset)
        {
            BaseOffset = baseOffset;
            Count = count;
            Size = size;
            ColumnOffset = columnOffset;
        }

        public async Task<T> ReadFromSource(int id)
        {
            var dataSource = await DataManager.Instance.GetDataSource(this);
            var data = await dataSource.ReadData<T>(id, ColumnOffset);
            return data;
        }

        public async Task WriteToMod()
        {
            return;
        }
    }
}
