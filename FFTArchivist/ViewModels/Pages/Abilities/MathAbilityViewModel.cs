using FFTArchivist.Managers;
using FFTArchivist.Models;
using System.ComponentModel;
using MathFlags = FFTArchivist.DataSources.EXE.TempInterfaces.MathAbility.MathFlags;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class MathAbilityViewModel : BaseDataPageViewModel, INotifyPropertyChanged
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flag3)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flag4)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flag5)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagPrime)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagHeight)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagExp)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagLevel)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagCT)));
                }
            }
        }

        public bool Flag3
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.Flag3) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.Flag3) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.Flag3 : mathAbility.MathFlags.Value & ~MathFlags.Flag3;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flag3)));
                }
            }
        }

        public bool Flag4
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.Flag4) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.Flag4) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.Flag4 : mathAbility.MathFlags.Value & ~MathFlags.Flag4;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flag4)));
                }
            }
        }

        public bool Flag5
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.Flag5) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.Flag5) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.Flag5 : mathAbility.MathFlags.Value & ~MathFlags.Flag5;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flag5)));
                }
            }
        }

        public bool FlagPrime
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagPrime) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagPrime) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.FlagPrime : mathAbility.MathFlags.Value & ~MathFlags.FlagPrime;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagPrime)));
                }
            }
        }

        public bool FlagHeight
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagHeight) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagHeight) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.FlagHeight : mathAbility.MathFlags.Value & ~MathFlags.FlagHeight;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagHeight)));
                }
            }
        }

        public bool FlagExp
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagExp) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagExp) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.FlagExp : mathAbility.MathFlags.Value & ~MathFlags.FlagExp;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagExp)));
                }
            }
        }

        public bool FlagLevel
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagLevel) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagLevel) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.FlagLevel : mathAbility.MathFlags.Value & ~MathFlags.FlagLevel;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagLevel)));
                }
            }
        }

        public bool FlagCT
        {
            get
            {
                return mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagCT) ?? false;
            }

            set
            {
                if (mathAbility.MathFlags?.Value.HasFlag(MathFlags.FlagCT) != value)
                {
                    mathAbility.MathFlags.Value = value ? mathAbility.MathFlags.Value | MathFlags.FlagCT : mathAbility.MathFlags.Value & ~MathFlags.FlagCT;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagCT)));
                }
            }
        }

        public void ChangeIndex(int index)
        {
            MathAbility = DataManager.Instance.GetDataList<MathAbility>()[index];
        }
    }
}
