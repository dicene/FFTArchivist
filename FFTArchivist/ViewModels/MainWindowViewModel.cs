using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.ViewModels.Pages;
using SQLitePCL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

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

            await Task.Run(() => App.ModManager.ImportMod(path));
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
            Status = "Loading...";
            ShowLoadingScreen = true;

            async void FailedLoad(string message)
            {
                Status = message;
                await Task.Delay(2000);
                ShowLoadingScreen = false;
                await SwitchToSettingsEditor();
            }

            List<string> moddedPacs = new();

            try
            {
                moddedPacs = Directory.GetFiles(App.DataManager.DataFolderPath, "modded*.pac").ToList();

                foreach (var moddedPac in moddedPacs)
                {
                    Debug.WriteLine($"Temporarily renaming modded pac {moddedPac}...");
                    File.Move(moddedPac, moddedPac + ".bak");
                }
            }
            catch (Exception ex)
            {
                FailedLoad($"Invalid FFT path.");
                return;
            }

            Status = "Opening pack...";
            var success = await Task.Run(() => App.DataManager.OpenPack(App.DataManager.DataFolderPath));

            if (!success)
            {
                FailedLoad("Failed to open pack.");
                return;
            }

            Status = "Opening data sources...";
            success = await Task.Run(App.DataManager.LoadDataSources);

            if (!success)
            {
                FailedLoad("Failed to open data sources.");
                return;
            }

            Status = "Loading data...";
            success = await Task.Run(App.DataManager.LoadData);

            if (!success)
            {
                FailedLoad("Failed to load data.");
                return;
            }

            Status = "Closing pack...";
            success = await Task.Run(App.DataManager.ClosePack);

            if (!success)
            {
                FailedLoad("Failed to close pack.");
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
            var modName = Properties.Settings.Default.ModName;
            var modId = Properties.Settings.Default.ModId;
            var modVersion = Properties.Settings.Default.ModVersion;
            var modAuthor = Properties.Settings.Default.ModAuthor;
            var modDescription = Properties.Settings.Default.ModDescription;
            var newModPath = System.IO.Path.Combine(Properties.Settings.Default.ReloadedIIModsPath, modName);
            Debug.WriteLine($"Exporting Mod: {modName} to {newModPath}");
            Status = "Exporting mod {modName}...";
            ShowLoadingScreen = true;
            await ModManager.Instance.ExportMod(modName, modId, modVersion, modAuthor, modDescription, newModPath);
            Status = "Complete";
            ShowLoadingScreen = false;
        }

        internal async Task ReloadData()
        {
            await LoadData();
        }
    }
}
