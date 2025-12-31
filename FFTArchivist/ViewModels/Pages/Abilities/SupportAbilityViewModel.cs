using FFTArchivist.Managers;
using FFTArchivist.Models;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class SupportAbilityViewModel : BaseDataPageViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private SupportAbility supportAbility { get; set; }
        public SupportAbility SupportAbility
        {
            get => supportAbility;
            set
            {
                if (supportAbility != value)
                {
                    supportAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SupportAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SupportAbility.AbilityId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityNames)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityName)));
                }
            }
        }

        public byte AbilityId
        {
            get
            {
                return supportAbility?.AbilityId?.Value ?? 0;
            }

            set
            {
                if (supportAbility.AbilityId.Value != value)
                {
                    supportAbility.AbilityId.Value = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SupportAbility.AbilityId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityName)));
                }
            }
        }

        public List<string> AbilityNames { get => supportAbility?.AbilityId != null ? App.DataManager.GetDataList<Ability>().Select(i => i.Name?.Value ?? "N/A").ToList() : new List<string>(); }
        public string AbilityName { get => supportAbility?.AbilityId != null ? App.DataManager.GetDataList<Ability>()[supportAbility?.AbilityId?.Value ?? 0].Name.Value : ""; }

        public void ChangeIndex(int index)
        {
            SupportAbility = DataManager.Instance.GetDataList<SupportAbility>()[index];
        }
    }
}
