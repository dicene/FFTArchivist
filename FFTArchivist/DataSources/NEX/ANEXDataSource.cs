using FF16Tools.Files.Nex;
using FF16Tools.Files.Nex.Entities;
using FF16Tools.Files.Nex.Managers;
using FF16Tools.Pack;
using System.Diagnostics;
using System.IO;

namespace FFTArchivist.DataSources
{
    public abstract class ANEXDataSource : INEXDataSource
    {
        public string TableName;
        public string NEXPath;
        public string LayoutName;
        public string CodeName = FF16Tools.Pack.Crypto.PackKeyStore.FFT_IVALICE_CODENAME;
        public string Locale = "en";

        private bool fileIsLoaded;
        private FF16Pack pack;
        public NexDataFile NEXFile { get; set; }
        private NexDataFileBuilder builder;
        public NexTableLayout TableLayout { get; set; }
        public List<NexRowInfo> AllRows { get; set; }
        public int RowCount { get; private set; }

        public void ForEachRow(Action<int> action)
        {
            foreach (var key in AllRows.Select(r => r.Key))
            {
                action(Convert.ToInt32(key));
            }
        }

        public ANEXDataSource(string name)
        {
            TableName = name.Replace("<locale>", Locale);
            NEXPath = $"nxd/{TableName}.nxd";
            LayoutName = NEXPath.Contains('.') ? NEXPath.Split('.')[0] : NEXPath;
        }

        public void CreateBuilderFromOriginalSource()
        {
            builder = new NexDataFileBuilder(TableLayout);

            List<NexRowInfo> rowInfos = NEXFile.RowManager!.GetAllRowInfos();

            if (NEXFile.Type == NexTableType.TripleKeyed)
            {
                NexTripleKeyedRowTableManager rowSetManager = (NexTripleKeyedRowTableManager)NEXFile.RowManager;
                foreach (var dk in rowSetManager.GetRowSets())
                {
                    builder.AddTripleKeyedSet(dk.Key);
                    foreach (var subSet in dk.Value.SubSets)
                        builder.AddTripleKeyedSubset(dk.Key, subSet.Key);
                }
            }
            else if (NEXFile.Type == NexTableType.DoubleKeyed)
            {
                NexDoubleKeyedRowTableManager rowSetManager = (NexDoubleKeyedRowTableManager)NEXFile.RowManager;
                foreach (var set in rowSetManager.GetRowSets())
                    builder.AddDoubleKeyedSet(set.Key);
            }

            for (int i = 0; i < rowInfos.Count; i++)
            {
                var row = rowInfos[i];
                List<object> cells = NexUtils.ReadRow(TableLayout, NEXFile.Buffer!, row.RowDataOffset);
                builder.AddRow(row.Key, row.Key2, row.Key3, cells);
            }
        }

        public async Task LoadPackSource(FF16PackManager packManager)
        {
            var fileData = packManager.GetFileData(NEXPath, false);

            NEXFile = new NexDataFile();
            NEXFile.Read(fileData.Span.ToArray());

            TableLayout = TableMappingReader.ReadTableLayout(TableName, new Version(1, 0, 0), CodeName);

            if (TableName.Contains("Ability") && TableLayout.Columns.ContainsKey("JpCost1"))
            {
                Debug.WriteLine($"Modifying JpCost1 and JpCost2 columns...");
                var jpCost1Column = TableLayout.Columns["JpCost1"];
                var jpCost2Column = TableLayout.Columns["JpCost2"];
                TableLayout.Columns.Remove("JpCost2");
                TableLayout.Columns["JpCost"] = jpCost1Column;
                TableLayout.Columns.Remove("JpCost1");
            }

            if (TableName.Contains("Ability") && TableLayout.Columns.ContainsKey("JpCost"))
            {
                if (TableLayout.Columns["JpCost"].Type == NexColumnType.Short)
                {
                    Debug.WriteLine($"Changing JpCost column from Short to UShort...");
                }
                else if (TableLayout.Columns["JpCost"].Type == NexColumnType.Byte)
                {
                    Debug.WriteLine($"Changing JpCost column from Byte to UShort...");
                }
                
                TableLayout.Columns["JpCost"].Type = NexColumnType.UShort;
            }

            AllRows = NEXFile.RowManager.GetAllRowInfos();
            RowCount = AllRows.Count;

            fileIsLoaded = true;

            CreateBuilderFromOriginalSource();
        }

