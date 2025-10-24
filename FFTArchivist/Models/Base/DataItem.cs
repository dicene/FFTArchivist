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
        private ILinkage<T> linkage;
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

        public DataItem(ILinkage<T> linkage, int id)
        {
            this.id = id;
            this.linkage = linkage;
            Task.Run(ReadFromSource).GetAwaiter().GetResult();
        }

        public async Task ReadFromSource()
        {
            OriginalValue = await linkage.ReadFromSource(id);
            Value = OriginalValue;
        }

        public async void WriteToMod()
        {
            await linkage.WriteToMod(id, value);
        }

        public void RevertToOriginal()
        {
            Value = OriginalValue;
        }
    }
}
