using BCnEncoder.Shared.ImageFiles;
using CommunityToolkit.HighPerformance.Buffers;
using FF16Tools.Files.Nex;
using FF16Tools.Files.Nex.Entities;
using FF16Tools.Files.Nex.Managers;
using FF16Tools.Pack;
using FFTArchivist.Managers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FFTArchivist.DataSources
{
    public abstract class ANEXDataSource : IDataSource
    {
        //public string PacFileName;
        public string TableName;
        public string NEXPath;
        public string LayoutName;
        public string TempFileLocation => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFTArchivist", "temp");
        public string CodeName = FF16Tools.Pack.Crypto.PackKeyStore.FFT_IVALICE_CODENAME;
        public string Locale = "en";

        private bool fileIsLoaded;
        private FF16Pack pack;
        private NexDataFile originalNexFile;
        private NexDataFile modifiedNexFile;
        private NexDataFileBuilder builder;
        private NexTableLayout layout;

        public ANEXDataSource(string name)
        {
            TableName = name.Replace("<locale>", Locale);
            NEXPath = $"nxd/{TableName}.nxd";
            LayoutName = NEXPath.Contains('.') ? NEXPath.Split('.')[0] : NEXPath;
            //LoadSource($"nxd/{NEXPath}.nxd");
            Task.Run(() => LoadSource(NEXPath)).GetAwaiter().GetResult();
            //PacFileName = pacFileName.EndsWith(".pac") ? pacFileName : $"{pacFileName}.pac";
            //NEXPath = Path.Combine("nxd", nexName);
            //if (pacFileName.Contains('.'))
            //{
            //    NEXPath += ".en";
            //}
            //NEXPath += ".nxd";
            //LayoutName = layoutName;
        }

        private async Task LoadSource(string directory)
        {
            //var packManager = new FF16PackManager();
            var fileData = DataManager.Instance.FF16PackManager.GetFileData(NEXPath, false);
            //var pack = packManager.GetFileDataBytesAsync.GetPack(PacFileName);
            //pack = await Task.Run(() => FF16Pack.Open(Path.Combine(App.DataManager.DataFolderPath, PacFileName), CodeName));
            //Directory.CreateDirectory(TempFileLocation);
            //pack.ExtractFile(NEXPath, TempFileLocation);
            //Print($"Processing nex changes for '{nexFile.Key}' ({nexPack.Key})");

            //NexTableLayout tableColumnLayout = TableMappingReader.ReadTableLayout(TableName, new Version(1, 0, 0), "ffto");

            //using MemoryOwner<byte> ogNexFileData = packManagerForGameMode.GetFileData(nexGamePath, includeDiff: false);

            originalNexFile = new NexDataFile();
            originalNexFile.Read(fileData.Span.ToArray());
            //var originalNexFile = new NexDataFile();

            layout = TableMappingReader.ReadTableLayout(TableName, new Version(1, 0, 0), CodeName);
            fileIsLoaded = true;

            //modifiedNexFile = new NexDataFile();
            //modifiedNexFile.Type = originalNexFile.Type;
            //modifiedNexFile.Version = originalNexFile.Version;
            //modifiedNexFile.Read(originalNexFile.Buffer);

            builder = new NexDataFileBuilder(layout);

            List<NexRowInfo> rowInfos = originalNexFile.RowManager!.GetAllRowInfos();
            if (originalNexFile.Type == NexTableType.DoubleKeyed)
            {
                NexTripleKeyedRowTableManager rowSetManager = (NexTripleKeyedRowTableManager)originalNexFile.RowManager;
                foreach (var dk in rowSetManager.GetRowSets())
                {
                    builder.AddTripleKeyedSet(dk.Key);
                    foreach (var subSet in dk.Value.SubSets)
                        builder.AddTripleKeyedSubset(dk.Key, subSet.Key);
                }
            }
            else if (originalNexFile.Type == NexTableType.DoubleKeyed)
            {
                NexDoubleKeyedRowTableManager rowSetManager = (NexDoubleKeyedRowTableManager)originalNexFile.RowManager;
                foreach (var set in rowSetManager.GetRowSets())
                    builder.AddDoubleKeyedSet(set.Key);
            }

            for (int i = 0; i < rowInfos.Count; i++)
            {
                var row = rowInfos[i];
                List<object> cells = NexUtils.ReadRow(layout, originalNexFile.Buffer!, row.RowDataOffset);
                builder.AddRow(row.Key, row.Key2, row.Key3, cells);
            }

            //using var fs = new FileStream(Path.Combine("built", originalNexFile.Key.ToLower() + ".nxd"), FileMode.Create);
            //builder.Write(fs);
        }

        public async Task<T> ReadData<T>(int id, int column)
        {
            if (!fileIsLoaded)
            {
                await LoadSource("");
            }

            List<object> cells = NexUtils.ReadRow(layout, originalNexFile.Buffer, originalNexFile.RowManager.GetRowInfo((uint)id).RowDataOffset);
            //Debug.WriteLine($"Row {id} Cell {column}: {cells[column]} ({cells[column].GetType()})");
            return (T)cells[column];
        }

        public async Task<T> ReadData<T>(int id, string columnName)
        {
            if (!fileIsLoaded)
            {
                await LoadSource("");
            }

            List<object> cells = NexUtils.ReadRow(layout, originalNexFile.Buffer, originalNexFile.RowManager.GetRowInfo((uint)id).RowDataOffset);
            //Debug.WriteLine($"Row {id} Cell {column}: {cells[column]} ({cells[column].GetType()})");
            var matchingColumns = layout.Columns.Where(c => c.Key == columnName).Select(c => c.Value).ToList();
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

            int columnIndex = layout.Columns.Values.ToList().IndexOf(matchingColumns[0]);
            return (T)cells[columnIndex];
        }

        public async Task WriteData<T>(int id, int column, T value)
        {
            var row = builder.GetRow((uint)id, 0, 0);
            row.Cells[column] = value;
            //NexUtils.WriteCell(layout, modifiedNexFile.Buffer, modifiedNexFile.RowManager.GetRowInfo((uint)id).RowDataOffset, column, value);
            Debug.WriteLine($"Wrote value {value} to row {id} column {column}.");
        }

        public async Task WriteData<T>(int id, string columnName, T value)
        {
            var row = builder.GetRow((uint)id, 0, 0);

            var matchingColumns = layout.Columns.Where(c => c.Key == columnName).Select(c => c.Value).ToList();
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

            int columnIndex = layout.Columns.Values.ToList().IndexOf(matchingColumns[0]);
            row.Cells[columnIndex] = value;
            //return (T)cells[columnIndex];

            //NexUtils.WriteCell(layout, modifiedNexFile.Buffer, modifiedNexFile.RowManager.GetRowInfo((uint)id).RowDataOffset, column, value);
            Debug.WriteLine($"Wrote value {value} to row {id} column {columnName}.");
        }

        public async Task WriteToFile(string filePath)
        {
            //var newNexFile = new NexDataFile();
            //newNexFile.Type = originalNexFile.Type;
            //newNexFile.Version = originalNexFile.Version;
            //newNexFile.Read(originalNexFile.Buffer);

            //using MemoryOwner<byte> ogNexFileData = packManagerForGameMode.GetFileData(nexGamePath, includeDiff: false);

            //NexDataFile originalTableFile = new NexDataFile();
            //originalTableFile.Read(ogNexFileData.Span.ToArray());
            //await File.WriteAllBytesAsync(filePath, newNexFile.Buffer);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                builder.Write(fileStream);
                Debug.WriteLine($"Wrote {fileStream.Length} bytes to new nex file {filePath}");
            }

            //await File.WriteAllBytesAsync(filePath, builder);
            //Debug.WriteLine($"Wrote {newNexFile.Buffer.Length} bytes to new nex file {filePath}");
        }
    }
}
