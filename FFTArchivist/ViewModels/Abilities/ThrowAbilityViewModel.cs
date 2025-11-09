using FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility;
using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.Views.Abilities;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
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

        public List<string> ItemNames { get => Enum.GetNames(typeof(ItemCategory)).ToList(); }
        public string ItemName => throwAbility?.ItemId != null ? Enum.GetName(typeof(ItemCategory), throwAbility?.ItemId?.Value ?? 0) ?? "???" : "???";

        public void ChangeIndex(int index)
        {
            ThrowAbility = DataManager.Instance.GetDataList<ThrowAbility>()[index];
        }
    }
}
