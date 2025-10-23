using FF16Tools.Files.Nex;
using FF16Tools.Files.Nex.Entities;
using FF16Tools.Pack;
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
    public class NEXDataSource : IDataSource
    {
        public string PacFileName;
        public string NEXPath;
        public string LayoutName;
        public string TempFileLocation => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFTArchivist", "temp");
        public string CodeName = FF16Tools.Pack.Crypto.PackKeyStore.FFT_IVALICE_CODENAME;

        private bool fileIsLoaded;
        private FF16Pack pack;
        private NexDataFile nexFile;
        private NexTableLayout layout;

        public NEXDataSource(string pacFileName, string nexPath, string layoutName)
        {
            PacFileName = pacFileName.EndsWith(".pac") ? pacFileName : $"{pacFileName}.pac";
            NEXPath = nexPath;
            LayoutName = layoutName;
        }

        private async Task LoadSource()
        {
            pack = await Task.Run(() => FF16Pack.Open(PacFileName, CodeName));
            Directory.CreateDirectory(TempFileLocation);
            pack.ExtractFile(NEXPath, TempFileLocation);
            nexFile = NexDataFile.FromFile(Path.Combine(TempFileLocation, NEXPath));
            layout = TableMappingReader.ReadTableLayout(LayoutName, new Version(1, 0, 0), CodeName);
            fileIsLoaded = true;
        }

        public async Task<T> ReadData<T>(int id, int column)
        {
            if (!fileIsLoaded)
            {
                await LoadSource();
            }

            List<object> cells = NexUtils.ReadRow(layout, nexFile.Buffer, nexFile.RowManager.GetRowInfo((uint)id).RowDataOffset);
            //Debug.WriteLine($"Row {id} Cell {column}: {cells[column]} ({cells[column].GetType()})");
            return (T)cells[column];
        }

        public async Task<T> ReadData<T>(int id, string columnName)
        {
            if (!fileIsLoaded)
            {
                await LoadSource();
            }

            List<object> cells = NexUtils.ReadRow(layout, nexFile.Buffer, nexFile.RowManager.GetRowInfo((uint)id).RowDataOffset);
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
    }
}
