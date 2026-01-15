using FFTArchivist.DataSources.NEX.NewFolder.UI;
using FFTArchivist.DataSources.NEX.PlaceName;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class PlaceName : BaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXMapping(typeof(PlaceNameNEXSource), nameof(DLCFlags))]
        public DataItem<int> DLCFlags { get; set; }

        [NEXMapping(typeof(PlaceNameNEXSource), nameof(Comment))]
        public DataItem<string> Comment { get; set; }

        [NEXMapping(typeof(PlaceNameNEXSource), nameof(PlaceNameId))]
        public DataItem<int> PlaceNameId { get; set; }

        [NEXMapping(typeof(PlaceNameNEXSource), nameof(Title))]
        public DataItem<string> Title { get; set; }

        [NEXMapping(typeof(PlaceNameNEXSource), nameof(Unknown10))]
        public DataItem<string> Unknown10 { get; set; }

        public PlaceName() : base() { }
        public PlaceName(int id) : base(id) { }

        public override string ToString() => $"{Id} {(Title?.Value?.Replace("\n", "") + (new string(' ', 30)))[0..30] ?? "N/A"}";
    }
}
