using FFTArchivist.Managers;
using FFTArchivist.Models;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class ChargeAbilityViewModel : BaseDataPageViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ChargeAbility chargeAbility { get; set; }
        public ChargeAbility ChargeAbility
        {
            get => chargeAbility;
            set
            {
                if (chargeAbility != value)
                {
                    chargeAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeAbility.CT)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeAbility.Power)));
                }
            }
        }

        public byte CT
        {
            get
            {
                return chargeAbility.CT?.Value ?? 0;
            }

            set
            {
                if (chargeAbility.CT.Value != value)
                {
                    chargeAbility.CT.Value = CT;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeAbility.CT)));
                }
            }
        }

        public byte Power
        {
            get
            {
                return chargeAbility.Power?.Value ?? 0;
            }

            set
            {
                if (chargeAbility.Power.Value != value)
                {
                    chargeAbility.Power.Value = Power;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeAbility.Power)));
                }
            }
        }

        public void ChangeIndex(int index)
        {
            ChargeAbility = DataManager.Instance.GetDataList<ChargeAbility>()[index];
        }
    }
}
