using FFTArchivist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FFTArchivist.Entries
{
    internal class AbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private Ability ability;
        private AbilityDefaultSecondary abilityDefaultSecondary;

        private string secondaryType { get; set; }
        public string SecondaryType
        {
            get => secondaryType;
            set
            {
                secondaryType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondaryType)));
            }
        }

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

        public AbilityDefaultSecondary AbilityDefaultSecondary
        {
            get
            {
                return abilityDefaultSecondary;
            }

            set
            {
                if (abilityDefaultSecondary == value)
                {
                    return;
                }

                abilityDefaultSecondary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityDefaultSecondary.Range)));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityDefaultSecondary.EffectArea)));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityDefaultSecondary.Vertical)));
            }
        }

        public int Id { get => ability.Id; set => ability.Id = value; }
        public string Name { get => ability.Name?.Value ?? "N/A"; set => ability.Name.Value = value; }
        public string Description { get => ability.Description?.Value ?? ""; set => ability.Description.Value = value; }
        public int JpCost1
        {
            get
            {
                return (ability.JpCost1?.Value ?? 0) + ((ability.JpCost2?.Value ?? 0) << 8);
            }

            set
            {
                ability.JpCost1.Value = (byte)(value & 0xff);
                ability.JpCost2.Value = (byte)(value >> 8);
            }
        }

        public byte Range
        {
            get => abilityDefaultSecondary.Range?.Value ?? 0;
            set => abilityDefaultSecondary.Range.Value = Range;
        }

        public AbilityViewModel()
        {
            this.ability = new Ability(0);
            this.abilityDefaultSecondary = new AbilityDefaultSecondary(0);
        }

        public AbilityViewModel(Ability ability, AbilityDefaultSecondary abilityDefaultSecondary)
        {
            this.ability = ability;
            this.abilityDefaultSecondary = abilityDefaultSecondary;
        }

        public void ChangeIndex(int index)
        {
            Ability = App.DataManager.GetDataList<Ability>()[index];

            if (index < 0x170)      //Default
            {
                SecondaryType = "Normal";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index];
                SecondaryPage = new Uri(@"NormalSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
            else if (index < 0x17E) //Item
            {
                SecondaryType = "Item";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index - 0x170];
                SecondaryPage = new Uri(@"ItemSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
            else if (index < 0x18A) //Throw
            {
                SecondaryType = "Throw";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index - 0x17E];
                SecondaryPage = new Uri(@"ThrowSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
            else if (index < 0x196) //Jump
            {
                SecondaryType = "Jump";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index - 0x18A];
                SecondaryPage = new Uri(@"JumpSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
            else if (index < 0x19E) //Charge
            {
                SecondaryType = "Charge";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index - 0x196];
                SecondaryPage = new Uri(@"ChargeSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
            else if (index < 0x1A6) //Math
            {
                SecondaryType = "Math";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index - 0x19E];
                SecondaryPage = new Uri(@"MathSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
            else if (index < 0x200) //RSM
            {
                SecondaryType = "RSM";
                AbilityDefaultSecondary = App.DataManager.GetDataList<AbilityDefaultSecondary>()[index - 0x1A6];
                SecondaryPage = new Uri(@"RSMSecondary.xaml", UriKind.RelativeOrAbsolute);
            }
        }

        //protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        //{
        //    if (!Equals(field, newValue))
        //    {
        //        field = newValue;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //        return true;
        //    }

        //    return false;
        //}

        private Uri secondaryPage;

        public Uri SecondaryPage
        {
            get => secondaryPage;
            set
            {
                secondaryPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondaryPage)));
            }
        }
    }
}
