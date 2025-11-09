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
    internal class ActionAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private ActionAbility actionAbility { get; set; }
        public ActionAbility ActionAbility
        {
            get => actionAbility;
            set
            {
                if (actionAbility != value)
                {
                    actionAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility.Range)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility.EffectArea)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility.Vertical)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public byte Range
        {
            get
            {
                return actionAbility.Range?.Value ?? 0;
            }

            set
            {
                if (actionAbility.Range.Value != value)
                {
                    actionAbility.Range.Value = Range;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility.Range)));
                }
            }
        }

        public byte EffectArea
        {
            get
            {
                return actionAbility.EffectArea?.Value ?? 0;
            }

            set
            {
                if (actionAbility.EffectArea.Value != value)
                {
                    actionAbility.EffectArea.Value = EffectArea;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility.EffectArea)));
                }
            }
        }

        public byte Vertical
        {
            get
            {
                return actionAbility.Vertical?.Value ?? 0;
            }

            set
            {
                if (actionAbility.Vertical.Value != value)
                {
                    actionAbility.Vertical.Value = Vertical;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility.Vertical)));
                }
            }
        }

        public void ChangeIndex(int index)
        {
            ActionAbility = DataManager.Instance.GetDataList<ActionAbility>()[index];
        }
    }
}
