using CommunityToolkit.HighPerformance;
using FFTArchivist.Managers;
using fftivc.utility.modloader.Interfaces.Tables;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using fftivc.utility.modloader.Interfaces.Tables.Models.Bases;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using fftivc.utility.modloader.Serializers;
using Reloaded.Memory.Extensions;
using Reloaded.Memory.Sigscan;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.DataSources.EXE
{
    public abstract class AEXEDataSource<TClass, TStruct, TTable> : IEXEDataSource, IDataSource where TClass : class where TStruct : struct where TTable : class, new()
    {
        public string Path { get; private set; }
        public long BaseOffset { get; private set; }
        public int Count { get; private set; }
        public Type RowType { get; private set; }
        public string Pattern { get; private set; }
        public string Filename { get; private set; }

        private List<TClass> rows { get; set; } = new List<TClass>();

        public AEXEDataSource(string filename, string pattern, int count)
        {
            Filename = filename;
            Path = DataManager.Instance.FFTExecutablePath;
            RowType = typeof(TClass);
            Pattern = pattern;
            Count = count;
            using (var reader = new BinaryReader(File.OpenRead(Path)))
            {
                using (Stream stream = File.OpenRead(Path))
                {
                    var scanner = new Scanner(stream.ReadBytes((int)stream.Length));
                    var result = scanner.FindPattern(pattern);
                    BaseOffset = result.Offset;
                    stream.Seek(BaseOffset, SeekOrigin.Begin);
                    for (int i = 0; i < Count; i++)
                    {
                        byte[] bytes = stream.ReadBytes(Marshal.SizeOf(typeof(TStruct)));
                        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                        TStruct @struct = (TStruct)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(TStruct));
                        TClass @class = default;

                        var convertMethod = typeof(TClass).GetMethods().FirstOrDefault(m => m.IsStatic && m.Name == "FromStructure");
                        TClass convertResult = default;

                        if (convertMethod != null)
                        {
                            var args = new object[] { i, @struct };
                            convertResult = convertMethod.Invoke(null, args) as TClass;
                        }

                        rows.Add(convertResult);

                        handle.Free();
                    }
                }
            }
        }

        public async Task<TDataType> ReadData<TDataType>(int id, string propertyName)
        {
            if (rows.Count <= id)
            {
                return default;
            }

            var prop = (typeof(TClass)).GetProperty(propertyName);
            var propValue = prop.GetValue(rows[id]);

            if (propValue == null)
            {
                return default;
            }

            var convertedValue = (TDataType)Convert.ChangeType(propValue, typeof(TDataType));

            return convertedValue;
        }

        public async Task WriteData<T2>(int id, string columnName, T2 value)
        {

            Debug.WriteLine($"Wrote value {value} to row {id} column {columnName}.");
            var row = rows[id];
            var prop = row.GetType().GetProperty(columnName);

            if (row != null && prop != null)
            {
                prop.SetValue(row, value);
            }
        }

        public async Task WriteToFile(string tablesPath)
        {
            using (var fileStream = new FileStream(System.IO.Path.Combine(tablesPath, Filename), FileMode.Create))
            {
                var table = new TTable();
                var entriesProperty = typeof(TTable).GetProperties().FirstOrDefault(m => m.Name == "Entries");

                if (entriesProperty != null)
                {
                    var entries = entriesProperty.GetValue(table) as List<TClass>;
                    entries.AddRange(rows);
                    //var addRangeMethod = entriesProperty.GetType().GetMethods().FirstOrDefault(m => m.Name == "AddRange");
                    //if (addRangeMethod != null)
                    //{
                    //var args = new object[] { rows };
                    //addRangeMethod.Invoke(entries, args);
                    //}
                }

                var serializer = new XmlModelFormatSerializer();
                serializer.Serialize(fileStream, table);
                //var serializedData = serializer.Serialize(rows);
                //fileStream.WriteString(serializedData);
                Debug.WriteLine($"Wrote {fileStream.Length} bytes to new xml file {tablesPath}");
            }
        }
    }
}
