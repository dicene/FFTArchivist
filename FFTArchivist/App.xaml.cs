using FFTArchivist.Managers;
using FFTArchivist.Properties;
using Narod.SteamGameFinder;
using Newtonsoft.Json;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Loader.IO;
using Reloaded.Mod.Loader.IO.Config;
using Reloaded.Mod.Loader.IO.Services;
using System.Diagnostics;
using System.IO;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        internal static Managers.IMod CurrentMod { get; set; } = null;
        internal static IModManager ModManager { get; private set; } = new ModManager();
        internal static IDataManager DataManager => Managers.DataManager.Instance;

        public App()
        {
            if (Settings.Default.UpdateSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                Settings.Default.Save();
            }

            if (string.IsNullOrWhiteSpace(Settings.Default.FFTIVCRootPath))
            {
                var steamGameFinder = new SteamGameLocator();
                
                try
                {
                    var fftSteamGame = steamGameFinder.getGameInfoByID("1004640");

                    Debug.WriteLine($"Automatically identified FFTIVC install path.");

                    Settings.Default.FFTIVCRootPath = fftSteamGame.steamGameLocation.Replace("\\\\", "\\").Replace("//", "/");
                    Settings.Default.Save();
                }
                catch (DirectoryNotFoundException)
                {
                    Debug.WriteLine("Could not automatically find FFTIVC install path.");
                }
            }

            var config = IConfig<LoaderConfig>.FromPathOrDefault(Paths.LoaderConfigPath);

            if (string.IsNullOrWhiteSpace(Settings.Default.ReloadedIIModsPath))
            {
                try
                {
                    var modDir = config.GetModConfigDirectory();

                    Debug.WriteLine($"Automatically identified Reloaded Mods path.");

                    Settings.Default.ReloadedIIModsPath = modDir.Replace("\\\\", "\\").Replace("//", "/");
                    Settings.Default.Save();
                }
                catch (DirectoryNotFoundException)
                {
                    Debug.WriteLine("Could not automatically find Reloaded II Mods path.");
                }
            }
            
            //var pluginData = new JObject()
            //{
            //    ["GitHubDependencies"] = new JObject()
            //    {
            //        ["IdToConfigMap"] = new JObject()
            //        {
            //            ["fftivc.utility.modloader"] = new JObject()
            //            {
            //                ["Config"] = new JObject()
            //                {
            //                    ["UserName"] = "Nenkai",
            //                    ["RepositoryName"] = "fftivc.utility.modloader",
            //                    ["UseReleaseTag"] = true,
            //                    ["AssetFileName"] = "Mod.zip"
            //                },
            //                ["ReleaseMetadataName"] = "fftivc.utility.modloader.ReleaseMetadata.json"
            //            },
            //            ["reloaded.sharedlib.hooks"] = new JObject()
            //            {
            //                ["Config"] = new JObject()
            //                {
            //                    ["UserName"] = "Sewer56",
            //                    ["RepositoryName"] = "Reloaded.SharedLib.Hooks.ReloadedII",
            //                    ["UseReleaseTag"] = true,
            //                    ["AssetFileName"] = "reloaded.sharedlib.hooks.zip"
            //                },
            //                ["ReleaseMetadataName"] = "Sewer56.Update.ReleaseMetadata.json"
            //            },
            //            ["Reloaded.Memory.SigScan.ReloadedII"] = new JObject()
            //            {
            //                ["Config"] = new JObject()
            //                {
            //                    ["UserName"] = "Reloaded-Project",
            //                    ["RepositoryName"] = "Reloaded.Memory.SigScan",
            //                    ["UseReleaseTag"] = false,
            //                    ["AssetFileName"] = "Mod.zip"
            //                },
            //                ["ReleaseMetadataName"] = "Reloaded.Memory.SigScan.ReloadedII.ReleaseMetadata.json"
            //            }
            //        }
            //    },
            //};
            //gitHubDependencies["IdToConfigMap"] = new JObject();


            var logFile = File.Create("log.txt");
            var listener = new TextWriterTraceListener(logFile);
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;

            Debug.WriteLine($"Initializing application...");
        }
    }
}