using FFTArchivist.DataSources.NEX.UI;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class UI : BaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXMapping(typeof(UINEXSource), nameof(UINEXModel.Unknown0))]
        public DataItem<int> Unknown0 { get; set; }

        [NEXMapping(typeof(UINEXSource), nameof(UINEXModel.Text))]
        public DataItem<string> Text { get; set; }

        public UI() : base() { }
        public UI(int id) : base(id) { }

        public override string ToString() => $"{Id} {(Text?.Value?.Replace("\n", "") + (new string(' ', 30)))[0..30] ?? "N/A"}";
    }
}
