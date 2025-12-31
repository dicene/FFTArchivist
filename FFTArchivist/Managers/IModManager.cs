namespace FFTArchivist.Managers
{
    internal interface IModManager
    {
        public static IModManager Instance { get; }
        public Task ExportMod(string modName, string modId, string modVersion, string modAuthor, string modDescription, string modPath);
        public Task ImportMod(string modPath);
    }
}
