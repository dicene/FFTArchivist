using CommunityToolkit.Mvvm.ComponentModel;
using FFTArchivist.Models;
using FFTArchivist.ViewModels.DataItems;
using fftivc.utility.modloader.Interfaces.Tables.Models;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages
{
    internal class TownViewModel : BaseDataPageViewModel, INotifyPropertyChanged
    {
        private Town town;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Town Town
        {
            get
            {
                return town;
            }

            set
            {
                if (town == value)
                {
                    return;
                }

                town = value;

                DLCFlags.DataItem = value.DLCFlags;
                Description = value.Description.Value;
                Unknown8.DataItem = value.Unknown8;
                UnknownC.DataItem = value.UnknownC;
                Unknown10.DataItem = value.Unknown10;
                PlaceNameId = value.PlaceNameId.Value;
                Unknown18 = value.Unknown18.Value;
                Unknown1C.DataItem = value.Unknown1C;
                Unknown20.DataItem = value.Unknown20;
                Unknown24.DataItem = value.Unknown24;
                UserSituationId.DataItem = value.UserSituationId;
                Unknown2C.DataItem = value.Unknown2C;
                Unknown2E.DataItem = value.Unknown2E;
                Unknown30 = value.Unknown30.Value == 1;
                UnkBool31 = value.UnkBool31.Value == 1;
                Unknown32 = value.Unknown32.Value == 1;
                Unknown33 = value.Unknown33.Value == 1;
                placeNameIndex = PlaceNames.IndexOf(PlaceNames.FirstOrDefault(p => p.Id == PlaceNameId));
                placeNameIndex = placeNameIndex < 0 ? 0 : placeNameIndex;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TownViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DLCFlags)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown8)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnknownC)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown10)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceNameId)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown18)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown1C)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown20)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown24)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserSituationId)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown2C)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown2E)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown30)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnkBool31)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown32)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown33)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceNames)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceNameIndex)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
            }
        }

        public IntDataItemViewModel DLCFlags { get; set; } = new();
        public string Description { get => town?.Description.Value ?? ""; set => town.Description.Value = value; }
        public IntDataItemViewModel Unknown8 { get; set; } = new();
        public IntDataItemViewModel UnknownC { get; set; } = new();
        public IntDataItemViewModel Unknown10 { get; set; } = new();
        public int PlaceNameId { get => town?.PlaceNameId.Value ?? 0; set => town.PlaceNameId.Value = value; }
        public string Unknown18 { get => town?.Unknown18.Value ?? ""; set => town.Unknown18.Value = value; }
        public IntDataItemViewModel Unknown1C { get; set; } = new();
        public IntDataItemViewModel Unknown20 { get; set; } = new();
        public IntDataItemViewModel Unknown24 { get; set; } = new();
        public IntDataItemViewModel UserSituationId { get; set; } = new();
        public shortDataItemViewModel Unknown2C { get; set; } = new();
        public shortDataItemViewModel Unknown2E { get; set; } = new();
        //public boolDataItemViewModel Unknown30 { get; set; } = new();
        //public boolDataItemViewModel UnkBool31 { get; set; } = new();
        //public boolDataItemViewModel Unknown32 { get; set; } = new();
        //public boolDataItemViewModel Unknown33 { get; set; } = new();

        //public int Id { get => town.Id; set => town.Id = value; }
        //public int DLCFlags { get => town?.DLCFlags.Value ?? 0; set => town.DLCFlags.Value = value; }
        //public int Unknown8 { get => town?.Unknown8.Value ?? 0; set => town.Unknown8.Value = value; }
        //public int UnknownC { get => town?.UnknownC.Value ?? 0; set => town.UnknownC.Value = value; }
        //public string Unknown10 { get => town?.Unknown10.Value ?? ""; set => town.Unknown10.Value = value; }
        //public int PlaceNameId { get => town?.PlaceNameId.Value ?? 0; set => town.PlaceNameId.Value = value; }
        //public string Unknown18 { get => town?.Unknown18.Value ?? ""; set => town.Unknown18.Value = value; }
        //public int Unknown1C { get => town?.Unknown1C.Value ?? 0; set => town.Unknown1C.Value = value; }
        //public int Unknown20 { get => town?.Unknown20.Value ?? 0; set => town.Unknown20.Value = value; }
        //public int Unknown24 { get => town?.Unknown24.Value ?? 0; set => town.Unknown24.Value = value; }
        //public int UserSituationId { get => town?.UserSituationId.Value ?? 0; set => town.UserSituationId.Value = value; }
        //public short Unknown2C { get => town?.Unknown2C.Value ?? 0; set => town.Unknown2C.Value = value; }
        //public short Unknown2E { get => town?.Unknown2E.Value ?? 0; set => town.Unknown2E.Value = value; }
        public bool Unknown30 { get => (town?.Unknown30.Value ?? 0) == 1; set => town.Unknown30.Value = (byte)(value ? 1 : 0); }
        public bool UnkBool31 { get => (town?.UnkBool31.Value ?? 0) == 1; set => town.UnkBool31.Value = (byte)(value ? 1 : 0); }
        public bool Unknown32 { get => (town?.Unknown32.Value ?? 0) == 1; set => town.Unknown32.Value = (byte)(value ? 1 : 0); }
        public bool Unknown33 { get => (town?.Unknown33.Value ?? 0) == 1; set => town.Unknown33.Value = (byte)(value ? 1 : 0); }

        //public int PlaceNameId { get => poach?.RewardID.Value ?? 0; set => poach.RewardID.Value = value; }
        public List<PlaceName> PlaceNames => App.DataManager.GetDataList<PlaceName>();
        
        private int placeNameIndex;
        public int PlaceNameIndex {
            get => placeNameIndex;
            set {
                placeNameIndex = value;
                PlaceNameId = PlaceNames[placeNameIndex].PlaceNameId.Value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceNameIndex)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceNameId)));
            }
        }

        public string ItemName { get => PlaceNames.FirstOrDefault(p => p.Id == PlaceNameId)?.Title?.Value ?? ""; }

        public TownViewModel()
        {
            town = null;
        }

        public void ChangeIndex(int index)
        {
            if (index < 0 || index >= App.DataManager.GetDataList<Town>().Count)
            {
                Town = new Town(0);
                return;
            }

            Town = App.DataManager.GetDataList<Town>()[index];
        }
    }
}
