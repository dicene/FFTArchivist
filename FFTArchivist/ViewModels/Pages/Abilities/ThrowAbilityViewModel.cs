using FFTArchivist.Managers;
using FFTArchivist.Models;
using fftivc.utility.modloader.Interfaces.Tables.Structures;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class ThrowAbilityViewModel : BaseDataPageViewModel, INotifyPropertyChanged
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
