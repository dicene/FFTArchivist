using FFTArchivist.DataSources;
using System.ComponentModel;
using System.Diagnostics;
using Vortice.Direct3D12;

namespace FFTArchivist.Models.Base
{
    public class DataItem<T> : ADataItem
    {
        private int id;
        public string ToolTipString => $"Orig({OriginalValue}), Revert({RevertValue}), Value({Value})";
        //public string ToolTipString => $"Value({Value}), OverrideValue({OverrideValue}), OriginalValue({OriginalValue}), OriginalOverrideValue({OriginalOverrideValue})";
        public T? OriginalValue { get; private set; }
        public T? RevertValue { get; private set; }

        private T? value;
        public T? Value
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

                if (this.value != null)
                {
                    //Debug.WriteLine($"Changing value of {GetType().Name} from ({this.value}) to ({value})");
                    WriteToMod();
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public override bool IsModified => !Equals(RevertValue, Value);
        //public override bool IsModified => !Equals(OriginalValue, Value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public DataItem(ISourceMapping sourceMapping, int id)
        {
            this.Type = typeof(T);
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
            this.Type = typeof(T);
            this.id = id;
            this.SourceMapping = sourceMapping;
            this.DestinationMapping = destinationMapping;
            //Task.Run(ReadFromSource).GetAwaiter().GetResult();
        }

        public override async Task ReadFromOriginalSource()
        {
            OriginalValue = await SourceMapping.ReadFromSource<T>(originalSource);
            RevertValue = OriginalValue;
            Value = OriginalValue;
            //value ??= OriginalValue;
        }

        public override async Task ReadFromModSource()
        {
            Value = await SourceMapping.ReadFromSource<T>(modSource);
        }

        public override async Task ReadFromOriginalOverrideSource()
        {
            if (typeof(T) == typeof(string))
            {
                var newValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);

                Debug.WriteLine($"String NewValue: {OriginalValue} -> {newValue}");
                //await SetOriginalValue<T>(newValue);
                RevertValue = newValue;
                Value = RevertValue;
                //OriginalValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);
                //Value = OriginalValue;
            }
            else
            {
                if (typeof(T) == typeof(byte))
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<short>(originalOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        RevertValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);
                        Value = RevertValue;
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        RevertValue = OriginalValue;
                        Value = OriginalValue;
                    }
                }
                else if (typeof(T) == typeof(short))
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<short>(originalOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        RevertValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);
                        Value = RevertValue;
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        RevertValue = OriginalValue;
                        Value = OriginalValue;
                    }
                }
                else if (typeof(T) == typeof(int))
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<int>(originalOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        RevertValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);
                        Value = RevertValue;
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        RevertValue = OriginalValue;
                        Value = OriginalValue;
                    }
                }
                else
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<int>(originalOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        RevertValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);
                        Value = RevertValue;
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        RevertValue = OriginalValue;
                        Value = OriginalValue;
                    }
                }
            }
        }

        public override async Task ReadFromModOverrideSource()
        {
            //Value = await SourceOverrideMapping.ReadFromSource<T>(modOverrideSource);

            if (typeof(T) == typeof(string))
            {
                var newValue = await SourceOverrideMapping.ReadFromSource<T>(modOverrideSource);

                Debug.WriteLine($"String NewValue: {newValue}");
                //await SetOriginalValue<T>(newValue);
                //RevertValue = newValue;
                //Value = RevertValue;
                Value = newValue;
                //OriginalValue = await SourceOverrideMapping.ReadFromSource<T>(originalOverrideSource);
                //Value = OriginalValue;
            }
            else
            {
                if (typeof(T) == typeof(byte))
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<short>(modOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        Value = await SourceOverrideMapping.ReadFromSource<T>(modOverrideSource);
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        Value = OriginalValue;
                    }
                }
                else if (typeof(T) == typeof(short))
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<short>(modOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        Value = await SourceOverrideMapping.ReadFromSource<T>(modOverrideSource);
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        Value = OriginalValue;
                    }
                }
                else if (typeof(T) == typeof(int))
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<int>(modOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        Value = await SourceOverrideMapping.ReadFromSource<T>(modOverrideSource);
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        Value = OriginalValue;
                    }
                }
                else
                {
                    var newValue = await SourceOverrideMapping.ReadFromSource<int>(modOverrideSource);

                    if (newValue > -1)
                    {
                        Debug.WriteLine($"NON -1 {ColumnName} Override Value: {OriginalValue} -> {newValue}");
                        Value = await SourceOverrideMapping.ReadFromSource<T>(modOverrideSource);
                    }
                    else
                    {
                        //Debug.WriteLine($"-1 {ColumnName} Override Value: {OriginalValue} -> {OriginalValue}");
                        Value = OriginalValue;
                    }
                }
            }
        }

        public async void WriteToMod()
        {
            if (SourceOverrideMapping != null && SourceOverrideMapping is IDestinationMapping destinationOverrideMapping)
            {
                if (EqualityComparer<T>.Default.Equals(OriginalValue, Value))
                {
                    if (Value is byte)
                    {
                        await destinationOverrideMapping.WriteToDestination(originalOverrideSource, Convert.ToSByte(-1));
                    }
                    else if (Value is short)
                    {
                        await destinationOverrideMapping.WriteToDestination(originalOverrideSource, Convert.ToInt16(-1));
                    }
                    else if (Value is ushort)
                    {
                        await destinationOverrideMapping.WriteToDestination(originalOverrideSource, Convert.ToInt16(-1));
                    }
                    else if (Value is int)
                    {
                        await destinationOverrideMapping.WriteToDestination(originalOverrideSource, -1);
                    }
                    else
                    {
                        Debug.WriteLine($"Unsure how to set override destination value to -1: T:{typeof(T).Name}, Value:({Value.GetType().Name}) {Value}");
                    }
                }
                else
                {
                    await destinationOverrideMapping.WriteToDestination(originalOverrideSource, Value);
                }
                return;
            }
            await DestinationMapping.WriteToDestination(originalSource, Value);
            //await DestinationMapping.WriteToDestination(Value);
        }

        public override void RevertToOriginal()
        {
            Value = RevertValue;
        }

        public override T1 GetValue<T1>()
        {
            if (Value == null)
                return default;

            return Value is T1 castedValue ? castedValue : throw new InvalidCastException($"Cannot cast value of type {typeof(T)} to {typeof(T1)}");
        }

        public override T1 GetOriginalValue<T1>()
        {
            if (OriginalValue == null)
                return default;

            return OriginalValue is T1 castedOriginalValue ? castedOriginalValue : throw new InvalidCastException($"Cannot cast original value of type {typeof(T)} to {typeof(T1)}");
        }

        public override T1 GetRevertValue<T1>()
        {
            if (RevertValue == null)
                return default;

            return RevertValue is T1 castedRevertValue ? castedRevertValue : throw new InvalidCastException($"Cannot cast revert value of type {typeof(T)} to {typeof(T1)}");
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
                throw new InvalidCastException($"Cannot cast OriginalValue of type {typeof(T1)} to {typeof(T)}");
            }
        }

        public override void SetRevertValue<T1>(T1 newRevertValue)
        {
            if (typeof(T) == typeof(T1) && newRevertValue is T castNewRevertValue)
            {
                RevertValue = castNewRevertValue;
            }
            else
            {
                throw new InvalidCastException($"Cannot cast RevertValue of type {typeof(T1)} to {typeof(T)}");
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
