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
            //Debug.WriteLine($"Pac: {pack.Name}, Files:{pack.GetNumFiles()}, Size:{pack.GetTotalDecompressedSize()}");
            //Debug.WriteLine($"Loading from {PacFileName}...");
            //Debug.WriteLine($"TempFileLocation: {TempFileLocation}");
            //var tempFilePath = Path.GetDirectoryName(TempFileLocation);
            //"nxd/ability.en.nxd" "C:\\Program Files (x86)\\Steam\\steamapps\\common\\FINAL FANTASY TACTICS - The Ivalice Chronicles\\data\\enhanced\\0004.en"
            Directory.CreateDirectory(TempFileLocation);
            pack.ExtractFile(NEXPath, TempFileLocation);
            nexFile = NexDataFile.FromFile(Path.Combine(TempFileLocation, NEXPath));
            //Debug.WriteLine($"Loaded file {NEXPath}: {nexFile.RowManager.GetAllRowInfos().Count} bytes");
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
            Debug.WriteLine($"Row {id} Cell {column}: {cells[column]} ({cells[column].GetType()})");
            return (T)cells[column];
            //using (var pack = await Task.Run(() => FF16Pack.Open(PacFileName, CodeName)))
            //{

            //    //var packBuilder = new FF16Tools.Pack.Packing.FF16PackBuilder();
            //    //packBuilder.InitFromDirectory(DataFolderPath);
            //    //packBuilder.
            //    Debug.WriteLine($"Loading from {PacFileName}...");
            //    Debug.WriteLine($"TempFileLocation: {TempFileLocation}");
            //    //var tempFilePath = Path.GetDirectoryName(TempFileLocation);
            //    //"nxd/ability.en.nxd" "C:\\Program Files (x86)\\Steam\\steamapps\\common\\FINAL FANTASY TACTICS - The Ivalice Chronicles\\data\\enhanced\\0004.en"
            //    Directory.CreateDirectory(TempFileLocation);
            //    pack.ExtractFile(NEXPath, TempFileLocation);
            //    //var file = await pack.GetFileDataBytesAsync(nxdPath);

            //    //new FF16Tools.Files.Nex.NexDataFileBuilder(new NexTableLayout("ffto"))
            //    NexDataFile nexFile = NexDataFile.FromFile(Path.Combine(TempFileLocation, NEXPath));
            //    //using var importer = new SQLiteToNexImporter(verbs.InputFile, new Version(1, 0, 0), codeName, verbs.Tables.ToList(), _loggerFactory);
            //    //importer.ReadSqlite();
            //    //importer.SaveTo(verbs.OutputFile);

            //    Debug.WriteLine($"Loaded file {NEXPath}: {nexFile.RowManager.GetAllRowInfos().Count} bytes");
            //    var layout = TableMappingReader.ReadTableLayout(LayoutName, new Version(1, 0, 0), CodeName);
            //    List<object> cells = NexUtils.ReadRow(layout, nexFile.Buffer, nexFile.RowManager.GetRowInfo(1).RowDataOffset);
            //    var nameColumn = layout.Columns["Name"];


            //    Debug.WriteLine($"Cells: {cells.Count}: {cells[0]}");
            //    //NexToSQLiteExporter

            //    return default;
            //}


            //File.WriteAllText("outTextFile.txt", $"Pac: {pack.Name}, Files:{pack.GetNumFiles()}, Size:{pack.GetTotalDecompressedSize()}");

            //using (var connection = new SqliteConnection($"Data Source=\"{pacFileName}\""))
            //{
            //Debug.WriteLine($"Connection opened...");
            //var abilities = (await connection.QueryAsync<Ability_en>($"SELECT * FROM 'Ability-en'")).ToList();
            //AbilityData = (await connection.QueryAsync($"SELECT * FROM 'Ability-en'")).ToList();
            //for (int i = 0; i < abilities.Count; i++)
            //{
            //    var ability = abilities[i];
            //    Debug.WriteLine($"Ability {i}: {ability.Name}");
            //}
            //}
        }
    }
}
