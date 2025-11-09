using FFTArchivist.Entries;
using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.Properties;
using System.Diagnostics;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ItemView ItemView { get; set; }
        public PoachView PoachView { get; set; }
        public AbilityView AbilityView { get; set; }
        public UIView UIView { get; set; }
        public SettingsView SettingsView { get; set; }
        public Page CurrentView;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //App.CurrentMod = App.ModManager.CreateMod();
            await App.DataManager.OpenPack(App.DataManager.DataFolderPath);
            await App.DataManager.LoadDataSources();
            await App.DataManager.LoadData();
            SettingsView = new SettingsView();
            //CurrentView = SettingsView;
            //EditorFrame.Navigate(CurrentView);
        }

        private void ItemEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.SelectedIndex = -1;
            EntryListBox.ItemsSource = App.DataManager.GetDataList<Item>().Select(item => $"{item.Id:X} {item.Name?.Value ?? "N/A"}");

            if (ItemView == null)
            {
                ItemView = new ItemView();
            }

            CurrentView = ItemView;
            EditorFrame.Navigate(CurrentView);
        }

        private void PoachEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.SelectedIndex = -1;
            EntryListBox.ItemsSource = App.DataManager.GetDataList<PoachItem>().Select(item => $"{item.Id:X} {item.Name?.Value.Replace("<Icon=103>", "+") ?? "N/A"}");

            if (PoachView == null)
            {
                PoachView = new PoachView();
            }

            CurrentView = PoachView;
            EditorFrame.Navigate(CurrentView);
        }

        private void AbilityEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.SelectedIndex = -1;
            EntryListBox.ItemsSource = App.DataManager.GetDataList<Ability>().Select(item => $"{item.Id:X} {item.Name?.Value ?? "N/A"}");

            if (AbilityView == null)
            {
                AbilityView = new AbilityView();
            }

            CurrentView = AbilityView;
            EditorFrame.Navigate(CurrentView);

            if (EntryListBox.Items.Count > 0)
            {
                EntryListBox.SelectedIndex = 0;
            }
        }

        private void UIEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.SelectedIndex = -1;
            EntryListBox.ItemsSource = App.DataManager.GetDataList<UI>().Select(ui => $"{ui.Id} {(ui.Text?.Value.Replace("\n", "") + (new string(' ', 30)))[0..30] ?? "N/A"}");

            if (UIView == null)
            {
                UIView = new UIView();
            }

            CurrentView = UIView;
            EditorFrame.Navigate(CurrentView);

            if (EntryListBox.Items.Count > 0)
            {
                EntryListBox.SelectedIndex = 0;
            }
        }

        private async void EntryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntryListBox.SelectedIndex == -1)
            {
                return;
            }

            if (CurrentView.DataContext is BaseDataViewModel viewModel)
            {
                viewModel.ChangeIndex(EntryListBox.SelectedIndex);
            }

            //if (CurrentView is ItemView itemView)
            //{
            //    itemView.ItemViewModel.Item = App.DataManager.GetDataList<Item>()[EntryListBox.SelectedIndex];
            //}
            //else if (CurrentView is PoachView poachView)
            //{
            //    poachView.PoachViewModel.Poach = App.DataManager.GetDataList<Poach>()[EntryListBox.SelectedIndex];
            //}
            //else if (CurrentView is AbilityView abilityView)
            //{
            //    abilityView.AbilityViewModel.Ability = App.DataManager.GetDataList<Ability>()[EntryListBox.SelectedIndex];
            //    abilityView.AbilityViewModel.AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[EntryListBox.SelectedIndex];
            //}
        }

        private async void ModDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            //App.CurrentMod = App.ModManager.CreateMod();
            await App.DataManager.LoadData();
        }

        private async void ImportModButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ExportModButton_Click(object sender, RoutedEventArgs e)
        {
            var modName = Properties.Settings.Default.ModName;
            var modId = Properties.Settings.Default.ModId;
            var modVersion = Properties.Settings.Default.ModVersion;
            var modAuthor = Properties.Settings.Default.ModAuthor;
            var modDescription = Properties.Settings.Default.ModDescription;
            var newModPath = System.IO.Path.Combine(Properties.Settings.Default.ReloadedIIModsPath, modName);
            Debug.WriteLine($"Exporting Mod: {modName} to {newModPath}");
            await ModManager.Instance.ExportMod(modName, modId, modVersion, modAuthor, modDescription, newModPath);
        }

        private void JobEditorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.ItemsSource = null;
            CurrentView = new SettingsView();
            EditorFrame.Navigate(CurrentView);
        }
    }
}