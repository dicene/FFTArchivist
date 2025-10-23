using FFTArchivist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Entries
{
    internal class AbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private Ability ability;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Ability Ability
        {
            get
            {
                return ability;
            }

            set
            {
                if (ability == value)
                {
                    return;
                }

                ability = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JpCost1)));
            }
        }

        public int Id { get => ability.Id; set => ability.Id = value; }
        public string Name { get => ability.Name.Value; set => ability.Name.Value = value; }
        public string Description { get => ability.Description.Value; set => ability.Description.Value = value; }
        public int JpCost1
        {
            get
            {
                return ability.JpCost1.Value + (ability.JpCost2.Value << 8);
            }

            set
            {
                ability.JpCost1.Value = (byte)(value & 0xff);
                ability.JpCost2.Value = (byte)(value >> 8);
            }
        }

        public AbilityViewModel()
        {
            this.ability = new Ability(0);
        }

        public AbilityViewModel(Ability ability)
        {
            this.ability = ability;
        }
    }
}
