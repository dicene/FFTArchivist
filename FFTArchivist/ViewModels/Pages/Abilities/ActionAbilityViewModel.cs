using FFTArchivist.Managers;
using FFTArchivist.Models;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class ActionAbilityViewModel : BaseDataPageViewModel, INotifyPropertyChanged
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Range)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EffectArea)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vertical)));
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
                    actionAbility.Range.Value = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Range)));
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
                    actionAbility.EffectArea.Value = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EffectArea)));
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
                    actionAbility.Vertical.Value = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vertical)));
                }
            }
        }

        public void ChangeIndex(int index)
        {
            ActionAbility = DataManager.Instance.GetDataList<ActionAbility>()[index];
        }
    }
}
