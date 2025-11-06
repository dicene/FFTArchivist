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
    internal class ThrowAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ThrowAbility throwAbility { get; set; }
        public ThrowAbility ThrowAbility
        {
            get => throwAbility;
            set
            {
                if (throwAbility != value)
                {
                    throwAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThrowAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThrowAbility.ItemId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemNames)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
                }
            }
        }

        public byte ItemId
        {
            get
            {
                return throwAbility.ItemId?.Value ?? 0;
            }

            set
            {
                if (throwAbility.ItemId.Value != value)
                {
                    throwAbility.ItemId.Value = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThrowAbility.ItemId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
                }
            }
        }

        public List<string> ItemNames { get => (throwAbility?.ItemId != null ? App.DataManager.GetDataList<Item>().Select(i => (i.Name?.Value.Replace("<icon=103>", "+") ?? "N/A")).ToList() : new List<string>()); }
        public string ItemName { get => throwAbility?.ItemId != null ? App.DataManager.GetDataList<Item>()[(throwAbility?.ItemId?.Value ?? 0)].Name.Value : ""; }

        public void ChangeIndex(int index)
        {
            ThrowAbility = DataManager.Instance.GetDataList<ThrowAbility>()[index];
        }
    }
}
