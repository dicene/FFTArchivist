using FFTArchivist.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FFTArchivist.ViewModels.DataItems
{
    internal class TextBoxStringDataItemViewModel : INotifyPropertyChanged
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
            }
        }

        //public TextBoxStringDataItemViewModel(string labelText)
        //{
        //    Label = labelText;
        //}

        private string label = "label";
        public string Label
        {
            get => label + ":";
            set
            {
                if (label == value)
                {
                    return;
                }

                label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
            }
        }

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

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
            }
        }

        public void Revert()
        {
            dataItem?.RevertToOriginal();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }
}
