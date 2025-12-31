using FFTArchivist.Managers;
using System.Diagnostics;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        internal static IMod CurrentMod { get; set; } = null;
        internal static IModManager ModManager { get; private set; } = new ModManager();
        internal static IDataManager DataManager => Managers.DataManager.Instance;

        public App()
        {
            Debug.WriteLine($"Initializing application...");
        }
    }
}