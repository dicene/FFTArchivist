using FFTArchivist.Controls.TextBox;
using FFTArchivist.Models.Base;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using YamlDotNet.Core.Tokens;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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

        public string ToolTipString => dataItem.ToolTipString;

        public string ValueString
        {
            get
            {
                if (dataItem == null)
                {
                    return default;
                }

                int value;

                try
                {
                    if (typeof(T) == typeof(int))
                    {
                        value = dataItem.GetValue<int>();
                    }
                    else if (typeof(T) == typeof(byte))
                    {
                        value = dataItem.GetValue<byte>();
                    }
                    else if (typeof(T) == typeof(ushort))
                    {
                        value = dataItem.GetValue<ushort>();
                    }
                    else if (typeof(T) == typeof(short))
                    {
                        value = dataItem.GetValue<short>();
                    }
                    else
                    {
                        Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.Type.Name}");
                        value = 0;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failure to convert value of type {typeof(T).Name} for dataitem: {DataItem.Type.Name}");
                    value = 0;
                }

                if (DisplayAsHex)
                {
                    return $"{value:X}";
                }

                return value.ToString();
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
            get
            {
                if (dataItem == null)
                {
                    return default;
                }

                if (typeof(T) == typeof(int))
                {
                    return (T)Convert.ChangeType(Convert.ToInt32(dataItem.GetValue()), typeof(T));
                }
                else if (typeof(T) == typeof(byte))
                {
                    return (T)Convert.ChangeType(Convert.ToByte(dataItem.GetValue()), typeof(T));
                }
                else if (typeof(T) == typeof(sbyte))
                {
                    return (T)Convert.ChangeType(Convert.ToSByte(dataItem.GetValue()), typeof(T));
                }
                else if (typeof(T) == typeof(ushort))
                {
                    return (T)Convert.ChangeType(Convert.ToUInt16(dataItem.GetValue()), typeof(T));
                }
                else
                {
                    Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                }
                return dataItem == null ? default : dataItem.GetValue<T>();
            }

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

                int value;

                try {
                    if (typeof(T) == typeof(int))
                    {
                        value = dataItem.GetOriginalValue<int>();
                    }
                    else if (typeof(T) == typeof(byte))
                    {
                        value = dataItem.GetOriginalValue<byte>();
                    }
                    else if (typeof(T) == typeof(ushort))
                    {
                        value = dataItem.GetOriginalValue<ushort>();
                    }
                    else
                    {
                        Debug.WriteLine($"Unsure how to convert type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                        value = 0;
                    }
                }                
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failure to convert value of type {typeof(T).Name} for dataitem: {DataItem.GetType().Name}");
                    value = 0;
                }


                if (DisplayAsHex)
                {
                    return $"{value:X}";
                }

                return value.ToString();
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
