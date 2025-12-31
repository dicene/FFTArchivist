using FFTArchivist.Properties;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool ValidExecutablePath => System.IO.File.Exists(System.IO.Path.Combine(FFTIVCRootPath, "fft_enhanced.exe"));
        public bool ValidReloadedPath => System.IO.Path.GetFileName(ReloadedIIModsPath).Equals("Mods")
            && System.IO.Directory.Exists(ReloadedIIModsPath);

        public string FFTIVCRootPath
        {
            get => Settings.Default.FFTIVCRootPath;
            set
            {
                if (Settings.Default.FFTIVCRootPath != value)
                {
                    Settings.Default.FFTIVCRootPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FFTIVCRootPath)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValidExecutablePath)));
                }
            }
        }

        public string ModName
        {
            get => Settings.Default.ModName;
            set
            {
                if (Settings.Default.ModName != value)
                {
                    Settings.Default.ModName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModName)));
                }
            }
        }

        public string ReloadedIIModsPath
        {
            get => Settings.Default.ReloadedIIModsPath;
            set
            {
                if (Settings.Default.ReloadedIIModsPath != value)
                {
                    Settings.Default.ReloadedIIModsPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReloadedIIModsPath)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValidReloadedPath)));
                }
            }
        }

        public string ModId
        {
            get => Settings.Default.ModId;
            set
            {
                if (Settings.Default.ModId != value)
                {
                    Settings.Default.ModId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModId)));
                }
            }
        }

        public string ModAuthor
        {
            get => Settings.Default.ModAuthor;
            set
            {
                if (Settings.Default.ModAuthor != value)
                {
                    Settings.Default.ModAuthor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModAuthor)));
                }
            }
        }

        public string ModVersion
        {
            get => Settings.Default.ModVersion;
            set
            {
                if (Settings.Default.ModVersion != value)
                {
                    Settings.Default.ModVersion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModVersion)));
                }
            }
        }

        public string ModDescription
        {
            get => Settings.Default.ModDescription;
            set
            {
                if (Settings.Default.ModDescription != value)
                {
                    Settings.Default.ModDescription = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModDescription)));
                }
            }
        }

        public SettingsViewModel()
        {

        }
    }
}
