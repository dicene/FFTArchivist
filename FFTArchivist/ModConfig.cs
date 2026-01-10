using Newtonsoft.Json.Linq;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Loader.IO.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace FFTArchivist
{
    internal class ModConfig : Reloaded.Mod.Loader.IO.Utility.ObservableObject, IConfig<ModConfig>, IModConfig
    {
        public const string ConfigFileName  = "ModConfig.json";
        public const string IconFileName    = "Preview.png";

        private const string DefaultId = "fftivc.main.generatedmodname";
        private const string DefaultName = "GeneratedMod";
        private const string DefaultAuthor = "AuthorName";
        private const string DefaultVersion = "0.1.0";
        private const string DefaultDescription = "Mod Generated via FFT Archivist";

        public string ModId { get; set; } = DefaultId;
        public string ModName { get; set; } = DefaultName;
        public string ModAuthor { get; set; } = DefaultAuthor;
        public string ModVersion { get; set; } = DefaultVersion;
        public string ModDescription { get; set; } = DefaultDescription;
        public string ModDll { get; set; } = String.Empty;
        public string ModIcon { get; set; } = IconFileName;
        public string ModR2RManagedDll32 { get; set; } = String.Empty;
        public string ModR2RManagedDll64 { get; set; } = String.Empty;
        public string ModNativeDll32 { get; set; } = String.Empty;
        public string ModNativeDll64 { get; set; } = String.Empty;
        public string[] Tags { get; set; } = [];
        public bool? CanUnload { get; set; } = null;
        public bool? HasExports { get; set; } = null;
        public bool IsLibrary { get; set; } = false;
        public string ReleaseMetadataFileName { get; set; } = String.Empty; //"Sewer56.Update.ReleaseMetadata.json";

        public List<string> IgnoreRegexes { get; set; } = [@".*\.json"];
        public List<string> IncludeRegexes { get; set; } = [@"\.deps\.json", @"\.runtimeconfig\.json", @"ModConfig\.json"];

        public string ModSubDirs { get; set; } = string.Empty;

        public string ModDisplayName => ModSubDirs.Length <= 0 ? ModName : $"{ModSubDirs}/{ModName}";

        public Dictionary<string, object> PluginData { get; set; } = new Dictionary<string, object>()
        {
            ["GitHubDependencies"] = new JObject()
            {
                ["IdToConfigMap"] = new JObject()
                {
                    ["fftivc.utility.modloader"] = new JObject()
                    {
                        ["Config"] = new JObject()
                        {
                            ["UserName"] = "Nenkai",
                            ["RepositoryName"] = "fftivc.utility.modloader",
                            ["UseReleaseTag"] = true,
                            ["AssetFileName"] = "Mod.zip"
                        },
                        ["ReleaseMetadataName"] = "fftivc.utility.modloader.ReleaseMetadata.json"
                    },
                    ["reloaded.sharedlib.hooks"] = new JObject()
                    {
                        ["Config"] = new JObject()
                        {
                            ["UserName"] = "Sewer56",
                            ["RepositoryName"] = "Reloaded.SharedLib.Hooks.ReloadedII",
                            ["UseReleaseTag"] = true,
                            ["AssetFileName"] = "reloaded.sharedlib.hooks.zip"
                        },
                        ["ReleaseMetadataName"] = "Sewer56.Update.ReleaseMetadata.json"
                    },
                    ["Reloaded.Memory.SigScan.ReloadedII"] = new JObject()
                    {
                        ["Config"] = new JObject()
                        {
                            ["UserName"] = "Reloaded-Project",
                            ["RepositoryName"] = "Reloaded.Memory.SigScan",
                            ["UseReleaseTag"] = false,
                            ["AssetFileName"] = "Mod.zip"
                        },
                        ["ReleaseMetadataName"] = "Reloaded.Memory.SigScan.ReloadedII.ReleaseMetadata.json"
                    }
                }
            },
        };

        public bool IsUniversalMod { get; set; } = false;

        public string[] ModDependencies { get; set; } = ["fftivc.utility.modloader"];
        public string[] OptionalDependencies { get; set; } = [];
        public string[] SupportedAppId { get; set; } = ["fft_enhanced.exe", "fft_classic.exe", "fft_enhanced - 1.2.0.exe"];
        public string ProjectUrl { get; set; } = String.Empty;

        public static JsonTypeInfo<ModConfig> GetJsonTypeInfo(out bool supportsSerialize)
        {
            throw new NotImplementedException();
        }
    }
}
