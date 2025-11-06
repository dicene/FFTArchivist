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
    internal class ChargeAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
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
