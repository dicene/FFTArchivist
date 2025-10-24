using FFTArchivist.DataSources;
using FFTArchivist.Models;
using FFTArchivist.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Managers
{
    internal class ModManager : IModManager
    {
        private static ModManager _instance = new ModManager();
        public static ModManager Instance { get => _instance; }
        public async Task ExportMod(string modName, string modPath)
        {
            //var dataManager = DataManager.Instance;
            //Path.CreateDir
            Directory.CreateDirectory(modPath);
            Directory.CreateDirectory(Path.Combine(modPath, "FFTIVC"));
            var enhancedDataPath = Path.Combine(modPath, "FFTIVC", "data", "enhanced");
            Directory.CreateDirectory(enhancedDataPath);
            var tablesPath = Path.Combine(modPath, "FFTIVC", "tables");
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
            modConfig["ModId"] = "new modid";
            modConfig["ModName"] = modName;
            modConfig["ModAuthor"] = "new modauthor";
            modConfig["ModVersion"] = "new modversion";
            modConfig["ModDescription"] = "new moddescription";

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

            foreach (var source in DataManager.Instance.DataSources)
            {
                if (source is NEXDataSource nexSource)
                {
                    var destinationPath = Path.Combine(enhancedDataPath, nexSource.NEXPath);
                    Debug.WriteLine($"Writing to NEX file at {destinationPath}");
                    nexSource.WriteToFile(destinationPath);
                }
            }
        }
    }
}
