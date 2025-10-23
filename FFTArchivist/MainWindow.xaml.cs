using FFTArchivist.Entries;
using FFTArchivist.Models;
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
        public Page CurrentView;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.CurrentMod = App.ModManager.CreateMod();
            await App.DataManager.LoadData();
            CurrentView = new ItemView();
            EditorFrame.Navigate(CurrentView);
        }

        private void ItemEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.SelectedIndex = -1;
            EntryListBox.ItemsSource = App.DataManager.GetDataList<Item>().Select(item => $"{item.Id:X} {item.Name.Value}");
            CurrentView = new ItemView();
            //CurrentView.ItemViewModel = new ItemViewModel(App.DataManager.Items[0]);
            EditorFrame.Navigate(CurrentView);
        }

        private void PoachEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EntryListBox.SelectedIndex = -1;
            EntryListBox.ItemsSource = App.DataManager.GetDataList<Poach>().Select(item => $"{item.Id:X} {item.Name.Value}");
            CurrentView = new PoachView();
            //CurrentView.ItemViewModel = new ItemViewModel(App.DataManager.Items[0]);
            EditorFrame.Navigate(CurrentView);
        }

        private void AbilityEditorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void EntryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntryListBox.SelectedIndex == -1)
            {
                return;
            }

            if (CurrentView is ItemView itemView)
            {
                itemView.ItemViewModel.Item = App.DataManager.GetDataList<Item>()[EntryListBox.SelectedIndex];
            }
            else if (CurrentView is PoachView poachView)
            {
                poachView.PoachViewModel.Poach = App.DataManager.GetDataList<Poach>()[EntryListBox.SelectedIndex];
            }
            else if (CurrentView is AbilityView abilityView)
            {
                //abilityView.AbilityViewModel.Ability = App.DataManager.GetDataList<AbilityViewModel>()[EntryListBox.SelectedIndex];
            }
        }

        private async void ModDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentMod = App.ModManager.CreateMod();
            await App.DataManager.LoadData();
        }

        private void JobEditorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsEditorButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}