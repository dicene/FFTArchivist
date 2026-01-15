using FFTArchivist.DataSources;
using System.ComponentModel;

namespace FFTArchivist.Models.Base
{
    public abstract class ADataItem : INotifyPropertyChanged
    {
        public Type Type { get; init; }
        private string columnName;
        public string ColumnName { get => columnName; set => columnName = value; }
        public string ToolTipString { get; }
        public ISourceMapping SourceMapping { get; set; }
        public ISourceMapping SourceOverrideMapping { get; set; }
        public IDestinationMapping DestinationMapping { get; set; }
        public IDataSource originalSource { get; set; }
        public IDataSource modSource { get; set; }
        public IDataSource originalOverrideSource { get; set; }
        public IDataSource modOverrideSource { get; set; }

        public virtual bool IsModified { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract Task ReadFromOriginalSource();
        public abstract Task ReadFromModSource();
        public abstract Task ReadFromOriginalOverrideSource();
        public abstract Task ReadFromModOverrideSource();
        public abstract void SetOriginalSource(IDataSource dataSource);
        public abstract void SetModSource(IDataSource dataSource);
        public abstract void SetOriginalOverrideSource(IDataSource dataSource);
        public abstract void SetModOverrideSource(IDataSource dataSource);
        public abstract void RevertToOriginal();
        public abstract T? GetValue<T>();
        public abstract T? GetRevertValue<T>();
        public abstract T? GetOriginalValue<T>();
        public abstract object GetValue();
        public abstract void SetValue<T>(T newvalue);
        public abstract void SetRevertValue<T>(T newvalue);
        public abstract void SetOriginalValue<T>(T newvalue);
    }
}
