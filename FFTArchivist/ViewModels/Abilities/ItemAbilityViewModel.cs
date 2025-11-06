using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.Views.Abilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FFTArchivist.Entries
{
    internal class ItemAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
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

        public List<string> ItemNames { get => (itemAbility?.ItemId != null ? App.DataManager.GetDataList<Item>().Select(i => (i.Name?.Value.Replace("<icon=103>", "+") ?? "N/A")).ToList() : new List<string>()); }
        public string ItemName { get => itemAbility?.ItemId != null ? App.DataManager.GetDataList<Item>()[(itemAbility?.ItemId?.Value ?? 0)].Name.Value : ""; }

        public void ChangeIndex(int index)
        {
            ItemAbility = DataManager.Instance.GetDataList<ItemAbility>()[index];
        }
    }
}
