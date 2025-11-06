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

        public void ChangeIndex(int index)
        {
            ActionAbility = DataManager.Instance.GetDataList<ActionAbility>()[index];
        }
    }
}
