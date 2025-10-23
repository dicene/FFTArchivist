using FFTArchivist.Managers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static IMod CurrentMod { get; set; } = null;
        internal static IModManager ModManager { get; private set; } = new ModManager();
        internal static IDataManager DataManager => Managers.DataManager.Instance;
    }
}