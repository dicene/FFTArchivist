using FFTArchivist.DataSources.EXE;
using FFTArchivist.DataSources.EXE.TempInterfaces.ItemAbility;
using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.Models.Base;
using FFTArchivist.ViewModels.DataItems;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages.Abilities
{
    internal class ActionAbilityPageViewModel : BaseDataPageViewModel, INotifyPropertyChanged
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

                    Range.DataItem = value.Range;
                    EffectArea.DataItem = value.EffectArea;
                    Vertical.DataItem = value.Vertical;
                    Range.DataItem = value.Range;
                    EffectArea.DataItem = value.EffectArea;
                    Vertical.DataItem = value.Vertical;
                    Flags1.DataItem = value.Flags1;
                    Flags2.DataItem = value.Flags2;
                    Flags3.DataItem = value.Flags3;
                    Flags4.DataItem = value.Flags4;
                    Element.DataItem = value.Element;
                    Formula.DataItem = value.Formula;
                    X.DataItem = value.X;
                    Y.DataItem = value.Y;
                    InflictStatus.DataItem = value.InflictStatus;
                    CT.DataItem = value.CT;
                    MPCost.DataItem = value.MPCost;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionAbility)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Range)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EffectArea)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vertical)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Range)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EffectArea)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vertical)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flags1)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flags2)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flags3)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flags4)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Element)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Formula)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InflictStatus)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CT)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MPCost)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ByteDataItemViewModel Range { get; set; } = new();
        public ByteDataItemViewModel EffectArea { get; set; } = new();
        public ByteDataItemViewModel Vertical { get; set; } = new();
        public ByteDataItemViewModel Flags1 { get; set; } = new();
        public ByteDataItemViewModel Flags2 { get; set; } = new();
        public ByteDataItemViewModel Flags3 { get; set; } = new();
        public ByteDataItemViewModel Flags4 { get; set; } = new();
        public ByteDataItemViewModel Element { get; set; } = new();
        public ushortDataItemViewModel Formula { get; set; } = new();
        public ByteDataItemViewModel X { get; set; } = new();
        public ByteDataItemViewModel Y { get; set; } = new();
        public ByteDataItemViewModel InflictStatus { get; set; } = new();
        public ByteDataItemViewModel CT { get; set; } = new();
        public ByteDataItemViewModel MPCost { get; set; } = new();

        public Dictionary<int, string> FormulaNames { get; set; } = Constants.AbilityFormulas;
        public string FormulaName { get => Formula?.Value != null ? FormulaNames[Formula.Value] : ""; }
        public List<string> StatusNames { get; set; } = new() { "abc" };

        public ActionAbilityPageViewModel()
        {
            Flags1.DisplayAsHex = true;
            Flags2.DisplayAsHex = true;
            Flags3.DisplayAsHex = true;
            Flags4.DisplayAsHex = true;
            Element.DisplayAsHex = true;
            actionAbility = new ActionAbility();
        }

        public ActionAbilityPageViewModel(ActionAbility actionAbility)
        {
            this.actionAbility = actionAbility;
        }

        public void ChangeIndex(int index)
        {
            ActionAbility = DataManager.Instance.GetDataList<ActionAbility>()[index];
        }
    }
}
