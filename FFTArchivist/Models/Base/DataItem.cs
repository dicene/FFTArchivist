using FFTArchivist.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Models.Base
{
    public abstract class ADataItem : INotifyPropertyChanged
    {
        private string columnName;
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
        public abstract T GetValue<T>();
        public abstract T GetOriginalValue<T>();
        public abstract object GetValue();
        public abstract void SetValue<T>(T newvalue);
        public abstract void SetOriginalValue<T>(T newvalue);
    }

    public class DataItem<T> : ADataItem
    {
        private int id;
        public T OriginalValue { get; private set; }

        private T value;
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                if (Equals(this.value, value))
                    return;

                if (this.value != null)
                {
                    //Debug.WriteLine($"Changing value of {GetType().Name} from ({this.value}) to ({value})");
                    WriteToMod();
                }

                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public override bool IsModified => !Equals(OriginalValue, Value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public DataItem(ISourceMapping sourceMapping, int id)
        {
            this.id = id;
            this.SourceMapping = sourceMapping;

            if (sourceMapping is IDestinationMapping destinationMapping)
            {
                this.DestinationMapping = destinationMapping;
            }

            //Task.Run(() => ReadFromSource(dataSource)).GetAwaiter().GetResult();
            //Task.Run(async (id) => return await ReadFromSource()})
            //Task.Run(ReadFromSource).GetAwaiter().GetResult();
        }

        public DataItem(ISourceMapping sourceMapping, IDestinationMapping destinationMapping, int id)
        {
            this.id = id;
            this.SourceMapping = sourceMapping;
            this.DestinationMapping = destinationMapping;
            //Task.Run(ReadFromSource).GetAwaiter().GetResult();
        }

        public override async Task ReadFromOriginalSource()
        {
            if (OriginalValue == null)
            {
                OriginalValue = await SourceMapping.ReadFromSource<T>(originalSource);
                value = OriginalValue;
            }
        }

        public override async Task ReadFromModSource()
        {
            Value = await SourceMapping.ReadFromSource<T>(modSource);
        }

        public override async Task ReadFromOriginalOverrideSource()
        {
            OriginalValue = await SourceMapping.ReadFromSource<T>(originalOverrideSource);
            Value = OriginalValue;
        }

        public override async Task ReadFromModOverrideSource()
        {
            Value = await SourceMapping.ReadFromSource<T>(modOverrideSource);
        }

        public async void WriteToMod()
        {
            await DestinationMapping.WriteToDestination(originalSource, Value);
            //await DestinationMapping.WriteToDestination(Value);
        }

        public override void RevertToOriginal()
        {
            Value = OriginalValue;
        }

        public override T1 GetValue<T1>()
        {
            return Value is T1 castedValue ? castedValue : throw new InvalidCastException($"Cannot cast value of type {typeof(T)} to {typeof(T1)}");
        }

        public override T1 GetOriginalValue<T1>()
        {
            return OriginalValue is T1 castedOriginalValue ? castedOriginalValue : throw new InvalidCastException($"Cannot cast original value of type {typeof(T)} to {typeof(T1)}");
        }

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue<T1>(T1 newValue)
        {
            if (typeof(T) == typeof(T1) && newValue is T castNewValue)
            {
                Value = castNewValue;
            }
            else
            {
                throw new InvalidCastException($"Cannot cast value of type {typeof(T1)} to {typeof(T)}");
            }            
        }

        public override void SetOriginalValue<T1>(T1 newOriginalValue)
        {
            if (typeof(T) == typeof(T1) && newOriginalValue is T castNewOriginalValue)
            {
                OriginalValue = castNewOriginalValue;
            }
            else
            {
                throw new InvalidCastException($"Cannot cast originalvalue of type {typeof(T1)} to {typeof(T)}");
            }
        }

        public override void SetOriginalSource(IDataSource dataSource)
        {
            originalSource = dataSource;
        }

        public override void SetModSource(IDataSource dataSource)
        {
            modSource = dataSource;
        }

        public override void SetOriginalOverrideSource(IDataSource dataSource)
        {
            originalOverrideSource = dataSource;
        }

        public override void SetModOverrideSource(IDataSource dataSource)
        {
            modOverrideSource = dataSource;
        }
    }
}
