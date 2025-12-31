using FFTArchivist.Models.Base;
using System.ComponentModel;
using System.Windows;

namespace FFTArchivist.ViewModels.DataItems
{
    internal class StringDataItemViewModel : DependencyObject, RevertableDataItemViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ADataItem dataItem;

        public ADataItem DataItem
        {
            get => dataItem;
            set
            {
                dataItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
            }
        }

        private string label;
        public string Label { 
            get => label;
            set
            {
                label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
            }
        }

        public string ValueString { get => Value; set => Value = value; }
        public string Value
        {
            get => dataItem?.GetValue<string>() ?? "";
            set
            {
                if (dataItem == null)
                {
                    return;
                }

                dataItem.SetValue(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
            }
        }

        public string OriginalValueString { get => Value; set => Value = value; }
        public string OriginalValue
        {
            get => dataItem?.IsModified == true ? $"Original Value: {dataItem?.GetOriginalValue<string>() ?? ""}" : null;
            set
            {
                if (dataItem == null)
                {
                    return;
                }

                dataItem.SetOriginalValue(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValueString)));
            }
        }

        public void Revert()
        {
            dataItem?.RevertToOriginal();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
        }
    }
}
