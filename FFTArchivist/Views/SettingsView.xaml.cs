using System.Windows;
using System.Windows.Controls;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void SelectRootPathButton_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select the base FFT - The Ivalice Chronicles folder",
                UseDescriptionForTitle = true,
                SelectedPath = SettingsViewModel.FFTIVCRootPath,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SettingsViewModel.FFTIVCRootPath = dialog.SelectedPath;
            }
        }

        private void SelectReloadedIIModsPathButton_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select the Reloaded II\\Mods folder",
                UseDescriptionForTitle = true,
                SelectedPath = SettingsViewModel.ReloadedIIModsPath,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SettingsViewModel.ReloadedIIModsPath = dialog.SelectedPath;
            }
        }
    }
}