        public async Task LoadNXDSource(string directory)
        {
            var filePath = Path.Combine(directory, NEXPath);
            var fileData = await File.ReadAllBytesAsync(filePath);

            if (fileData.Length == 0)
            {
                throw new FileFormatException(new Uri(filePath), $"Invalid NXD file \"{filePath}\", size: {fileData.Length}");
            }

            NEXFile = new NexDataFile();
            NEXFile.Read(fileData);

            TableLayout = TableMappingReader.ReadTableLayout(TableName, new Version(1, 0, 0), CodeName);

            if (TableName.Contains("Ability") && TableLayout.Columns.ContainsKey("JpCost1"))
            {
                Debug.WriteLine($"Modifying JpCost1 and JpCost2 columns...");
                var jpCost1Column = TableLayout.Columns["JpCost1"];
                var jpCost2Column = TableLayout.Columns["JpCost2"];
                TableLayout.Columns.Remove("JpCost2");
                TableLayout.Columns["JpCost"] = jpCost1Column;
                TableLayout.Columns.Remove("JpCost1");
            }

            if (TableName.Contains("Ability") && TableLayout.Columns.ContainsKey("JpCost") && TableLayout.Columns["JpCost"].Type == NexColumnType.Short)
            {
                Debug.WriteLine($"Changing JpCost column from Short to UShort...");
                TableLayout.Columns["JpCost"].Type = NexColumnType.UShort;
            }

            AllRows = NEXFile.RowManager.GetAllRowInfos();
            RowCount = AllRows.Count;

            fileIsLoaded = true;
        }

        public async Task LoadModSource(string directory)
        {
            var filePath = Path.Combine(directory, NEXPath);
            var fileData = await File.ReadAllBytesAsync(filePath);

            if (fileData.Length == 0)
            {
                throw new FileFormatException(new Uri(filePath), $"Invalid NXD file \"{filePath}\", size: {fileData.Length}");
            }

            NEXFile = new NexDataFile();
            try
            {
                NEXFile.Read(fileData);

                TableLayout = TableMappingReader.ReadTableLayout(TableName, new Version(1, 0, 0), CodeName);

                if (TableName.Contains("Ability") && TableLayout.Columns.ContainsKey("JpCost1"))
                {
                    var jpCost1Column = TableLayout.Columns["JpCost1"];
                    var jpCost2Column = TableLayout.Columns["JpCost2"];
                    TableLayout.Columns.Remove("JpCost2");
                    TableLayout.Columns["JpCost"] = jpCost1Column;
                    TableLayout.Columns.Remove("JpCost1");
                }

                AllRows = NEXFile.RowManager.GetAllRowInfos();
                RowCount = AllRows.Count;

                fileIsLoaded = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while attempting to NEX data file {filePath}: {ex}");
                throw;
            }
        }

        public async Task<T> ReadData<T>(int id, int column)
        {
            if (!fileIsLoaded)
            {
                throw new FileNotFoundException($"NEX file not loaded for table {TableName}.");
            }

            List<object> cells = NexUtils.ReadRow(TableLayout, NEXFile.Buffer, NEXFile.RowManager.GetRowInfo((uint)id).RowDataOffset);
            //Debug.WriteLine($"Row {id} Cell {column}: {cells[column]} ({cells[column].GetType()})");
            return (T)cells[column];
        }

