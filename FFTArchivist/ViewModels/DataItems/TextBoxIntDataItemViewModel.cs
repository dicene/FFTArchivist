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
    internal class TextBoxIntDataItemViewModel : INotifyPropertyChanged
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

        //public TextBoxIntDataItemViewModel(string labelText)
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

        public int Value
        {
            get => dataItem?.GetValue<int>() ?? 0;
            set
            {
                if (dataItem == null)
                {
                    return;
                }

                dataItem.SetValue(Convert.ToInt32(value));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public int OriginalValue
        {
            get => dataItem?.GetOriginalValue<int>() ?? 0;
            set
            {
                if (dataItem == null)
                {
                    return;
                }

                dataItem.SetOriginalValue(Convert.ToInt32(value));
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
