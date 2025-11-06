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
    internal class JumpAbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private JumpAbility jumpAbility { get; set; }
        public JumpAbility JumpAbility
        {
            get => jumpAbility;
            set
            {
                if (jumpAbility != value)
                {
                    jumpAbility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JumpAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JumpAbility.Range)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JumpAbility.Vertical)));
                }
            }
        }

        public byte Range
        {
            get
            {
                return jumpAbility.Range?.Value ?? 0;
            }

            set
            {
                if (jumpAbility.Range.Value != value)
                {
                    jumpAbility.Range.Value = Range;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JumpAbility.Range)));
                }
            }
        }

        public byte Vertical
        {
            get
            {
                return jumpAbility.Vertical?.Value ?? 0;
            }

            set
            {
                if (jumpAbility.Vertical.Value != value)
                {
                    jumpAbility.Vertical.Value = Vertical;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JumpAbility.Vertical)));
                }
            }
        }

        public void ChangeIndex(int index)
        {
            JumpAbility = DataManager.Instance.GetDataList<JumpAbility>()[index];
        }
    }
}
