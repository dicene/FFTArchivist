using FFTArchivist.DataSources.NEX.UI;
using FFTArchivist.Managers;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class Town : BaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.DLCFlags))]
        public DataItem<int> DLCFlags { get; set; }

        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Description))]
        public DataItem<string> Description { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown8))]
        public DataItem<int> Unknown8 { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.UnknownC))]
        public DataItem<int> UnknownC { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown10))]
        public DataItem<int> Unknown10 { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.PlaceNameId))]
        public DataItem<int> PlaceNameId { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown18))]
        public DataItem<string> Unknown18 { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown1C))]
        public DataItem<int> Unknown1C { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown20))]
        public DataItem<int> Unknown20 { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown24))]
        public DataItem<int> Unknown24 { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.UserSituationId))]
        public DataItem<int> UserSituationId { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown2C))]
        public DataItem<short> Unknown2C { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown2E))]
        public DataItem<short> Unknown2E { get; set; }
        
        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown30))]
        public DataItem<byte> Unknown30 { get; set; }

        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.UnkBool31))]
        public DataItem<byte> UnkBool31 { get; set; }

        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown32))]
        public DataItem<byte> Unknown32 { get; set; }

        [NEXMapping(typeof(TownNEXSource), nameof(TownNEXModel.Unknown33))]
        public DataItem<byte> Unknown33 { get; set; }


        public Town() : base() { }
        public Town(int id) : base(id) { }

        public override string ToString()
        {
            var placeNames = DataManager.Instance.GetDataList<PlaceName>();
            var placeName = placeNames.FirstOrDefault(p => p.Id == PlaceNameId.Value)?.Title.Value;
            return $"{Id} {placeName}";
            //return $"{Id} {(Description?.Value?.Replace("\n", "") + (new string(' ', 30)))[0..30] ?? "N/A"}";
        }
    }
}
