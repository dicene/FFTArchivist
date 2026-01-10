using FFTArchivist.DataSources;
using FFTArchivist.DataSources.EXE;
using FFTArchivist.Models.Base;
using fftivc.utility.modloader.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Item = FFTArchivist.Models.Item;

namespace FFTArchivist.Managers
{
    internal class ModManager : IModManager
    {
        private static ModManager _instance = new ModManager();
        public static ModManager Instance { get => _instance; }
        public async Task ExportMod(string modName, string modId, string modVersion, string modAuthor, string modDescription, string modPath)
        {
            Directory.CreateDirectory(modPath);
            Directory.CreateDirectory(Path.Combine(modPath, "FFTIVC"));
            var enhancedDataPath = Path.Combine(modPath, "FFTIVC", "data", "enhanced");
            Directory.CreateDirectory(Path.Combine(enhancedDataPath, "nxd"));
            var tablesPath = Path.Combine(modPath, "FFTIVC", "tables", "enhanced");
            Directory.CreateDirectory(tablesPath);

            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            var file = assembly.GetManifestResourceStream("FFTArchivist.Preview.png");
            using (var resFile = new FileStream(Path.Combine(modPath, "Preview.png"), FileMode.Create))
            {
                file.CopyTo(resFile);
            }

            var resourceName = "FFTArchivist.ModConfig.json";

            var modConfig = new ModConfig();
            
            modConfig.ModId = modId;
            modConfig.ModName = modName;
            modConfig.ModAuthor = modAuthor;
            modConfig.ModVersion = modVersion;
            modConfig.ModDescription = modDescription;

            var modConfigJson = JsonConvert.SerializeObject(modConfig, Formatting.Indented);
            Debug.WriteLine($"{modConfigJson}");

            await File.WriteAllTextAsync(Path.Combine(modPath, "ModConfig.json"), modConfigJson);

            var changedItems = DataManager.Instance.GetDataList<Item>().ToList();

            if (changedItems.Count > 0)
            {
                Debug.WriteLine($"Exporting {changedItems.Count} changed items.");
            }
            else
            {
                Debug.WriteLine($"No changed items to write...");
            }

            foreach ((Type type, IDataSource dataSource) in DataManager.Instance.DataSources)
            {
                if (dataSource is ANEXDataSource nexSource)
                {
                    var destinationPath = Path.Combine(enhancedDataPath, nexSource.NEXPath);
                    try
                    {
                        Debug.WriteLine($"Writing to NEX file at {destinationPath}");
                        await nexSource.WriteToFile(destinationPath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to write NEX file to {destinationPath}: {ex}");
                        throw;
                    }
                }
                else if (dataSource is IEXEDataSource exeDataSource)
                {
                    Debug.WriteLine($"Writing to XML file at {tablesPath}");
                    await exeDataSource.WriteToFile(tablesPath);
                }
            }
        }

        public async Task ImportMod(string modPath)
        {
            Dictionary<Type, IDataSource> modDataSources = new();

            var nxdBasePath = Path.Combine(modPath, "FFTIVC", "data", "enhanced");
            var tablesPath = Path.Combine(modPath, "FFTIVC", "tables", "enhanced");

            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterface("IDataSource") != null && !type.IsAbstract))
            {
                Debug.WriteLine($"Initializing new datasource from mod: {t.Name}");
                if (Activator.CreateInstance(t) is IDataSource dataSource)
                {
                    modDataSources.Add(t, dataSource);

                    if (dataSource is ANEXDataSource aNEXDataSource)
                    {
                        try
                        {
                            await aNEXDataSource.LoadModSource(nxdBasePath);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Failed to load NEX Mod Source {nxdBasePath}: {ex}");
                            throw;
                        }
                    }
                    else if (dataSource is IEXEDataSource iEXEDataSource)
                    {
                        await iEXEDataSource.LoadModSource(tablesPath);
                    }
                }
            }

            foreach (var datalist in DataManager.Instance.DataLists.Values)
            {
                foreach (var model in datalist.ToList())
                {
                    foreach (var (name, dataItem) in model.DataItems)
                    {
                        var sourceType = dataItem.SourceMapping.SourceType;
                        if (sourceType != null && modDataSources.TryGetValue(sourceType, out var source))
                        {
                            dataItem.SetModSource(source);
                            await dataItem.ReadFromModSource();
                        }
                    }
                }
            }
            
            foreach ((Type type, IDataSource dataSource) in DataManager.Instance.DataSources)
            {
                if ((type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(AEXEDataSource<,,>)) && dataSource is IEXEDataSource exeDataSource)
                {
                    //var sourcePath = Path.Combine(enhancedDataPath);
                    var tableType = type.BaseType.GetGenericArguments()[2];

                    var tablesDirectory = Path.Combine(modPath, "FFTIVC", "tables", "enhanced");
                    var tablePath = Path.Combine(tablesDirectory, exeDataSource.Filename);
                    //Debug.WriteLine($"Loading from EXE file at {modPath}: {tablePath}");
                    var abilitySerializer = new XmlModelFormatSerializer();
                    var deserializeMethod = abilitySerializer.GetType().GetMethods().FirstOrDefault(m => m.Name == "Deserialize");
                    if (deserializeMethod != null)
                    {
                        var typedDeserializeMethod = deserializeMethod.MakeGenericMethod([tableType]);

                        var args = new object[] { tablePath };
                        var results = typedDeserializeMethod.Invoke(abilitySerializer, args);
                        var entriesProp = tableType.GetProperties().FirstOrDefault(p => p.Name == "Entries");
                        if (entriesProp != default)
                        {
                            var entries = entriesProp.GetValue(results);

                            //Debug.WriteLine($"Entries: {entries}");

                            var dataList = DataManager.Instance.GetDataList(type.BaseType.GetGenericArguments()[0]);

                            //Debug.WriteLine($"Corresponding data list: {dataList}");
                        }
                        //var entryType = type.BaseType.GetGenericArguments()[0];

                        //var results = abilitySerializer.Deserialize<AbilityTable>(File.OpenRead(tablePath));

                        //Debug.WriteLine($"Results: {results}");
                    }
                }
            }
        }
    }
}
