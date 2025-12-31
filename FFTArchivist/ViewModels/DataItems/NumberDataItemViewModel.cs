using FFTArchivist.Controls.TextBox;
using FFTArchivist.Models.Base;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Windows;

namespace FFTArchivist.ViewModels.DataItems
{
    internal class NumberDataItemViewModel<T> : DependencyObject, RevertableDataItemViewModel, INotifyPropertyChanged where T: INumber<T>
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string DataItemSourceName { get; set; }

        public string CreatedBy { get; set; } = "Constructor";

        private ADataItem dataItem;

        public ADataItem DataItem
        {
            get => dataItem;
            set
            {
                dataItem = value;

                if (dataItem?.SourceMapping is EXESourceMapping exeSourceMapping)
                {
                    DataItemSourceName = exeSourceMapping.PropertyName;
                }
                else if (dataItem?.SourceMapping is NEXMapping nexMapping)
                {
                    DataItemSourceName = nexMapping.ColumnName;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayAsHex)));
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

        //private bool displayAsHex = false;
        //public bool DisplayAsHex
        //{
        //    get => displayAsHex;
        //    set
        //    {
        //        if (displayAsHex == value)
        //        {
        //            return;
        //        }
        //        displayAsHex = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayAsHex)));
        //    }
        //}

        public bool DisplayAsHex
        {
            get { return (bool)GetValue(DisplayAsHexProperty); }
            set
            {
                SetValue(DisplayAsHexProperty, value);
                //var context = DataContext as IntDataItemViewModel;
                //context.DisplayAsHex = value;
                //DataItemViewModel.DisplayAsHex = value;
            }
        }

        public static readonly DependencyProperty DisplayAsHexProperty =
            DependencyProperty.Register("DisplayAsHex", typeof(bool), typeof(NumberDataItemViewModel<T>), new PropertyMetadata(false, OnDisplayAsHexChanged));

        private static void OnDisplayAsHexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumberDataItemViewModel<T>) d;
            Debug.WriteLine($"OnDisplayAsHexChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue}");
            //var context = control.DataContext as IntDataItemViewModel;
            //context.DisplayAsHex = (bool)e.NewValue;
            //control.DataItemViewModel.DisplayAsHex = (bool)e.NewValue;
            //Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue} {d.GetValue(MaxLengthProperty)}");
        }

        public string ValueString
        {
            get
            {
                if (dataItem == null)
                {
                    return default;
                }

                if (DisplayAsHex)
                {
                    return $"{dataItem.GetValue<T>():X}";
                }

                return dataItem.GetValue<T>().ToString();
            }

            set
            {
                if (dataItem == null)
                {
                    return;
                }

                if (typeof(T) == typeof(int))
                {
                    dataItem.SetValue(Convert.ToInt32(value, DisplayAsHex ? 16 : 10));
                }
                else if (typeof(T) == typeof(byte))
                {
                    dataItem.SetValue(Convert.ToByte(value, DisplayAsHex ? 16 : 10));
                }
                else if (typeof(T) == typeof(ushort))
                {
                    dataItem.SetValue(Convert.ToUInt16(value, DisplayAsHex ? 16 : 10));
                }
                else
                {
                    Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                    //dataItem.SetValue(Convert.ToInt32(value));
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public T? Value
        {
            get => dataItem == null ? default : dataItem.GetValue<T>();

            set
            {
                if (dataItem == null)
                {
                    return;
                }

                if (typeof(T) == typeof(int))
                {
                    dataItem.SetValue(Convert.ToInt32(value));
                }
                else if (typeof(T) == typeof(byte))
                {
                    dataItem.SetValue(Convert.ToByte(value));
                }
                else if (typeof(T) == typeof(ushort))
                {
                    dataItem.SetValue(Convert.ToUInt16(value));
                }
                else
                {
                    Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public string OriginalValueString
        {
            get
            {
                if (dataItem == null)
                {
                    return default;
                }

                if (DisplayAsHex)
                {
                    return $"{dataItem.GetOriginalValue<T>():X}";
                }

                return dataItem.GetOriginalValue<T>().ToString();
            }

            set
            {
                if (dataItem == null)
                {
                    return;
                }

                if (typeof(T) == typeof(int))
                {
                    dataItem.SetOriginalValue(Convert.ToInt32(value));
                }
                else if (typeof(T) == typeof(byte))
                {
                    dataItem.SetOriginalValue(Convert.ToByte(value));
                }
                else if (typeof(T) == typeof(ushort))
                {
                    dataItem.SetOriginalValue(Convert.ToUInt16(value));
                }
                else
                {
                    Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                    //dataItem.SetOriginalValue(Convert.ToInt32(value));
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValue)));
            }
        }

        public T? OriginalValue
        {
            get
            {
                return dataItem == null ? default : dataItem.GetOriginalValue<T>();
            }

            set
            {
                if (dataItem == null)
                {
                    return;
                }

                if (typeof(T) == typeof(int))
                {
                    dataItem.SetOriginalValue(Convert.ToInt32(value));
                }
                else if (typeof(T) == typeof(byte))
                {
                    dataItem.SetOriginalValue(Convert.ToByte(value));
                }
                else if (typeof(T) == typeof(ushort))
                {
                    dataItem.SetOriginalValue(Convert.ToUInt16(value));
                }
                else
                {
                    Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                    //dataItem.SetValue(Convert.ToInt32(value));
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValueString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalValue)));
            }
        }

        public void Revert()
        {
            dataItem?.RevertToOriginal();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataItem)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueString)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        public NumberDataItemViewModel()
        {
            Debug.WriteLine($"Constructing new NumberDataItemViewModel");
        }
    }
}
