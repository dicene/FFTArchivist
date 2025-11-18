using FFTArchivist.Managers;
using FFTArchivist.Models;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class ItemAbilityViewModel : BaseDataPageViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ItemAbility itemAbility { get; set; }
        public ItemAbility ItemAbility
        {
            get => itemAbility;
            set
            {
                if (itemAbility != value)
                {
                    itemAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemAbility.ItemId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemNames)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
                }
            }
        }

        public byte ItemId
        {
            get
            {
                return itemAbility.ItemId?.Value ?? 0;
            }

            set
            {
                if (itemAbility.ItemId.Value != value)
                {
                    itemAbility.ItemId.Value = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemAbility.ItemId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
                }
            }
        }

        public List<string> ItemNames { get => itemAbility?.ItemId != null ? App.DataManager.GetDataList<Item>().Select(i => i.Name?.Value?.Replace("<icon=103>", "+") ?? "N/A").ToList() : new List<string>(); }
        public string ItemName { get => itemAbility?.ItemId != null ? App.DataManager.GetDataList<Item>()[itemAbility?.ItemId?.Value ?? 0].Name.Value : ""; }

        public void ChangeIndex(int index)
        {
            ItemAbility = DataManager.Instance.GetDataList<ItemAbility>()[index];
        }
    }
}