        public async Task<T> ReadData<T>(int id, string columnName)
        {
            if (!fileIsLoaded)
            {
                throw new FileNotFoundException($"NEX file not loaded for table {TableName}.");
            }

            List<object> cells = NexUtils.ReadRow(TableLayout, NEXFile.Buffer, NEXFile.RowManager.GetRowInfo((uint)id).RowDataOffset);
            //Debug.WriteLine($"Row {id} Cell {column}: {cells[column]} ({cells[column].GetType()})");
            var matchingColumns = TableLayout.Columns.Where(c => c.Key == columnName).Select(c => c.Value).ToList();
            if (matchingColumns.Count == 0)
            {
                Debug.WriteLine($"No columns matching name {columnName}.");
                return default;
            }
            else if (matchingColumns.Count > 1)
            {
                Debug.WriteLine($"Multiple columns ({matchingColumns.Count}) matching name {columnName}.");
                return default;
            }

            int columnIndex = TableLayout.Columns.Values.ToList().IndexOf(matchingColumns[0]);
            
            var value = cells[columnIndex];
            
            if (typeof(T) == typeof(string))
            {
                return (T)value;
            }

            if (value is string stringVal && stringVal.Equals(""))
            {
                value = "0";
            }

            if (typeof(T) == typeof(byte))
            {
                try
                {
                    return (T)Convert.ChangeType(Convert.ToByte(value), typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to Byte: {ex}");
                }
            }
            else if (typeof(T) == typeof(sbyte))
            {
                try
                {
                    return (T)Convert.ChangeType(Convert.ToSByte(value), typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to SByte: {ex}");
                }
            }
            else if (typeof(T) == typeof(short))
            {
                try
                {
                    return (T)Convert.ChangeType(Convert.ToInt16(value), typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to Int16: {ex}");
                }
            }
            else if (typeof(T) == typeof(ushort))
            {
                try
                {
                    return (T)Convert.ChangeType(Convert.ToUInt16(value), typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to UInt16: {ex}");
                }
            }
            else if (typeof(T) == typeof(int))
            {
                try
                {
                    return (T)Convert.ChangeType(Convert.ToInt32(value), typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to Int: {ex}");
                }
            }
            else if (typeof(T) == typeof(bool))
            {
                try
                {
                    return (T)Convert.ChangeType(Convert.ToInt32(value) == 1, typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to bool: {ex}");
                }
            }
            else if (typeof(T) == typeof(string))
            {
                try
                {
                    if (((string)value).Equals(""))
                    {
                        return (T)Convert.ChangeType(0, typeof(T));
                    }

                    return (T)Convert.ChangeType(Convert.ToInt32(value), typeof(T));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed while converting {value:X} to bool: {ex}");
                }
            }
            try
            {
                Debug.WriteLine($"\t\tUnsure how to convert cell {columnName} from {value.GetType().Name} to {typeof(T)}!");
                return (T)value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed while converting {cells[columnIndex]:X} to {typeof(T).Name}");
            }
            return default;
        }

        public async Task WriteData<T>(int id, string columnName, T value)
        {
            var row = builder.GetRow((uint)id, 0, 0);

            var matchingColumns = TableLayout.Columns.Where(c => c.Key == columnName).Select(c => c.Value).ToList();
            if (matchingColumns.Count == 0)
            {
                Debug.WriteLine($"No columns matching name {columnName}.");
                return;
            }
            else if (matchingColumns.Count > 1)
            {
                Debug.WriteLine($"Multiple columns ({matchingColumns.Count}) matching name {columnName}.");
                return;
            }

            int columnIndex = TableLayout.Columns.Values.ToList().IndexOf(matchingColumns[0]);

            if (typeof(T) == typeof(byte) && matchingColumns[0].Type == NexColumnType.Short)
            {
                row.Cells[columnIndex] = Convert.ToInt16(value);
                return;
            }
            else if (typeof(T) == typeof(sbyte) && matchingColumns[0].Type == NexColumnType.Short)
            {
                row.Cells[columnIndex] = Convert.ToInt16(value);
                return;
            }

            row.Cells[columnIndex] = value;
        }

        public async Task WriteToFile(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                try {
                    var columns = TableLayout.Columns;
                    var row = builder.GetRow(1, 0, 0);
                    var cells = row.Cells;

                    int i = 0;
                    foreach (var column in columns.Values)
                    {
                        Debug.WriteLine($"Column: {column.Name}, Type: {column.Type}, Value: {cells[i]}, ValueType: {cells[i].GetType().Name}");
                        i++;
                    }

                    builder.Write(fileStream);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to write to file: {ex}");
                    throw;
                }

                Debug.WriteLine($"Wrote {fileStream.Length} bytes to new nex file {filePath}");
            }
        }
    }
}
