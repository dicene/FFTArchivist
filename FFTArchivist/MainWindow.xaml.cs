using AutoUpdaterDotNET;
using FFTArchivist.ViewModels;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var a = AutoUpdater.PersistenceProvider.GetRemindLater();
            System.Windows.Application.Current.MainWindow.Title = $"FFT Archivist v{System.Windows.Application.ResourceAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? "?"}";
            AutoUpdater.Start("https://gist.githubusercontent.com/dicene/48a982e63686acc0b5f959376a34bc3b/raw/FFT_Archivist_Version.xml");
            await MainWindowViewModel.WindowLoaded();
        }

        private async void ItemEditorButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.SwitchToItemEditor();
        }

        private async void PoachEditorButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.SwitchToPoachEditor();
        }

        private async void AbilityEditorButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.SwitchToAbilityEditor();
        }

        private async void UIEditorButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.SwitchToUIEditor();
        }

        private async void TownEditorButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.SwitchToTownEditor();
        }

        private async void SettingsEditorButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.SwitchToSettingsEditor();
        }

        private async void EntryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntryListBox.SelectedIndex == -1)
            {
                return;
            }
        }

        private async void ImportModButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.ImportMod();
        }

        private async void ExportModButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.ExportMod();
        }

        private async void JobEditorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ReloadDataButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.ReloadData();
        }

        private async void ErrorReloadDataButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.ReloadData();
        }

        private async void ErrorCloseButton_Click(object sender, RoutedEventArgs e)
        {
            await MainWindowViewModel.CloseError();
        }
    }
}