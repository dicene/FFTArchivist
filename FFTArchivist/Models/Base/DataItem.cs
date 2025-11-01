using FFTArchivist.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Models.Base
{

    public class DataItem<T> : INotifyPropertyChanged
    {
        private int id;
        private string columnName;
        private ISourceMapping<T> sourceMapping;
        private IDestinationMapping<T> destinationMapping;
        private IDataSource source;
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

                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                if (!Equals(this.value, this.OriginalValue))
                {
                    WriteToMod();
                }
            }
        }

        public bool IsModified => !Equals(OriginalValue, Value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public DataItem(ISourceMapping<T> sourceMapping, int id)
        {
            this.id = id;
            this.sourceMapping = sourceMapping;

            if (sourceMapping is IDestinationMapping<T> destinationMapping)
            {
                this.destinationMapping = destinationMapping;
            }

            Task.Run(ReadFromSource).GetAwaiter().GetResult();
        }

        public DataItem(ISourceMapping<T> sourceMapping, IDestinationMapping<T> destinationMapping, int id)
        {
            this.id = id;
            this.sourceMapping = sourceMapping;
            this.destinationMapping = destinationMapping;
            Task.Run(ReadFromSource).GetAwaiter().GetResult();
        }

        public async Task ReadFromSource()
        {
            OriginalValue = await sourceMapping.ReadFromSource();
            Value = OriginalValue;
        }

        public async void WriteToMod()
        {
            await destinationMapping.WriteToDestination(Value);
        }

        public void RevertToOriginal()
        {
            Value = OriginalValue;
        }
    }
}
