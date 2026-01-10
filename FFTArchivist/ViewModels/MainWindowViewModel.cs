using FFTArchivist.Controls;
using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.ViewModels.Pages;
using Microsoft.VisualBasic;
using SQLitePCL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FFTArchivist.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string status;
        public string Status {
            get => status;
            set {
                if (status != value)
                {
                    status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        public float BlurRadius { get => ShowLoadingScreen ? 6f : 0f; }

        private bool showLoadingScreen = false;
        public bool ShowLoadingScreen {
            get => showLoadingScreen;
            set
            {
                if (showLoadingScreen != value)
                {
                    showLoadingScreen = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowLoadingScreen)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingScreenHidden)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BlurRadius)));
                }
            }
        }

        public bool LoadingScreenHidden => !ShowLoadingScreen;

        private bool errorOccurred = false;
        public bool ErrorOccurred
        {
            get => errorOccurred;
            set
            {
                errorOccurred = value;

                if (!value)
                {
                    ErrorForceReloadData = false;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowLoadingScreen)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingScreenHidden)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BlurRadius)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorOccurred)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoErrorOccurred)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorForceReloadData)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowCloseErrorButton)));
            }
        }

        public bool NoErrorOccurred => !ErrorOccurred;

        private bool errorForceReloadData = false;
        public bool ErrorForceReloadData
        {
            get => errorForceReloadData;
            set
            {
                errorForceReloadData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorForceReloadData)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowCloseErrorButton)));
            }
        }

        public bool ShowCloseErrorButton => ErrorOccurred && !ErrorForceReloadData;

        private bool isDataLoaded = false;

        public bool IsDataLoaded
        {
            get => isDataLoaded;
            set
            {
                if (isDataLoaded != value)
                {
                    isDataLoaded = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDataLoaded)));
                }
            }
        }

        public ItemView ItemView { get; set; }
        public PoachView PoachView { get; set; }
        public AbilityView AbilityView { get; set; }
        public UIView UIView { get; set; }
        public SettingsView SettingsView { get; set; }

        private Page? currentPage;
        public Page? CurrentPage
        {
            get => currentPage; set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    SelectedIndex = -1;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
                }
            }
        }

        private int selectedIndex = 0;
        public int SelectedIndex {
            get => selectedIndex;
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    
                    if (CurrentPage?.DataContext is BaseDataPageViewModel viewModel)
                    {
                        viewModel.ChangeIndex(selectedIndex);
                    }

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
                }
            }
        }

        private ObservableCollection<string> itemList = new();
        public ObservableCollection<string> ItemList
        {
            get => itemList;
            set
            {
                if (itemList != value)
                {
                    itemList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                }
            }
        }

        public MainWindowViewModel()
        {
            Debug.WriteLine($"Creating MainWindowViewModel");
        }

        public async Task ImportMod()
        {
            ErrorOccurred = false;
            Status = "Importing mod...";
            ShowLoadingScreen = true;

            var path = Properties.Settings.Default.ReloadedIIModsPath;
            path = System.IO.Path.TrimEndingDirectorySeparator(path) + System.IO.Path.DirectorySeparatorChar;
            path = System.IO.Path.Combine(path, Properties.Settings.Default.ModName) + System.IO.Path.DirectorySeparatorChar;

            using var dialog = new FolderBrowserDialog
            {
                Description = "Select the base FFT - The Ivalice Chronicles folder",
                UseDescriptionForTitle = true,
                SelectedPath = path,
                ShowNewFolderButton = false
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
            else
            {
                ShowLoadingScreen = false;
                return;
            }

            var importModTask = Task.Run(async () =>
            {
                try
                {
                    await App.ModManager.ImportMod(path);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to import mod {path}: {ex}");
                    throw;
                }
            });

            try
            {
                await importModTask;
            }
            catch (Exception ex)
            {
                Status = $"Failed to import mod \n{path.Split(System.IO.Path.DirectorySeparatorChar)[^1]}\nCheck the log for more details.";
                ErrorOccurred = true;
                ErrorForceReloadData = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                return;
            }

            //if (!importModTask.IsCompletedSuccessfully)
            //{
            //    Status = "Failed to import mod! See console for details...";
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
            //    return;
            //}

            Status = "Complete";

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));

            ShowLoadingScreen = false;
        }

        public async Task SwitchToItemEditor()
        {
            ItemList.Clear();

            foreach (var item in App.DataManager.GetDataList<Item>().Select(item => item.ToString()))
            {
                ItemList.Add(item);
            }

            CurrentPage = ItemView ?? new ItemView();
        }

        internal async Task WindowLoaded()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            ErrorOccurred = false;
            Status = "Loading...";
            ShowLoadingScreen = true;

            async void FailedLoad(string message)
            {
                Status = message;
                await Task.Delay(3000);
                ShowLoadingScreen = false;
                await SwitchToSettingsEditor();
            }

            List<string> moddedPacs = new();

            moddedPacs = Directory.GetFiles(App.DataManager.DataFolderPath, "modded*.pac").ToList();

            Status = "Temporarily renaming modded pacs...";
            foreach (var moddedPac in moddedPacs)
            {
                try
                {
                    var moddedPacBak = moddedPac + ".bak";
                    Debug.WriteLine($"Temporarily renaming modded pac {moddedPac}...");

                    if (File.Exists(moddedPacBak))
                    {
                        File.Delete(moddedPacBak);
                    }

                    File.Move(moddedPac, moddedPacBak);
                }
                catch (Exception ex)
                {
                    FailedLoad($"Failed to move modded pac file: {ex}");
                    throw;
                }
            }

            Status = "Opening pack...";
            var openPackTask = Task.Run(async () =>
            {
                var success = false;

                try
                {
                    success = await App.DataManager.OpenPack(App.DataManager.DataFolderPath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to open packs {App.DataManager.DataFolderPath}: {ex}");
                    throw;
                }
            });

            try
            {
                await openPackTask;
            }
            catch (Exception ex)
            {
                Status = $"Failed to open packs at \n{App.DataManager.DataFolderPath}\nCheck the log for more details.";
                ErrorOccurred = true;
                await SwitchToSettingsEditor();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                return;
            }

            Status = "Opening data sources...";
            var openSourcesTask = Task.Run(async () =>
            {
                var success = false;

                try
                {
                    success = await App.DataManager.LoadDataSources();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to open data sources: {ex}");
                    throw;
                }
            });

            try
            {
                await openSourcesTask;
            }
            catch (Exception ex)
            {
                Status = $"Failed to open data sources.\nCheck the log for more details.";
                ErrorOccurred = true;
                await SwitchToSettingsEditor();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                return;
            }
            
            Status = "Loading data...";
            var loadDataTask = Task.Run(async () =>
            {
                var success = false;

                try
                {
                    success = await App.DataManager.LoadData();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load data: {ex}");
                    throw;
                }
            });

            try
            {
                await loadDataTask;
            }
            catch (Exception ex)
            {
                Status = $"Failed to load data.\nCheck the log for more details.";
                ErrorOccurred = true;
                await SwitchToSettingsEditor();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                return;
            }
            
            Status = "Closing pack...";
            var closePackTask = Task.Run(async () =>
            {
                var success = false;

                try
                {
                    success = await App.DataManager.ClosePack();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to close packs: {ex}");
                    throw;
                }
            });

            try
            {
                await closePackTask;
            }
            catch (Exception ex)
            {
                Status = $"Failed to close packs.\nCheck the log for more details.";
                ErrorOccurred = true;
                await SwitchToSettingsEditor();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                return;
            }

            Status = "Complete";

            try
            {

                foreach (var moddedPac in moddedPacs)
                {
                    Debug.WriteLine($"Restoring modded pac {moddedPac}...");
                    File.Move(moddedPac + ".bak", moddedPac);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to restore modded pacs: {moddedPacs}: {ex}");
                FailedLoad($"Failed to restore modded packs.");
                return;
            }

            IsDataLoaded = true;

            ShowLoadingScreen = false;
        }

        internal async Task SwitchToPoachEditor()
        {
            //EntryListBox.SelectedIndex = -1;
            ItemList.Clear();

            foreach (var poachItem in App.DataManager.GetDataList<PoachItem>().Select(p => p.ToString()))
            {
                ItemList.Add(poachItem);
            }

            if (PoachView == null)
            {
                PoachView = new PoachView();
            }

            CurrentPage = PoachView;

            SelectedIndex = 0;
        }

        internal async Task SwitchToAbilityEditor()
        {
            ItemList.Clear();

            foreach (var ability in App.DataManager.GetDataList<Ability>().Select(a => a.ToString()))
            {
                ItemList.Add(ability);
            }

            if (AbilityView == null)
            {
                AbilityView = new AbilityView();
            }

            CurrentPage = AbilityView;

            SelectedIndex = 0;
        }

        internal async Task SwitchToUIEditor()
        {
            ItemList.Clear();

            foreach (var item in App.DataManager.GetDataList<UI>().Select(ui => ui.ToString()))
            {
                ItemList.Add(item);
            }

            if (UIView == null)
            {
                UIView = new UIView();
            }

            CurrentPage = UIView;

            SelectedIndex = 0;
        }

        internal async Task SwitchToSettingsEditor()
        {
            CurrentPage = new SettingsView();
        }

        internal async Task ExportMod()
        {
            ErrorOccurred = false;
            var modName = Properties.Settings.Default.ModName;
            var modId = Properties.Settings.Default.ModId;
            var modVersion = Properties.Settings.Default.ModVersion;
            var modAuthor = Properties.Settings.Default.ModAuthor;
            var modDescription = Properties.Settings.Default.ModDescription;
            var newModPath = System.IO.Path.Combine(Properties.Settings.Default.ReloadedIIModsPath, modName);
            Debug.WriteLine($"Exporting Mod: {modName} to {newModPath}");
            Status = $"Exporting mod {modName}...";
            ShowLoadingScreen = true;
            var exportModTask = Task.Run(async () =>
            {
                try
                {
                    await ModManager.Instance.ExportMod(modName, modId, modVersion, modAuthor, modDescription, newModPath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to export mod {modName}: {ex}");
                    throw;
                }
            });

            try
            {
                await exportModTask;
            }
            catch (Exception ex)
            {
                Status = $"Failed to export mod \n{modName}\nCheck the log for more details.";
                ErrorOccurred = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemList.Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWindowViewModel)));
                return;
            }

            Status = "Complete";
            ShowLoadingScreen = false;
        }

        internal async Task ReloadData()
        {
            ErrorOccurred = false;
            App.DataManager.ClearDataSources();
            await LoadData();
        }

        internal async Task CloseError()
        {
            ErrorOccurred = false;
            ShowLoadingScreen = false;
            ErrorForceReloadData = false;
        }
    }
}
