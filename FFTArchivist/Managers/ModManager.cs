using FFTArchivist.DataSources;
using FFTArchivist.DataSources.EXE;
using FFTArchivist.Models;
using FFTArchivist.Models.Base;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace FFTArchivist.Managers
{
    internal class ModManager : IModManager
    {
        private static ModManager _instance = new ModManager();
        public static ModManager Instance { get => _instance; }
        public async Task ExportMod(string modName, string modId, string modVersion, string modAuthor, string modDescription, string modPath)
        {
            //var dataManager = DataManager.Instance;
            //Path.CreateDir
            Directory.CreateDirectory(modPath);
            Directory.CreateDirectory(Path.Combine(modPath, "FFTIVC"));
            var enhancedDataPath = Path.Combine(modPath, "FFTIVC", "data", "enhanced");
            Directory.CreateDirectory(enhancedDataPath);
            var tablesPath = Path.Combine(modPath, "FFTIVC", "tables", "enhanced");
            Directory.CreateDirectory(tablesPath);

            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            var file = assembly.GetManifestResourceStream("FFTArchivist.Preview.png");
            using (var resFile = new FileStream(Path.Combine(modPath, "Preview.png"), FileMode.Create))
            {
                file.CopyTo(resFile);
            }

            var resourceName = "FFTArchivist.ModConfig.json";

            var modConfigJson = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    modConfigJson = reader.ReadToEnd();
                }
            }

            var modConfig = JObject.Parse(modConfigJson);
            modConfig["ModId"] = modId;
            modConfig["ModName"] = modName;
            modConfig["ModAuthor"] = modAuthor;
            modConfig["ModVersion"] = modVersion;
            modConfig["ModDescription"] = modDescription;

            await File.WriteAllTextAsync(Path.Combine(modPath, "ModConfig.json"), modConfig.ToString());

            var changedItems = DataManager.Instance.GetDataList<Item>().ToList();
            if (changedItems.Count > 0)
            {
                Debug.WriteLine($"Exporting {changedItems.Count} changed items.");

                var itemProperties = typeof(Item).GetProperties().Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DataItem<>)).ToList();

                //var itemsXML = new Xdocu

                //foreach (var item in changedItems)
                //{
                //foreach (var prop in itemProperties)
                //{
                //    var propValue = prop.GetValue(item);
                //    writeToModFunction propValue.GetType().GetMethods().FirstOrDefault(m => m.Name == "WriteToMod");
                //    if (propValue)
                //if (prop is DataItem<)
                //}
                //if (itemProperties[0])
                //}
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
                    Debug.WriteLine($"Writing to NEX file at {destinationPath}");
                    nexSource.WriteToFile(destinationPath);
                }

                if ((type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(AEXEDataSource<,,>)))
                {
                    var destinationPath = Path.Combine(enhancedDataPath);
                    Debug.WriteLine($"Writing to EXE file at {destinationPath}");

                    var writeToFileMethod = type.GetMethods().FirstOrDefault(m => m.Name == "WriteToFile");

                    if (writeToFileMethod != default)
                    {
                        var args = new object[] { tablesPath };
                        writeToFileMethod.Invoke(dataSource, args);
                        //exeSource.WriteToFile(destinationPath);
                    }
                }
            }
        }
    }
}
