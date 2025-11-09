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
    internal class SupportAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
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

        public List<string> AbilityNames { get => (supportAbility?.AbilityId != null ? App.DataManager.GetDataList<Ability>().Select(i => (i.Name?.Value ?? "N/A")).ToList() : new List<string>()); }
        public string AbilityName { get => supportAbility?.AbilityId != null ? App.DataManager.GetDataList<Ability>()[(supportAbility?.AbilityId?.Value ?? 0)].Name.Value : ""; }

        public void ChangeIndex(int index)
        {
            SupportAbility = DataManager.Instance.GetDataList<SupportAbility>()[index];
        }
    }
}
