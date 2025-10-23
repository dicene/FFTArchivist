using CommunityToolkit.HighPerformance;
using Dapper;
using FF16Tools.Pack;
using FFTArchivist.DataSources;
using FFTArchivist.Models;
using FFTArchivist.Models.Base;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Managers
{
    public class DataManager : IDataManager
    {
        private static DataManager _instance = new DataManager();
        public static DataManager Instance { get => _instance; }
        public string FFTBasePath = @"C:\Program Files (x86)\Steam\steamapps\common\FINAL FANTASY TACTICS - The Ivalice Chronicles\";
        public string FFTExecutablePath => Path.Combine([FFTBasePath, "fft_enhanced.exe"]);
        public string DataFolderPath => Path.Combine([FFTBasePath, "data", "enhanced"]);

        public event EventHandler OnDataReloaded;

        private List<IDataSource> dataSources = new();

        // TODO: At some point consider refactoring this to a custom class.
        public Dictionary<Type, IEnumerable<BaseModel>> DataLists { get; } = new();

        //public List<dynamic> AbilityData { get; private set; } = new();
        //public List<Item> Items { get; private set; } = new();
        //public List<Poach> Poaches { get; private set; } = new();

        private DataManager()
        {
            Debug.WriteLine($"Creating DataManager...");
        }

        public async Task<bool> LoadData()
        {
            var items = new List<Item>();
            for (int i = 0; i < 256; i++)
            {
                items.Add(new Item(i));
            }

            var poaches = new List<Poach>();

            for (int i = 1; i < 97; i++)
            {
                poaches.Add(new Poach(i));
            }

            DataLists[typeof(Item)] = items;
            DataLists[typeof(Poach)] = poaches;

            //int id = Random.Shared.Next(1, Items.Count - 1);

            //Debug.Print($"Item [{id}]: {Items[id].Name.Value}");

            //NEXDataSource AbilityEnDataSource = new(pacFileName: Path.Combine(DataFolderPath, "0004.en.pac"), nexPath: @"nxd/Ability.en.nxd", layoutName: "Ability");
            //dataSources.Add(AbilityEnDataSource);

            //Debug.WriteLine("Loading data from original pac sources...");
            //var pack = FF16Pack.Open(Path.Combine([DataFolderPath, "0004.en.pac"]), FF16Tools.Pack.Crypto.PackKeyStore.FFT_IVALICE_CODENAME);
            //Debug.WriteLine($"Pac: {pack.Name}, Files:{pack.GetNumFiles()}, Size:{pack.GetTotalDecompressedSize()}");
            ////var packBuilder = new FF16Tools.Pack.Packing.FF16PackBuilder();
            ////packBuilder.InitFromDirectory(DataFolderPath);
            ////packBuilder.
            //Debug.WriteLine($"Loading from {TextDbPath}");

            //var results = await AbilityEnDataSource.ReadData<List<object>>();
            ////var results = await AbilityEnDataSource.ReadData<List<object>>();

            ////File.WriteAllText("outTextFile.txt", $"Pac: {pack.Name}, Files:{pack.GetNumFiles()}, Size:{pack.GetTotalDecompressedSize()}");

            ////using (var connection = new SqliteConnection($"Data Source=\"{TextDbPath}\""))
            ////{
            ////    Debug.WriteLine($"Connection opened...");
            ////    var abilities = (await connection.QueryAsync<Ability_en>($"SELECT * FROM 'Ability-en'")).ToList();
            ////    AbilityData = (await connection.QueryAsync($"SELECT * FROM 'Ability-en'")).ToList();
            ////    for (int i = 0; i < abilities.Count; i++)
            ////    {
            ////        var ability = abilities[i];
            ////        Debug.WriteLine($"Ability {i}: {ability.Name}");
            ////    }
            ////}

            OnDataReloaded?.Invoke(this, EventArgs.Empty);

            //using (var connection = new SqlConnection(connectionString))

            return true;
        }

        // TODO: Move data source loading and caching logic to their respective classes

        public async Task<NEXDataSource> LoadNewDataSource(string pacFileName, string nexPath, string layoutName)
        {
            Debug.WriteLine($"Loading new NEX data source: {pacFileName}, {nexPath}, {layoutName}");
            var dataSource = new NEXDataSource(Path.Combine(DataFolderPath, pacFileName), nexPath: nexPath, layoutName: layoutName);
            dataSources.Add(dataSource);
            return dataSource;
        }

        public async Task<EXEDataSource> LoadNewDataSource(long baseOffset, int count, int size)
        {
            Debug.WriteLine($"Loading new EXE data source: {baseOffset}, {count}, {size}");
            var dataSource = new EXEDataSource(FFTExecutablePath, baseOffset, count, size);
            dataSources.Add(dataSource);
            return dataSource;
        }

        public async Task<NEXDataSource> GetDataSource<T>(NEXLinkage<T> linkage)
        {
            // Determine the appropriate data source based on the linkage information
            var existingDataSource = dataSources.OfType<NEXDataSource>().FirstOrDefault(ds => ds.LayoutName == linkage.TableName);

            if (existingDataSource != default)
            {
                return existingDataSource;
            }

            return await LoadNewDataSource(Path.Combine(DataFolderPath, linkage.PackName), $"nxd/{linkage.TableName}{(linkage.PackName.Count('.') > 0 ? $".{linkage.PackName.Split('.')[1]}" : "")}.nxd", linkage.TableName);
        }

        public async Task<EXEDataSource> GetDataSource<T>(EXELinkage<T> linkage)
        {
            // Determine the appropriate data source based on the linkage information
            var existingDataSource = dataSources.OfType<EXEDataSource>().FirstOrDefault(ds => ds.BaseOffset == linkage.BaseOffset && ds.Count == linkage.Count && ds.Size == linkage.Size);

            if (existingDataSource != default)
            {
                return existingDataSource;
            }

            return await LoadNewDataSource(linkage.BaseOffset, linkage.Count, linkage.Size);
        }

        public List<T> GetDataList<T>() where T : BaseModel
        {
            if (DataLists.TryGetValue(typeof(T), out var list))
            {
                return list.Cast<T>().ToList();
            }

            return new List<T>();
        }
    }
}
