using AutoUpdaterDotNET;
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
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Reflection;

//[assembly:AssemblyFileVersion("0.1.0.3")]

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

        //internal static MyWebClient GetWebClient(Uri uri, IAuthentication basicAuthentication)
        //{
        //    MyWebClient webClient = new MyWebClient
        //    {
        //        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
        //    };

        //    if (uri.Scheme.Equals(Uri.UriSchemeFtp))
        //    {
                
        //    }
        //    else
        //    {
        //        basicAuthentication?.Apply(ref webClient);
        //        webClient.Headers[HttpRequestHeader.UserAgent] = HttpUserAgent;
        //    }

        //    return webClient;
        //}

        public App()
        {
            if (Settings.Default.UpdateSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                Settings.Default.Save();
            }

            //AutoUpdater.InstalledVersion = new Version("0.1.0.3");
            //var BaseUri = new Uri("http://gist.githubusercontent.com/dicene/45ea351d3136df2c6c2351708920ed3f/raw/95efa4636279e712efaec9ce0b78044e8f206a4b/updatetest.xml");
            //UpdateInfoEventArgs updateInfoEventArgs;

            //using (MyWebClient myWebClient = GetWebClient(BaseUri, BasicAuthXML))
            //{
            //    string text = myWebClient.DownloadString(BaseUri);
            //}
            //AutoUpdater.Start("http://gist.githubusercontent.com/dicene/45ea351d3136df2c6c2351708920ed3f/raw/95efa4636279e712efaec9ce0b78044e8f206a4b/updatetest.xml");
            //var client = new HttpClient();
            //var req = client.GetAsync("https://gist.githubusercontent.com/dicene/45ea351d3136df2c6c2351708920ed3f/raw/95efa4636279e712efaec9ce0b78044e8f206a4b/updatetest.xml");
            //req.Wait();
            //var res = req.Result;
            //Debug.WriteLine($"result: {res.StatusCode}: {res.Content}");
            //var resReadTask = res.Content.ReadAsStringAsync();
            //resReadTask.Wait();
            //var resText = resReadTask.Result;
            //Debug.WriteLine($"resText: {resText}");
            //AutoUpdater.Start("https://gist.githubusercontent.com/dicene/45ea351d3136df2c6c2351708920ed3f/raw/95efa4636279e712efaec9ce0b78044e8f206a4b/updatetest.xml");
            //Debug.WriteLine($"Version: {AutoUpdater.InstalledVersion}");
            //AutoUpdater.ShowUpdateForm(new UpdateInfoEventArgs());

            //if (string.IsNullOrWhiteSpace(Settings.Default.FFTIVCRootPath))
            //{
            //    var steamGameFinder = new SteamGameLocator();
                
            //    try
            //    {
            //        var fftSteamGame = steamGameFinder.getGameInfoByID("1004640");

            //        Debug.WriteLine($"Automatically identified FFTIVC install path.");

            //        Settings.Default.FFTIVCRootPath = fftSteamGame.steamGameLocation.Replace("\\\\", "\\").Replace("//", "/");
            //        Settings.Default.Save();
            //    }
            //    catch (DirectoryNotFoundException)
            //    {
            //        Debug.WriteLine("Could not automatically find FFTIVC install path.");
            //    }
            //}

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

            var modConfigService = new ModConfigService(config);
            
            if (string.IsNullOrWhiteSpace(Settings.Default.FFTIVCRootPath))
            {
                try
                {
                    //var fftSteamGame = steamGameFinder.getGameInfoByID("1004640");
                    var applicationConfigService = new ApplicationConfigService(config);
                    var fftEnhancedItem = applicationConfigService.Items.FirstOrDefault(i => i.Config.AppId.Equals("fft_enhanced.exe", StringComparison.OrdinalIgnoreCase));

                    if (fftEnhancedItem != default)
                    {
                        Debug.WriteLine($"Automatically identified FFTIVC install path.");

                        //Settings.Default.FFTIVCRootPath = fftSteamGame.steamGameLocation.Replace("\\\\", "\\").Replace("//", "/");
                        Settings.Default.FFTIVCRootPath = Path.GetDirectoryName(fftEnhancedItem.Config.AppLocation);
                        Settings.Default.Save();
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    Debug.WriteLine("Could not automatically find FFTIVC install path.");
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