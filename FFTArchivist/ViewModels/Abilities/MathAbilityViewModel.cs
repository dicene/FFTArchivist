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
    internal class MathAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private MathAbility mathAbility { get; set; }
        public MathAbility MathAbility
        {
            get => mathAbility;
            set
            {
                if (mathAbility != value)
                {
                    mathAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MathAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MathAbility.Key)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MathAbility.Value)));
                }
            }
        }

        public byte Key
        {
            get
            {
                return mathAbility.Key?.Value ?? 0;
            }

            set
            {
                if (mathAbility.Key.Value != value)
                {
                    mathAbility.Key.Value = Key;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MathAbility.Key)));
                }
            }
        }

        public byte Value
        {
            get
            {
                return mathAbility.Value?.Value ?? 0;
            }

            set
            {
                if (mathAbility.Value.Value != value)
                {
                    mathAbility.Value.Value = Value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MathAbility.Value)));
                }
            }
        }

        public void ChangeIndex(int index)
        {
            MathAbility = DataManager.Instance.GetDataList<MathAbility>()[index];
        }
    }
}
